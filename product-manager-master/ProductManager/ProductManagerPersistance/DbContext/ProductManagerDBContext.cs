using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManager.ModelConfiguration;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Helpers
{
    /// <summary>
    /// DbContext for the application.
    /// </summary>
    public class ProductManagerDBContext : IdentityDbContext<ApiUser>
    {
        /// <summary>
        /// DbContext creator.
        /// </summary>
        /// <param name="options">Options for creating db context.</param>
        public ProductManagerDBContext(DbContextOptions<ProductManagerDBContext> options)
                    : base(options)
        {
        }

        /// <summary>
        /// Represents DpSet of the table Products
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Represents DpSet of the table Category
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Represents DpSet of the table Sale
        /// </summary>
        public DbSet<Sale> Sales { get; set; }

        /// <summary>
        /// Represents DpSet of the table Purchase
        /// </summary>
        public DbSet<Purchase> Purchases { get; set; }

        /// <summary>
        /// Represents DpSet of the table Customer
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Represents DpSet of the table Seller
        /// </summary>
        public DbSet<Seller> Sellers { get; set; }

        /// <summary>
        /// Helps to configure models so additional configuration per entity is possible.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder from ef api.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<BaseEntity>();
            modelBuilder.Ignore<Person>();

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new SellerConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());  
        }
    }
}
