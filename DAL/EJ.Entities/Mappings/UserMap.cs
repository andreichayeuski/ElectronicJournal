using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.BirthDay).HasColumnType("datetime");

            builder.Property(e => e.Email).HasMaxLength(70);

            builder.Property(e => e.PersonalNumber).HasMaxLength(15);

            builder.Property(e => e.StartDate).HasColumnType("datetime");
            builder.Property(e => e.RemovalDate).HasColumnType("datetime");

            builder.Property(e => e.FName)
                .HasColumnName("FName")
                .HasMaxLength(50);

            builder.Property(e => e.MName)
                .HasColumnName("MName")
                .HasMaxLength(50);

            builder.Property(e => e.SName)
                .HasColumnName("SName")
                .HasMaxLength(50);

            builder.HasOne(d => d.Group)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.GroupId);

            builder.HasMany(d => d.UserHistories)
                .WithOne(p => p.User)
                .HasForeignKey(d => d.UserId);

            builder.HasMany(d => d.Absences)
                .WithOne(p => p.User)
                .HasForeignKey(d => d.UserId);
        }
    }
}
