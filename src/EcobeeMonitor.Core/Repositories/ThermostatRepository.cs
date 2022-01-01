using EcobeeMonitor.Core.Configuration;
using EcobeeMonitor.Core.Mappers;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Data;
using EcobeeMonitor.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Repositories
{
    public class ThermostatRepository
    {
        private readonly CosmosDbService _cosmosDbService;
        private readonly ThermostatMapper _mapper;

        public ThermostatRepository(CosmosDbService cosmosDbService,
            ThermostatMapper mapper)
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }
        
        public async Task<Thermostat> Get(string thermostatId)
        {
            var data = await _cosmosDbService.Get<ThermostatData>(thermostatId, CosmosConfiguration.Containers.Thermostats, CosmosConfiguration.DefaultPartitionKey);

            var result = _mapper.Map(data);

            return result;
        }

        public async Task<ICollection<Thermostat>> Get()
        {
            var query = _cosmosDbService
                .Query<ThermostatData>(CosmosConfiguration.Containers.Thermostats);

            var result = await _cosmosDbService.ExecuteRead(query, _mapper.Map);

            return result;
        }

        public async Task<Thermostat> Save(Thermostat toSave)
        {
            var data = _mapper.Map(toSave);
            data.Id = toSave.ThermostatId;
            data.Area = CosmosConfiguration.DefaultPartitionKey;

            var result = await _cosmosDbService.Save(data, CosmosConfiguration.Containers.Thermostats);

            return _mapper.Map(result);
        }

        public Task Delete(string thermostatId)
        {
            return _cosmosDbService.Delete<ThermostatData>(thermostatId, CosmosConfiguration.Containers.Thermostats, CosmosConfiguration.DefaultPartitionKey);
        }
    }
}
