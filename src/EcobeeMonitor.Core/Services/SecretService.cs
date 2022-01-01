using Azure.Security.KeyVault.Secrets;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Services
{
    public class SecretService
    {
        private readonly SecretClient _secretClient;

        public SecretService(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<string> Get(string clientId, string name)
        {
            var resolvedName = GetResolvedSecretname(clientId, name);
            var secret = await _secretClient.GetSecretAsync(resolvedName);

            return secret?.Value?.Value;
        }

        public Task Save(string clientId, string name, string value)
        {
            var resolvedName = GetResolvedSecretname(clientId, name);
            return _secretClient.SetSecretAsync(resolvedName, value);
        }

        private string GetResolvedSecretname(string clientId, string name)
        {
            return $"{name}-{clientId}";
        }
    }
}
