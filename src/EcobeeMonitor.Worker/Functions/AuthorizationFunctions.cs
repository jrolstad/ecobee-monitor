using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EcobeeMonitor.Core.Orchestrators;
using EcobeeMonitor.Worker.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace EcobeeMonitor.Worker.Functions
{
    public class AuthorizationFunctions
    {
        private readonly AuthorizationOrchestrator _authorizationOrchestrator;

        public AuthorizationFunctions(AuthorizationOrchestrator authorizationOrchestrator)
        {
            _authorizationOrchestrator = authorizationOrchestrator;
        }

        [Function("authorization-request")]
        public async Task<HttpResponseData> RequestAuthorization([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "authorization/request")] HttpRequestData req)
        {
            var response = await _authorizationOrchestrator.RequestAuthorization();

            return await req.OkResponseAsync(response);
        }

        [Function("authorization-approve")]
        public async Task<HttpResponseData> ApproveAuthorization([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "authorization/approve")] HttpRequestData req)
        {
            var parameters = req.Url.ParseQueryString();
            var code=parameters["code"];

            await _authorizationOrchestrator.ApproveAuthorization(code);

            return await req.OkResponseAsync();
        }
    }
}
