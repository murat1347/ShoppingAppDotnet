using Microsoft.EntityFrameworkCore;
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
    public class SaleDeleteTest : AbstractTest
    {
        [Fact]
        public async void DeleteSale_WithUnAuthorized_Except401()
        {
            var sale = DbContext.Sales.First();

            var response = await Client.DeleteAsync(testRoute.Single(testRoute.Sale, sale.Id));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void DeleteSale_WithUnExistedId_ExceptNotFound()
        {
            var response = await AuthorizedClient.DeleteAsync(testRoute.Single(testRoute.Sale, 99999));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void DeleteSale_WithExisted_ExceptDeleted()
        {
            var sale = DbContext.Sales.First();

            DbContext.Entry(sale).State = EntityState.Detached;

            var response = await AuthorizedClient
                .DeleteAsync(testRoute.Single(testRoute.Sale, sale.Id));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Null(DbContext.Sales.Find(sale.Id));
        }
    }
}
