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
            var userId = User.IsInRole("SysAdmin") ? "all" : (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var model = new DeadlineDtoViewModel
            {
                User = await _userManager.FindByNameAsync(User.Identity.Name)
            };
            model.CasesSelectList.AddRange(await _caseServices.CreateCasesSelectList("all", userId));

            if (!User.IsInRole("SysAdmin"))
            {
                return View(model);
            }

            model.UsersSelectList.AddRange(_userManager.CreateUsersSelectList());
            var indexOfNone = model.UsersSelectList.FindIndex(x => x.Text == "Brak");
            if (indexOfNone > -1)
            {
                model.UsersSelectList.RemoveAt(indexOfNone);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeadlineDtoViewModel model)
        {
            await ValidateDeadlineDto(model);

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
            ModelState.Remove(nameof(model.DeadlineId));
            if (model.Case.Name == null)
            {
                ModelState.Remove("Case.Name");
            }
        }
    }
}
