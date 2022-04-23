using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HYS.Application.Services.Interfaces;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Interfaces;
using HYS.Persistence.Repositories.Concrete;
namespace HYS.Application.Services.Concrete
{
    public class ProductManager : GenericManager<Product>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IProductRepository _repository;
        

        public ProductManager(IGenericRepository<Product> genericService, IProductRepository repository) : base(
            genericService)
        {
            _repository = repository;
        }
       
    }
}
