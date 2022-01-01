using EcobeeMonitor.Core.Mappers;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Orchestrators
{
    public class ThermostatOrchestrator
    {
        private readonly ThermostatMapper _thermostatMapper;
        private readonly ThermostatRepository _thermostatRepository;

        public ThermostatOrchestrator(ThermostatMapper thermostatMapper,
           ThermostatRepository thermostatRepository)
        {
            _thermostatMapper = thermostatMapper;
            _thermostatRepository = thermostatRepository;
        }

        public Task<Thermostat> Add(string clientId, string thermostatId)
        {
            var data = _thermostatMapper.Map(clientId, thermostatId);

            return _thermostatRepository.Save(data);
        }

        public Task<Thermostat> Get(string thermostatId)
        {
            var data = _thermostatRepository.Get(thermostatId);

            return data;
        }

        public async Task<IEnumerable<Thermostat>> GetMonitored()
        {
            var data = await _thermostatRepository.Get();

            return data
                .Where(t => t.Monitoring?.IsSensorDataMonitored == true);        
        }

        public Task Remove(string thermostatId)
        {
            return _thermostatRepository.Delete(thermostatId);
        }
    }
}
