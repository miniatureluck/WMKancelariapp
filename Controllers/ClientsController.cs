using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMKancelariapp.Extensions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;
        private readonly ICaseServices _caseServices;
        private readonly UserManager<User> _userManager;

        public ClientsController(IClientServices clientServices, IMapper mapper, ICaseServices caseServices, UserManager<User> userManager)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _caseServices = caseServices;
            _userManager = userManager;
        }
        // GET: ClientsController
        public async Task<ActionResult> Index()
        {
            var model = new ClientIndexViewModel();
            
            var clients = await _clientServices.GetAll();
            foreach (var item in clients)
            {
                model.AllClients.Add(_mapper.Map(item, new ClientDtoViewModel()));
            }

            return View(model);
        }

        // GET: ClientsController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var client = await _clientServices.GetByIdWithIncludes(id, x=>x.AssignedUser, x=>x.Cases, x=>x.Prices, x=>x.Tasks);
            var model = _mapper.Map(client, new ClientDtoViewModel());
            return View(model);
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            var model = new ClientDtoViewModel();
            return View(model);
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientDtoViewModel model)
        {
            try
            {
                await _clientServices.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientsController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _clientServices.GetDtoById(id);
            model.AllCasesSelectList.AddRange(await _caseServices.CreateCasesSelectList());
            model.AllUsersSelectList.AddRange(_userManager.CreateUsersSelectList());
            model.SelectedCases.AddRange(model.Cases.Select(x=>x.Id));

            return View(model);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClientDtoViewModel model)
        {
            try
            {
                model.Cases.Clear();
                foreach (var item in model.SelectedCases)
                {
                    model.Cases.Add(await _caseServices.GetById(item));
                }
                model.AssignedUser = await _userManager.FindByIdAsync(model.AssignedUser.Id);

                await _clientServices.Edit(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientsController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var model = await _clientServices.GetDtoById(id);
            return View(model);
        }

        // POST: ClientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _clientServices.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
