using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Models;

namespace WMKancelariapp.Services
{
    public interface ISettlementServices
    {
        Task<IEnumerable<SettlementDtoViewModel>> GetAll();
        Task Create(SettlementDtoViewModel newSettlement);
        Task Edit(SettlementDtoViewModel editedSettlement);
        Task<bool> Delete(string id);
        Task<Settlement> GetById(string id);
        Task<Settlement> GetByIdWithIncludes(string id, params Expression<Func<Settlement, object>>[] includes);
        Task<SettlementDtoViewModel> GetDtoById(string id);
    }
}