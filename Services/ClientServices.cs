﻿using AutoMapper;
using System.Linq.Expressions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;
using WMKancelariapp.Repository;

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
            var clientToEdit = await GetByName(editedClient.Name, editedClient.Surname ?? string.Empty);
            if (clientToEdit == null)
            {
                return;
            }
            await _clients.Update(clientToEdit);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clients.GetAll(x => x.Cases, x => x.AssignedUser, x => x.Prices, x => x.Tasks);
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
            return clientList.First(x => x.Name.Equals(name) && x.Surname != null && x.Surname.Equals(surname));
        }
    }
}
