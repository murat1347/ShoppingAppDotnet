using Microsoft.EntityFrameworkCore;
using ProductManager.DTO;
using ProductManager.ModelConfiguration;
using ProductManager.Validators;
using ProductManagerDTO.DTO.Customer;
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
    public class SellerUpdateTest : AbstractTest
    {
        [Fact]
        public async void UpdateCustomer_WithUnAuthorized_Except401()
        {
            var customerCreateDTO = new CustomerUpdateDTO();

            var response = await Client.PutAsync(testRoute.Single(testRoute.Customer,1),
                SerializeJsonObject(customerCreateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void UpdateCustomer_WithNonExisted_Except401()
        {
            var customerCreateDTO = new CustomerUpdateDTO();

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Customer, 999999),
                SerializeJsonObject(customerCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void UpdateCustomer_WithUpperLimit_Except400()
        {
            var customer = DbContext.Customers.First();

            var customerCreateDTO = new CustomerUpdateDTO
            {
                FirstName = new string('a', CustomerConfiguration.FirstNameMax + 1),
                LastName = new string('a', CustomerConfiguration.LastNameMax + 1),
                Address = new string('a', CustomerConfiguration.AddressMax + 1),
                Phone = new string('a', CustomerConfiguration.PhoneMax + 1),
                Email = new string('a', CustomerConfiguration.EmailMax + 1),
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Customer, customer.Id),
                SerializeJsonObject(customerCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.FirstName))
                , ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.LastName))
                , ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.Address))
                , ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.Phone))
                , ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.Email))
                , ValidationMessageKey.TooLong);
        }

        [Fact]
        public async void UpdateCustomer_WithRequired_Except400()
        {
            var customer = DbContext.Customers.First();

            var customerUpdateDTO = new CustomerCreateDTO
            {
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Customer, customer.Id),
                SerializeJsonObject(customerUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.FirstName))
                , ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.LastName))
                , ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.Address))
                , ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.Phone))
                , ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.Email))
                , ValidationMessageKey.Required);
        }

        [Fact]
        public async void UpdateCustomer_WithBadFormat_Except400()
        {
            var customer = DbContext.Customers.First();

            var customerUpdateDTO = new CustomerUpdateDTO
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Email = "111111",
                Phone = "asdasdasd"
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Customer, customer.Id),
                SerializeJsonObject(customerUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.Phone))
                , ValidationMessageKey.InvalidPhoneNumberFormat);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CustomerCreateDTO.Email))
                , ValidationMessageKey.InvalidEmailAdresssFormat);
        }

        [Fact]
        public async void CreateCustomer_WithValid_ExceptCreated()
        {
            var customer = DbContext.Customers.First();

            DbContext.Entry(customer).State = EntityState.Detached;

            var customerUpdateDTO = new CustomerCreateDTO
            {
                FirstName = "New Customer FN",
                LastName = "New Customer LN",
                Address = "New Customer Address",
                Phone = "+61.423.676.123",
                Email = "newcustomer@email.com"
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Customer, customer.Id),
                SerializeJsonObject(customerUpdateDTO));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updatedCustomer = DbContext.Customers.Find(customer.Id);

            Assert.Equal(customerUpdateDTO.FirstName, updatedCustomer.FirstName);
            Assert.Equal(customerUpdateDTO.LastName, updatedCustomer.LastName);
            Assert.Equal(customerUpdateDTO.Address, updatedCustomer.Address);
            Assert.Equal(customerUpdateDTO.Phone, updatedCustomer.Phone);
            Assert.Equal(customerUpdateDTO.Email, updatedCustomer.Email);
        }
    }
}
