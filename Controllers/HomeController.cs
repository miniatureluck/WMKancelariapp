using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDeadlineServices _deadlineServices;

        public HomeController(ILogger<HomeController> logger, IDeadlineServices deadlineServices)
        {
            _logger = logger;
            _deadlineServices = deadlineServices;
        }

        public async Task<IActionResult> Index()
        {
            var allDeadlines = await _deadlineServices.GetAll();
            var model = new HomeIndexViewModel();
            model.Deadlines.AddRange(allDeadlines);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}