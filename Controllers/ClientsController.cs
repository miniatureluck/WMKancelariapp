using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;

        public ClientsController(IClientServices clientServices, IMapper mapper)
        {
            _clientServices = clientServices;
            _mapper = mapper;
        }
        // GET: ClientsController
        public async Task<ActionResult> Index()
        {
            var model = new ClientIndexViewModel();
            var clientsDto = new List<ClientDtoViewModel>();
            
            var clients = await _clientServices.GetAll();
            foreach (var item in clients)
            {
                model.AllClients.Add(_mapper.Map(item, new ClientDtoViewModel()));
            }

            return View(model);
        }

        // GET: ClientsController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var client = await _clientServices.GetByIdWithIncludes(id, x=>x.AssignedUser, x=>x.Cases, x=>x.Prices);
            var model = _mapper.Map(client, new ClientDtoViewModel());
            return View(model);
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            var model = new ClientDtoViewModel();
            return View(model);
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientDtoViewModel model)
        {
            try
            {
                await _clientServices.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientsController/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
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

        // GET: ClientsController/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: ClientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
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
