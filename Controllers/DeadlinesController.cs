﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMKancelariapp.Extensions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    [Authorize]
    public class DeadlinesController : Controller
    {
        private readonly IDeadlineServices _deadlineServices;
        private readonly UserManager<User> _userManager;
        private readonly ICaseServices _caseServices;
        public DeadlinesController(IDeadlineServices deadlineServices, UserManager<User> userManager, ICaseServices caseServices)
        {
            _deadlineServices = deadlineServices;
            _userManager = userManager;
            _caseServices = caseServices;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _deadlineServices.GetAll();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new DeadlineDtoViewModel
            {
                User = await _userManager.FindByNameAsync(User.Identity.Name)
            };
            model.CasesSelectList.AddRange(await _caseServices.CreateCasesSelectList("all"));

            if (User.IsInRole("SysAdmin"))
            {
                model.UsersSelectList.AddRange(_userManager.CreateUsersSelectList());
                model.UsersSelectList.RemoveAt(model.UsersSelectList.FindIndex(x=>x.Text == "Brak"));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeadlineDtoViewModel model)
        {
            ValidateDeadlineDto(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            model.User = model.User.Id == null ? currentUser : await _userManager.FindByIdAsync(model.User.Id);
            model.Case = await _caseServices.GetByIdWithIncludes(model.Case.Id, x => x.Tasks, x => x.Client, x => x.Deadlines, x => x.AssignedUser);
            
            try
            {
                await _deadlineServices.Create(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }

        private async Task ValidateDeadlineDto(DeadlineDtoViewModel model)
        {
            if (model.Case.Name == null)
            {
                ModelState.Remove("Case.Name");
            }
        }
    }
}