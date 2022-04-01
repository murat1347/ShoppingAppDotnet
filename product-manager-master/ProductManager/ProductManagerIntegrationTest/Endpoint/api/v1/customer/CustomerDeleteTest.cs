using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.customer
{
    [Collection("TestCollection1 #1")]
    public class CustomerDeleteTest : AbstractTest
    {
        [Fact]
        public async void DeleteCustomer_WithUnAuthorized_Except401()
        {
            var customer = DbContext.Customers.First();

            var response = await Client.DeleteAsync(testRoute.Single(testRoute.Customer, customer.Id));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void DeleteCustomer_WithUnExistedId_ExceptNotFound()
        {
            var response = await AuthorizedClient.DeleteAsync(testRoute.Single(testRoute.Customer, 99999));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void DeleteCustomer_WithExisted_ExceptDeleted()
        {
            var customer = DbContext.Customers.First();

            DbContext.Entry(customer).State = EntityState.Detached;

            var response = await AuthorizedClient
                .DeleteAsync(testRoute.Single(testRoute.Customer, customer.Id));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Null(DbContext.Customers.Find(customer.Id));
        }
    }
}
