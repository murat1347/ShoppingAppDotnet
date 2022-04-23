using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HYS.Persistence.Repositories.Concrete
{
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        private readonly IDbConnection _dbConnection;
        public ProductRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
            
        }

        //public List<Product> Pagging()
        //{
        //    var query2 = _dbConnection.Query<Product>(
        //        "SELECT * FROM dbo.Products ORDER BY id OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY;");
        //    return query2.ToList();
        //    //_dbConnection.Query("sp_DeleteValue", entity, commandType: CommandType.StoredProcedure);
        //}

    }
}
