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
    public class ProductManager:GenericManager<Product>,IProductService
    {

        readonly IProductRepository _repository;
        public ProductManager(IGenericRepository<Product> genericService,IProductRepository repository) : base(genericService)
        {
            _repository = repository;
        }
    }
}
