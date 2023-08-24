using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        Task<IEnumerable<TaskType>> GetAllTaskTypes();
        Task<TaskType> GetTaskTypeById(string id);
        Task CreateTaskType(TaskTypeDtoViewModel newTaskType);
        Task EditTaskType(TaskTypeDtoViewModel editedTaskType);
        Task<bool> DeleteTaskType(string id);
        Task<IEnumerable<SelectListItem>> CreateTaskTypeSelectList();
        Task<IEnumerable<Case>>? FindMostFrequentCaseForTaskType(TaskType taskType);
        UserTaskDtoViewModel CalculateDuration(UserTaskDtoViewModel model);
        Task<int> CountTaskTypes();
        Task<TaskType> GetTaskTypeByName(string name);
    }
}
