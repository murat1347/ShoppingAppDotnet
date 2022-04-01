using Microsoft.EntityFrameworkCore;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.product
{
    [Collection("TestCollection1 #1")]
    public class ProductDeleteTest : AbstractTest
    {
        [Fact]
        public async Task DeleteProduct_WithUnAuthorized_Except401()
        {
            var product = DbContext.Products.AsNoTracking().First();

            var response = await Client.DeleteAsync(testRoute.Single(testRoute.Product, product.Id));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_WithUnExistedId_ExceptNotFound()
        {
            var response = await AuthorizedClient.DeleteAsync(testRoute.Single(testRoute.Product, 99999));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void DeleteProduct_WithExisted_ExceptDeleted()
        {
            var product = DbContext.Products.Where(p=>p.Sales.Count()==0 && p.Purchases.Count()==0).AsNoTracking().First();

            var response = await AuthorizedClient
                .DeleteAsync(testRoute.Single(testRoute.Product, product.Id));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Null(DbContext.Products.Find(product.Id));
        }

    }
}
