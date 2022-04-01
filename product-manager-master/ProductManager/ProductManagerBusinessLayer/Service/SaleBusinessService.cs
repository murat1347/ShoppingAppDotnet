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
    public class SaleBusinessService : ISaleBusinessService
    {
        private IProductService _productService;

        private ISaleService _saleService;

        public SaleBusinessService(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }

        public async Task Add(Sale sale){
            
            var product = await _productService.GetById(sale.ProductId);

            if(product.Stock < sale.Amount){
                throw new NotEnoughStockException();
            }

            product.Stock -= sale.Amount;

             _productService.Update(product);

            await _saleService.Add(sale);
        }

        public async Task Update(Sale sale)
        {

            var oldSale = await _saleService.GetById(sale.Id);

            var product = await _productService.GetById(sale.ProductId);

            if (oldSale.Amount != sale.Amount)
            {

                var diff = sale.Amount - oldSale.Amount;

                if (product.Stock - diff < 0)
                {
                    throw new NotEnoughStockException();
                }

                product.Stock -= diff;
            }

            _productService.Update(product);

             _saleService.Update(sale);
        }

        public async Task Delete(long id)
        {

            var sale = await _saleService.GetById(id);

            var product = await _productService.GetById(sale.ProductId);


            product.Stock += sale.Amount;

            _productService.Update(product);

            await _saleService.Delete(sale);
        }

    }
}
