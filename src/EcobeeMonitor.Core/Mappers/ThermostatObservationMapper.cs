using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;
using EcobeeMonitor.Core.Services;
using System.Collections.Generic;

namespace EcobeeMonitor.Core.Mappers
{
    public class ThermostatObservationMapper
    {
        private readonly DeviceDataMapper _deviceDataMapper;
        private readonly DeviceObservationMapper _deviceObservationMapper;
        private readonly SystemObservationDataMapper _systemObservationDataMapper;

        public ThermostatObservationMapper(DeviceDataMapper deviceDataMapper,
            DeviceObservationMapper deviceObservationMapper,
            SystemObservationDataMapper systemObservationDataMapper)
        {
            _deviceDataMapper = deviceDataMapper;
            _deviceObservationMapper = deviceObservationMapper;
            _systemObservationDataMapper = systemObservationDataMapper;
        }

        public ThermostatObservation Map(string thermostatId, RuntimeReportResult toMap)
        {
            var devices = _deviceDataMapper.Map(toMap);

            return new ThermostatObservation
            {
                At = ClockService.Now,
                ThermostatId = thermostatId,
                Devices = devices,
                DeviceObservations = _deviceObservationMapper.Map(toMap, devices),
                SystemObsevations = _systemObservationDataMapper.Map(toMap)
            };
        }
    }
}
