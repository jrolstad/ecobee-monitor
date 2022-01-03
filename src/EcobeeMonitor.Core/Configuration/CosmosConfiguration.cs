namespace EcobeeMonitor.Core.Configuration
{
    public static class CosmosConfiguration
    {
        public static string DefaultPartitionKey = "Default";
        public static string DatabaseId = "ecobeemonitor";
        public static CosmosContainers Containers = new CosmosContainers();
    }

    public class CosmosContainers
    {
        public string Clients = "Clients";
        public string Thermostats = "Thermostats";
    }
}
