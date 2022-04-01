using ProductManager.DTO;
using ProductManagerDTO.DTO.Product;
using ProductManagerIntegrationTest.Endpoint.api.v1;
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
    public class PurchaseListTest : AbstractTest
    {
        [Fact]
        public async void GetAllPurchase_WithUnAuthorized_Except401()
        {
            var response = await Client.GetAsync(testRoute.Purchase);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void GetAllPurchases_WithNoParameter_Except20Purchases()
        {
            var response = await AuthorizedClient.GetAsync(testRoute.Purchase);
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var purchasePagedDTO = await DeSerializeJsonObject<PurchasePagedDTO>(response);
            var purchases = purchasePagedDTO.Purchases;

            var dbPurchases = DbContext.Purchases.ToList();

            Assert.NotEmpty(dbPurchases);

            Assert.Equal(20, purchases.Count);

            for (int i = 0; i < 10; i++)
            {
                var dbPurchase = dbPurchases[i];
                var purchase = purchases[i];

                Assert.Equal(dbPurchase.Id, purchase.Id);
                Assert.Equal(dbPurchase.Amount, purchase.Amount);
                Assert.Equal(dbPurchase.DateTime, purchase.DateTime);
            }
        }

        [Fact]
        public async void GetAllPurchases_WithPageTwo_Except20Purchases()
        {
            var response = await AuthorizedClient.GetAsync(testRoute.Paged(testRoute.Purchase, 2));
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var purchasePagedDTO = await DeSerializeJsonObject<PurchasePagedDTO>(response);
            var purchases = purchasePagedDTO.Purchases;

            var dbPurchases = DbContext.Purchases.
                                Skip(PagingRequestParams.DEFAULT_PAGE_SIZE).
                                Take(PagingRequestParams.DEFAULT_PAGE_SIZE).
                                ToList();

            Assert.NotEmpty(dbPurchases);

            Assert.Equal(20, purchases.Count);

            for (int i = 0; i < dbPurchases.Count; i++)
            {
                var dbPurchase = dbPurchases[i];
                var purchase = purchases[i];

                Assert.Equal(dbPurchase.Id, purchase.Id);
                Assert.Equal(dbPurchase.Amount, purchase.Amount);
                Assert.Equal(dbPurchase.DateTime, purchase.DateTime);
            }
        }

        [Fact]
        public async void GetAllPurchases_WithPageTwoPageSize_Except50Purchases()
        {
            var response = await AuthorizedClient.GetAsync(testRoute.Paged(testRoute.Purchase, 2, 50));
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var purchasePagedDTO = await DeSerializeJsonObject<PurchasePagedDTO>(response);
            var purchases = purchasePagedDTO.Purchases;


            var dbPurchases = DbContext.Purchases.
                                Skip(50).
                                Take(50).
                                ToList();

            Assert.NotEmpty(dbPurchases);

            Assert.Equal(dbPurchases.Count, purchases.Count);

            for (int i = 0; i < dbPurchases.Count; i++)
            {
                var dbPurchase = dbPurchases[i];
                var purchase = purchases[i];

                Assert.Equal(dbPurchase.Id, purchase.Id);
                Assert.Equal(dbPurchase.Amount, purchase.Amount);
                Assert.Equal(dbPurchase.DateTime, purchase.DateTime);
            }
        }

       
    }
}
