using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    public class CasesController : Controller
    {
        private readonly ICaseServices _caseServices;
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;
        public CasesController(ICaseServices caseServices, IClientServices clientServices, IMapper mapper)
        {
            _caseServices = caseServices;
            _clientServices = clientServices;
            _mapper = mapper;
        }
        // GET: CasesController
        public async Task<ActionResult> Index()
        {
            var model = new CaseIndexViewModel();

            var cases = await _caseServices.GetAll();
            foreach (var item in cases)
            {
                model.AllCases.Add(_mapper.Map(item, new CaseDtoViewModel()));
            }

            return View(model);
        }

        // GET: CasesController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var userCase =
                await _caseServices.GetByIdWithIncludes(id, x => x.AssignedUser, x => x.Tasks, x => x.Client);
            var model = _mapper.Map(userCase, new CaseDtoViewModel());
            return View(model);
        }

        // GET: CasesController/Create
        public async Task<ActionResult> Create()
        {
            var model = new CaseDtoViewModel();
            model.AllClients.AddRange(await _clientServices.GetAll());
            return View(model);
        }

        // POST: CasesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CaseDtoViewModel model)
        {
            try
            {
                await _caseServices.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CasesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _caseServices.GetDtoById(id);
            return View(model);
        }

        // POST: CasesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CaseDtoViewModel model)
        {
            try
            {
                await _caseServices.Edit(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CasesController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var model = await _caseServices.GetDtoById(id);
            return View(model);
        }

        // POST: CasesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _caseServices.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
