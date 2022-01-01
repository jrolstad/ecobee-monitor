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
    }

    
}
