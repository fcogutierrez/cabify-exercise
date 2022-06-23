using Cabify.CarPooling.Infra.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Infra.Common.Services
{
    internal sealed class HealthService
        : IHealthService
    {
        private readonly IEnumerable<IHealthChecker> _healthCheckers;

        public HealthService(IEnumerable<IHealthChecker> healthCheckers)
        {
            _healthCheckers = healthCheckers;
        }

        public async Task<bool> IsInfrastructureHealthy()
        {
            foreach (var healthChecker in _healthCheckers)
            {
                var isHealthy = await healthChecker.IsHealthy();
                if (!isHealthy)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
