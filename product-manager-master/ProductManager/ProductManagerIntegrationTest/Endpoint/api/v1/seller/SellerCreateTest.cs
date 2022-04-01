using ProductManager.DTO;
using ProductManager.ModelConfiguration;
using ProductManager.Validators;
using ProductManagerDTO.DTO.Seller;
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
    public class SellerCreateTest : AbstractTest
    {
        [Fact]
        public async void CreateSeller_WithUnAuthorized_Except401()
        {
            var sellerCreateDTO = new SellerCreateDTO();

            var response = await Client.PostAsync(testRoute.Seller,
                SerializeJsonObject(sellerCreateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void CreateSeller_WithUpperLimit_Except400()
        {
            var sellerCreateDTO = new SellerCreateDTO
            {
                FirstName = new string('a', SellerConfiguration.FirstNameMax + 1),
                LastName = new string('a', SellerConfiguration.LastNameMax + 1),
                Address = new string('a', SellerConfiguration.AddressMax + 1),
                Phone = new string('a', SellerConfiguration.PhoneMax + 1),
                Email = new string('a', SellerConfiguration.EmailMax + 1),
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Seller,
                SerializeJsonObject(sellerCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.FirstName))
                , ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.LastName))
                , ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Address))
                , ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Phone))
                , ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Email))
                , ValidationMessageKey.TooLong);
        }

        [Fact]
        public async void CreateSeller_WithRequired_Except400()
        {
            var sellerCreateDTO = new SellerCreateDTO
            {
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Seller,
                SerializeJsonObject(sellerCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.FirstName))
                , ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.LastName))
                , ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Address))
                , ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Phone))
                , ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Email))
                , ValidationMessageKey.Required);
        }

        [Fact]
        public async void CreateSeller_WithBadFormat_Except400()
        {
            var sellerCreateDTO = new SellerCreateDTO
            {
                Email ="111111",
                Phone ="asdasdasd"
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Seller,
                SerializeJsonObject(sellerCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Phone))
                , ValidationMessageKey.InvalidPhoneNumberFormat);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Email))
                , ValidationMessageKey.InvalidEmailAdresssFormat);
        }

        [Fact]
        public async void CreateSeller_WithValid_ExceptCreated()
        {
            var sellerCreateDTO = new SellerCreateDTO
            {
                FirstName = "New Seller FN",
                LastName = "New Seller LN",
                Address = "New Seller Address",
                Phone = "+61.423.676.123",
                Email = "newseller@email.com"
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Seller,
                SerializeJsonObject(sellerCreateDTO));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var sellerDTO = await DeSerializeJsonObject<SellerDTO>(response);

            Assert.Equal(sellerCreateDTO.FirstName, sellerDTO.FirstName);
            Assert.Equal(sellerCreateDTO.LastName, sellerDTO.LastName);
            Assert.Equal(sellerCreateDTO.Address, sellerDTO.Address);
            Assert.Equal(sellerCreateDTO.Phone, sellerDTO.Phone);
            Assert.Equal(sellerCreateDTO.Email, sellerDTO.Email);
        }
    }
}
