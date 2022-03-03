using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Interfaces;

namespace HYS.Persistence.Repositories.Concrete
{
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        private readonly IDbConnection _dbConnection;
        public ProductRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}
