using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Services
{
    public class ClientServices : IClientServices
    {
        public Task Create(CreateClientViewModel newClient)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(CreateClientViewModel editedClient)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
