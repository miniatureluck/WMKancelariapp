using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
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
        private readonly ICaseServices _caseServices;
        public UserTasksController(IUserTaskServices userTaskServices, IMapper mapper, IClientServices clientServices, UserManager<User> userManager, ICaseServices caseServices)
        {
            _userTaskServices = userTaskServices;
            _mapper = mapper;
            _clientServices = clientServices;
            _userManager = userManager;
            _caseServices = caseServices;
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
            await PopulateSelectionListsForCreateView(model);

            return View(model);
        }

        // POST: UserTasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserTaskDtoViewModel modelFromView)
        {
            var model = ValidateUserTask(modelFromView);

            try
            {
                model.Client = model.Client.Id == null ? null : await _clientServices.GetById(model.Client.Id);
                model.User = model.User.Id == null ? null : await _userManager.FindByIdAsync(model.User.Id);
                model.TaskType = await _userTaskServices.GetTaskTypeById(model.TaskType.Id);
                model.Case = model.Case.Id == null ? null : await _caseServices.GetById(model.Case.Id);
                model.Duration = !model.DurationMinutes.IsNullOrEmpty() ? TimeSpan.FromMinutes(double.Parse(model.DurationMinutes?.ConvertTimeToMinutes())).Ticks : 0;


                if (!ModelState.IsValid)
                {
                    await PopulateSelectionListsForCreateView(model);
                    return View(model);
                }

                _userTaskServices.CalculateDuration(model);

                await _userTaskServices.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UserTasksController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _userTaskServices.GetDtoById(id);

            await PopulateSelectionListsForCreateView(model);

            return View(model);
        }

        // POST: UserTasksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserTaskDtoViewModel modelFromView)
        {
            var model = ValidateUserTask(modelFromView);

            try
            {
                model.Client = model.Client.Id == null ? null : await _clientServices.GetById(model.Client.Id);
                model.User = model.User.Id == null ? null : await _userManager.FindByIdAsync(model.User.Id);
                model.TaskType = await _userTaskServices.GetTaskTypeById(model.TaskType.Id);
                model.Case = model.Case.Id == null ? null : await _caseServices.GetById(model.Case.Id);
                model.Duration = !model.DurationMinutes.IsNullOrEmpty() ? TimeSpan.FromMinutes(double.Parse(model.DurationMinutes?.ConvertTimeToMinutes())).Ticks : 0;

                if (!ModelState.IsValid)
                {
                    await PopulateSelectionListsForCreateView(model);
                    return View(model);
                }

                _userTaskServices.CalculateDuration(model);

                await _userTaskServices.Edit(model);
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
                await _userTaskServices.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<ActionResult> Types()
        {
            var types = await _userTaskServices.GetAllTaskTypes();
            var model = (from item in types let dto = new TaskTypeDtoViewModel { TaskTypeId = item.TaskTypeId } select _mapper.Map(item, dto)).ToList();
            foreach (var item in model)
            {
                item.MostFrequentCase.AddRange(
                    await _userTaskServices.FindMostFrequentCaseForTaskType(
                        await _userTaskServices.GetTaskTypeById(item.TaskTypeId)));
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
            try
            {
                await _userTaskServices.CreateTaskType(model);
            }
            catch
            {
                return RedirectToAction(nameof(Types));

            }

            return RedirectToAction(nameof(Types));
        }

        public async Task<ActionResult> EditType(string id)
        {
            var model = _mapper.Map(await _userTaskServices.GetTaskTypeById(id), new TaskTypeDtoViewModel { TaskTypeId = id });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditType(TaskTypeDtoViewModel model)
        {
            try
            {
                await _userTaskServices.EditTaskType(model);
            }
            catch
            {
                return RedirectToAction(nameof(Types));
            }

            return RedirectToAction(nameof(Types));
        }

        public async Task<ActionResult> DeleteType(string id)
        {
            await _userTaskServices.DeleteTaskType(id);

            return RedirectToAction(nameof(Types));
        }

        public async Task<IActionResult> GetJsonClientByCaseId(string caseId)
        {
            if (caseId.IsNullOrEmpty() || caseId == "0")
            {
                var freshlist = await _clientServices.CreateClientsSelectList();
                var newFreshlist = freshlist.ToList();
                newFreshlist.RemoveAt(0);
                return Json(newFreshlist);
            }

            var clients = await _clientServices.GetAll();
            var client = clients.FirstOrDefault(x => x.Cases.Any(y => y.Id == caseId));
            var result = new List<SelectListItem>();

            if (client == null)
            {
                return Json(result);
            }

            var filteredClient = _mapper.Map<ClientDtoViewModel>(client);

            result.Add(new SelectListItem()
            {
                Value = filteredClient.ClientId,
                Text = filteredClient.Name + " " + filteredClient.Surname
            });

            return Json(result);
        }

        public async Task<IActionResult> GetJsonCasesByClientId(string clientId)
        {
            if (clientId.IsNullOrEmpty() || clientId == "all")
            {
                var allCasesList = await _caseServices.CreateCasesSelectList(clientId);
                var allCasesSelectList = allCasesList.ToList();
                allCasesSelectList.RemoveAt(0);
                return Json(allCasesSelectList);
            }

            var cases = await _caseServices.GetAll();
            var filteredCases = cases.Where(x => x.Client?.Id == clientId);
            var casesSelectList = filteredCases.Select(item => new SelectListItem() { Text = item.Name, Value = item.Id }).ToList();

            return Json(casesSelectList);
        }

        private async Task<UserTaskDtoViewModel> PopulateSelectionListsForCreateView(UserTaskDtoViewModel model)
        {

            model.AllTaskTypesSelectList.AddRange(await _userTaskServices.CreateTaskTypeSelectList());
            model.AllClientsSelectList.AddRange(await _clientServices.CreateClientsSelectList());
            model.AllCasesSelectList.AddRange(await _caseServices.CreateCasesSelectList(model.Client?.Id ?? "all"));
            if (User.IsInRole("SysAdmin"))
            {
                model.AllUsersSelectList.AddRange(_userManager.CreateUsersSelectList());
                model.AllUsersSelectList.RemoveAt(0);
            }
            else
            {
                model.User = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            return model;
        }

        private UserTaskDtoViewModel ValidateUserTask(UserTaskDtoViewModel model)
        {

            if (model.DurationMinutes?.Count(x => x == ':') > 1)
            {
                ModelState.AddModelError("DurationMinutes", "Czas trwania może zawierać tylko jeden znak ':'");
            }


            if (model.StartTime?.Ticks > model.EndTime?.Ticks)
            {
                ModelState.AddModelError("StartTime", "Czas rozpoczęcia musi być przed czasem zakończenia");
            }

            if (model.StartTime?.Ticks > DateTime.Now.Ticks)
            {
                ModelState.AddModelError("StartTime", "Czas rozpoczęcia nie może być w przyszłości");
            }

            if (model.EndTime?.Ticks > DateTime.Now.Ticks)
            {
                ModelState.AddModelError("EndTime", "Czas zakończenia nie może być w przyszłości");
            }

            var convertedTicks = !model.DurationMinutes.IsNullOrEmpty() ? TimeSpan.FromMinutes(double.Parse(model.DurationMinutes?.ConvertTimeToMinutes())).Ticks : 0;
            var startEndGiven = model is { EndTime: not null, StartTime: not null };

            if (!model.DurationMinutes.IsNullOrEmpty() && startEndGiven && model.EndTime?.Ticks - model.StartTime?.Ticks != convertedTicks)
            {
                ModelState.AddModelError("DurationMinutes", $"Czas trwania nie zgadza się z różnicą między rozpoczęciem a zakończeniem");
            }

            var userCase = _caseServices.GetByIdWithIncludes(model.Case.Id, x => x.Client).Result;
            if (userCase?.Client != null && model.Client.Id != userCase.Client.Id)
            {
                ModelState.AddModelError("Client", $"Błąd danych - niezgodność");
            }

            ModelState.Remove("UserTaskId");
            ModelState.Remove("Case.Name");
            ModelState.Remove("Client.Name");
            ModelState.Remove("Client.Surname");
            ModelState.Remove("TaskType.Name");

            return model;
        }
    }
}
