using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Interfaces;

namespace HYS.Persistence.Repositories.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        private readonly IDbConnection _dbConnection;

        public CategoryRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public List<Product> GetPostsByCategory(int id)
        {
            var p = new DynamicParameters();
            p.Add("Id", id);

            string sql = "select * from dbo.Products where categoryId ="+id;  // Code in another language, stored in a string!
            
            var values =  _dbConnection.Query<Product>(sql);


            return values.ToList();
        }
    }
}