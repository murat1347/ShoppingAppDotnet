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
    public class CustomerService : GenericService<Customer,int>, ICustomerService
    {
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<CustomerPagedDTO> GetAllCustomers(CustomerRequestParams requestParams)
        {
            List<Expression<Func<Customer, bool>>> expressions = new List<Expression<Func<Customer, bool>>>();

            if(requestParams.Id != null){
                expressions.Add(c =>c.Id == requestParams.Id);
            }

            if (requestParams.FirstName != null)
            {
                expressions.Add(c => c.FirstName.Contains(requestParams.FirstName));
            }

            if (requestParams.Lastname != null)
            {
                expressions.Add(c => c.LastName.Contains(requestParams.Lastname));
            }

            if (requestParams.Email != null)
            {
                expressions.Add(c => c.Email.Contains(requestParams.Email));
            }

            if (requestParams.Phone != null)
            {
                expressions.Add(c => c.Phone.Contains(requestParams.Phone));
            }

            Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderByQuery = null;

            Expression<Func<Customer, object>> orderBy = p => p.Id;

            if (requestParams.Sort != null)
            {
                if (nameof(Customer.FirstName)
                    .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase))
                {
                    orderBy = p => p.FirstName;
                }
                else if ((nameof(Customer.LastName)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.LastName;
                }
                else if ((nameof(Customer.Email)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Email;
                }
                else if ((nameof(Customer.Phone)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Phone;
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

            var result = await _unitOfWork.Repository<Customer>()
                .GetAllPaged(requestParams.PageNumber,
                             requestParams.PageSize,
               expressions: expressions,
               orderBy: orderByQuery
               );

            var count = await _unitOfWork.Repository<Customer>().Count(
                expressions: expressions);

            var results = _mapper.Map<IList<CustomerDTO>>(result);

            var customerPagedDTO = new CustomerPagedDTO
            {
                Customers = results,
                Count = count
            };

            return customerPagedDTO;
        }
    }
}
