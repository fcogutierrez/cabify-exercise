using Cabify.CarPooling.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cabify.CarPooling.Infra.Persistence.EntityFramework
{
    internal sealed class CabifyDbContext
        : DbContext
    {
        public CabifyDbContext(DbContextOptions<CabifyDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Journey> Journeys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
