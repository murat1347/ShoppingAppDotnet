using Microsoft.EntityFrameworkCore;
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
    public class PurchaseDeleteTest : AbstractTest
    {
        [Fact]
        public async void DeletePurchase_WithUnAuthorized_Except401()
        {
            var purchase = DbContext.Purchases.First();

            var response = await Client.DeleteAsync(testRoute.Single(testRoute.Purchase, purchase.Id));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void DeletePurchase_WithUnExistedId_ExceptNotFound()
        {
            var response = await AuthorizedClient.DeleteAsync(testRoute.Single(testRoute.Purchase, 99999));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void DeletePurchase_WithExisted_ExceptDeleted()
        {
            var purchase = DbContext.Purchases.First();

            DbContext.Entry(purchase).State = EntityState.Detached;

            var response = await AuthorizedClient
                .DeleteAsync(testRoute.Single(testRoute.Purchase, purchase.Id));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Null(DbContext.Purchases.Find(purchase.Id));
        }
    }
}
