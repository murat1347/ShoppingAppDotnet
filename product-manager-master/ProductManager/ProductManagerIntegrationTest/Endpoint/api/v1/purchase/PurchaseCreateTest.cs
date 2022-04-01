using ProductManager.DTO;
using ProductManager.DTO.Purchase;
using ProductManager.Validators;
using ProductManagerDTO.DTO.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.purchase
{
    [Collection("TestCollection1 #1")]
    public class PurchaseUpdateTest : AbstractTest
    {
        [Fact]
        public async void UpdatePurchase_WithUnAuthorized_Except401()
        {
            var purchaseCreateDTO = new PurchaseCreateDTO();

            var response = await Client.PostAsync(testRoute.Purchase,
                SerializeJsonObject(purchaseCreateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void CreatePurchase_WithoutRequired_Except401()
        {
            var purchaseCreateDTO = new PurchaseCreateDTO();

            var response = await AuthorizedClient.PostAsync(testRoute.Purchase,
                SerializeJsonObject(purchaseCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.Amount))
            ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.Cost))
            ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.DateTime))
            ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.SellerId))
            ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.ProductId))
            ,ValidationMessageKey.Required);
        }

        [Fact]
        public async void CreatePurchase_WithNonExistedProductAndCustomer_Except401()
        {
            var purchaseCreateDTO = new PurchaseCreateDTO{
                Amount = 1,
                DateTime = DateTime.Now,
                ProductId = 9999999,
                SellerId = 99999,
                Cost = 1
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Purchase,
                SerializeJsonObject(purchaseCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.SellerId))
             ,ValidationMessageKey.NotExists);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.ProductId))
            , ValidationMessageKey.NotExists);
        }

        [Fact]
        public async void CreatePurchase_WithValid_ExceptCreatedPurchase()
        {
            var product = DbContext.Products.First();

            var customer = DbContext.Products.First();

            var purchaseCreateDTO = new PurchaseCreateDTO
            {
                Amount = 1,
                DateTime = DateTime.Now,
                ProductId = product.Id,
                SellerId = customer.Id,
                Cost = 1
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Purchase,
                SerializeJsonObject(purchaseCreateDTO));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var purchaseDTO = await DeSerializeJsonObject<PurchaseSingleResultDTO>(response);

            Assert.NotNull(purchaseDTO.Id);
            Assert.Equal(purchaseCreateDTO.Amount, purchaseDTO.Amount);
            Assert.Equal(purchaseCreateDTO.Cost, purchaseDTO.Cost);
            Assert.Equal(purchaseCreateDTO.ProductId, purchaseDTO.ProductId);
            Assert.Equal(purchaseCreateDTO.SellerId, purchaseDTO.SellerId);
            Assert.Equal(purchaseCreateDTO.DateTime, purchaseDTO.DateTime);
        }
    }
}
