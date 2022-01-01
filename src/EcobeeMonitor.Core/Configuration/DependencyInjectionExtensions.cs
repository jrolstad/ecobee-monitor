using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EcobeeMonitor.Core.Mappers;
using EcobeeMonitor.Core.Orchestrators;
using EcobeeMonitor.Core.Repositories;
using EcobeeMonitor.Core.Services;
using Microsoft.Azure.Cosmos;
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

            services.AddTransient<AuthorizationResponseMapper>();
            services.AddTransient<ClientDataMapper>();
            services.AddTransient<ThermostatMapper>();

            services.AddTransient<AuthorizationOrchestrator>();
            services.AddTransient<ClientOrchestrator>();
            services.AddTransient<RuntimeReportOrchestrator>();
            services.AddTransient<ThermostatOrchestrator>();

            services.AddTransient<ClientRepository>();
            services.AddTransient<ThermostatRepository>();

            services.AddTransient<EcobeeService>();
            services.AddTransient<SecretService>();

            ConfigureKeyVault(services);
            ConfigureCosmosDb(services);
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

        private static void ConfigureCosmosDb(IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var secretClient = provider.GetService<SecretClient>();
                var configuration = provider.GetService<IConfiguration>();
                var endpoint = configuration[ConfigurationNames.Cosmos.BaseUri];

                var secret = secretClient.GetSecret(SecretNames.CosmosPrimaryKey);
                var key = secret.Value.Value;

                var client = GetCosmosClient(endpoint, key);

                return client;
            });

            services.AddTransient(provider =>
            {
                var client = provider.GetService<CosmosClient>();
                var queryFactory = provider.GetService<ICosmosLinqQueryFactory>();
                return new CosmosDbService(client, CosmosConfiguration.DatabaseId, queryFactory);
            });

            services.AddTransient<ICosmosLinqQueryFactory, CosmosLinqQueryFactory>();
        }

        private static CosmosClient GetCosmosClient(string endpoint, string key)
        {
            var options = new CosmosClientOptions
            {
                AllowBulkExecution = true,
                MaxRetryAttemptsOnRateLimitedRequests = 19,
                MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromMinutes(1),
                SerializerOptions = new CosmosSerializationOptions
                {
                    IgnoreNullValues = true
                }
            };

            var client = new CosmosClient(endpoint, key, options);
            return client;
        }
    }
}
