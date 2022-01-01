using Newtonsoft.Json;

namespace EcobeeMonitor.Core.Models.Data
{
    public class ThermostatData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Area { get; set; }
        public string ThermostatId { get; set; }
        public string ClientId { get; set; }
        public MonitoringConfiguration Monitoring { get; set; }
        public class MonitoringConfiguration
        {
            public bool IsSensorDataMonitored { get; set; }
        }
    }


}
