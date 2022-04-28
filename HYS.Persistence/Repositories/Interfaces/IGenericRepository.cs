using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HYS.Domain.Entities;
using HYS.Domain.Params;

namespace HYS.Persistence.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        TEntity GetById(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        List<Product> GetAll(string search, int? CategoryId, string sortBy, int page, int PAGE_SIZE);
        List<TEntity> GetAlls();
    }
}