using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMKancelariapp.Extensions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    [Authorize]
    public class SettlementsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISettlementServices _settlementServices;
        private readonly IUserTaskServices _userTaskServices;
        private readonly IClientServices _clientServices;
        private readonly UserManager<User> _userManager;

        public SettlementsController(IMapper mapper, ISettlementServices settlementServices, IUserTaskServices userTaskServices, IClientServices clientServices, UserManager<User> userManager)
        {
            _mapper = mapper;
            _settlementServices = settlementServices;
            _userTaskServices = userTaskServices;
            _clientServices = clientServices;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _settlementServices.GetAll();
            foreach (var item in model)
            {
                foreach (var userTask in item.UserTasks)
                {
                    var taskEntity = await _userTaskServices.GetByIdWithIncludes(userTask.Id, x=>x.TaskType);
                    userTask.TaskType = taskEntity.TaskType;
                }
            }

            return View(model.Where(x => x.UserTasks.Any(y=>y.SettlementId!=null)));
        }

        public async Task<IActionResult> Settle(string? id = null)
        {
            var userIsSysAdmin = User.IsInRole("SysAdmin");
            var model = new SettlementDtoViewModel();
            if (userIsSysAdmin)
            {
                model.UsersSelectList.AddRange(_userManager.CreateUsersSelectList());
                model.UsersSelectList.RemoveAt(0);
            }
            else
            {
                model.AssignedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            if (id != null)
            {
                model.Client = await _clientServices.GetByIdWithIncludes(id, x => x.Tasks);
                model.ClientsSelectList.Add(new SelectListItem()
                {
                    Text = $"{model.Client.Name} {model.Client.Surname}",
                    Value = model.Client.Id
                });
                model.UserTasks = new List<UserTask>(model.Client.Tasks);
                model.SelectedUserTasksStatus = new List<bool>(model.UserTasks.Count);
                for (var i = 0; i < model.UserTasks.Count; i++)
                {
                    model.SelectedUserTasksStatus.Add(false);
                }

                foreach (var task in model.UserTasks)
                {
                    var taskEntity = await _userTaskServices.GetByIdWithIncludes(task.Id, x => x.Case, x => x.Client, x => x.TaskType, x => x.Settlement);
                    task.Case = taskEntity.Case;
                    task.Client = taskEntity.Client;
                    task.TaskType = taskEntity.TaskType;
                    task.Settlement = taskEntity.Settlement;
                }
            }
            else
            {
                model.ClientsSelectList.AddRange(await _clientServices.CreateClientsSelectList());
            }

            model.UserTasks = model.UserTasks.Where(x => x.Settlement == null).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settle(SettlementDtoViewModel model)
        {
            model.Client = await _clientServices.GetById(model.Client.Id);

            for (var i = 0; i < model.UserTasks.Count; i++)
            {
                model.UserTasks[i] = await _userTaskServices.GetById(model.UserTasks[i].Id);
                var isSelected = model.SelectedUserTasksStatus[i];

                if (isSelected)
                {
                    model.TotalPrice += model.UserTasks[i].Value;
                }
            }

            for (var i = model.UserTasks.Count-1; i >= 0; i--)
            {
                if (model.SelectedUserTasksStatus[i]==false)
                {
                    model.UserTasks.RemoveAt(i);
                }
            }

            
            try
            {
                await _settlementServices.Create(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Settle");
            }

            return RedirectToAction("Index");
        }
    }
}
