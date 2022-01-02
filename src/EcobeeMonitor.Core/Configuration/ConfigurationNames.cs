namespace EcobeeMonitor.Core.Configuration
{
    public class ConfigurationNames
    {
        public static KeyVaultConfigurationNames KeyVault = new KeyVaultConfigurationNames();
        public class KeyVaultConfigurationNames
        {
            public string BaseUri = "KeyVault_BaseUri";
            public string ManagedIdentityClient = "KeyVault_ManagedIdentityClientId";
        }

        public static CosmosConfigurationNames Cosmos = new CosmosConfigurationNames();
        public class CosmosConfigurationNames
        {
            public string BaseUri = "Cosmos_BaseUri";
        }

        public static DataLakeConfigurationNames DataLake = new DataLakeConfigurationNames();
        public class DataLakeConfigurationNames
        {
            public string BaseUri = "DataLake_BaseUri";
            public string ManagedIdentityClient = "DataLake_ManagedIdentityClientId";
        }
    }

    
}
