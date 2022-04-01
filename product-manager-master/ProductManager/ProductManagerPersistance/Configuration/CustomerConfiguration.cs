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
    /// CustomerConfiguration for configuration Customer Entity and Customer Table.
    /// </summary>
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Maximum length of the first name of the customer that can be used.
        /// </summary>
        public const int FirstNameMax = 32;

        /// <summary>
        /// Maximum length of the last name of the customer that can be used.
        /// </summary>
        public const int LastNameMax = 32;

        /// <summary>
        /// Maximum length of the email of the customer that can be used.
        /// </summary>
        public const int EmailMax = 62;

        /// <summary>
        /// Maximum length of the phone of the customer that can be used.
        /// </summary>
        public const int PhoneMax = 32;

        /// <summary>
        /// Maximum length of the adress of the customer that can be used.
        /// </summary>
        public const int AddressMax = 255;

        /// <summary>
        /// Configure Customer model.
        /// </summary>
        /// <param name="builder">EF API for model building.</param>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasMany(c=>c.Purchases)
                .WithOne(p=>p.Customer)
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

            builder.HasIndex(p=>p.FirstName);

            builder.HasIndex(p => p.LastName);

            builder.HasIndex(p => p.Email);

            builder.HasIndex(p => p.Phone);
        }
    }
}
