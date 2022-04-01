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
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(ProductManagerDBContext context)
            : base(context)
        {}

        public async Task<object> CategoryIncomeResult(int take)
        {
            return await _db.Select(c=> new {
                CategoryName = c.Name,
                TotalIncome = c.Products.Sum(p=>p.Sales.Sum(s=>s.Income))
            }).OrderByDescending(g=>g.TotalIncome).Take(take).ToListAsync();
        }
    }
}
