using EcobeeMonitor.Core.Models.Data;
using EcobeeMonitor.Core.Services;
using System.Threading.Tasks;
using EcobeeMonitor.Core.Configuration;

namespace EcobeeMonitor.Core.Repositories
{
    public class ClientRepository
    {
        private readonly CosmosDbService _cosmosDbService;

        public ClientRepository(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
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
