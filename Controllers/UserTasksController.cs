using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Controllers
{
    public class UserTasksController : Controller
    {
        // GET: UserTasksController
        public ActionResult Index()
        {
            var model = new UserTaskIndexViewModel();
            return View(model);
        }

        // GET: UserTasksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserTasksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserTasksController/Create
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

        // GET: UserTasksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserTasksController/Edit/5
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

        // GET: UserTasksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserTasksController/Delete/5
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
