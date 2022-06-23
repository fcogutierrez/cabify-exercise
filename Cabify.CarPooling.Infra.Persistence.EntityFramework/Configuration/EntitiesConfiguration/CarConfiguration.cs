using Cabify.CarPooling.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cabify.CarPooling.Infra.Persistence.EntityFramework.Configuration.EntitiesConfiguration
{
    public sealed class CarConfiguration
        : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
