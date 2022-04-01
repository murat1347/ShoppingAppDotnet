using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Helpers
{
    /// <summary>
    /// Used for initialize data for development
    /// </summary>
    public static class DevelopmentDataInitializer
    {

        private static Random rnd = new Random();

        const int ProductSize = 200;

        const int CustomerSize = 100;

        const int SellerSize = 100;

        /// <summary>
        /// Initialize development data 
        /// </summary>
        /// <param name="app">Application builder required for database context.</param>
        public static void InitializeDevelopmentData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = serviceScope
                .ServiceProvider.GetService<ProductManagerDBContext>())
               
            {
                UserManager<ApiUser> userService = (UserManager<ApiUser>)serviceScope
                    .ServiceProvider.GetService(typeof(UserManager<ApiUser>));

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                InitializeCategories(context);
                InitializeUsers(context,userService);
                InitializeProducts(context);
                InitializeCustomers(context);
                InitializeSellers(context);
                InitializePurchases(context);
                InitializeSales(context);
            }
        }

        /// <summary>
        /// Add initial product data.
        /// </summary>
        /// <param name="context">DBContext for db access.</param>
        private static void InitializeProducts(ProductManagerDBContext context)
        {
           var phones = context.Categories.Where(c=>c.Name == "Phones").First();

           for(int i=0;i<100;i++){
              var product = new Product();
                product.Name = "Samsung Galaxy A"+i;
                product.ImageUrl = "https://productimages.hepsiburada.net/s/66/550/110000008428332.jpg";
                product.Category = phones;
                context.Products.Add(product);
           }

            var computers = context.Categories.Where(c => c.Name == "Computers").First();

            for (int i = 0; i < 100; i++)
            {
                var product = new Product();
                product.Name = "Dell Latitude A" + i;
                product.ImageUrl = "https://www.notebookcheck-tr.com/typo3temp/_processed_/a/2/csm_7310_b639ebe727.png";
                product.Category = computers;
                context.Products.Add(product);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Add initial category data.
        /// </summary>
        /// <param name="context">DBContext for db access.
        static void InitializeCategories(ProductManagerDBContext dBContext){
           
                var electronic = new Category
                {
                    Name = "Electronic",
                };

                var phones = new Category
                {
                    Name = "Phones"
                };
                    
                var smartPhones = new Category{
                    Name = "Smart Phones",
                    Parent = phones
                };

                var computers = new Category
                {
                    Name = "Computers"
                };

                var computerParts = new Category
                {
                    Name = "Computer Parts",
                    Parent = computers
                };
                
                var laptop = new Category{
                    Name = "Laptop",
                    Parent = computers
                };

                dBContext.Categories.AddRange(new List<Category>{
                   electronic,phones,smartPhones,computers, computerParts, laptop
                });

                dBContext.SaveChanges();
        }

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="dBContext">Provided for feature use.</param>
        /// <param name="userManager">Required for creating a user.</param>
        static void InitializeUsers(ProductManagerDBContext dBContext, UserManager<ApiUser> userManager)
        {
            ApiUser user = new ApiUser();
            user.Email = "test@test.com";
            user.FirstName = "User FirstName";
            user.LastName = "User LastName";
            user.UserName = "test@test.com";
            userManager.CreateAsync(user,"Test$123456").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user,"Admin");
        }

        /// <summary>
        /// Add initial customer data.
        /// </summary>
        /// <param name="context">DBContext for db access.
        static void InitializeCustomers(ProductManagerDBContext context)
        {
            for(int i=0;i<100;i++){
                Customer customer = new Customer(){
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Phone = Faker.Phone.Number(),
                    Address = Faker.Address.SecondaryAddress(),
                };
                context.Add(customer);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Add initial customer data.
        /// </summary>
        /// <param name="context">DBContext for db access.
        static void InitializeSellers(ProductManagerDBContext context)
        {

            for (int i = 0; i < 100; i++)
            {
                Seller seller = new Seller()
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Phone = Faker.Phone.Number(),
                    Address =Faker.Address.SecondaryAddress(),
                };
                context.Add(seller);
            }

            context.SaveChanges();
        }

        static DateTime RandomDay()
        {
            DateTime start = new DateTime(2015, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        /// <summary>
        /// Add initial purchase data.
        /// </summary>
        /// <param name="context">DBContext for db access.
        static void InitializePurchases(ProductManagerDBContext context)
        {


            for (int i = 0; i < 100; i++)
            {
                Purchase purchase = new Purchase()
                {
                   Amount = rnd.Next(10_000),
                   Cost =rnd.Next(10_000),
                   DateTime = RandomDay(),
                   Product = context.Products.Find(1+Convert.ToInt64(rnd.Next(ProductSize-1))),
                   Seller = context.Sellers.Find(1+rnd.Next(SellerSize-1))
                };

                context.Add(purchase);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Add initial purchase data.
        /// </summary>
        /// <param name="context">DBContext for db access.
        static void InitializeSales(ProductManagerDBContext context)
        {

            for (int i = 0; i < 100; i++)
            {
                Sale sale = new Sale()
                {
                    Amount = rnd.Next(10_000),
                    Income = rnd.Next(10_000),
                    DateTime = RandomDay(),
                    Product = context.Products.Find(1 + Convert.ToInt64(rnd.Next(ProductSize - 1))),
                    Customer = context.Customers.Find(1 + rnd.Next(SellerSize - 1))
                };

                context.Add(sale);
            }

            context.SaveChanges();
        }
    }
}
