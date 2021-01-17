using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Dob).IsRequired();

        }
    }
}
