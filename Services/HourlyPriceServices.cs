﻿using AutoMapper;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Repository;

namespace WMKancelariapp.Services
{
    public class HourlyPriceServices : IHourlyPriceServices
    {
        private readonly IRepository<HourlyPrice> _hourlyPricesRepository;
        private readonly IMapper _mapper;

        public HourlyPriceServices(IRepository<HourlyPrice> hourlyPricesRepository, IMapper mapper)
        {
            _hourlyPricesRepository = hourlyPricesRepository;
            _mapper = mapper;
        }

        public async Task<int> CountSpecifiedPricesForCase(string caseId)
        {
            return (await GetByCaseId(caseId)).Count();
        }

        public async Task Create(HourlyPriceDtoViewModel newHourlyPrice)
        {
            await _hourlyPricesRepository.Insert(_mapper.Map<HourlyPrice>(newHourlyPrice));
        }

        public async Task<bool> Delete(string id)
        {
            var hourlyPriceToDelete = await _hourlyPricesRepository.GetById(id);
            return await _hourlyPricesRepository.Delete(hourlyPriceToDelete);
        }

        public async Task Edit(HourlyPriceDtoViewModel editedHourlyPrice)
        {
            var hourlyPrice = _mapper.Map(editedHourlyPrice,
                await _hourlyPricesRepository.GetById(editedHourlyPrice.HourlyPriceId, x => x.Case, x => x.TaskType, x => x.User));
            if (hourlyPrice == null)
            {
                return;
            }

            await _hourlyPricesRepository.Update(hourlyPrice);
        }

        public async Task<IEnumerable<HourlyPrice>> GetAll()
        {
            return await _hourlyPricesRepository.GetAll(x=>x.Case, x=>x.TaskType);
        }

        public async Task<IEnumerable<HourlyPrice>> GetByCaseId(string caseId)
        {
            var hourlyPrices = await GetAll();
            return hourlyPrices.Where(x => x.Case?.Id == caseId);
        }

        public async Task<HourlyPrice> GetById(string id)
        {
            return await _hourlyPricesRepository.GetById(id);
        }

        public async Task<HourlyPrice> GetByIdWithIncludes(string id, params Expression<Func<HourlyPrice, object>>[] includes)
        {
            return await _hourlyPricesRepository.GetById(id, includes);
        }

        public async Task<HourlyPriceDtoViewModel> GetDtoById(string id)
        {
            return _mapper.Map<HourlyPriceDtoViewModel>(await GetByIdWithIncludes(id, x => x.TaskType, x => x.Case,
                x => x.User));
        }

        public async Task<HourlyPrice> GetByCaseAndTaskTypeName(string caseId, string taskTypeName)
        {
            if (caseId.IsNullOrEmpty() || taskTypeName.IsNullOrEmpty())
            {
                return null;
            }
            var hourlyPrices = await GetByCaseId(caseId);
            var result = hourlyPrices.FirstOrDefault(x => x.TaskType.Name == taskTypeName);

            return result;
        }

        public async Task<int> GetHourlyRateForUserTask(string caseId, string taskTypeId)
        {
            var hourlyPrice = (await GetAll()).FirstOrDefault(x => x.CaseId == caseId && x.TaskTypeId == taskTypeId);
            var result = hourlyPrice?.Price ?? -1;

            return result;
        }
    }
}
