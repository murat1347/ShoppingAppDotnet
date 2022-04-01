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
    public class SaleService : GenericService<Sale,long>, ISaleService
    {
        private readonly IMapper _mapper;

        public SaleService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async new Task<Sale> GetOne(long id)
        {
            return await _unitOfWork.SaleRepository.GetSingle(id);
        }

        public async Task<SalePagedDTO> GetAllSales(SaleRequestParams requestParams)
        {
            List<Expression<Func<Sale, bool>>> expressions = new List<Expression<Func<Sale, bool>>>();

            if (requestParams.Id != null)
            {
                expressions.Add(c => c.Id == requestParams.Id);
            }

            if (requestParams.CustomerFirstName != null)
            {
                expressions.Add(c => c.Customer.FirstName.Contains(requestParams.CustomerFirstName));
            }

            if (requestParams.CustomerLastName != null)
            {
                expressions.Add(c => c.Customer.LastName.Contains(requestParams.CustomerLastName));
            }

            if (requestParams.ProductName != null)
            {
                expressions.Add(c => c.Product.Name.Contains(requestParams.ProductName));
            }

            if (requestParams.AmountMin != null)
            {
                expressions.Add(c => c.Amount >= requestParams.AmountMin);
            }

            if (requestParams.AmountMax != null)
            {
                expressions.Add(c => c.Amount <= requestParams.AmountMax);
            }

            if (requestParams.IncomeMin != null)
            {
                expressions.Add(c => c.Income >= requestParams.IncomeMin);
            }

            if (requestParams.IncomeMax != null)
            {
                expressions.Add(c => c.Income <= requestParams.IncomeMax);
            }

            if (requestParams.DateTimeMin != null)
            {
                expressions.Add(c => c.DateTime >= requestParams.DateTimeMin);
            }

            if (requestParams.DateTimeMax != null)
            {
                expressions.Add(c => c.DateTime <= requestParams.DateTimeMax);
            }

            Func<IQueryable<Sale>, IOrderedQueryable<Sale>> orderByQuery = null;

            Expression<Func<Sale, object>> orderBy = p => p.Id;

            if (requestParams.Sort != null)
            {
                if (nameof(PurchaseRequestParams.SellerFirstName)
                    .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase))
                {
                    orderBy = p => p.Customer.FirstName;
                }
                else if ((nameof(PurchaseRequestParams.SellerLastName)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Customer.LastName;
                }
                else if ((nameof(PurchaseRequestParams.ProductName)
                 .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Product.Name;
                }
                else if ((nameof(Sale.Amount)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Amount;
                }
                else if ((nameof(Sale.Income)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Income;
                }
                else if ((nameof(Sale.DateTime)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.DateTime;
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

            var result = await _unitOfWork.Repository<Sale>()
                .GetAllPaged(requestParams.PageNumber,
                             requestParams.PageSize,
               expressions: expressions,
               includes: new List<string> { nameof(Product), nameof(Customer) },
               orderBy: orderByQuery
               );

            var count = await _unitOfWork.Repository<Sale>().Count(
                expressions: expressions);

            var results = _mapper.Map<IList<SaleDTO>>(result);

            var purchasePagedDTO = new SalePagedDTO
            {
                Sales = results,
                Count = count
            };

            return purchasePagedDTO;
        }

        public async Task Delete(Sale sale)
        {
            await _unitOfWork.SaleRepository.Delete(sale);
        }


        public async Task<Sale> GetById(long id)
        {
            return await _unitOfWork.SaleRepository.GetById(id);
        }

        public async Task Add(Sale sale)
        {
            await _unitOfWork.SaleRepository.Insert(sale);
        }

    }
}
