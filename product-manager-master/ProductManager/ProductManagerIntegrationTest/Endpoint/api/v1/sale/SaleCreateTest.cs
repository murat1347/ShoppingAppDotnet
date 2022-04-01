using ProductManager.DTO;
using ProductManager.DTO.Sale;
using ProductManager.Validators;
using ProductManagerDTO.DTO.Sale;
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
    public class SaleUpdateTest : AbstractTest
    {
        [Fact]
        public async void UpdateSale_WithUnAuthorized_Except401()
        {
            var saleCreateDTO = new SaleCreateDTO();

            var response = await Client.PostAsync(testRoute.Sale,
                SerializeJsonObject(saleCreateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void CreateSale_WithoutRequired_Except401()
        {
            var saleCreateDTO = new SaleCreateDTO();

            var response = await AuthorizedClient.PostAsync(testRoute.Sale,
                SerializeJsonObject(saleCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.Amount))
            ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.Income))
            ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.DateTime))
            ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.CustomerId))
            ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.ProductId))
            ,ValidationMessageKey.Required);
        }

        [Fact]
        public async void CreateSale_WithNonExistedProductAndCustomer_Except401()
        {
            var saleCreateDTO = new SaleCreateDTO{
                Amount = 1,
                DateTime = DateTime.Now,
                ProductId = 9999999,
                CustomerId = 99999,
                Income = 1
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Sale,
                SerializeJsonObject(saleCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.CustomerId))
             ,ValidationMessageKey.NotExists);

            EnsureErrorValidationContains(errorDTO, (nameof(SaleCreateDTO.ProductId))
            , ValidationMessageKey.NotExists);
        }

       

        [Fact]
        public async void CreateSale_WithValid_ExceptCreated()
        {
            var product = DbContext.Products.First();

            var customer = DbContext.Customers.First();

            var saleCreateDTO = new SaleCreateDTO
            {
                Amount = 0,
                DateTime = DateTime.Now,
                ProductId = product.Id,
                CustomerId = customer.Id,
                Income = 1
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Sale,
                SerializeJsonObject(saleCreateDTO));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var saleDTO = await DeSerializeJsonObject<SaleSingleResultDTO>(response);

            Assert.NotNull(saleDTO.Id);
            Assert.Equal(saleCreateDTO.Amount, saleDTO.Amount);
            Assert.Equal(saleCreateDTO.Income, saleDTO.Income);
            Assert.Equal(saleCreateDTO.ProductId, saleDTO.ProductId);
            Assert.Equal(saleCreateDTO.CustomerId, saleDTO.CustomerId);
            Assert.Equal(saleCreateDTO.DateTime, saleDTO.DateTime);
        }
    }
}
