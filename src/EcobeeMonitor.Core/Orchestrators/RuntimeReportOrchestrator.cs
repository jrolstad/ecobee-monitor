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
        private readonly AuthorizationOrchestrator _authorizationOrchestrator;
        private readonly SecretClient _secretClient;

        public RuntimeReportOrchestrator(EcobeeService ecobeeService,
            AuthorizationOrchestrator authorizationOrchestrator)
        {
            _ecobeeService = ecobeeService;
            _authorizationOrchestrator = authorizationOrchestrator;
        }

        public async Task CaptureData()
        {
            var token = await _authorizationOrchestrator.GetAccessToken(null);


        }

    }
}
