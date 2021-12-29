using EcobeeMonitor.Core.Orchestrators;
using EcobeeMonitor.Worker.Models;
using Microsoft.Azure.Functions.Worker;

namespace EcobeeMonitor.Worker
{
    public class RuntimeReportFunctions
    {
        private readonly RuntimeReportOrchestrator _orchestrator;

        public RuntimeReportFunctions(RuntimeReportOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [Function("runtimereport-retrieve")]
        public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            _orchestrator.CaptureData();
        }
    }
}
