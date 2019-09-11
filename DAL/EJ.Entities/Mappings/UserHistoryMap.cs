using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    class UserHistoryMap : IEntityTypeConfiguration<UserHistory>
    {
        public void Configure(EntityTypeBuilder<UserHistory> builder)
        {
            builder.Property(e => e.EditingDate).HasColumnType("datetime");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserHistories)
                .HasForeignKey(d => d.UserId);
        }
    }
}
