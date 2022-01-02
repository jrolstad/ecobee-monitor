using System.Net.Http;
using System.Threading.Tasks;
using EcobeeMonitor.Core.Orchestrators;
using EcobeeMonitor.Api.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace EcobeeMonitor.Api.Functions
{
    public class AuthorizationFunctions
    {
        private readonly AuthorizationOrchestrator _authorizationOrchestrator;

        public AuthorizationFunctions(AuthorizationOrchestrator authorizationOrchestrator)
        {
            _authorizationOrchestrator = authorizationOrchestrator;
        }

        [Function("authorization-request")]
        public async Task<HttpResponseData> RequestAuthorization([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "authorization/{clientId}/request")] HttpRequestData req,
            string clientId)
        {
            var response = await _authorizationOrchestrator.RequestAuthorization(clientId);

            return await req.OkResponseAsync(response);
        }

        [Function("authorization-approve")]
        public async Task<HttpResponseData> ApproveAuthorization([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "authorization/{clientId}/approve")] HttpRequestData req,
            string clientId)
        {

            await _authorizationOrchestrator.ApproveAuthorization(clientId);

            return await req.OkResponseAsync();
        }
    }
}
