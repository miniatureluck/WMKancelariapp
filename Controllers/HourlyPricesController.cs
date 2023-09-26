using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    [Authorize]
    public class HourlyPricesController : Controller
    {

        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;
        private readonly ICaseServices _caseServices;
        private readonly IUserTaskServices _userTaskServices;
        private readonly UserManager<User> _userManager;
        private readonly IHourlyPriceServices _hourlyPriceServices;
        public HourlyPricesController(IClientServices clientServices, IMapper mapper, ICaseServices caseServices,
            UserManager<User> userManager, IUserTaskServices userTaskServices, IHourlyPriceServices hourlyPriceServices)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _caseServices = caseServices;
            _userManager = userManager;
            _userTaskServices = userTaskServices;
            _hourlyPriceServices = hourlyPriceServices;
        }

        public async Task<ActionResult> Index()
        {
            var hourlyPrices = await _hourlyPriceServices.GetAll();
            var model = hourlyPrices.Select(item => _mapper.Map<HourlyPriceDtoViewModel>(item)).ToList();

            return View(model);
        }

        public async Task<ActionResult> Update(string id)
        {
            var userCase = await _caseServices.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            var taskTypes = await _userTaskServices.GetAllTaskTypes();
            
            var model = new HourlyPriceDtoViewModel
            {
                Case = userCase,
                User = user
            };

            foreach (var taskType in taskTypes)
            {
                var hourlyPrice = await _hourlyPriceServices.GetByCaseAndTaskTypeName(id, taskType.Name);
                model.TaskTypesPriceList.Add(new SelectListItem()
                {
                    Text = taskType.Name,
                    Value = hourlyPrice?.Price.ToString()
                });
            }

            var cases = await _caseServices.CreateCasesSelectList("all");
            model.CasesSelectList = cases.Where(x => x.Value == id).ToList();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(HourlyPriceDtoViewModel model)
        {
            var userCase = await _caseServices.GetByIdWithIncludes(model.Case.Id, x => x.AssignedUser, x => x.Tasks, x => x.Client, x => x.Prices);
            var user = userCase.AssignedUser;
            try
            {
                foreach (var taskType in model.TaskTypesPriceList.Where(x => x.Value != null))
                {
                    var taskTypeDto = await _userTaskServices.GetTaskTypeByName(taskType.Text);
                    var hourlyPriceEntity =
                        await _hourlyPriceServices.GetByCaseAndTaskTypeName(userCase.Id, taskType.Text);
                    

                    var hourlyPriceDto = new HourlyPriceDtoViewModel()
                    {
                        Case = userCase,
                        CaseId = userCase.Id,
                        TaskType = await _userTaskServices.GetTaskTypeById(taskTypeDto.TaskTypeId),
                        TaskTypeId = taskTypeDto.TaskTypeId,
                        Price = int.Parse(taskType.Value),
                        User = user,
                        HourlyPriceId = hourlyPriceEntity?.Id,
                    };

                    if (await _hourlyPriceServices.GetByCaseAndTaskTypeName(userCase.Id, taskType.Text) == null)
                    {
                        await _hourlyPriceServices.Create(hourlyPriceDto);
                    }
                    else
                    {
                        if (hourlyPriceEntity.Price != int.Parse(taskType.Value))
                        {
                            await _hourlyPriceServices.Edit(hourlyPriceDto);
                        }
                    }

                    UpdateUserTaskValuesByCase(userCase.Id, taskTypeDto.TaskTypeId, hourlyPriceDto.Price);

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }


        public async Task<ActionResult> Delete(string id)
        {
            await _hourlyPriceServices.Delete(id);

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> DeleteAllForCase(string id)
        {
            try
            {
                var userCase = await _caseServices.GetByIdWithIncludes(id, x => x.AssignedUser, x => x.Client, x => x.Tasks, x => x.Prices);
                userCase.Prices?.Clear();
                var caseDto = _mapper.Map<CaseDtoViewModel>(userCase);
                await _caseServices.Edit(caseDto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        private async Task UpdateUserTaskValuesByCase(string userCaseId, string taskTypeId, int newValue)
        {
            var userTasks = (await _caseServices.GetByIdWithIncludes(userCaseId, x=>x.Tasks)).Tasks.Where(x=>x.TaskType.Id == taskTypeId);

            foreach (var item in userTasks)
            {
                item.Value = newValue;
                await _userTaskServices.Edit(_mapper.Map<UserTaskDtoViewModel>(item));
            }
        }
    }
}
