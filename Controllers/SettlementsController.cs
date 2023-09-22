using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    public class SettlementsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISettlementServices _settlementServices;
        public SettlementsController(IMapper mapper, ISettlementServices settlementServices)
        {
            _mapper = mapper;
            _settlementServices = settlementServices;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _settlementServices.GetAll();

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
