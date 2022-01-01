using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<string> Get(string name)
        {
            var secret = await _secretClient.GetSecretAsync(name);

            return secret?.Value?.Value;
        }

        public Task Save(string name, string value)
        {
            return _secretClient.SetSecretAsync(name, value);
        }
    }
}
