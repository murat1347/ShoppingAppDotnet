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
    /// SellerConfiguration for configuration Seller and Seller Table.
    /// </summary>
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        /// <summary>
        /// Maximum length of the first name of the seller that can be used.
        /// </summary>
        public const int FirstNameMax = 32;

        /// <summary>
        /// Maximum length of the last name of the seller that can be used.
        /// </summary>
        public const int LastNameMax = 32;

        /// <summary>
        /// Maximum length of the email of the seller that can be used.
        /// </summary>
        public const int EmailMax = 62;

        /// <summary>
        /// Maximum length of the phone of the seller that can be used.
        /// </summary>
        public const int PhoneMax = 32;


        /// <summary>
        /// Maximum length of the adress of the seller that can be used.
        /// </summary>
        public const int AddressMax = 255;

        /// <summary>
        /// Configure Seller model.
        /// </summary>
        /// <param name="builder">EF API for model building.</param>
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasMany(s => s.Sales)
                .WithOne(s => s.Seller)
                .IsRequired();

            builder.Property(c => c.FirstName)
              .HasMaxLength(FirstNameMax);

            builder.Property(c => c.LastName)
             .HasMaxLength(LastNameMax);

            builder.Property(c => c.Email)
             .HasMaxLength(EmailMax);

            builder.Property(c => c.Address)
             .HasMaxLength(AddressMax);

            builder.Property(c => c.Phone)
             .HasMaxLength(PhoneMax);

            builder.HasIndex(s=>s.FirstName);

            builder.HasIndex(s=>s.LastName);

            builder.HasIndex(s=>s.Email);

            builder.HasIndex(s=>s.Phone);
        }
    }
}
