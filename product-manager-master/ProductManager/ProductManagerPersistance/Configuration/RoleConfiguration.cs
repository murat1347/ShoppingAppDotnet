using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.ModelConfiguration
{
    /// <summary>
    /// PurchaseConfiguration for configuration Users'Role and IdentityRole Table.
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        /// <summary>
        /// Configure IdentityRole model
        /// Default roles added.
        /// </summary>
        /// <param name="builder">EF API for model building.</param>
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
        }
    }
}
