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
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(ProductManagerDBContext context) : base(context)
        {

        }
    }
}
