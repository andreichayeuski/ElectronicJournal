using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class TimeSpendingMap : IEntityTypeConfiguration<TimeSpending>
    {
        public void Configure(EntityTypeBuilder<TimeSpending> builder)
        {
            builder.Property(e => e.StartTime).HasColumnType("time");

            builder.Property(e => e.EndTime).HasColumnType("time");

            builder.HasMany(t => t.SheduleTimeSpendings)
                   .WithOne(t => t.TimeSpending)
                   .HasForeignKey(t => t.TimeSpendingId);
        }
    }
}
