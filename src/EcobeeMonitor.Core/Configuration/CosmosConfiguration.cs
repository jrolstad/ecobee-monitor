using System;
using System.Collections.Generic;
using System.Text;

namespace EcobeeMonitor.Core.Configuration
{
    public static class CosmosConfiguration
    {
        public static string DefaultPartitionKey = "Default";
        public static string DatabaseId = "ecobee-monitor";
        public static CosmosContainers Containers = new CosmosContainers();
    }

    public class CosmosContainers
    {
        public string Clients = "Clients";
    }
}
