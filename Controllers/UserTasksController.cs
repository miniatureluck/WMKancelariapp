using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    public class UserTasksController : Controller
    {
        private readonly IUserTaskServices _userTaskServices;
        private readonly IMapper _mapper;
        public UserTasksController(IUserTaskServices userTaskServices, IMapper mapper)
        {
            _userTaskServices = userTaskServices;
            _mapper = mapper;
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
