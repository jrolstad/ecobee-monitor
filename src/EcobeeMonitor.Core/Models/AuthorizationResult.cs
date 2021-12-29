using Newtonsoft.Json;

namespace EcobeeMonitor.Core.Models
{
    public class AuthorizationResult
    {
        [JsonProperty("ecobeePin")]
        public string EcobeePin { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("interval")]
        public int Interval { get; set; }
    }
}
