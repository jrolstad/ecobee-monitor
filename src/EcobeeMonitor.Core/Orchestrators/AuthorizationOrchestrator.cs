using EcobeeMonitor.Core.Configuration;
using EcobeeMonitor.Core.Mappers;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Data;
using EcobeeMonitor.Core.Repositories;
using EcobeeMonitor.Core.Services;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class AuthorizationOrchestrator
    {
        private readonly EcobeeService _ecobeeService;
        private readonly SecretService _secretService;
        private readonly ClientOrchestrator _clientOrchestrator;

        public AuthorizationOrchestrator(EcobeeService ecobeeService,
            SecretService secretService,
            ClientOrchestrator clientOrchestrator)
        {
            _ecobeeService = ecobeeService;
            _secretService = secretService;
            _clientOrchestrator = clientOrchestrator;
        }

        public async Task<AuthorizationResult> RequestAuthorization(string clientId)
        {
            var authorizationResult = await _ecobeeService.Authorize(clientId);

            await _clientOrchestrator.SaveAuthorizationStatus(clientId, AuthorizationStatus.Requested);

            return authorizationResult;
        }

        public async Task ApproveAuthorization(string clientId, string code)
        {
            await _secretService.Save(clientId, SecretNames.EcobeeAuthorizationCode, code);
            
            var token = await _ecobeeService.RequestAccessToken(clientId, code);
            await _secretService.Save(clientId, SecretNames.EcobeeRefreshToken, token.RefreshToken);

            await _clientOrchestrator.SaveAuthorizationStatus(clientId, AuthorizationStatus.Approved);
        }

        public async Task<string> GetAccessToken(string clientId)
        {
            var refreshToken = await _secretService.Get(clientId, SecretNames.EcobeeRefreshToken);

            var token = await _ecobeeService.RefreshAccessToken(clientId, refreshToken);

            await _secretService.Save(clientId, SecretNames.EcobeeRefreshToken, token.RefreshToken);

            return token.AccessToken;
        }


    }
}
