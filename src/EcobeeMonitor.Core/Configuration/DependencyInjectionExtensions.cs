using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EcobeeMonitor.Core.Orchestrators;
using EcobeeMonitor.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EcobeeMonitor.Core.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<RuntimeReportOrchestrator>();

            services.AddTransient<EcobeeService>();

            ConfigureKeyVault(services);
        }

        private static void ConfigureKeyVault(IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var endpoint = configuration[ConfigurationNames.KeyVault.BaseUri];
                var endpointUrl = new Uri(endpoint);
                var managedIdentityClientId = configuration[ConfigurationNames.KeyVault.ManagedIdentityClient];

                var credentials = GetCredential(managedIdentityClientId);

                return new SecretClient(vaultUri: endpointUrl, credential: credentials);
            });

        }

        private static TokenCredential GetCredential(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId)) return new DefaultAzureCredential();

            return new ManagedIdentityCredential(clientId);
        }
    }
}
