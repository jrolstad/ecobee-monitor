using EcobeeMonitor.Core.Orchestrators;
using EcobeeMonitor.Worker.Models;
using Microsoft.Azure.Functions.Worker;
using System.Threading.Tasks;

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
        public Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            return _orchestrator.CaptureData();
        }
    }
}
