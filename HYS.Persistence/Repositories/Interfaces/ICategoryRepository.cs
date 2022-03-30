using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.Domain.Entities;

namespace HYS.Persistence.Repositories.Interfaces
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        public List<Product>GetPostsByCategory(int id);
      
    }
}
