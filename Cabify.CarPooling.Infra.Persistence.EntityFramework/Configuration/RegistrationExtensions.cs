using Cabify.CarPooling.Infra.Common.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cabify.CarPooling.Infra.Persistence.EntityFramework.Configuration
{
    public static class RegistrationExtensions
    {
        public static void AddEntityFrameworkPersistence(this IServiceCollection serviceCollection)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            serviceCollection.AddDbContext<CabifyDbContext>(options => options.UseSqlite(connection));

            serviceCollection.AddScoped<IUnitOfWork, EfUnitOfWork>();
            serviceCollection.AddScoped<IHealthChecker, EfHealthChecker>();
        }
    }
}