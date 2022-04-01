using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.ModelConfiguration
{
    /// <summary>
    /// SaleConfiguration for configuration Sale and Sale Table.
    /// </summary>
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        /// <summary>
        /// Configure Sale model
        /// Default roles added.
        /// </summary>
        /// <param name="builder">EF API for model building.</param>
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(s => s.Amount)
                 .HasColumnType(DecimalType.Type);

            builder.Property(s => s.Income)
                .HasColumnType(DecimalType.Type);

            builder.HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .IsRequired()
                .HasForeignKey(s=>s.ProductId);

            builder.HasOne(s => s.Customer)
                .WithMany(c => c.Purchases)
                .IsRequired()
                .HasForeignKey(s=>s.CustomerId);

            builder.HasIndex(s=>s.Amount);

            builder.HasIndex(s=>s.Income);

            builder.HasIndex(s=>s.DateTime);
        }
    }
}
