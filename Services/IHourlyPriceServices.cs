using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Models;

namespace WMKancelariapp.Services
{
    public interface IHourlyPriceServices
    {
        Task<IEnumerable<HourlyPrice>> GetAll();
        Task Create(HourlyPriceDtoViewModel newHourlyPrice);
        Task Edit(HourlyPriceDtoViewModel editedHourlyPrice);
        Task<bool> Delete(string id);
        Task<HourlyPrice> GetById(string id);
        Task<HourlyPrice> GetByIdWithIncludes(string id, params Expression<Func<HourlyPrice, object>>[] includes);
        Task<HourlyPriceDtoViewModel> GetDtoById(string id);
        Task<IEnumerable<HourlyPrice>> GetByCaseId(string caseId);
        Task<string> GetPriceByCaseAndTaskTypeName (string caseId, string taskTypeName);
    }
}
