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
        private readonly IUserTaskServices _userTaskServices;
        public SettlementsController(IMapper mapper, ISettlementServices settlementServices, IUserTaskServices userTaskServices)
        {
            _mapper = mapper;
            _settlementServices = settlementServices;
            _userTaskServices = userTaskServices;
        }
        public async Task<IActionResult> Index()
        {
            //var model = await _settlementServices.GetAll();
            var model = _mapper.Map<IEnumerable<UserTaskDtoViewModel>>(await _userTaskServices.GetAll());
            return View(model.Where(x=>x.SettlementId == null));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
