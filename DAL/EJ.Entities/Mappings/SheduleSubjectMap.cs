using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class SheduleSubjectMap : IEntityTypeConfiguration<SheduleSubject>
    {
        public void Configure(EntityTypeBuilder<SheduleSubject> builder)
        {
            builder.HasOne(d => d.GroupShedule)
                .WithMany(p => p.SheduleSubjects)
                .HasForeignKey(d => d.GroupSheduleId);

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.SheduleSubjects)
                .HasForeignKey(d => d.SubjectId);

            builder.HasMany(d => d.SheduleTimeSpendings)
                .WithOne(p => p.SheduleSubject)
                .HasForeignKey(d => d.SheduleSubjectId);
        }
    }
}
