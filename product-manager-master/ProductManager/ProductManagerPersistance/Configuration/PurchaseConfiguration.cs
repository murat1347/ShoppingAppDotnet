using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductManager.ModelConfiguration
{
    /// <summary>
    /// PurchaseConfiguration for configuration Purchase Entity and Purchase Table.
    /// </summary>
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        /// <summary>
        /// Configure Purchase model.
        /// </summary>
        /// <param name="builder">EF API for model building.</param>
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.Property(p=>p.Amount)
                .HasColumnType(DecimalType.Type);

            builder.Property(p=>p.Cost)
                .HasColumnType(DecimalType.Type);

            builder.HasOne(p=>p.Product)
                .WithMany(pr=>pr.Purchases)
                .IsRequired()
                .HasForeignKey(p=>p.ProductId);

            builder.HasOne(p=>p.Seller)
                .WithMany(s=>s.Sales)
                .IsRequired()
                .HasForeignKey(p=>p.SellerId);


            builder.HasIndex(p=>p.Amount);

            builder.HasIndex(p=>p.Cost);

            builder.HasIndex(p=>p.DateTime);
        }
    }
}
