﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Services;

namespace WMKancelariapp.Controllers
{
    [Authorize]
    public class HourlyPricesController : Controller
    {

        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;
        private readonly ICaseServices _caseServices;
        private readonly IUserTaskServices _userTaskServices;
        private readonly UserManager<User> _userManager;
        private readonly IHourlyPriceServices _hourlyPriceServices;
        public HourlyPricesController(IClientServices clientServices, IMapper mapper, ICaseServices caseServices,
            UserManager<User> userManager, IUserTaskServices userTaskServices, IHourlyPriceServices hourlyPriceServices)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _caseServices = caseServices;
            _userManager = userManager;
            _userTaskServices = userTaskServices;
            _hourlyPriceServices = hourlyPriceServices;
        }

        public async Task<ActionResult> Index()
        {
            var hourlyPrices = await _hourlyPriceServices.GetAll();
            var model = hourlyPrices.Select(item => _mapper.Map<HourlyPriceDtoViewModel>(item)).ToList();

            return View(model);
        }

        public async Task<ActionResult> Update(string id)
        {
            var userCase = await _caseServices.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            var taskTypes = await _userTaskServices.GetAllTaskTypes();

            var model = new HourlyPriceDtoViewModel
            {
                Case = userCase,
                User = user
            };

            foreach (var taskType in taskTypes)
            {
                var hourlyPrice = await _hourlyPriceServices.GetByCaseAndTaskTypeName(id, taskType.Name);
                model.TaskTypesPriceList.Add(new SelectListItem()
                {
                    Text = taskType.Name,
                    Value = hourlyPrice?.Price.ToString()
                });
            }

            var cases = await _caseServices.CreateCasesSelectList("all");
            model.CasesSelectList = cases.Where(x => x.Value == id).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(HourlyPriceDtoViewModel model)
        {
            var userCase = await _caseServices.GetByIdWithIncludes(model.Case.Id, x => x.AssignedUser, x => x.Tasks, x => x.Client, x => x.Prices);
            var user = userCase.AssignedUser;
            try
            {
                foreach (var taskType in model.TaskTypesPriceList.Where(x => x.Value != null))
                {
                    var taskTypeDto = await _userTaskServices.GetTaskTypeByName(taskType.Text);
                    var hourlyPriceEntity =
                        await _hourlyPriceServices.GetByCaseAndTaskTypeName(userCase.Id, taskType.Text);


                    var hourlyPriceDto = new HourlyPriceDtoViewModel()
                    {
                        Case = userCase,
                        CaseId = userCase.Id,
                        TaskType = await _userTaskServices.GetTaskTypeById(taskTypeDto.TaskTypeId),
                        TaskTypeId = taskTypeDto.TaskTypeId,
                        Price = int.Parse(taskType.Value),
                        User = user,
                        HourlyPriceId = hourlyPriceEntity?.Id,
                    };
                    UpdateUserTaskValuesByCase(userCase, taskTypeDto.TaskTypeId, hourlyPriceDto.Price);

                    if (await _hourlyPriceServices.GetByCaseAndTaskTypeName(userCase.Id, taskType.Text) == null)
                    {
                        await _hourlyPriceServices.Create(hourlyPriceDto);
                    }
                    else
                    {
                        if (hourlyPriceEntity.Price != int.Parse(taskType.Value))
                        {
                            await _hourlyPriceServices.Edit(hourlyPriceDto);
                        }
                    }


                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
        
        public async Task<ActionResult> Delete(string id)
        {
            ResetUserTasksValuesForThisHourlyPrice(id);

            await _hourlyPriceServices.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<ActionResult> DeleteAllForCase(string id)
        {
            try
            {
                var userCase = await _caseServices.GetByIdWithIncludes(id, x => x.AssignedUser, x => x.Client, x => x.Tasks, x => x.Prices);
                foreach (var item in userCase.Prices)
                {
                    await ResetUserTasksValuesForThisHourlyPrice(item.Id);
                }
                userCase.Prices?.Clear();
                var caseDto = _mapper.Map<CaseDtoViewModel>(userCase);
                await _caseServices.Edit(caseDto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task ResetUserTasksValuesForThisHourlyPrice(string id)
        {
            var hourlyPriceDto = await _hourlyPriceServices.GetDtoById(id);
            var userCase = await _caseServices.GetByIdWithIncludes(hourlyPriceDto.CaseId, x => x.Tasks);
            UpdateUserTaskValuesByCase(userCase, hourlyPriceDto.TaskTypeId, -1);
        }

        private static void UpdateUserTaskValuesByCase(Case userCase, string taskTypeId, int newRate)
        {
            var userTasks = userCase.Tasks.Where(x => x.TaskType.Id == taskTypeId);

            foreach (var item in userTasks)
            {
                var duration = item.Duration;

                if (!duration.HasValue)
                {
                    continue;
                }

                if (newRate == -1)
                {
                    item.Value = newRate;
                }
                else
                {
                    var timeRate = TimeSpan.FromTicks((long)duration).TotalMinutes / 60;
                    var result = newRate * timeRate;

                    item.Value = Convert.ToInt32(result);
                }
            }
        }
    }
}
