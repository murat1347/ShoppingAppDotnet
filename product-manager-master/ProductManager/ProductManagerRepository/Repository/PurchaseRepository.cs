using Microsoft.EntityFrameworkCore;
using ProductManager.Helpers;
using ProductManager.IRepository;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Repository
{
    /// <summary>
    /// Spesific implementation of generic repository for Purchase model.
    /// </summary>
    public class PurchaseRepository : GenericRepository<Purchase>
    {
        public PurchaseRepository(ProductManagerDBContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get A purchase with Product and Category with paging
        /// </summary>
        /// <param name="pageNumber">How much record will be skipped from first.</param>
        /// <param name="numberOfRecords">How much record exists in a page.</param>
        /// <returns>List of paged Purchases</returns>
        public async Task<IList<Purchase>> GetAllPagedWithCategoryAndProduct(int pageNumber, int numberOfRecords)
        {
            return await _db.Include(s => s.Seller)
                .Include(s => s.Product)
                .Select(s => new Purchase
                {
                    Id = s.Id,
                    Amount = s.Amount,
                    Cost = s.Cost,
                    Seller = new Seller
                    {
                        Id = s.SellerId,
                        FirstName = s.Seller.FirstName,
                        LastName = s.Seller.LastName
                    },
                    Product = new Product
                    {
                        Id = s.ProductId,
                        Name = s.Product.Name
                    }
                })
                .Skip((pageNumber - 1) * numberOfRecords)
                .Take(numberOfRecords)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Purchase> GetSingle(long id)
        {
            return await _db.Select(p=>new Purchase{
              Id = p.Id,
              Amount = p.Amount,
              Cost = p.Cost,
              DateTime = p.DateTime,
              Seller = new Seller{
                  Id = p.SellerId,
                  FirstName = p.Seller.FirstName,
                  LastName = p.Seller.LastName,
              },
              Product = new Product{
                  Id = p.ProductId,
                  Name = p.Product.Name
              }
            }).Where(p=>p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<decimal> TotalCost()
        {
            return await _db.SumAsync(p => p.Cost);
        }
    }
}
