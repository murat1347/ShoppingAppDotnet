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
            var blogPosts = _dbConnection.Query<Product>("GetPostsByCategory", p, commandType: CommandType.StoredProcedure).ToList();
            return blogPosts;
        }
    }
}