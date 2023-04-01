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
    public class FlightScheduleConfiguration : IEntityTypeConfiguration<FlightSchedule>
    {
        public void Configure(EntityTypeBuilder<FlightSchedule> builder)
        {
            builder.ToTable("FlightSchedules");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.FlightRouteId).IsRequired();
            builder.Property(x => x.AviationId)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(x => x.FlightNumber)
                .IsRequired()
                .HasMaxLength(7);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.ScheduledTimeDeparture).IsRequired();
            builder.Property(x => x.ScheduledTimeArrival).IsRequired();
            builder.Property(x => x.SeatEconomy).IsRequired();
            builder.Property(x => x.SeatBusiness).IsRequired();
            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(1);
            builder.HasOne(x => x.FlightRoute)
                .WithMany(x => x.FlightSchedules)
                .HasForeignKey(x => x.FlightRouteId);
            builder.HasOne(x => x.Aviation)
               .WithMany(x => x.FlightSchedules)
               .HasForeignKey(x => x.AviationId);
        }
    }
}
