using EcobeeMonitor.Core.Extensions;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;
using EcobeeMonitor.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EcobeeMonitor.Core.Mappers
{
    public class ThermostatObservationMapper
    {
        private readonly DeviceDataMapper _deviceDataMapper;
        private readonly DeviceObservationMapper _deviceObservationMapper;

        public ThermostatObservationMapper(DeviceDataMapper deviceDataMapper,
            DeviceObservationMapper deviceObservationMapper)
        {
            _deviceDataMapper = deviceDataMapper;
            _deviceObservationMapper = deviceObservationMapper;
        }

        public ThermostatObservation Map(string thermostatId, RuntimeReportResult toMap)
        {
            var devices = _deviceDataMapper.Map(toMap);

            return new ThermostatObservation
            {
                At = ClockService.Now,
                ThermostatId = thermostatId,
                Devices = devices,
                DeviceObservations = _deviceObservationMapper.Map(toMap, devices)
            };
        }
    }
}
