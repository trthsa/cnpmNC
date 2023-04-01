using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ReservationFlight.Data.Entities;
using ReservationFlight.Data.Enums;

namespace ReservationFlight.Data.Configurations
{
    internal class AirportConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airports");
            builder.HasKey(x => x.IATA).IsClustered();
            builder.Property(x => x.IATA)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Status)
                .HasDefaultValue((int)Status.Active);
        }
    }
}
