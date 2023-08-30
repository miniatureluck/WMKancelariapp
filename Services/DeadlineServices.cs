using System.Linq.Expressions;
using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Repository;

namespace WMKancelariapp.Services
{
    public class DeadlineServices : IDeadlineServices
    {
        private readonly IRepository<Deadline> _deadlineRepository;
        private readonly IMapper _mapper;
        public DeadlineServices(IRepository<Deadline> deadlineRepository, IMapper mapper)
        {
            _deadlineRepository = deadlineRepository;
            _mapper = mapper;
        }

        public async Task Create(DeadlineDtoViewModel deadlineDto)
        {
            var deadline = _mapper.Map<Deadline>(deadlineDto);
            await _deadlineRepository.Insert(deadline);
        }

        public async Task<bool> Delete(string id)
        {
            var deadline = await _deadlineRepository.GetById(id);
            return await _deadlineRepository.Delete(deadline);
        }

        public async Task Edit(DeadlineDtoViewModel deadlineDto)
        {
            var deadline = await GetById(deadlineDto.DeadlineId);
            _mapper.Map(deadlineDto, deadline);
            await _deadlineRepository.Update(deadline);
        }

        public async Task<IEnumerable<DeadlineDtoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<DeadlineDtoViewModel>>(await _deadlineRepository.GetAll(x=>x.Case, x=>x.User));
        }

        public async Task<Deadline> GetById(string id)
        {
            return await _deadlineRepository.GetById(id);
        }

        public async Task<Deadline> GetByIdWithIncludes(string id, params Expression<Func<Deadline, object>>[] includes)
        {
            return await _deadlineRepository.GetById(id, includes);
        }

        public async Task<DeadlineDtoViewModel> GetDtoById(string id)
        {
            var deadlineDto = _mapper.Map<DeadlineDtoViewModel>(await _deadlineRepository.GetById(id, x=>x.Case, x=>x.User));
            return deadlineDto;
        }
    }
}
