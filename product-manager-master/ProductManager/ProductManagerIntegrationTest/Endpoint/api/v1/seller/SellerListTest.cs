using ProductManager.DTO;
using ProductManager.Models;
using ProductManagerDTO.DTO.Product;
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
    public class CustomerListTest : AbstractTest
    {
        [Fact]
        public async void GetAllCustomer_WithUnAuthorized_Except401()
        {
            var response = await Client.GetAsync(testRoute.Customer);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void GetOneCustomer_WithUnAuthorized_Except401()
        {
            var response = await Client.GetAsync(testRoute.Single(testRoute.Customer,1));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void GetAllCustomers_WithNoParameter_ExceptPagedCustomers()
        {
            var response = await AuthorizedClient.GetAsync(testRoute.Customer);
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var customerPagedDTO = await DeSerializeJsonObject<CustomerPagedDTO>(response);

            var customers = customerPagedDTO.Customers;

            var dbCustomers = DbContext.Customers.ToList();

            Assert.NotEmpty(dbCustomers);

            Assert.Equal(PagingRequestParams.DEFAULT_PAGE_SIZE, customers.Count);

            for (int i = 0; i < PagingRequestParams.DEFAULT_PAGE_SIZE; i++)
            {
                var dbCustomer = dbCustomers[i];
                var customer = customers[i];

                Assert.Equal(dbCustomer.Id, customer.Id);
                Assert.Equal(dbCustomer.Address, customer.Address);
                Assert.Equal(dbCustomer.FirstName, customer.FirstName);
                Assert.Equal(dbCustomer.LastName, customer.LastName);
                Assert.Equal(dbCustomer.Phone, customer.Phone);
                Assert.Equal(dbCustomer.Email, customer.Email);
            }
        }

        [Fact]
        public async void GetOneCustomer_WithId1_ExceptOneProduct()
        {
            var customer = DbContext.Customers.First();
            Assert.NotNull(customer);

            var response = await AuthorizedClient.GetAsync(testRoute.Single(testRoute.Customer, customer.Id));

            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var customerDTO = await DeSerializeJsonObject<CustomerDTO>(response);

            Assert.Equal(customer.Id, customerDTO.Id);
            Assert.Equal(customer.FirstName, customerDTO.FirstName);
            Assert.Equal(customer.LastName, customerDTO.LastName);
            Assert.Equal(customer.Address, customerDTO.Address);
            Assert.Equal(customer.Email, customerDTO.Email);
            Assert.Equal(customer.Phone, customerDTO.Phone);
        }
    }
}
