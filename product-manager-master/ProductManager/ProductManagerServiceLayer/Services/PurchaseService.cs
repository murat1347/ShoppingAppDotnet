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
    public class PurchaseService : GenericService<Purchase,long>, IPurchaseService
    {
        private readonly IMapper _mapper;

        public PurchaseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async new Task<Purchase> GetOne(long id)
        {
            return await _unitOfWork.PurchaseRepository.GetSingle(id);
        }

        public async Task<PurchasePagedDTO> GetAllPurchases(PurchaseRequestParams requestParams)
        {
            List<Expression<Func<Purchase, bool>>> expressions = new List<Expression<Func<Purchase, bool>>>();

            if (requestParams.Id != null)
            {
                expressions.Add(c => c.Id == requestParams.Id);
            }

            if (requestParams.SellerFirstName != null)
            {
                expressions.Add(c => c.Seller.FirstName.Contains(requestParams.SellerFirstName));
            }

            if (requestParams.SellerLastName != null)
            {
                expressions.Add(c => c.Seller.LastName.Contains(requestParams.SellerLastName));
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

            if (requestParams.CostMin != null)
            {
                expressions.Add(c => c.Cost >= requestParams.CostMin);
            }

            if (requestParams.CostMax != null)
            {
                expressions.Add(c => c.Cost <= requestParams.CostMax);
            }

            if (requestParams.DateTimeMin != null)
            {
                expressions.Add(c => c.DateTime >= requestParams.DateTimeMin);
            }

            if (requestParams.DateTimeMax != null)
            {
                expressions.Add(c => c.DateTime <= requestParams.DateTimeMax);
            }

            Func<IQueryable<Purchase>, IOrderedQueryable<Purchase>> orderByQuery = null;

            Expression<Func<Purchase, object>> orderBy = p => p.Id;

            if (requestParams.Sort != null)
            {
                if (nameof(PurchaseRequestParams.SellerFirstName)
                    .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase))
                {
                    orderBy = p => p.Seller.FirstName;
                }
                else if ((nameof(PurchaseRequestParams.SellerLastName)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Seller.LastName;
                }
                else if ((nameof(PurchaseRequestParams.ProductName)
                 .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Product.Name;
                }
                else if ((nameof(Purchase.Amount)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Amount;
                }
                else if ((nameof(Purchase.Cost)
                   .Equals(requestParams.Sort, StringComparison.OrdinalIgnoreCase)))
                {
                    orderBy = p => p.Cost;
                }
                else if ((nameof(Purchase.DateTime)
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

            var result = await _unitOfWork.Repository<Purchase>()
                .GetAllPaged(requestParams.PageNumber,
                             requestParams.PageSize,
               expressions: expressions,
               includes: new List<string> { nameof(Product), nameof(Seller) },
               orderBy: orderByQuery
               );

            var count = await _unitOfWork.Repository<Purchase>().Count(
                expressions: expressions);

            var results = _mapper.Map<IList<PurchaseDTO>>(result);

            var purchasePagedDTO = new PurchasePagedDTO
            {
                Purchases = results,
                Count = count
            };

            return purchasePagedDTO;
        }

        public async Task Delete(Purchase purchase)
        {
            await _unitOfWork.PurchaseRepository.Delete(purchase.Id);
        }

        public async Task<Purchase> GetById(long id)
        {
           return await _unitOfWork.PurchaseRepository.GetById(id);
        }
    }
}
