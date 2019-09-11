using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class AuditoriumMap : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> builder)
        {
            builder.Property(e => e.Number);

            builder.HasMany(d => d.SheduleTimeSpendings)
                .WithOne(p => p.Auditorium)
                .HasForeignKey(d => d.AuditoriumId);
        }
    }
}
