# Ecobee Monitor
The Ecobee Monitor enables monitoring and analysis of data produced by Ecobee thermostats.  Using the [Ecobee API](https://www.ecobee.com/en-us/developers/), data from registered thermostats and their associated sensors are captured in regular intervals and made available for analysis.

# Monitoring
The Ecobee Monitor captures runtime, sensor, and current weather data at regular intervals, then saves this data to a long term storage mechanism (Azure Data Lake is the current option) to be analyzed later.

## Thermostat and Sensor Observations
At intervals defined in the worker function, the following data points are retrieved in 5 minute intervals, converted to a readable JSON document, and then saved to an Azure Data Lake storage account.  Note all temperatures are in Fahrenheit by default and dates/times are in UTC.

### Thermostats
* Thermostat Metadata (id)
* Outdoor Data (Temperature, Humidity, Sky Cover)
* Zone Data (Average Temperature, Heat Target Temperature, Cool Target Temperature, HVAC mode)
* Heating Runtime Data (Stage 1-3 runtimes in minutes for the period)
* Cooling Runtime Data (Stage 1-2 runtimes in minutes for the period)
* HVAC Data (Fan runtime in minutes for the period, System HVAC Mode)
### Sensors
* Sensor Metadata (id, name, type)
* Humidity (%)
* Temperature

# Solution Architecture
This solution is broken up into two main components, an API and a background worker.  The API enables [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) operations on the configuration, and the background worker is the component that applies and enforces the configuration.  As this is a native Azure application, all components use Azure PaaS services such as Azure Functions, Key Vault, CosmosDb, and Managed Identities.

### Component Diagram
For more detail, a [Threat model](https://docs.microsoft.com/en-us/azure/security/develop/threat-modeling-tool) for the solution can be found at [docs/threat-model.tm7](docs/threat-model.tm7) that shows all the components and their interactions.  For a more detailed view of the individual components, underlying code, and their interactions see [src/README.md](src/README.md)

## Infrastructure
The infrastructure that is needed to run this service can be found at https://github.com/jrolstad/ecobee-monitor-infra where it is defined using [Terraform](https://www.terraform.io/)

# How to use
## Setup Solution
If this solution is something that you want to implement for Ecobee thermostats that you manage, below is a high level of steps needed.  If there are further questions, contact the Administrators of this repository who will be able to give more guidance.
1. Create the infrastucture in Azure using https://github.com/jrolstad/ecobee-monitor-infra or your own setup.
2. Deploy the Ecobee Monitor to those components using your continuous delivery tool of choice (GitHub Actions, Azure DevOps, Jenkins, etc).  As a part of this deployment be sure to set the interval for the background worker to the expected value so monitoring functionality captures data on the interval desired.

## Register Ecobee Client Application
Once the solution is setup and running, one or more ecobee clients (apps) need to be configured for the Ecobee Monitor to authenticate as and act on behalf of.
1. Create an Ecobee application in the [Ecobee Portal](https://www.ecobee.com/consumerportal/index.html#/login) if you do not already have one (Menu > Developer > Create New).  Note the 'API Key' value
2. Perform a GET http request to the Ecobee Monitor api endpoint at api/authorization/{api-key}/request where api-key is the value from step 1 (example: api/authorization/asdfadsf/request). The response will contain a PIN value, be sure and note this.
3. In the Ecobee Portal, enable the application from step 1 to run on your behalf.  Open the MyApps section (Menu > My Apps) and select 'Create Application'.  When requested, enter the PIN value from the previous step. 
4. Once the application has been successfully authorized, perform a GET http request to the Ecobee Monitor API at api/authorization/{api-key}/approve where api-key is the value from step 1 (example: api/authorization/asdfadsf/approve).  Once complete, the application is now fully onboarded and able to perform requests on your behalf.

## Register Thermostats
Once the Ecobee Client Application(s) are registered, the thermostats to be monitored need to be registered as well so the worker know which ones to monitor.
1. In the [Ecobee Portal](https://www.ecobee.com/consumerportal/index.html#/login), select the About tile, then select 'Thermostat Information' if it's not already present.  Note the thermostat serial number (this is the 'Thermostat Id')
2. Perform a POST http request to the Ecobee Monitor api endpoint at api/thermostat/{thermostat-id}?clientId={api-key} where thermostat-id is the serial number noted in the previous step and clientId is the application configued above.  An example is /api/thermostat/123435?clientId=asdfadsf

## Review Data
Once at least one thermostat has been registered, the monitoring functionality of the worker will capture data for it and all associated sensors.  After the monitoring function has ran, review the data in the solution storage account.