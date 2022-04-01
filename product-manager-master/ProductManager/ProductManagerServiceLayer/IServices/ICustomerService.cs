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
    public interface ICustomerService : IGenericService<Customer, int>
    {
        Task<CustomerPagedDTO> GetAllCustomers(CustomerRequestParams requestParams);

    }
}
