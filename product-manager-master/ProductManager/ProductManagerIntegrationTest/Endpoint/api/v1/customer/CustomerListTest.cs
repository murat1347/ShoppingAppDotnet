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

namespace ProductManagerIntegrationTest.Endpoint.api.v1.seller
{
    [Collection("TestCollection1 #1")]
    public class SellerListTest : AbstractTest
    {
        [Fact]
        public async void GetAllSeller_WithUnAuthorized_Except401()
        {
            var response = await Client.GetAsync(testRoute.Seller);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void GetOneSeller_WithUnAuthorized_Except401()
        {
            var response = await Client.GetAsync(testRoute.Single(testRoute.Seller,1));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void GetAllSellers_WithNoParameter_ExceptAllCategories()
        {
            var response = await AuthorizedClient.GetAsync(testRoute.Seller);
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);


            var sellerPagedDTO = await DeSerializeJsonObject<SellerPagedDTO>(response);

            var sellers = sellerPagedDTO.Sellers;

            var dbSellers = DbContext.Sellers.ToList();

            Assert.NotEmpty(dbSellers);

            Assert.Equal(20, sellers.Count);

            for (int i = 0; i <20; i++)
            {
                var dbSeller = dbSellers[i];
                var seller = sellers[i];

                Assert.Equal(dbSeller.Id, seller.Id);
                Assert.Equal(dbSeller.Address, seller.Address);
                Assert.Equal(dbSeller.FirstName, seller.FirstName);
                Assert.Equal(dbSeller.LastName, seller.LastName);
                Assert.Equal(dbSeller.Phone, seller.Phone);
                Assert.Equal(dbSeller.Email, seller.Email);
            }
        }

        [Fact]
        public async void GetOneSeller_WithId1_ExceptOneProduct()
        {
            var seller = DbContext.Sellers.First();
            Assert.NotNull(seller);

            var response = await AuthorizedClient.GetAsync(testRoute.Single(testRoute.Seller, seller.Id));

            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var sellerDTO = await DeSerializeJsonObject<SellerDTO>(response);

            Assert.Equal(seller.Id, sellerDTO.Id);
            Assert.Equal(seller.FirstName, sellerDTO.FirstName);
            Assert.Equal(seller.LastName, sellerDTO.LastName);
            Assert.Equal(seller.Address, sellerDTO.Address);
            Assert.Equal(seller.Email, sellerDTO.Email);
            Assert.Equal(seller.Phone, sellerDTO.Phone);
        }
    }
}
