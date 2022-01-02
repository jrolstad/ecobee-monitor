namespace EcobeeMonitor.Core.Configuration
{
    public static class DataLakeConfiguration
    {
        public static DataLakeContainers Containers = new DataLakeContainers();

        public class DataLakeContainers
        {
            public string ThermostatObservationData = "thermostat-observations";
        }
    }
}
