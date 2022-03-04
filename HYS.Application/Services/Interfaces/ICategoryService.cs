using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.Domain.Entities;

namespace HYS.Application.Services.Interfaces
{
    public interface ICategoryService:IGenericService<Category>
    {
        public List<Product> GetPostsByCategory(int id);
    }
}
