using EcobeeMonitor.Core.Orchestrators;
using EcobeeMonitor.Worker.Models;
using Microsoft.Azure.Functions.Worker;
using System.Threading.Tasks;

namespace EcobeeMonitor.Worker
{
    public class RuntimeReportFunctions
    {
        private readonly ThermostatMonitoringOrchestrator _orchestrator;

        public RuntimeReportFunctions(ThermostatMonitoringOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [Function("runtimereport-retrieve")]
        public Task Run([TimerTrigger("%RuntimeReport_Cron%")] TimerInfo myTimer)
        {
            return _orchestrator.CaptureData();
        }
    }
}
