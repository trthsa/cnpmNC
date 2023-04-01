using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationFlight.Data.Entities;

namespace ReservationFlight.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ReservationCode).IsRequired();
            builder.Property(x => x.TravelClass).IsRequired();
            builder.Property(x => x.IdentityNumber).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(0);
        }
    }
}
