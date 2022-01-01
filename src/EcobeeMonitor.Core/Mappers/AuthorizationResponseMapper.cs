using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;

namespace EcobeeMonitor.Core.Mappers
{
    public class AuthorizationResponseMapper
    {
        public AuthorizationResponse Map(string clientId, AuthorizationResult toMap)
        {
            return new AuthorizationResponse
            {
                ClientId = clientId,
                Pin = toMap?.EcobeePin,
                Scope = toMap?.Scope
            };
        }
    }
}
