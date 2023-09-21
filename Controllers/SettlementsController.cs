using Microsoft.AspNetCore.Mvc;

namespace WMKancelariapp.Controllers
{
    public class SettlementsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
