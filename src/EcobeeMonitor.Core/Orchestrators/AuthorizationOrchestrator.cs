using EcobeeMonitor.Core.Configuration;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Services;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class AuthorizationOrchestrator
    {
        private readonly EcobeeService _ecobeeService;
        private readonly SecretService _secretService;

        public AuthorizationOrchestrator(EcobeeService ecobeeService,
            SecretService secretService)
        {
            _ecobeeService = ecobeeService;
            _secretService = secretService;
        }

        public async Task<AuthorizationResult> RequestAuthorization()
        {
            var clientId = await _secretService.Get(SecretNames.EcobeeClientId);
            var authorizationResult = await _ecobeeService.Authorize(clientId);

            return authorizationResult;
        }

        public async Task ApproveAuthorization(string code)
        {
            await _secretService.Save(SecretNames.EcobeeAuthorizationCode, code);

            var clientId = await _secretService.Get(SecretNames.EcobeeClientId);

            var token = await _ecobeeService.RequestAccessToken(clientId, code);

            await _secretService.Save(SecretNames.EcobeeRefreshToken, token.RefreshToken);
        }

        public async Task<string> GetAccessToken()
        {
            var clientId = await _secretService.Get(SecretNames.EcobeeClientId);
            var refreshToken = await _secretService.Get(SecretNames.EcobeeRefreshToken);

            var token = await _ecobeeService.RefreshAccessToken(clientId, refreshToken);

            await _secretService.Save(SecretNames.EcobeeRefreshToken, token.RefreshToken);

            return token.AccessToken;
        }


    }
}
