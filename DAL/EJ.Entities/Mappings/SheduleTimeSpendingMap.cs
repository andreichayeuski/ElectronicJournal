using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    class SheduleTimeSpendingMap : IEntityTypeConfiguration<SheduleTimeSpending>
    {
        public void Configure(EntityTypeBuilder<SheduleTimeSpending> builder)
        {
            builder.HasOne(d => d.SheduleSubject)
                .WithMany(p => p.SheduleTimeSpendings)
                .HasForeignKey(d => d.SheduleSubjectId);

            builder.HasOne(d => d.WeekDay)
                .WithMany(p => p.SheduleTimeSpendings)
                .HasForeignKey(d => d.WeekDayId);

            builder.HasOne(d => d.TimeSpending)
                .WithMany(p => p.SheduleTimeSpendings)
                .HasForeignKey(d => d.TimeSpendingId);

            builder.HasOne(d => d.Auditorium)
                .WithMany(p => p.SheduleTimeSpendings)
                .HasForeignKey(d => d.AuditoriumId);

            builder.HasOne(d => d.ClassType)
                .WithMany(p => p.SheduleTimeSpendings)
                .HasForeignKey(d => d.ClassTypeId);

            builder.HasMany(d => d.CalendarSheduleTimeSpendings)
                .WithOne(p => p.SheduleTimeSpending)
                .HasForeignKey(d => d.SheduleTimeSpendingId);
        }
    }
}
