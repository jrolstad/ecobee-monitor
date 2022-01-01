using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Data;

namespace EcobeeMonitor.Core.Mappers
{
    public class ThermostatMapper
    {
        public Thermostat Map(string clientId, string thermostatId)
        {
            return new Thermostat
            {
                ThermostatId = thermostatId,
                ClientId = clientId,
                Monitoring = new Thermostat.MonitoringConfiguration
                {
                    IsSensorDataMonitored = true
                }
            };
        }

        public ThermostatData Map(Thermostat toMap)
        {
            return new ThermostatData
            {
                ThermostatId = toMap?.ThermostatId,
                ClientId = toMap?.ClientId,
                Monitoring = new ThermostatData.MonitoringConfiguration
                {
                    IsSensorDataMonitored = toMap?.Monitoring?.IsSensorDataMonitored ?? false
                }
            };
        }

        public Thermostat Map(ThermostatData toMap)
        {
            return new Thermostat
            {
                ThermostatId = toMap?.ThermostatId,
                ClientId = toMap?.ClientId,
                Monitoring = new Thermostat.MonitoringConfiguration
                {
                    IsSensorDataMonitored = toMap?.Monitoring?.IsSensorDataMonitored ?? false
                }
            };
        }
    }
}
