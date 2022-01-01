using EcobeeMonitor.Core.Models.Ecobee;
using EcobeeMonitor.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class RuntimeReportOrchestrator
    {
        private readonly EcobeeService _ecobeeService;
        private readonly AuthorizationOrchestrator _authorizationOrchestrator;

        public RuntimeReportOrchestrator(EcobeeService ecobeeService,
            AuthorizationOrchestrator authorizationOrchestrator)
        {
            _ecobeeService = ecobeeService;
            _authorizationOrchestrator = authorizationOrchestrator;
        }

        public async Task CaptureData()
        {
            var token = await _authorizationOrchestrator.GetAccessToken(null);

            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now;
            var thermostatId = "511897079559";
            var reportColumns = new List<string>
            {
                RuntimeReportColumns.outdoorTemp.Name,
                RuntimeReportColumns.zoneAveTemp.Name
            };

            var result = await _ecobeeService.GetRuntimeReport(accessToken: token,
                startDate: start,
                endDate: end,
                columns: reportColumns,
                thermostatId: thermostatId,
                includeSensors: true);
        }

    }
}
