using EcobeeMonitor.Core.Mappers;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;
using EcobeeMonitor.Core.Repositories;
using EcobeeMonitor.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class ThermostatMonitoringOrchestrator
    {
        private readonly EcobeeService _ecobeeService;
        private readonly AuthorizationOrchestrator _authorizationOrchestrator;
        private readonly ThermostatOrchestrator _thermostatOrchestrator;
        private readonly ThermostatObservationMapper _thermostatRuntimeReportMapper;
        private readonly ThermostatObservationRepository _thermostatObservationRepository;

        public ThermostatMonitoringOrchestrator(EcobeeService ecobeeService,
            AuthorizationOrchestrator authorizationOrchestrator,
            ThermostatOrchestrator thermostatOrchestrator,
            ThermostatObservationMapper thermostatRuntimeReportMapper,
            ThermostatObservationRepository thermostatObservationRepository)
        {
            _ecobeeService = ecobeeService;
            _authorizationOrchestrator = authorizationOrchestrator;
            _thermostatOrchestrator = thermostatOrchestrator;
            _thermostatRuntimeReportMapper = thermostatRuntimeReportMapper;
            _thermostatObservationRepository = thermostatObservationRepository;
        }

        public async Task CaptureData()
        {
            var thermostats = await _thermostatOrchestrator.GetMonitored();
            var thermostatsByClient = thermostats
                .GroupBy(t => t.ClientId);

            var captureTasks = thermostatsByClient
                .Select(t=>CaptureRuntimeData(clientId: t.Key,thermostats: t));

            await Task.WhenAll(captureTasks);
        }

        private async Task CaptureRuntimeData(string clientId, IEnumerable<Thermostat> thermostats)
        {
            var token = await _authorizationOrchestrator.GetAccessToken(clientId);

            var thermostatTasks = thermostats
                .Select(t => CaptureThermostatData(token, t));

            await Task.WhenAll(thermostatTasks);
        }

        private async Task CaptureThermostatData(string accessToken, Thermostat thermostat)
        {

            var start = ClockService.Now.AddDays(-1);
            var end = ClockService.Now;
            var thermostatId = thermostat.ThermostatId;
            var reportColumns = GetRuntimeDataFields();

            var data = await _ecobeeService.GetRuntimeReport(accessToken: accessToken,
                startDate: start,
                endDate: end,
                columns: reportColumns,
                thermostats: new[] {thermostatId},
                includeSensors: true);

            var result = _thermostatRuntimeReportMapper.Map(thermostatId, data);
            await _thermostatObservationRepository.Save(result);
        }

        private static IEnumerable<RuntimeReportColumn> GetRuntimeDataFields()
        {
            return new List<RuntimeReportColumn>
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
        }
    }
}
