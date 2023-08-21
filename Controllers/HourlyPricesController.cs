using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        // GET: HourlyPricesController
        public async Task<ActionResult> Index()
        {
            var hourlyPrices = await _hourlyPriceServices.GetAll();
            var model = hourlyPrices.Select(item => _mapper.Map<HourlyPriceDtoViewModel>(item)).ToList();

            return View(model);
        }

        // GET: HourlyPricesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HourlyPricesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HourlyPricesController/Create
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

        // GET: HourlyPricesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HourlyPricesController/Edit/5
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

        // GET: HourlyPricesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HourlyPricesController/Delete/5
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
