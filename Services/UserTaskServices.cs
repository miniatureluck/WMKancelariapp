using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Repository;

namespace WMKancelariapp.Services
{
    public class UserTaskServices : IUserTaskServices
    {
        private readonly IRepository<UserTask> _userTasks;
        private readonly IRepository<TaskType> _taskTypes;
        private readonly IMapper _mapper;
        public UserTaskServices(IRepository<UserTask> userTasks, IMapper mapper, IRepository<TaskType> taskTypes)
        {
            _userTasks = userTasks;
            _mapper = mapper;
            _taskTypes = taskTypes;
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
            return await _userTasks.GetAll(x => x.Client, x => x.Case, x => x.HourlyPrice, x => x.User, x=>x.TaskType);
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
            var userTaskDto = _mapper.Map<UserTaskDtoViewModel>(await _userTasks.GetById(id, x=>x.Client, x=>x.TaskType, x=>x.Case, x=>x.User));
            return userTaskDto;
        }

        public async Task<IEnumerable<TaskType>> GetAllTaskTypes()
        {
            return await _taskTypes.GetAll();
        }

        public async Task<IEnumerable<SelectListItem>> CreateTaskTypeSelectList()
        {
            var model = new List<SelectListItem>();
            var taskTypes = await _taskTypes.GetAll();
            foreach (var item in taskTypes)
            {
                var taskType = new SelectListItem
                {
                    Value = item.Id,
                    Text = item.Name
                };
                model.Add(taskType);
            }
            return model;
        }

        public async Task<TaskType> GetTaskTypeById(string id)
        {
            return await _taskTypes.GetById(id);
        }

        public async Task CreateTaskType(TaskTypeDtoViewModel newTaskType)
        {
            var taskType = _mapper.Map(newTaskType, new TaskType());
            await _taskTypes.Insert(taskType);

        }

        public async Task EditTaskType(TaskTypeDtoViewModel editedTaskType)
        {
            var taskToEdit = await _taskTypes.GetById(editedTaskType.TaskTypeId);
            await _taskTypes.Update(taskToEdit);
        }

        public async Task<bool> DeleteTaskType(string id)
        {
            var typeToDelete = await _taskTypes.GetById(id);
            return await _taskTypes.Delete(typeToDelete);
        }
    }
}
