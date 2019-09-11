using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class WeekDayMap : IEntityTypeConfiguration<WeekDay>
    {
        public void Configure(EntityTypeBuilder<WeekDay> builder)
        {
            builder.HasMany(t => t.SheduleTimeSpendings)
                   .WithOne(t => t.WeekDay)
                   .HasForeignKey(t => t.WeekDayId);
        }
    }
}
