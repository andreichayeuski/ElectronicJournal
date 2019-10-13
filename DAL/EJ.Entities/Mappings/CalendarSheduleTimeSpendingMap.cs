using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class CalendarSheduleTimeSpendingMap : IEntityTypeConfiguration<CalendarSheduleTimeSpending>
    {
        public void Configure(EntityTypeBuilder<CalendarSheduleTimeSpending> builder) 
        {
            builder.ToTable("CalendarSheduleTimeSpending");

            builder.HasOne(d => d.Calendar)
                .WithMany(p => p.CalendarSheduleTimeSpendings)
                .HasForeignKey(d => d.CalendarId);

            builder.HasOne(d => d.SheduleTimeSpending)
                .WithMany(p => p.CalendarSheduleTimeSpendings)
                .HasForeignKey(d => d.SheduleTimeSpendingId);

            builder.HasMany(d => d.Absences)
                .WithOne(p => p.CalendarSheduleTimeSpending)
                .HasForeignKey(d => d.CalendarSheduleTimeSpendingId);
        }
    }
}
