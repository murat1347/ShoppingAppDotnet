using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.Application.Services.Interfaces;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Interfaces;

namespace HYS.Application.Services.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        readonly ICategoryRepository _repository;

        public CategoryManager(IGenericRepository<Category> genericService, ICategoryRepository repository) : base(
            genericService)
        {
            _repository = repository;
        }
        public List<Product> GetPostsByCategory(int id)
        {
            return _repository.GetPostsByCategory(id);
        }
    }
}
