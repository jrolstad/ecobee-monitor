using EcobeeMonitor.Core.Mappers;
using EcobeeMonitor.Core.Models.Data;
using EcobeeMonitor.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class ClientOrchestrator
    {
        private readonly ClientRepository _clientRepository;
        private readonly ClientDataMapper _clientDataMapper;

        public ClientOrchestrator(ClientRepository clientRepository,
            ClientDataMapper clientDataMapper)
        {
            _clientRepository = clientRepository;
            _clientDataMapper = clientDataMapper;
        }

        public async Task<ICollection<ClientData>> Get()
        {
            var data = await _clientRepository.Get();

            return data;
        }

        public async Task<ClientData> Get(string clientId)
        {
            var data = await _clientRepository.Get(clientId);
            if (data == null) data = _clientDataMapper.Map(clientId);

            return data;
        }

        public async Task SaveAuthorizationStatus(string clientId, AuthorizationStatus status)
        {
            var data = await Get(clientId);

            data.AuthorizationStatus = status;
            await _clientRepository.Save(data);
        }
    }
}
