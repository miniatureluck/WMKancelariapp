using System.Linq.Expressions;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Models;

namespace WMKancelariapp.Services
{
    public interface IUserTaskServices
    {
        Task<IEnumerable<UserTask>> GetAll();
        Task Create(UserTaskDtoViewModel newUserTask);
        Task Edit(UserTaskDtoViewModel editedUserTask);
        Task<bool> Delete(string id);
        Task<UserTask> GetById(string id);
        Task<UserTask> GetByIdWithIncludes(string id, params Expression<Func<UserTask, object>>[] includes);
        Task<UserTask> GetByClientNameSurname(string name, string surname);
        Task<UserTaskDtoViewModel> GetDtoById(string id);
    }
}
