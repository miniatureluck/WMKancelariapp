using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WMKancelariapp.Controllers
{
    public class CasesController : Controller
    {
        // GET: CasesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CasesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CasesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CasesController/Create
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

        // GET: CasesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CasesController/Edit/5
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

        // GET: CasesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CasesController/Delete/5
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
