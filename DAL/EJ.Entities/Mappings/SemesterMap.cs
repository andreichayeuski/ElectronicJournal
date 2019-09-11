using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class SemesterMap : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            builder.Property(e => e.StartDate).HasColumnType("datetime");

            builder.Property(e => e.EndDate).HasColumnType("datetime");

            builder.HasMany(t => t.GroupShedules)
                   .WithOne(t => t.Semester)
                   .HasForeignKey(t => t.SemesterId);
        }
    }
}
