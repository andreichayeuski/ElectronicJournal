using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class CalendarMap : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder) 
        {
            builder.ToTable("Calendar");

            builder.HasMany(d => d.CalendarSheduleTimeSpendings)
                .WithOne(p => p.Calendar)
                .HasForeignKey(d => d.CalendarId);
        }
    }
}
