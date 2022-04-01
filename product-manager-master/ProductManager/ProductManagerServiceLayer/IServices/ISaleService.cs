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
    public interface ISaleService : IGenericService<Sale, long>
    {
        Task Add(Sale sale);
        Task Delete(Sale sale);
        Task<SalePagedDTO> GetAllSales(SaleRequestParams requestParams);
        Task<Sale> GetById(long id);
    }
}
