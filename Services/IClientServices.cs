using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Services
{
    public interface IClientServices
    {
        Task<IEnumerable<Client>> GetAll();
        Task Create(ClientDtoViewModel newClient);
        Task Edit(ClientDtoViewModel editedClient);
        Task<bool> Delete(string id);
        Task<Client> GetById(string id);
        Task<Client> GetByIdWithIncludes(string id, params Expression<Func<Client, object>>[] includes);
        Task<Client> GetByName(string name, string surname);
        Task<ClientDtoViewModel> GetDtoById(string id);
        public Task<IEnumerable<SelectListItem>> CreateClientsSelectList();
        public Task<IEnumerable<SelectListItem>> GetJsonClientByCaseId(string caseId);

    }
}
