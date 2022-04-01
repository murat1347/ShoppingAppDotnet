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
    public class SellerRepository : GenericRepository<Seller>
    {
        public SellerRepository(ProductManagerDBContext context) : base(context)
        {
        }
    }
}
