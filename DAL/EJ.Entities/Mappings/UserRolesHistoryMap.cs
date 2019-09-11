using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    class UserRolesHistoryMap : IEntityTypeConfiguration<UserRolesHistory>
    {
        public void Configure(EntityTypeBuilder<UserRolesHistory> builder)
        {
            builder.Property(e => e.EditingDate).HasColumnType("datetime");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserRolesHistories)
                .HasForeignKey(d => d.UserId);
        }
    }
}
