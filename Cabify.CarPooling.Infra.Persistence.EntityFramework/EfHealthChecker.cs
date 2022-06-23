using Cabify.CarPooling.Infra.Common.Interfaces;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Infra.Persistence.EntityFramework
{
    internal sealed class EfHealthChecker
        : IHealthChecker
    {
        private readonly CabifyDbContext _dbContext;

        public EfHealthChecker(CabifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsHealthy()
        {
            var isHealthy = await _dbContext.Database.CanConnectAsync();

            return isHealthy;
        }
    }
}