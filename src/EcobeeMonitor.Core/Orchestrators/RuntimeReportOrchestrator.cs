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
            var clientId = await _secretClient.GetSecretAsync(SecretNames.EcobeeClientId);
            var refreshToken = await _secretClient.GetSecretAsync(SecretNames.EcobeeRefreshToken);

            if(string.IsNullOrEmpty(refreshToken?.Value?.Value))
            {
                var authorizationResult = await _ecobeeService.Authorize(clientId.Value.Value);
                _secretClient.SetSecret(SecretNames.EcobeeAuthorizationCode, authorizationResult.Code);
            }
           
        }
    }
}
