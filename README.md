# ecobee-monitor

# Development Environment Setup
To run the solution in your development environment, use the following steps.

## Azure Function Settings File
Be sure you have a local.settings.json file in the function root directory.  A sample file is below:
```
{
    "IsEncrypted": false,
    "Values": {
      "AzureWebJobsStorage": "UseDevelopmentStorage=true",
      "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
      "KeyVault_BaseUri": "https://ecobee-monitor-dev.vault.azure.net/",
      "Cosmos_BaseUri": "https://ecobee-monitor-dev.documents.azure.com:443/",
      "DataLake_BaseUri": "https://ecobeemonitorlakedev.dfs.core.windows.net",
      "RuntimeReport_Cron": "0 */1 * * * *"
    }
}
```