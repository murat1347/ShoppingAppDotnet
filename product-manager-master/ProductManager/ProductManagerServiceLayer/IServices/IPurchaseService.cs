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
    public interface IPurchaseService : IGenericService<Purchase, long>
    {
        Task Delete(Purchase purchase);
        Task<PurchasePagedDTO> GetAllPurchases(PurchaseRequestParams requestParams);
        Task<Purchase> GetById(long id);
    }
}
