using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.seller
{
    [Collection("TestCollection1 #1")]
    public class PurchaseDeleteTest : AbstractTest
    {
        [Fact]
        public async void DeleteSeller_WithUnAuthorized_Except401()
        {
            var seller = DbContext.Sellers.First();

            var response = await Client.DeleteAsync(testRoute.Single(testRoute.Seller, seller.Id));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void DeleteSeller_WithUnExistedId_ExceptNotFound()
        {
            var response = await AuthorizedClient.DeleteAsync(testRoute.Single(testRoute.Seller, 99999));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void DeleteSeller_WithExisted_ExceptDeleted()
        {
            var seller = DbContext.Sellers.First();

            DbContext.Entry(seller).State = EntityState.Detached;

            var response = await AuthorizedClient
                .DeleteAsync(testRoute.Single(testRoute.Seller, seller.Id));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Null(DbContext.Sellers.Find(seller.Id));
        }
    }
}
