using EcobeeMonitor.Core.Configuration;
using EcobeeMonitor.Core.Mappers;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Data;
using EcobeeMonitor.Core.Repositories;
using EcobeeMonitor.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class AuthorizationOrchestrator
    {
        private readonly EcobeeService _ecobeeService;
        private readonly SecretService _secretService;
        private readonly ClientRepository _clientRepository;
        private readonly ClientDataMapper _clientDataMapper;

        public AuthorizationOrchestrator(EcobeeService ecobeeService,
            SecretService secretService,
            ClientRepository clientRepository,
            ClientDataMapper clientDataMapper)
        {
            _ecobeeService = ecobeeService;
            _secretService = secretService;
            _clientRepository = clientRepository;
            _clientDataMapper = clientDataMapper;
        }

        public async Task<AuthorizationResult> RequestAuthorization(string clientId)
        {
            var authorizationResult = await _ecobeeService.Authorize(clientId);

            await SaveAuthorizationStatus(clientId, AuthorizationStatus.Requested);

            return authorizationResult;
        }

        public async Task ApproveAuthorization(string clientId, string code)
        {
            await _secretService.Save(clientId, SecretNames.EcobeeAuthorizationCode, code);
            
            var token = await _ecobeeService.RequestAccessToken(clientId, code);
            await _secretService.Save(clientId, SecretNames.EcobeeRefreshToken, token.RefreshToken);

            await SaveAuthorizationStatus(clientId, AuthorizationStatus.Approved);
        }

        private async Task SaveAuthorizationStatus(string clientId, AuthorizationStatus status)
        {
            var data = await GetClient(clientId);

            data.AuthorizationStatus = status;
            await _clientRepository.Save(data);
        }

        private async Task<ClientData> GetClient(string clientId)
        {
            var data = await _clientRepository.Get(clientId);
            if (data == null) data = _clientDataMapper.Map(clientId);

            return data;
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
