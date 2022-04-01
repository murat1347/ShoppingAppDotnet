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
    /// CategoryConfiguration for configuration Category Entity and Category Table.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Maximum length of the name of the category that can be used.
        /// </summary>
        public const int NameMaxLength = 32;

        /// <summary>
        /// Configure Category model.
        /// </summary>
        /// <param name="builder">EF API for model building.</param>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
               .HasMaxLength(NameMaxLength)
               .IsRequired();

            builder.HasOne(c => c.Parent)
              .WithMany(c => c.Children)
              .IsRequired(false)
              .HasForeignKey(c=>c.ParentId);

            builder.HasMany(c=>c.Products)
                .WithOne(p=>p.Category)
                .IsRequired();

            builder.HasIndex(c=>c.Name);
        }
    }
}
