using System.Linq.Expressions;
using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Repository;

namespace WMKancelariapp.Services
{
    public class CaseServices : ICaseServices
    {
        private readonly IRepository<Case> _caseRepository;
        private readonly IMapper _mapper;

        public CaseServices(IRepository<Case> caseRepository, IMapper mapper)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
        }
        public async Task Create(CaseDtoViewModel newCase)
        {
            await _caseRepository.Insert(_mapper.Map<Case>(newCase));
        }

        public async Task<bool> Delete(string id)
        {
            var caseToDelete = await _caseRepository.GetById(id);
            return await _caseRepository.Delete(caseToDelete);
        }

        public async Task Edit(CaseDtoViewModel editedCase)
        {
            var caseToEdit = _mapper.Map(editedCase, await _caseRepository.GetById(editedCase.CaseId, x=>x.Client, x=>x.AssignedUser, x=>x.Tasks));
            if (caseToEdit == null)
            {
                return;
            }
            
            await _caseRepository.Update(caseToEdit);
        }

        public async Task<IEnumerable<Case>> GetAll()
        {
            return await _caseRepository.GetAll(x=>x.AssignedUser, x=>x.Client, x=>x.Tasks);
        }

        public async Task<Case> GetById(string id)
        {
            return await _caseRepository.GetById(id);
        }

        public async Task<Case> GetByIdWithIncludes(string id, params Expression<Func<Case, object>>[] includes)
        {
            return await _caseRepository.GetById(id, includes);
        }

        public async Task<CaseDtoViewModel> GetDtoById(string id)
        {
            return _mapper.Map<CaseDtoViewModel>(await _caseRepository.GetById(id));
        }

        public async Task<CaseDtoViewModel> GetDtoByName(string name)
        {
            var cases = await _caseRepository.GetAll();
            var userCase = cases.FirstOrDefault(x=>x.Name == name);
            return _mapper.Map<CaseDtoViewModel>(userCase);
        }
    }
}
