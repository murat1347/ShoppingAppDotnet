using Microsoft.EntityFrameworkCore;
using ProductManager.DTO;
using ProductManager.DTO.Sale;
using ProductManager.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.sale
{
    [Collection("TestCollection1 #1")]
    public class SaleCreateTest : AbstractTest
    {
        [Fact]
        public async void UpdateSale_WithUnAuthorized_Except401()
        {
            var sale = DbContext.Sales.First();

            var saleUpdateDTO = new SaleUpdateDTO();

            var response = await Client.PutAsync(testRoute.Single(testRoute.Sale,sale.Id),
                SerializeJsonObject(saleUpdateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void UpdateSale_WithoutRequired_Except401()
        {
            var sale = DbContext.Sales.First();

            var saleUpdateDTO = new SaleUpdateDTO();

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Sale, sale.Id),
                SerializeJsonObject(saleUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.Amount))
            , ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.Income))
            , ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.DateTime))
            , ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.CustomerId))
            , ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.ProductId))
            , ValidationMessageKey.Required);
        }

        [Fact]
        public async void UpdateSale_WithNonExistedProductAndCustomer_Except401()
        {
            var sale = DbContext.Sales.First();

            var saleUpdateDTO = new SaleUpdateDTO(){
                Amount = sale.Amount,
                Income = sale.Income,
                ProductId = 9999999,
                CustomerId = 99999,
                DateTime = sale.DateTime
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Sale, sale.Id),
                SerializeJsonObject(saleUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.CustomerId))
             , ValidationMessageKey.NotExists);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.ProductId))
            ,  ValidationMessageKey.NotExists);
        }

        [Fact]
        public async void UpdateSale_WithValid_ExceptUpdateSale()
        {
            var sale = DbContext.Sales.First();
            DbContext.Entry(sale).State = EntityState.Detached;

            var saleUpdateDTO = new SaleUpdateDTO()
            {
                Amount = 3,
                Income = 3,
                ProductId = sale.ProductId,
                CustomerId = sale.CustomerId,
                DateTime = DateTime.Now
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Sale, sale.Id),
                SerializeJsonObject(saleUpdateDTO));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updatedSale = DbContext.Sales.Find(sale.Id);

            Assert.Equal(updatedSale.Amount, saleUpdateDTO.Amount);
            Assert.Equal(updatedSale.Income, saleUpdateDTO.Income);
            Assert.Equal(updatedSale.ProductId, saleUpdateDTO.ProductId);
            Assert.Equal(updatedSale.CustomerId, saleUpdateDTO.CustomerId);
            Assert.Equal(updatedSale.DateTime, saleUpdateDTO.DateTime);
        }
    }
}
