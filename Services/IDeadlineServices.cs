using System.Linq.Expressions;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Models;

namespace WMKancelariapp.Services
{
    public interface IDeadlineServices
    {
        Task<IEnumerable<DeadlineDtoViewModel>> GetAll();
        Task Create(DeadlineDtoViewModel deadlineDto);
        Task Edit(DeadlineDtoViewModel deadlineDto);
        Task<bool> Delete(string id);
        Task<Deadline> GetById(string id);
        Task<Deadline> GetByIdWithIncludes(string id, params Expression<Func<Deadline, object>>[] includes);
        Task<DeadlineDtoViewModel> GetDtoById(string id);
    }
}
