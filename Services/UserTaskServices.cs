using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Repository;

namespace WMKancelariapp.Services
{
    public class UserTaskServices : IUserTaskServices
    {
        private readonly IRepository<UserTask> _userTasks;
        private readonly IMapper _mapper;
        public UserTaskServices(IRepository<UserTask> userTasks, IMapper mapper)
        {
            _userTasks = userTasks;
            _mapper = mapper;
        }
        public async Task Create(UserTaskDtoViewModel newUserTask)
        {
            var userTask = _mapper.Map<UserTask>(newUserTask);
            await _userTasks.Insert(userTask);
        }

        public async Task<bool> Delete(string id)
        {
            var userTaskToDelete = await _userTasks.GetById(id);
            return await _userTasks.Delete(userTaskToDelete);
        }

        public async Task Edit(UserTaskDtoViewModel editedUserTask)
        {
            var userTaskToEdit = _mapper.Map(editedUserTask, await GetById(editedUserTask.UserTaskId));
            if (userTaskToEdit == null)
            {
                return;
            }

            await _userTasks.Update(userTaskToEdit);
        }

        public async Task<IEnumerable<UserTask>> GetAll()
        {
            return await _userTasks.GetAll(x => x.Client, x => x.Case, x => x.HourlyPrice, x => x.User);
        }

        public async Task<UserTask> GetById(string id)
        {
            return await _userTasks.GetById(id);
        }

        public async Task<UserTask> GetByIdWithIncludes(string id, params Expression<Func<UserTask, object>>[] includes)
        {
            return await _userTasks.GetById(id, includes);
        }

        public async Task<UserTask> GetByClientNameSurname(string name, string surname)
        {
            var userTasks = await GetAll();
            return userTasks.FirstOrDefault(x => x.Client.Surname.Equals(surname));
        }

        public async Task<UserTaskDtoViewModel> GetDtoById(string id)
        {
            var userTaskDto = _mapper.Map<UserTaskDtoViewModel>(await _userTasks.GetById(id));
            return userTaskDto;
        }
    }
}
