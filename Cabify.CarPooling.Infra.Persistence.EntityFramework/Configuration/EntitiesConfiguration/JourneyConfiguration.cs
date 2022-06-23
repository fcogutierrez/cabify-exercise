using Cabify.CarPooling.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cabify.CarPooling.Infra.Persistence.EntityFramework.Configuration.EntitiesConfiguration
{
    public sealed class JourneyConfiguration
        : IEntityTypeConfiguration<Journey>
    {
        public void Configure(EntityTypeBuilder<Journey> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Car)
                .WithMany(x => x.Journeys);
        }
    }
}
