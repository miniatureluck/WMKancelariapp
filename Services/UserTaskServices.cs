using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using WMKancelariapp.Extensions;
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
            return await _userTasks.GetAll(x => x.Client, x => x.Case, x => x.User, x=>x.TaskType);
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
            var result = await _taskTypes.GetAll(x => x.Tasks, x => x.HourlyPrices);
            var tempResult = new List<UserTask>();
            foreach (var item in result)
            {
                foreach (var userTask in item.Tasks)
                {
                    tempResult.Add(await _userTasks.GetById(userTask.Id, x=>x.Case, x=>x.Client));
                }
                item.Tasks = tempResult;
                tempResult.Clear();
            }

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> CreateTaskTypeSelectList()
        {
            var taskTypes = await _taskTypes.GetAll();
            var model = new List<SelectListItem>();
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
            var taskToEdit = _mapper.Map(editedTaskType, await _taskTypes.GetById(editedTaskType.TaskTypeId));
            await _taskTypes.Update(taskToEdit);
        }

        public async Task<bool> DeleteTaskType(string id)
        {
            var typeToDelete = await _taskTypes.GetById(id);
            return await _taskTypes.Delete(typeToDelete);
        }

        public async Task<IEnumerable<Case>>? FindMostFrequentCaseForTaskType(TaskType taskType)
        {
            if (taskType == null)
            {
                return null;
            }

            var tasks = await _userTasks.GetAll();
            var tasksOfType = tasks.Where(x => x.TaskType == taskType && x.Case != null);

            var caseCounts = tasksOfType.GroupBy(x => x.Case).Select(x => new { Case = x.Key, Count = x.Count() });

            var mostFrequentCases = caseCounts.Where(x=>x.Count == caseCounts.MaxBy(x => x.Count)?.Count).Select(x=>x.Case);

            return mostFrequentCases;
        }

        public UserTaskDtoViewModel CalculateDuration(UserTaskDtoViewModel model)
        {
            var startTicks = model.StartTime?.Ticks ?? 0;
            var endTicks = model.EndTime?.Ticks ?? 0;
            var duration = !model.DurationMinutes.IsNullOrEmpty() ? TimeSpan.FromMinutes(double.Parse(model.DurationMinutes?.ConvertTimeToMinutes())).Ticks : 0;

            if (duration == 0 && startTicks != 0 && endTicks != 0)
            {
                model.Duration = TimeSpan.FromTicks(endTicks - startTicks).Ticks;
            }

            if (startTicks == 0 && endTicks !=0 && duration != 0)
            {
                model.StartTime = new DateTime(endTicks - duration);
                model.Duration = TimeSpan.FromTicks(duration).Ticks;
            }

            if (startTicks != 0 && endTicks == 0 && duration != 0)
            {
                model.EndTime = new DateTime(startTicks + duration);
                model.Duration = TimeSpan.FromTicks(duration).Ticks;
            }

            if (startTicks == 0 && endTicks == 0 && duration != 0)
            {
                model.Duration = TimeSpan.FromTicks(duration).Ticks;
            }

            return model;
        }

        public async Task<int> CountTaskTypes()
        {
            return (await _taskTypes.GetAll()).Count;
        }

        public async Task<TaskType> GetTaskTypeByName(string name)
        {
            return (await GetAllTaskTypes()).FirstOrDefault(x => x.Name == name);
        }
    }
}
