using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EcobeeMonitor.Core.Models.Data
{
    public class ClientData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Area { get; set; }
        public string ClientId { get; set; }
        public List<string> Thermostats { get; set; }
    }
}
