using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EJ.Entities.Mappings
{
    public class GroupSheduleMap : IEntityTypeConfiguration<GroupShedule>
    {
        public void Configure(EntityTypeBuilder<GroupShedule> builder)
        {
            builder.HasMany(t => t.SheduleSubjects)
                   .WithOne(t => t.GroupShedule)
                   .HasForeignKey(t => t.GroupSheduleId);

            builder.HasOne(d => d.Group)
                .WithMany(p => p.GroupShedules)
                .HasForeignKey(d => d.GroupId);

            builder.HasOne(d => d.Semester)
                .WithMany(p => p.GroupShedules)
                .HasForeignKey(d => d.SemesterId);
        }
    }
}
