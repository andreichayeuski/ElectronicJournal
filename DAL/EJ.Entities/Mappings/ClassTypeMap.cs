using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class ClassTypeMap : IEntityTypeConfiguration<ClassType>
    {
        public void Configure(EntityTypeBuilder<ClassType> builder)
        {
            builder.HasMany(t => t.SheduleTimeSpendings)
                   .WithOne(t => t.ClassType)
                   .HasForeignKey(t => t.ClassTypeId);
        }
    }
}
