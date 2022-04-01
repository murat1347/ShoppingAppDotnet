using Microsoft.EntityFrameworkCore;
using ProductManager.DTO;
using ProductManager.DTO.Purchase;
using ProductManager.Validators;
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
    public class PurchaseCreateTest : AbstractTest
    {
        [Fact]
        public async void UpdatePurchase_WithUnAuthorized_Except401()
        {
            var purchase = DbContext.Purchases.First();

            var purchaseUpdateDTO = new PurchaseUpdateDTO();

            var response = await Client.PutAsync(testRoute.Single(testRoute.Purchase,purchase.Id),
                SerializeJsonObject(purchaseUpdateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void UpdatePurchase_WithoutRequired_Except401()
        {
            var purchase = DbContext.Purchases.First();

            var purchaseUpdateDTO = new PurchaseUpdateDTO();

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Purchase, purchase.Id),
                SerializeJsonObject(purchaseUpdateDTO));

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
        public async void UpdatePurchase_WithNonExistedProductAndSeller_Except401()
        {
            var purchase = DbContext.Purchases.First();

            var purchaseUpdateDTO = new PurchaseUpdateDTO(){
                Amount = purchase.Amount,
                Cost = purchase.Cost,
                ProductId = 9999999,
                SellerId = 99999,
                DateTime = purchase.DateTime
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Purchase, purchase.Id),
                SerializeJsonObject(purchaseUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.SellerId))
             ,ValidationMessageKey.NotExists);

            EnsureErrorValidationContains(errorDTO, (nameof(PurchaseCreateDTO.ProductId))
            , ValidationMessageKey.NotExists);
        }

        [Fact]
        public async void UpdatePurchase_WithValid_ExceptUpdatePurchase()
        {
            var purchase = DbContext.Purchases.First();
            DbContext.Entry(purchase).State = EntityState.Detached;

            var purchaseUpdateDTO = new PurchaseUpdateDTO()
            {
                Amount = 3,
                Cost = 3,
                ProductId = purchase.ProductId,
                SellerId = purchase.SellerId,
                DateTime = DateTime.Now
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Purchase, purchase.Id),
                SerializeJsonObject(purchaseUpdateDTO));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updatedPurchase = DbContext.Purchases.Find(purchase.Id);

            Assert.Equal(updatedPurchase.Amount, purchaseUpdateDTO.Amount);
            Assert.Equal(updatedPurchase.Cost, purchaseUpdateDTO.Cost);
            Assert.Equal(updatedPurchase.ProductId, purchaseUpdateDTO.ProductId);
            Assert.Equal(updatedPurchase.SellerId, purchaseUpdateDTO.SellerId);
            Assert.Equal(updatedPurchase.DateTime, purchaseUpdateDTO.DateTime);
        }
    }
}
