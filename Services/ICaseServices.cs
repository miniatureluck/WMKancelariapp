using System.Linq.Expressions;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMKancelariapp.Services
{
    public interface ICaseServices
    {
        Task<IEnumerable<Case>> GetAll();
        Task Create(CaseDtoViewModel newCase);
        Task Edit(CaseDtoViewModel editedCase);
        Task<bool> Delete(string id);
        Task<Case> GetById(string id);
        Task<Case> GetByIdWithIncludes(string id, params Expression<Func<Case, object>>[] includes);
        Task<CaseDtoViewModel> GetDtoById(string id);
        Task<IEnumerable<SelectListItem>> CreateCasesSelectList(string clientId, string? userId = "all");
    }
}
