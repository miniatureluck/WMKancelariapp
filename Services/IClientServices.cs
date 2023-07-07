using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Services
{
    public interface IClientServices
    {
        Task<IEnumerable<Client>> GetAll();
        Task Create(CreateClientViewModel newClient);
        Task Edit(CreateClientViewModel editedClient);
        Task<bool> Delete(string id);
        Task<Client> GetById(string id);
        Task<Client> GetByName(string name);
    }
}
