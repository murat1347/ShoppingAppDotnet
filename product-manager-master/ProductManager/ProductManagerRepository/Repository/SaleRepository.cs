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
    /// Spesific implementation of generic repository for Sale model.
    /// </summary>
    public class SaleRepository : GenericRepository<Sale>
    {
        public SaleRepository(ProductManagerDBContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get A sale with Product and Category with paging
        /// </summary>
        /// <param name="pageNumber">How much record will be skipped from first.</param>
        /// <param name="numberOfRecords">How much record exists in a page.</param>
        /// <returns></returns>
        public async Task<IList<Sale>> GetAllPagedWithCategoryAndProduct(int pageNumber, int numberOfRecords)
        {
            return await _db.Include(s => s.Customer)
                .Include(s => s.Product)
                .Select(s => new Sale
                {
                    Id = s.Id,
                    Amount = s.Amount,
                    Income = s.Income,
                    DateTime = s.DateTime,
                    Customer = new Customer
                    {
                        Id = s.CustomerId,
                        FirstName = s.Customer.FirstName,
                        LastName = s.Customer.LastName
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

        public async Task<decimal> TotalIncome()
        {
            return await _db.SumAsync(p=>p.Income);
        }
        public async Task<object> MounthlyIncomes(int take)
        {
            return await _db.Select(p => new {p.DateTime.Month, p.Income })
                .GroupBy(x => new { x.Month }, (key, group) => new
            {
                month = key.Month,
                income = group.Sum(k => k.Income)
            }).OrderByDescending(p=>p.income)
            .Take(take)
            .ToListAsync();
        }

        public async Task<Sale> GetSingle(long id)
        {
            return await _db.Select(p => new Sale
            {
                Id = p.Id,
                Amount = p.Amount,
                Income = p.Income,
                DateTime = p.DateTime,
                Customer = new Customer
                {
                    Id = p.CustomerId,
                    FirstName = p.Customer.FirstName,
                    LastName = p.Customer.LastName,
                },
                Product = new Product
                {
                    Id = p.ProductId,
                    Name = p.Product.Name
                }
            }).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

    }
}
