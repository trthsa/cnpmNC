using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationFlight.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Data.Configurations
{
    public class FlightRouteConfiguration : IEntityTypeConfiguration<FlightRoute>
    {
        public void Configure(EntityTypeBuilder<FlightRoute> builder)
        {
            builder.ToTable("FlightRoutes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.DepartureId)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(x => x.ArrivalId)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(1);          
            builder.HasOne(x => x.Airport)
                .WithMany(x => x.FlightRoutes)
                .HasForeignKey(x => x.DepartureId)
                .HasForeignKey(x => x.ArrivalId);
        }
    }
}
