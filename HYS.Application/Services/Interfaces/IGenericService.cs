using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Application.Services.Interfaces
{
    public interface IGenericService<TEntity>
    {
        public List<TEntity> GetAll();
        public TEntity GetById(int id);
        public void Insert(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public void Delete(int id);
    }
}
