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
    public class CustomerCreateTest : AbstractTest
    {
        [Fact]
        public async void CreateCustomer_WithUnAuthorized_Except401()
        {
            var customerCreateDTO = new CustomerCreateDTO();

            var response = await Client.PostAsync(testRoute.Customer,
                SerializeJsonObject(customerCreateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void CreateCustomer_WithUpperLimit_Except400()
        {
            var customerCreateDTO = new CustomerCreateDTO
            {
                FirstName = new string('a', CustomerConfiguration.FirstNameMax + 1),
                LastName = new string('a', CustomerConfiguration.LastNameMax + 1),
                Address = new string('a', CustomerConfiguration.AddressMax + 1),
                Phone = new string('a', CustomerConfiguration.PhoneMax + 1),
                Email = new string('a', CustomerConfiguration.EmailMax + 1),
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Customer,
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
        public async void CreateCustomer_WithRequired_Except400()
        {
            var customerCreateDTO = new CustomerCreateDTO
            {
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Customer,
                SerializeJsonObject(customerCreateDTO));

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
        public async void CreateCustomer_WithBadFormat_Except400()
        {
            var customerCreateDTO = new CustomerCreateDTO
            {
                Email ="111111",
                Phone ="asdasdasd"
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Customer,
                SerializeJsonObject(customerCreateDTO));

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
            var customerCreateDTO = new CustomerCreateDTO
            {
                FirstName = "New Customer FN",
                LastName = "New Customer LN",
                Address = "New Customer Address",
                Phone = "+61.423.676.123",
                Email = "newcustomer@email.com"
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Customer,
                SerializeJsonObject(customerCreateDTO));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var customerDTO = await DeSerializeJsonObject<CustomerDTO>(response);

            Assert.Equal(customerCreateDTO.FirstName, customerDTO.FirstName);
            Assert.Equal(customerCreateDTO.LastName, customerDTO.LastName);
            Assert.Equal(customerCreateDTO.Address, customerDTO.Address);
            Assert.Equal(customerCreateDTO.Phone, customerDTO.Phone);
            Assert.Equal(customerCreateDTO.Email, customerDTO.Email);
        }
    }
}
