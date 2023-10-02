using AutoMapper;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Repository;
using Microsoft.IdentityModel.Tokens;

namespace WMKancelariapp.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IRepository<Client> _clients;
        private readonly IMapper _mapper;
        public ClientServices(IRepository<Client> clients, IMapper mapper)
        {
            _clients = clients;
            _mapper = mapper;
        }
        public async Task Create(ClientDtoViewModel newClient)
        {
            var client = _mapper.Map<Client>(newClient);
            await _clients.Insert(client);
        }

        public async Task<bool> Delete(string id)
        {
            var clientToDelete = await _clients.GetById(id);
            return await _clients.Delete(clientToDelete);
        }
        public async Task Edit(ClientDtoViewModel editedClient)
        {
            var clients = await GetAll();
            var clientToEdit = _mapper.Map(editedClient, clients.FirstOrDefault(x=>x.Id == editedClient.ClientId));
            if (clientToEdit == null)
            {
                return;
            }
            
            await _clients.Update(clientToEdit);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clients.GetAll(x => x.Cases, x => x.AssignedUser, x => x.Tasks);
        }

        public async Task<Client> GetById(string id)
        {
            return await _clients.GetById(id);
        }

        public async Task<Client> GetByIdWithIncludes(string id, params Expression<Func<Client, object>>[] includes)
        {
            return await _clients.GetById(id, includes);
        }

        public async Task<Client> GetByName(string name, string surname)
        {
            var clientList = await GetAll();
            return clientList.FirstOrDefault(x => x.Name.Equals(name) && x.Surname != null && x.Surname.Equals(surname));
        }

        public async Task<ClientDtoViewModel> GetDtoById(string id)
        {
            var clientDto = _mapper.Map<ClientDtoViewModel>(await _clients.GetById(id, x=>x.AssignedUser, x=>x.Cases, x=>x.Tasks));
            return clientDto;
        }

        public async Task<IEnumerable<SelectListItem>> CreateClientsSelectList()
        {
            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Brak",
                    Value = "all"

                }
            };

            var clients = await GetAll();

            model.AddRange(clients.Select(item => new SelectListItem { Text = $"{item.Name} {item.Surname}", Value = item.Id }));
            return model;
        }

        public async Task<IEnumerable<SelectListItem>> GetJsonClientByCaseId(string caseId)
        {
            if (caseId.IsNullOrEmpty() || caseId == "0")
            {
                var freshlist = await CreateClientsSelectList();
                var newFreshlist = freshlist.ToList();
                newFreshlist.RemoveAt(0);
                return newFreshlist;
            }

            var clients = await GetAll();
            var client = clients.FirstOrDefault(x => x.Cases.Any(y => y.Id == caseId));
            var result = new List<SelectListItem>();

            if (client == null)
            {
                return result;
            }

            var filteredClient = _mapper.Map<ClientDtoViewModel>(client);

            result.Add(new SelectListItem()
            {
                Value = filteredClient.ClientId,
                Text = filteredClient.Name + " " + filteredClient.Surname
            });

            return result;
        }
    }
}
