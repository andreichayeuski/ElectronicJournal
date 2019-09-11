using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class AbsenceMap : IEntityTypeConfiguration<Absence>
    {
        public void Configure(EntityTypeBuilder<Absence> builder)
        {
            builder.HasOne(d => d.User)
                   .WithMany(p => p.Absences)
                   .HasForeignKey(d => d.UserId);

            builder.HasOne(d => d.CalendarSheduleTimeSpending)
                .WithMany(p => p.Absences)
                .HasForeignKey(d => d.CalendarSheduleTimeSpendingId);

            builder.HasOne(p => p.AbsenceNotification)
                .WithOne(d => d.Absence)
                .HasForeignKey<AbsenceNotification>(p => p.AbsenceId);
        }
    }
}
