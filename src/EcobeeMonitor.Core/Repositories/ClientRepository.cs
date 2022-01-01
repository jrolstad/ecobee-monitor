using EcobeeMonitor.Core.Models.Data;
using EcobeeMonitor.Core.Services;
using System.Threading.Tasks;
using EcobeeMonitor.Core.Configuration;
using System.Collections.Generic;

namespace EcobeeMonitor.Core.Repositories
{
    public class ClientRepository
    {
        private readonly CosmosDbService _cosmosDbService;

        public ClientRepository(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<ICollection<ClientData>> Get()
        {
            var query = _cosmosDbService
                .Query<ClientData>(CosmosConfiguration.Containers.Clients);

            var result = await _cosmosDbService.ExecuteRead(query, d=>d);

            return result;
        }

        public Task<ClientData> Get(string clientId)
        {
            return _cosmosDbService.Get<ClientData>(clientId, CosmosConfiguration.Containers.Clients, CosmosConfiguration.DefaultPartitionKey);
        }

        public Task<ClientData> Save(ClientData toSave)
        {
            toSave.Id = toSave.ClientId;
            toSave.Area = CosmosConfiguration.DefaultPartitionKey;

            return _cosmosDbService.Save(toSave, CosmosConfiguration.Containers.Clients);
        }
    }
}
