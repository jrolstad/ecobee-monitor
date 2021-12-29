using Azure.Security.KeyVault.Secrets;
using EcobeeMonitor.Core.Configuration;
using EcobeeMonitor.Core.Models.Ecobee;
using EcobeeMonitor.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class AuthorizationOrchestrator
    {
        private readonly EcobeeService _ecobeeService;
        private readonly SecretClient _secretClient;

        public AuthorizationOrchestrator(EcobeeService ecobeeService,
            SecretClient secretClient)
        {
            _ecobeeService = ecobeeService;
            _secretClient = secretClient;
        }

        public async Task<AuthorizationResult> RequestAuthorization()
        {
            var clientId = await _secretClient.GetSecretAsync(SecretNames.EcobeeClientId);
            var authorizationResult = await _ecobeeService.Authorize(clientId.Value.Value);

            return authorizationResult;
        }

        public async Task ApproveAuthorization(string code)
        {
            await _secretClient.SetSecretAsync(SecretNames.EcobeeAuthorizationCode, code);

            var clientId = await _secretClient.GetSecretAsync(SecretNames.EcobeeClientId);

            var token = await _ecobeeService.RequestAccessToken(clientId.Value.Value, code);

            await _secretClient.SetSecretAsync(SecretNames.EcobeeRefreshToken, token.RefreshToken);
        }

        public async Task<string> GetAccessToken()
        {
            var clientId = await _secretClient.GetSecretAsync(SecretNames.EcobeeClientId);
            var refreshToken = await _secretClient.GetSecretAsync(SecretNames.EcobeeRefreshToken);

            var token = await _ecobeeService.RefreshAccessToken(clientId.Value.Value, refreshToken.Value.Value);

            await _secretClient.SetSecretAsync(SecretNames.EcobeeRefreshToken, token.RefreshToken);

            return token.AccessToken;
        }


    }
}
