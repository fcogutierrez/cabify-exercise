using System.Threading.Tasks;

namespace Cabify.CarPooling.Infra.Common.Interfaces
{
    public interface IHealthChecker
    {
        Task<bool> IsHealthy();
    }
}