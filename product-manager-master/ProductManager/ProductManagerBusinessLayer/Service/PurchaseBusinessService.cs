using ProductManager.IRepository;
using ProductManager.Models;
using ProductManagerBusinessLayer.exception;
using ProductManagerBusinessLayer.IService;
using ProductManagerServiceLayer.IServices;
using ProductManagerServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBusinessLayer.Service
{
    public class PurchaseBusinessService : IPurchaseBusinessService
    {
        private IProductService _productService;

        private IPurchaseService _purchaseService;

        public PurchaseBusinessService(IPurchaseService purchaseService, IProductService productService)
        {
            _purchaseService = purchaseService;
            _productService = productService;
        }

        public async void Add(Purchase purchase){
            
            var product = await _productService.GetById(purchase.ProductId);

            product.Stock += purchase.Amount;

             _productService.Update(product);

             _purchaseService.Update(purchase);
        }

        public async void Update(Purchase purchase)
        {

            var oldPurchase = await _purchaseService.GetById(purchase.Id);

            var product = await _productService.GetById(purchase.ProductId);

            if(oldPurchase.Amount != purchase.Amount){
                
                var diff = purchase.Amount - oldPurchase.Amount;

                if(product.Stock + diff < 0){
                    throw new NotEnoughStockException();
                }

                product.Stock += diff;
            }

            _productService.Update(product);

             _purchaseService.Update(purchase);
        }

        public async void Delete(long id)
        {

            var purchase = await _purchaseService.GetById(id);

            var product = await _productService.GetById(purchase.ProductId);

            if(product.Stock - purchase.Amount < 0){
                throw new NotEnoughStockException();
            }

            product.Stock -= purchase.Amount;

            _productService.Update(product);

            await _purchaseService.Delete(purchase);
        }

    }
}
