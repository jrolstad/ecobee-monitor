using System.Net.Http;
using System.Threading.Tasks;
using EcobeeMonitor.Core.Orchestrators;
using EcobeeMonitor.Worker.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace EcobeeMonitor.Worker.Functions
{
    public class ThermostatFunctions
    {
        private readonly ThermostatOrchestrator _orchestrator;

        public ThermostatFunctions(ThermostatOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [Function("thermostat-get")]
        public async Task<HttpResponseData> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "thermostat/{thermostatId}")] HttpRequestData req,
           string thermostatId)
        {
            var response = await _orchestrator.Get(thermostatId);

            return await req.OkResponseAsync(response);
        }

        [Function("thermostat-post")]
        public async Task<HttpResponseData> Post([HttpTrigger(AuthorizationLevel.Function, "post", Route = "thermostat/{thermostatId}")] HttpRequestData req,
            string thermostatId)
        {
            var parameters = req.Url.ParseQueryString();
            var clientId = parameters["clientId"];

            var response = await _orchestrator.Add(clientId, thermostatId);

            return await req.OkResponseAsync(response);
        }


        [Function("thermostat-delete")]
        public async Task<HttpResponseData> Delete([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "thermostat/{thermostatId}")] HttpRequestData req,
            string thermostatId)
        {
            await _orchestrator.Remove(thermostatId);

            return await req.OkResponseAsync();
        }
    }
}
