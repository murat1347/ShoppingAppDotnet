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
using System.Transactions;

namespace ProductManager.Helpers
{
    /// <summary>
    /// Used for initialize data for development
    /// </summary>
    public static class ProductionDataInitializer
    {

        private static Random rnd = new Random();

        const int ProductSize = 200;

        const int CustomerSize = 100;

        const int SellerSize = 100;

        /// <summary>
        /// Initialize development data 
        /// </summary>
        /// <param name="app">Application builder required for database context.</param>
        public static void InitializeProductionData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = serviceScope
                .ServiceProvider.GetService<ProductManagerDBContext>())
               
            {
                UserManager<ApiUser> userService = (UserManager<ApiUser>)serviceScope
                    .ServiceProvider.GetService(typeof(UserManager<ApiUser>));
                //InitializePurchases(context);
                //InitializeUsers(context, userService);
                //InitializeCategories(context);
                //InitializeCustomers(context);
                //InitializeSellers(context);
                //InitializeProducts(context);
                //InitializeSales(context);
                /*
                InitializeProducts(context);

                */
            }
        }



        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Add initial product data.
        /// </summary>
        /// <param name="context">DBContext for db access.</param>
        private static void InitializeProducts(ProductManagerDBContext context)
        {
            List<string> images = new List<string>{
                "https://productimages.hepsiburada.net/s/79/400-592/110000021519486.jpg",
                "https://productimages.hepsiburada.net/s/40/280-413/10669116063794.jpg",
                "https://productimages.hepsiburada.net/s/73/400-592/110000014642167.jpg",
                "https://productimages.hepsiburada.net/s/75/280-413/110000017452947.jpg",
                "https://productimages.hepsiburada.net/s/78/280-413/110000020465903.jpg",
                "https://productimages.hepsiburada.net/s/88/400-592/110000030397950.jpg",
                "https://productimages.hepsiburada.net/s/73/280-413/110000014912741.jpg",
                "https://productimages.hepsiburada.net/s/79/400-592/110000021519472.jpg",
                "https://productimages.hepsiburada.net/s/47/400-592/10852066328626.jpg",
                "https://productimages.hepsiburada.net/s/75/400-592/110000017452919.jpg",
                "https://productimages.hepsiburada.net/s/81/400-592/110000023450746.jpg",
                "https://productimages.hepsiburada.net/s/74/400-592/110000016233813.jpg",
                "https://productimages.hepsiburada.net/s/45/400-592/10825450782770.jpg",
                "https://productimages.hepsiburada.net/s/73/400-592/110000014895014.jpg",
                "https://productimages.hepsiburada.net/s/44/400-592/10770321342514.jpg"
            };

            List<string> names = new List<string>{
                "Samsung", "Xiaomi", "Huawei", "Oppo", "General Mobile", "LG", "Oppo Realme", "HONOR", "Apple"
            };

            var phones = context.Categories.Where(c=>c.Name == "Smart Phones").First();

           string tag = " Phone ";
            int j =0;
           for(int i=0;i<2_500_000;i++){
              var product = new Product();
                product.Name = names[rnd.Next(names.Count())]+ tag + RandomString(5) + i;
                product.ImageUrl =images[rnd.Next(images.Count())];
                product.Category = phones;
                context.Products.Add(product);

                if (i / 10_000 == j)
                {
                    context.SaveChanges();
                    j++;
                    Console.WriteLine(j + " * 10_000");
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Add initial category data.
        /// </summary>
        /// <param name="context">DBContext for db access.
        static void InitializeCategories(ProductManagerDBContext dBContext){
           
            if(dBContext.Categories.Count() == 0)
            {

                var electronic = new Category
                {
                    Name = "Electronic",
                };

                    var sound = new Category
                    {
                        Name = "Sound Systems",
                        Parent = electronic
                    };

                        var amplifiers = new Category
                        {
                            Name = "Amplifiers",
                            Parent = sound
                        };

                        var headPhones = new Category
                        {
                            Name = "Head Phones",
                            Parent = sound
                        };

                        var speakers = new Category
                        {
                            Name = "Speakers",
                            Parent = sound
                        };

                    var antenna = new Category
                        {
                            Name = "Antenna",
                            Parent = electronic
                    };

                    var cable = new Category
                    {
                        Name = "Cable",
                        Parent = electronic
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

                        var powerSupply = new Category
                        {
                            Name = "Power Supply",
                            Parent = computerParts
                        };

                        var motherboard = new Category
                        {
                            Name = "Motherboard",
                            Parent = computerParts
                        };

                        var cpu = new Category
                        {
                            Name = "CPU",
                            Parent = computerParts
                        };

                        var gpu = new Category
                        {
                            Name = "GPU",
                            Parent = computerParts
                        };

                    var computerEquipment = new Category
                    {
                        Name = "Computer Equipment",
                        Parent = computers
                    };
                        var keyboard = new Category
                        {
                            Name = "Keyboard",
                            Parent = computerEquipment
                        };

                        var mouse = new Category
                        {
                            Name = "Mouse",
                            Parent = computerEquipment
                        };

                            var wirelessMouse = new Category
                            {
                                Name = "Wireless Mouse",
                                Parent = mouse
                            };

                            var cableMouse = new Category
                            {
                                Name = "Cable Mouse",
                                Parent = mouse
                            };

                        var screen = new Category
                        {
                            Name = "Screen",
                            Parent = computerEquipment
                        };

                            var lcdScreen = new Category
                            {
                                Name = "LCD Screen",
                                Parent = screen
                            };

                            var ledScreen = new Category
                            {
                                Name = "LED Screen",
                                Parent = screen
                            };

                    var laptop = new Category{
                        Name = "Laptop",
                        Parent = computers
                    };

                    var desktop = new Category
                    {
                        Name = "Desktop",
                        Parent = computers
                    };


                var tablet = new Category
                {
                    Name = "Tablet"
                };

                    var drawingTablet = new Category
                    {
                        Name = "Drawing Tablet",
                        Parent = tablet
                    };

                    var androidTablet = new Category
                    {
                        Name = "Android Tablet",
                        Parent = tablet
                    };

                var apple = new Category
                {
                    Name = "Apple"
                };

                    var macintosh = new Category
                    {
                        Name = "Macintosh",
                        Parent = apple
                    };

                    var macbook = new Category
                    {
                        Name = "Macbook",
                        Parent = apple
                    };

                    var ipad = new Category
                    {
                        Name = "IPad",
                        Parent = apple
                    };

                    var iphone = new Category
                    {
                        Name = "IPhone",
                        Parent = apple
                    };

                dBContext.Categories.AddRange(new List<Category>{
                    electronic,phones,smartPhones,computers, computerParts, laptop, desktop, powerSupply,sound,amplifiers,headPhones
                    ,apple,macbook,macintosh,ipad,iphone,tablet,drawingTablet,androidTablet,antenna,cpu,gpu,keyboard,mouse,computerEquipment,screen,lcdScreen,ledScreen
                    ,wirelessMouse,cableMouse,phones,speakers
                });

                dBContext.SaveChanges();
            }
        }

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="dBContext">Provided for feature use.</param>
        /// <param name="userManager">Required for creating a user.</param>
        static void InitializeUsers(ProductManagerDBContext dBContext, UserManager<ApiUser> userManager)
        {
            if(dBContext.Users.Count() == 0){
                ApiUser user = new ApiUser();
                user.Email = "test@test.com";
                user.FirstName = "User FirstName";
                user.LastName = "User LastName";
                user.UserName = "test@test.com";
                userManager.CreateAsync(user,"Test$123456").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user,"Admin");
            }
        }

        /// <summary>
        /// Add initial customer data.
        /// </summary>
        /// <param name="context">DBContext for db access.
        static void InitializeCustomers(ProductManagerDBContext context)
        {
            int j =0;
            for(int i=0;i<1_000_000;i++){
                Customer customer = new Customer(){
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Phone = Faker.Phone.Number(),
                    Address = Faker.Address.City() + " " + Faker.Address.Country() + " " +
                    Faker.Address.StreetAddress() + " " +Faker.Address.SecondaryAddress(),
                };
                context.Add(customer);

                if(i / 10_000 == j){
                    j++;
                    Console.WriteLine(j + " * 10_000");
                }
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Add initial customer data.
        /// </summary>
        /// <param name="context">DBContext for db access.
        static void InitializeSellers(ProductManagerDBContext context)
        {
            int j = 0;

            for (int i = 0; i < 1_000_000; i++)
            {
                Seller seller = new Seller()
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Phone = Faker.Phone.Number(),
                    Address = Faker.Address.City() + " " + Faker.Address.Country() + " " +
                    Faker.Address.StreetAddress() + " " + Faker.Address.SecondaryAddress(),
                };
                context.Add(seller);

                if (i / 10_000 == j)
                {
                    context.SaveChanges();
                    j++;
                    Console.WriteLine(j + " * 10_000");
                }
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
           
            int a = 50;
            for(int k=0;k<100;k++){

                var product = context.Products.Find(1 + Convert.ToInt64(k+a));
                var seller = context.Sellers.Find(k+a);
                int j = 0;
                using(var scope = new TransactionScope()){
                    for (int i = 0; i < 10_000; i++)
                    {
                        Purchase purchase = new Purchase()
                        {
                            Amount = rnd.Next(10_000),
                            Cost = rnd.Next(10_000),
                            DateTime = RandomDay(),
                            Product = product,
                            Seller = seller
                        };

                        context.Add(purchase);

                        if (i % 1000 == j)
                        {
                            Console.WriteLine("K=" + k + ",I=" + i);
                            context.SaveChanges();
                        }
                    }
                    scope.Complete();
                }
               

            }


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
