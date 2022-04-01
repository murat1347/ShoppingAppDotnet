using AutoMapper;
using ProductManager.DTO;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.RequestParams;
using ProductManagerDTO.DTO.Product;
using ProductManagerPersistance.Helpers;
using ProductManagerServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerServiceLayer.Services
{
    public class ProductService : GenericService<Product,long>, IProductService 
    {
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<Product> GetById(long id){
            return await _unitOfWork.ProductRepository.GetById(id);
        }

        public async new Task<Product> GetOne(long id)
        {
            List<string> include = new List<string>();
            include.Add(nameof(Category));

            return await _unitOfWork.Repository<Product>().Get(c => c.Id == id, include);
        }

        public async Task<ProductPagedDTO> GetAllProducts(ProductRequestParams requestParams)
        {
            List<Expression<Func<Product, bool>>> expressions = new List<Expression<Func<Product, bool>>>();

            if (requestParams.Id != null)
            {
                expressions.Add(p => p.Id == requestParams.Id);
            }

            if (requestParams.CategoryId != null)
            {
                expressions.Add(p => p.Category.Id == requestParams.CategoryId);
            }

            if (requestParams.ProductName != null)
            {
                expressions.Add(p => p.Name.Contains(requestParams.ProductName));
            }

            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderByQuery = null;

            Expression<Func<Product, Object>> orderBy = p => p.Id;

            if (requestParams.Sort != null)
            {

                if (requestParams.Sort.ToLower() == nameof(Product.Name).ToLower())
                {
                    orderBy = p => p.Name;
                }

                if (requestParams.Sort.ToLower() == nameof(Product.Category).ToLower())
                {
                    orderBy = p => p.CategoryId;
                }
            }

            if (requestParams.Order != null)
            {
                if (requestParams.Order.ToLower() == Ordering.DESC)
                {
                    orderByQuery = q => q.OrderByDescending(orderBy);
                }
                else
                {
                    orderByQuery = q => q.OrderBy(orderBy);
                }
            }


            var result = await _unitOfWork.Repository<Product>()
                .GetAllPaged(requestParams.PageNumber,
                             requestParams.PageSize,
               expressions: expressions,
               includes: new List<string> { nameof(Category) },
               orderBy: orderByQuery
               );

            var count = await _unitOfWork.Repository<Product>().Count(
                expressions: expressions);

           
            var results = _mapper.Map<IList<ProductDTO>>(result);

            var productPagedDTO = new ProductPagedDTO
            {
                Products = results,
                Count = count
            };
            
            return productPagedDTO;
        }
    }
}
