using Azure.Security.KeyVault.Secrets;
using EcobeeMonitor.Core.Configuration;
using EcobeeMonitor.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class RuntimeReportOrchestrator
    {
        private readonly EcobeeService _ecobeeService;
        private readonly SecretClient _secretClient;

        public RuntimeReportOrchestrator(EcobeeService ecobeeService,
            SecretClient secretClient)
        {
            _ecobeeService = ecobeeService;
            _secretClient = secretClient;
        }

        public async Task CaptureData()
        {
            
        }

    }
}
