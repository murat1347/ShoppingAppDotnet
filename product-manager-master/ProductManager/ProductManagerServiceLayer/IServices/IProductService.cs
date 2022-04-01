using ProductManager.Models;
using ProductManager.RequestParams;
using ProductManagerDTO.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerServiceLayer.IServices
{
    public interface IProductService : IGenericService<Product, long>
    {
        Task<ProductPagedDTO> GetAllProducts(ProductRequestParams requestParams);
        Task<Product> GetById(long id);
    }
}
