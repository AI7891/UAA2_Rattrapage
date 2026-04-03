using DomainEntityModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureDatabase.Configurations
{
    public class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            #region Table_Member
            builder.ToTable("Members");
            #endregion

            #region Keys
            builder.HasKey(m => m.Id);
            #endregion

            #region Properties

            builder.Property(m => m.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(m => m.Name)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Phone)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.PasswordHash)
                .HasMaxLength(500);

            builder.Property(m => m.Status)
                .HasColumnType("MemberStatus")
                .IsRequired();

            #endregion
        }
    }
}
