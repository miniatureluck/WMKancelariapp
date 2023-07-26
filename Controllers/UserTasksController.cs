using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMKancelariapp.Extensions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    [Authorize]
    public class UserTasksController : Controller
    {
        private readonly IUserTaskServices _userTaskServices;
        private readonly IMapper _mapper;
        private readonly IClientServices _clientServices;
        private readonly UserManager<User> _userManager;
        public UserTasksController(IUserTaskServices userTaskServices, IMapper mapper, IClientServices clientServices, UserManager<User> userManager)
        {
            _userTaskServices = userTaskServices;
            _mapper = mapper;
            _clientServices = clientServices;
            _userManager = userManager;
        }
        // GET: UserTasksController
        public async Task<ActionResult> Index()
        {
            var model = new UserTaskIndexViewModel();

            var userTasks = await _userTaskServices.GetAll();
            foreach (var item in userTasks)
            {
                model.AllUserTasks.Add(_mapper.Map(item, new UserTaskDtoViewModel()));
            }

            return View(model);
        }

        // GET: UserTasksController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var model = await _userTaskServices.GetDtoById(id);
            return View(model);
        }

        // GET: UserTasksController/Create
        public async Task<ActionResult> Create()
        {
            var model = new UserTaskDtoViewModel();
            model.TaskTypeSelectList.AddRange(await _userTaskServices.CreateTaskTypeSelectList());
            model.ClientSelectList.AddRange(await _clientServices.CreateClientsSelectList());
            if (User.IsInRole("SysAdmin"))
            {
                model.UserSelectList.AddRange(_userManager.CreateUsersSelectList());
            }
            else
            {
                model.User = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            return View(model);
        }

        // POST: UserTasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserTaskDtoViewModel model)
        {
            try
            {
                model.Client = await _clientServices.GetById(model.Client.Id);
                model.User = await _userManager.FindByIdAsync(model.User.Id);
                model.TaskType = await _userTaskServices.GetTaskTypeById(model.TaskType.Id);
                await _userTaskServices.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserTasksController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _userTaskServices.GetDtoById(id);

            model.TaskTypeSelectList.AddRange(await _userTaskServices.CreateTaskTypeSelectList());
            model.ClientSelectList.AddRange(await _clientServices.CreateClientsSelectList());
            model.UserSelectList.AddRange(_userManager.CreateUsersSelectList());

            return View(model);
        }

        // POST: UserTasksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserTaskDtoViewModel model)
        {
            try
            {
                model.Client = await _clientServices.GetById(model.Client.Id);
                model.User = await _userManager.FindByIdAsync(model.User.Id);
                model.TaskType = await _userTaskServices.GetTaskTypeById(model.TaskType.Id);

                await _userTaskServices.Edit(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            await _userTaskServices.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Types()
        {
            var model = new List<TaskTypeDtoViewModel>();
            var types = await _userTaskServices.GetAllTaskTypes();
            foreach (var item in types)
            {
                model.Add(_mapper.Map(item, new TaskTypeDtoViewModel()));
            }

            return View(model);
        }

        public ActionResult CreateType()
        {
            var model = new TaskTypeDtoViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateType(TaskTypeDtoViewModel model)
        {
            await _userTaskServices.CreateTaskType(model);

            return RedirectToAction("Types");
        }
    }
}
