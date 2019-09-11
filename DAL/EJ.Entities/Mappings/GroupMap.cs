using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class GroupMap : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(e => e.EndDate).HasColumnType("datetime");

            builder.Property(e => e.StartDate).HasColumnType("datetime");

            builder.Property(e => e.Number);

            builder.Property(e => e.HalfGroup).HasColumnType("bit");

            builder.HasOne(d => d.Course)
                .WithMany(p => p.Groups)
                .HasForeignKey(d => d.CourseId);
        }
    }
}
