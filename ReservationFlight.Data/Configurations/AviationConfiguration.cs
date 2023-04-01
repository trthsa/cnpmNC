using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationFlight.Data.Entities;

namespace ReservationFlight.Data.Configurations
{
    internal class AviationConfiguration : IEntityTypeConfiguration<Aviation>
    {
        public void Configure(EntityTypeBuilder<Aviation> builder)
        {
            builder.ToTable("Aviations");
            builder.HasKey(x => x.AviationCode)
                .IsClustered();
            builder.Property(x => x.AviationCode)
                .HasMaxLength(3);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(x => x.ImageAviation).IsRequired(false);
            builder.Property(x => x.Status).HasDefaultValue((int)Enums.Status.Active);
        }
    }
}