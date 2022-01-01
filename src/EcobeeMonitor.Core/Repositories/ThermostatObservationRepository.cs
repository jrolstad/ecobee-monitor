using EcobeeMonitor.Core.Models;
using System.Threading.Tasks;

namespace EcobeeMonitor.Core.Repositories
{
    public class ThermostatObservationRepository
    {
        public Task Save(ThermostatObservation toSave)
        {
            return Task.CompletedTask;
        }
    }
}
