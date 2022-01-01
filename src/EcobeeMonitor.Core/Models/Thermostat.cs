namespace EcobeeMonitor.Core.Models
{
    public class Thermostat
    {
        public string ThermostatId { get; set; }
        public string ClientId { get; set; }
        public MonitoringConfiguration Monitoring { get; set; }
        public class MonitoringConfiguration
        {
            public bool IsSensorDataMonitored { get; set; }
        }
    }
}
