using Cabify.CarPooling.Infra.Common.Interfaces;
using Cabify.CarPooling.Infra.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cabify.CarPooling.Infra.Common.Configuration
{
    public static class RegistrationExtensions
    {
        public static void AddHealthChecking(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IHealthService, HealthService>();
        }
    }
}