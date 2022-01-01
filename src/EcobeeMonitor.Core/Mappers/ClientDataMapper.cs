using EcobeeMonitor.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

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
