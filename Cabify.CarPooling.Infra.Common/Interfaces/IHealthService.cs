using System.Threading.Tasks;

namespace Cabify.CarPooling.Infra.Common.Interfaces
{
    public interface IHealthService
    {
        Task<bool> IsInfrastructureHealthy();
    }
}
