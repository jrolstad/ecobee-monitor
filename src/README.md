# Ecobee Monitor

# Requirements
* Visual Studio 2022 or later
* .NET 6
* Azure Functions Runtime v4 or later
* Specflow for Visual Studio Extension (Used for editing AzureAdLicenseGovernor.Worker tests)

# Projects
## Applications
| Project              | Type            | Purpose                                     |
| -------------------- | --------------- | ------------------------------------------- |
| EcobeeMonitor.Api    | Azure Functions | API Layer for the application               |
| EcobeeMonitor.Worker | Azure Functions | Background worker tasks for the application |

## Libraries
| Project            | Type          | Purpose                                             |
| ------------------ | ------------- | --------------------------------------------------- |
| EcobeeMonitor.Core | .NET Standard | Shared library that contains the core functionality |

## Tests
| Project | Type | Purpose |
| ------- | ---- | ------- |

# Pipelines
GitHub Actions are used for continuous integration builds
## Builds
| Build                                                       | Purpose                                                                                                       |
| ----------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------- |
| [continuous-integration](../.github/workflows/ci-build.yml) | Continuous integration build.  Builds the solution, runs tests, and passes only when all tests are successful |

# Development Environment Setup
To run the solution in your development environment, use the following steps.

## Azure Function Settings File
Be sure you have a local.settings.json file in the function root directory.  Sample files are below:

### API
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "KeyVault_BaseUri": "https://ecobee-monitor-dev.vault.azure.net/",
    "Cosmos_BaseUri": "https://ecobee-monitor-dev.documents.azure.com:443/"
  }
}
```

### Worker
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