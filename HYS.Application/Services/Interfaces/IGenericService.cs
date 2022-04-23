using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.Domain.Entities;
using HYS.Domain.Params;

namespace HYS.Application.Services.Interfaces
{
    public interface IGenericService<TEntity>
    {
        public List<Product> GetAll(string search, int? CategoryId, string sortBy, int page, int PAGE_SIZE);
        public TEntity GetById(int id);
        public void Insert(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public void Delete(int id);


    }
}
