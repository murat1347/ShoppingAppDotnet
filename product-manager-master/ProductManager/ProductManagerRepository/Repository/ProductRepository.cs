using Microsoft.EntityFrameworkCore;
using ProductManager.Helpers;
using ProductManager.Models;
using ProductManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerRepository.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ProductManagerDBContext context)
            : base(context)
        {}

        public async Task<object> ProductSummarySaleResult(int take)
        {
            return await _db.Select(p=> new 
            {
                ProductName = p.Name,
                TotalSale = p.Sales.Count(),
            }).OrderByDescending(g=>g.TotalSale)
            .Take(take)
            .ToListAsync();
        }

        public async Task<object> ProductSummaryPurchaseResult(int take)
        {
            return await _db.Select(p => new
            {
                ProductName = p.Name,
                TotalPurchase = p.Purchases.Count()
            }).OrderByDescending(g => g.TotalPurchase)
            .Take(take)
            .ToListAsync();
        }


        public async Task<long> ProductCriticalStockCount(int criticalStockValue)
        {
            return await _db.Where(p=>p.Stock <= criticalStockValue).LongCountAsync();
        }

        public async Task<object> ProductMostProfitResult(int take)
        {
            return await _db.Select(p => new
            {
                ProductName = p.Name,
                Profit = p.Sales.Sum(s=>s.Income) - p.Purchases.Sum(pu=>pu.Cost)
            }).OrderByDescending(g => g.Profit)
            .Take(take)
            .ToListAsync();
        }

        public async Task<object> ProductMostLossProfitResult(int take)
        {
            return await _db.Select(p => new
            {
                ProductName = p.Name,
                Profit = p.Sales.Sum(s => s.Income) - p.Purchases.Sum(pu => pu.Cost)
            }).OrderBy(g => g.Profit)
            .Take(take)
            .ToListAsync();
        }
    }
}
