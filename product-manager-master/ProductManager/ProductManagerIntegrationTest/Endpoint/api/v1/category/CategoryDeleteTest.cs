using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.category
{
    [Collection("TestCollection1 #1")]
    public class CategoryDeleteTest : AbstractTest
    {
        [Fact]
        public async void DeleteCategory_WithUnAuthorized_Except401()
        {
            var category = DbContext.Categories.AsNoTracking().First();

            var response = await Client.DeleteAsync(testRoute.Single(testRoute.Category, category.Id));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void DeleteCategory_WithUnExistedId_ExceptNotFound()
        {
            var response = await AuthorizedClient.DeleteAsync(testRoute.Single(testRoute.Category, 99999));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void DeleteCategory_WithExisted_ExceptDeleted()
        {
            var category = DbContext.Categories.AsNoTracking()
                .Where(c=>c.Children.Count() == 0 && c.Products.Count() == 0).First();

            var response = await AuthorizedClient
                .DeleteAsync(testRoute.Single(testRoute.Category, category.Id));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var cont = DbContext;

            // cont.Products.Remove(cont.Products.Find(long.Parse("1")));
            await cont.SaveChangesAsync();

            Assert.Null(cont.Categories.Find(category.Id));
        }
    }
}
