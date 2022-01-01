using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EcobeeMonitor.Core.Models.Data
{
    public class ClientData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Area { get; set; }
        public string ClientId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AuthorizationStatus AuthorizationStatus { get; set; }
    }

    public enum AuthorizationStatus
    {
        New,
        Requested,
        Approved
    }
}
