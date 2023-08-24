using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
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
            var taskTypes = await _userTaskServices.GetAllTaskTypes();
            var model = new HourlyPriceDtoViewModel
            {
                Case = userCase
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
            var userCase = await _caseServices.GetByIdWithIncludes(model.Case.Id, x=>x.AssignedUser, x=>x.Tasks, x=>x.Client, x=>x.Prices);
            var user = userCase.AssignedUser;
            try
            {
                foreach (var taskType in model.TaskTypesPriceList.Where(x=>x.Value != null))
                {
                    var taskTypeEntity = await _userTaskServices.GetTaskTypeByName(taskType.Text);
                    var hourlyPriceDto = new HourlyPriceDtoViewModel()
                    {
                        Case = userCase,
                        CaseId = userCase.Id,
                        TaskType = taskTypeEntity,
                        TaskTypeId = taskTypeEntity.Id,
                        Price = int.Parse(taskType.Value),
                        User = user,
                        LastModified = DateTime.Now,
                        HourlyPriceId = (await _hourlyPriceServices.GetByCaseAndTaskTypeName(userCase.Id, taskType.Text))?.Id
                    };
                    if (await _hourlyPriceServices.GetByCaseAndTaskTypeName(userCase.Id, taskType.Text) == null)
                    {
                        await _hourlyPriceServices.Create(hourlyPriceDto);
                    }
                    else
                    {
                        await _hourlyPriceServices.Edit(hourlyPriceDto);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
