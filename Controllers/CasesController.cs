﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WMKancelariapp.Extensions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{

    [Authorize]
    public class CasesController : Controller
    {
        private readonly ICaseServices _caseServices;
        private readonly IClientServices _clientServices;
        private readonly IUserTaskServices _userTaskServices;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHourlyPriceServices _hourlyPriceServices;
        public CasesController(ICaseServices caseServices, IClientServices clientServices, IMapper mapper, UserManager<User> userManager, IUserTaskServices userTaskServices, IHourlyPriceServices hourlyPriceServices)
        {
            _caseServices = caseServices;
            _clientServices = clientServices;
            _mapper = mapper;
            _userManager = userManager;
            _userTaskServices = userTaskServices;
            _hourlyPriceServices = hourlyPriceServices;
        }
        // GET: CasesController
        public async Task<ActionResult> Index()
        {
            var model = new CaseIndexViewModel();

            var cases = await _caseServices.GetAll();
            if (!User.IsInRole("SysAdmin"))
            {
                cases = cases.Where(x => x.AssignedUser == null || x.AssignedUser.UserName == User.Identity.Name);
            }

            foreach (var item in cases)
            {
                model.AllCases.Add(_mapper.Map(item, new CaseDtoViewModel(){CaseId = item.Id}));
            }

            var maxPrices = await _userTaskServices.CountTaskTypes();
            foreach (var userCase in model.AllCases)
            {
                userCase.PricesMaxCount = maxPrices;
                userCase.SpecifiedPrices = await _hourlyPriceServices.CountSpecifiedPricesForCase(userCase.CaseId);
            }

            return View(model);
        }

        // GET: CasesController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var userCase =
                await _caseServices.GetByIdWithIncludes(id, x => x.AssignedUser, x => x.Tasks, x => x.Client, x=>x.Prices, x=>x.Deadlines);
            var model = _mapper.Map(userCase, new CaseDtoViewModel());
            return View(model);
        }

        // GET: CasesController/Create
        public async Task<ActionResult> Create()
        {
            var model = new CaseDtoViewModel
            {
                Client = new Client()
            };
            model.AllClientsSelectList.AddRange(await _clientServices.CreateClientsSelectList());
            model.AllUsersSelectList.AddRange(_userManager.CreateUsersSelectList());

            return View(model);
        }

        // POST: CasesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CaseDtoViewModel model)
        {
            try
            {
                model.Client = model.Client.Id.IsNullOrEmpty() ? null : await _clientServices.GetById(model.Client.Id);
                model.AssignedUser = model.AssignedUser.Id.IsNullOrEmpty() ? null : await _userManager.FindByIdAsync(model.AssignedUser.Id);
                await _caseServices.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CasesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _caseServices.GetDtoById(id);

            if (!User.IsInRole("SysAdmin") && model.AssignedUser?.UserName != User.Identity.Name)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            model.AllClientsSelectList.AddRange(await _clientServices.CreateClientsSelectList());
            model.AllUsersSelectList.AddRange(_userManager.CreateUsersSelectList());
            return View(model);
        }

        // POST: CasesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CaseDtoViewModel model)
        {
            try
            {
                model.Client = model.Client.Id.IsNullOrEmpty() ? null : await _clientServices.GetById(model.Client.Id);
                model.AssignedUser = model.AssignedUser.Id.IsNullOrEmpty() ? null : await _userManager.FindByIdAsync(model.AssignedUser.Id);

                var tasks = new List<UserTask>();
                foreach (var item in model.Tasks)
                {
                    tasks.Add(await _userTaskServices.GetById(item.Id));
                }
                model.Tasks.Clear();
                model.Tasks.AddRange(tasks);

                await _caseServices.Edit(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _caseServices.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
