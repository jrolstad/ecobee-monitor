using System.Threading.Tasks;
using EcobeeMonitor.Api.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace EcobeeMonitor.Api.Functions
{
    public class HealthFunctions
    {

        [Function("HealthFunctions")]
        public async Task<HttpResponseData> Probe([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route="health/probe")] HttpRequestData req)
        {
            return await req.OkResponseAsync();
        }
    }
}
