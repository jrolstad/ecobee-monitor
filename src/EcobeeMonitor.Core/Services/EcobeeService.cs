using EcobeeMonitor.Core.Models.Ecobee;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            string scope = "smartRead")
        {
            var url = $"https://api.ecobee.com/authorize?response_type={responseType}&client_id={clientId}&scope={scope}";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<AuthorizationResult>();

            return data;
        }

        public async Task<TokenResult> RequestAccessToken(string clientId, 
            string code,
            string tokenType="jwt")
        {
            var url = $"https://api.ecobee.com/token?grant_type=ecobeePin&code={code}&client_id={clientId}&ecobee_type={tokenType}";

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(url, null);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<TokenResult>();

            return data;
        }

        public async Task<TokenResult> RefreshAccessToken(string clientId,
            string refreshToken,
            string tokenType = "jwt")
        {
            var url = $"https://api.ecobee.com/token?grant_type=refresh_token&code={refreshToken}&client_id={clientId}&ecobee_type={tokenType}";

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(url, null);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<TokenResult>();

            return data;
        }

        public async Task<RuntimeReportResult> GetRuntimeReport(string accessToken,
            DateTime? startDate,
            DateTime? endDate,
            List<string> columns,
            string thermostatId,
            bool includeSensors)
        {
            var request = new RuntimeReportRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                Columns = columns,
                Selection = new RuntimeReportSelection
                {
                    SelectionType = "thermostats",
                    SelectionMatch = thermostatId
                },
                IncludeSensors = includeSensors
            };

            
            var requestJson = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
            var url = $"https://api.ecobee.com/1/runtimeReport?format=json&body={requestJson}";

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<RuntimeReportResult>();

            return data;

        }


    }
}
