namespace EcobeeMonitor.Core.Models
{
    public class AuthorizationResponse
    {
        public string ClientId { get; set; }
        public string Pin { get; set; }
        public string Scope { get; set; }
    }
}
