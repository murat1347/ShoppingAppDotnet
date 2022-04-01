using Microsoft.EntityFrameworkCore;
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
    public class SellerUpdateTest : AbstractTest
    {
        [Fact]
        public async void UpdateSeller_WithUnAuthorized_Except401()
        {
            var sellerCreateDTO = new SellerUpdateDTO();

            var response = await Client.PutAsync(testRoute.Single(testRoute.Seller,1),
                SerializeJsonObject(sellerCreateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void UpdateSeller_WithNonExisted_Except401()
        {
            var sellerCreateDTO = new SellerUpdateDTO();

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Seller, 999999),
                SerializeJsonObject(sellerCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void UpdateSeller_WithUpperLimit_Except400()
        {
            var seller = DbContext.Sellers.First();

            var sellerCreateDTO = new SellerUpdateDTO
            {
                FirstName = new string('a', SellerConfiguration.FirstNameMax + 1),
                LastName = new string('a', SellerConfiguration.LastNameMax + 1),
                Address = new string('a', SellerConfiguration.AddressMax + 1),
                Phone = new string('a', SellerConfiguration.PhoneMax + 1),
                Email = new string('a', SellerConfiguration.EmailMax + 1),
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Seller, seller.Id),
                SerializeJsonObject(sellerCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.FirstName))
                ,ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.LastName))
                ,ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Address))
                ,ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Phone))
                ,ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Email))
                ,ValidationMessageKey.TooLong);
        }

        [Fact]
        public async void UpdateSeller_WithRequired_Except400()
        {
            var seller = DbContext.Sellers.First();

            var sellerUpdateDTO = new SellerCreateDTO
            {
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Seller, seller.Id),
                SerializeJsonObject(sellerUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.FirstName))
                ,ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.LastName))
                ,ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Address))
                ,ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Phone))
                ,ValidationMessageKey.Required);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Email))
                ,ValidationMessageKey.Required);
        }

        [Fact]
        public async void UpdateSeller_WithBadFormat_Except400()
        {
            var seller = DbContext.Sellers.First();

            var sellerUpdateDTO = new SellerUpdateDTO
            {
                FirstName = seller.FirstName,
                LastName = seller.LastName,
                Address = seller.Address,
                Email = "111111",
                Phone = "asdasdasd"
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Seller, seller.Id),
                SerializeJsonObject(sellerUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Phone))
                ,ValidationMessageKey.InvalidPhoneNumberFormat);

            EnsureErrorValidationCorrect(errorDTO, (nameof(SellerCreateDTO.Email))
                ,ValidationMessageKey.InvalidEmailAdresssFormat);
        }

        [Fact]
        public async void UpdateSeller_WithValid_ExceptUpdated()
        {
            var seller = DbContext.Sellers.First();

            DbContext.Entry(seller).State = EntityState.Detached;

            var sellerUpdateDTO = new SellerCreateDTO
            {
                FirstName = "New Seller FN",
                LastName = "New Seller LN",
                Address = "New Seller Address",
                Phone = "+61.423.676.123",
                Email = "newseller@email.com"
            };

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Seller, seller.Id),
                SerializeJsonObject(sellerUpdateDTO));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updatedSeller = DbContext.Sellers.Find(seller.Id);

            Assert.Equal(sellerUpdateDTO.FirstName, updatedSeller.FirstName);
            Assert.Equal(sellerUpdateDTO.LastName, updatedSeller.LastName);
            Assert.Equal(sellerUpdateDTO.Address, updatedSeller.Address);
            Assert.Equal(sellerUpdateDTO.Phone, updatedSeller.Phone);
            Assert.Equal(sellerUpdateDTO.Email, updatedSeller.Email);
        }
    }
}
