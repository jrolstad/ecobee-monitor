using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;
using EcobeeMonitor.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class RuntimeReportOrchestrator
    {
        private readonly EcobeeService _ecobeeService;
        private readonly AuthorizationOrchestrator _authorizationOrchestrator;
        private readonly ThermostatOrchestrator _thermostatOrchestrator;

        public RuntimeReportOrchestrator(EcobeeService ecobeeService,
            AuthorizationOrchestrator authorizationOrchestrator,
            ThermostatOrchestrator thermostatOrchestrator)
        {
            _ecobeeService = ecobeeService;
            _authorizationOrchestrator = authorizationOrchestrator;
            _thermostatOrchestrator = thermostatOrchestrator;
        }

        public async Task CaptureData()
        {
            var thermostats = await _thermostatOrchestrator.GetMonitored();
            var thermostatsByClient = thermostats
                .GroupBy(t => t.ClientId);

            var captureTasks = thermostatsByClient
                .Select(t=>CaptureClient(t.Key,t));

            await Task.WhenAll(captureTasks);
        }

        private async Task CaptureClient(string clientId, IEnumerable<Thermostat> thermostats)
        {
            var token = await _authorizationOrchestrator.GetAccessToken(clientId);
            var thermostatIds = thermostats.Select(t => t.ThermostatId);

            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now;
            
            var reportColumns = new List<RuntimeReportColumn>
            {
                RuntimeReportColumns.outdoorTemp,
                RuntimeReportColumns.sky,
                RuntimeReportColumns.zoneAveTemp,
                RuntimeReportColumns.zoneHeatTemp,
                RuntimeReportColumns.zoneCoolTemp,
                RuntimeReportColumns.zoneHvacMode,
                RuntimeReportColumns.auxHeat1,
                RuntimeReportColumns.auxHeat2,
                RuntimeReportColumns.auxHeat3,
                RuntimeReportColumns.compCool1,
                RuntimeReportColumns.compCool2,
                RuntimeReportColumns.fan
            };

            var result = await _ecobeeService.GetRuntimeReport(accessToken: token,
                startDate: start,
                endDate: end,
                columns: reportColumns,
                thermostats: thermostatIds,
                includeSensors: true);
        }

    }
}
