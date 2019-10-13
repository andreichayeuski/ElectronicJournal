using System;
using System.Collections.Generic;
using System.Text;
using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EJ.Entities.Mappings
{
    public class AbsenceNotificationMap : IEntityTypeConfiguration<AbsenceNotification>
    {
        public void Configure(EntityTypeBuilder<AbsenceNotification> builder)
        {
            builder.ToTable("AbsenceNotification");

            builder.HasOne(p => p.Absence)
                .WithOne(d => d.AbsenceNotification)
                .HasForeignKey<Absence>(p => p.AbsenceNotificationId);
        }
    }
}
