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
            _userTaskServices = userTaskServices;
            _hourlyPriceServices = hourlyPriceServices;
        }

        public async Task<ActionResult> Index()
        {
            var hourlyPrices = await _hourlyPriceServices.GetAll();
            var model = hourlyPrices.Select(item => _mapper.Map<HourlyPriceDtoViewModel>(item)).ToList();

            return View(model);
        }


        public ActionResult Details(string id)
        {
            return View();
        }


        public async Task<ActionResult> Create(string caseId = null)
        {
            var userCase = await _caseServices.GetById(caseId);
            var taskTypes = await _userTaskServices.GetAllTaskTypes();
            var model = new HourlyPriceDtoViewModel
            {
                Case = userCase
            };

            foreach (var taskType in taskTypes)
            {
                model.TaskTypesPriceList.Add(new SelectListItem()
                {
                    Text = taskType.Name,
                    Value = await _hourlyPriceServices.GetPriceByCaseAndTaskTypeName(caseId, taskType.Name)
                });
            }
            
            var cases = await _caseServices.CreateCasesSelectList("all");
            model.CasesSelectList = caseId == null ? cases.ToList() : cases.Where(x => x.Value == caseId).ToList();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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


        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
