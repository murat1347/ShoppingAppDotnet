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
    /// ProductConfiguration for configuration Product Entity and Product Table.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Maximum length of the name of the product that can be used.
        /// </summary>
        public const int NameMaxLength = 64;

        /// <summary>
        /// Maximum length of the imageUrl of the product that can be used.
        /// </summary>
        public const int ImageUrlMaxLength = 255;

        /// <summary>
        /// Configure Product model.
        /// </summary>
        /// <param name="builder">EF API for model building.</param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(NameMaxLength)
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(ImageUrlMaxLength)
                .IsRequired();

            builder.Property(p => p.Stock)
                 .HasColumnType(DecimalType.Type);

            builder.HasMany(p => p.Purchases)
                .WithOne(pu => pu.Product)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Sales)
                .WithOne(s => s.Product)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .IsRequired()
                .HasForeignKey(p=>p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(p=>p.Name);
        }
    }
}
