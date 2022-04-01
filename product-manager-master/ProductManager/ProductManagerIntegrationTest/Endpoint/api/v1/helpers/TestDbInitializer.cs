using ProductManager.Helpers;
using ProductManager.Models;
using System;


namespace ProductManagerIntegrationTest.Endpoint.api.v1
{
    public static class TestDbInitializer
    {
        public static void InitializeDatabase(ProductManagerDBContext db)
        {
            InitializeCategories(db);
            InitializeProducts(db);
            InitializeCustomers(db);
            InitializeSellers(db);
            InitializeSales(db);
            InitializePurchases(db);
        }

        private static void InitializeProducts(ProductManagerDBContext db)
        {
            for (int i = 0; i < 100; i++)
            {
                var product = new Product
                {
                    Name = "Test Product " + i,
                    Category = db.Categories.Find((i+1)),
                    ImageUrl = $"product-{i}.img"
                };

                db.Products.Add(product);
            }
            db.SaveChanges();
        }

        public static void InitializeCategories(ProductManagerDBContext db)
        {
            Category before = null;
            for (int i = 0; i < 100; i++)
            {
                var cat = new Category
                {
                    Name = "Test Category " + i,
                };

                if (before != null)
                {
                    cat.Parent = before;
                }

                before = cat;

                db.Categories.Add(cat);
            }

            db.SaveChanges();
        }

        public static void InitializeCustomers(ProductManagerDBContext db)
        {
            for (int i = 0; i < 100; i++)
            {
                var customer = new Customer{
                    FirstName = $"{i} Customer FN",
                    LastName = $"{i} Customer LN",
                    Address = $"{i} Customer Address",
                    Email = $"customer{i}@mail.com",
                    Phone = "0000" +i,
                };

                db.Customers.Add(customer);
            }
            db.SaveChanges();
        }

        public static void InitializeSellers(ProductManagerDBContext db)
        {
            for (int i = 0; i < 100; i++)
            {
                var seller = new Seller
                {
                    FirstName = $"{i} Customer FN",
                    LastName = $"{i} Customer LN",
                    Address = $"{i} Customer Address",
                    Email = $"customer{i}@mail.com",
                    Phone = "0000" + i,
                };

                db.Sellers.Add(seller);
            }
            db.SaveChanges();
        }

        public static void InitializeSales(ProductManagerDBContext db)
        {
            for (int i = 0; i < 100; i++)
            {
                long casted = i+1;
                var sale = new Sale
                {
                    Product = db.Products.Find(casted),
                    Customer = db.Customers.Find(i+1),
                    Income = i * 10,
                    Amount = i *5
                };

                db.Sales.Add(sale);
            }
            db.SaveChanges();
        }

        public static void InitializePurchases(ProductManagerDBContext db)
        {
            for (int i = 0; i < 100; i++)
            {
                long casted = i + 1;

                var purchase = new Purchase
                {
                    Product = db.Products.Find(casted),
                    Seller = db.Sellers.Find(i + 1),
                    Cost = i * 10,
                    Amount = i * 5
                };

                db.Purchases.Add(purchase);
            }
            db.SaveChanges();
        }
    }
}
