using Azure.Security.KeyVault.Secrets;
using EcobeeMonitor.Core.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Services
{
    public class EcobeeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EcobeeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AuthorizationResult> Authorize(string clientId, 
            string responseType="ecobeePin",
            string scope = "SCOPE")
        {
            var url = $"https://api.ecobee.com/authorize?response_type={responseType}&client_id={clientId}&scope={scope}";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            
            var body = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<AuthorizationResult>(body);

            return data;
        }
    }
}
