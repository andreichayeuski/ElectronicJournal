using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class UserStateMap : IEntityTypeConfiguration<UserState>
    {
        public void Configure(EntityTypeBuilder<UserState> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(t => t.Users)
                .WithOne(t => t.UserState)
                .HasForeignKey(t => t.UserStateId);
        }
    }
}
