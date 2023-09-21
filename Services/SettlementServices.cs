using System.Linq.Expressions;
using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Repository;

namespace WMKancelariapp.Services
{
    public class SettlementServices : ISettlementServices
    {
        private readonly IRepository<Settlement> _settlementRepository;
        private readonly IMapper _mapper;

        public SettlementServices(IRepository<Settlement> settlementRepository, IMapper mapper)
        {
            _settlementRepository = settlementRepository;
            _mapper = mapper;
        }

        public async Task Create(SettlementDtoViewModel newSettlement)
        {
            var settlement = _mapper.Map<Settlement>(newSettlement);
            await _settlementRepository.Insert(settlement);
        }
        
        public async Task<bool> Delete(string id)
        {
            var settlement = await _settlementRepository.GetById(id);
            return await _settlementRepository.Delete(settlement);
        }

        public async Task Edit(SettlementDtoViewModel editedSettlement)
        {
            var settlement = await GetById(editedSettlement.SettlementId);
            _mapper.Map(editedSettlement, settlement);
            await _settlementRepository.Update(settlement);
        }

        public async Task<IEnumerable<SettlementDtoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<SettlementDtoViewModel>>(await _settlementRepository.GetAll(x=>x.HourlyRate, x=>x.UserTask));
        }

        public async Task<Settlement> GetById(string id)
        {
            return await _settlementRepository.GetById(id);
        }

        public async Task<Settlement> GetByIdWithIncludes(string id, params Expression<Func<Settlement, object>>[] includes)
        {
            return await _settlementRepository.GetById(id, includes);
        }

        public async Task<SettlementDtoViewModel> GetDtoById(string id)
        {
            var settlement = _mapper.Map<SettlementDtoViewModel>(await _settlementRepository.GetById(id));
            return settlement;
        }
    }
}
