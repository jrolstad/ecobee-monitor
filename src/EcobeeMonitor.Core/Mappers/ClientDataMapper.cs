using EcobeeMonitor.Core.Models.Data;

namespace EcobeeMonitor.Core.Mappers
{
    public class ClientDataMapper
    {
        public ClientData Map(string clientId)
        {
            return new ClientData
            {
                ClientId = clientId,
                AuthorizationStatus = AuthorizationStatus.New
            };
        }
    }
}
