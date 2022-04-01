using ProductManager.DTO;
using ProductManagerDTO.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.product
{
    [Collection("TestCollection1 #1")]
    public class ProductListTest : AbstractTest
    {
        [Fact]
        public async void GetAllProducts_WithNoParameter_Except20Products()
        {
            var response = await Client.GetAsync(testRoute.Product);
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var productPagedDTO = await DeSerializeJsonObject<ProductPagedDTO>(response);

            var products = productPagedDTO.Products;

            var dbProducts = DbContext.Products.ToList();

            Assert.NotEmpty(dbProducts);

            Assert.Equal(20, products.Count);

            for (int i = 0; i < 10; i++)
            {
                var dbProduct = dbProducts[i];
                var product = products[i];

                Assert.Equal(dbProduct.Id, product.Id);
                Assert.Equal(dbProduct.Name, product.Name);         
            }
        }

        [Fact]
        public async void GetAllProducts_WithPageTwo_Except20Products()
        {
            var response = await Client.GetAsync(testRoute.Paged(testRoute.Product,2));
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var productPagedDTO = await DeSerializeJsonObject<ProductPagedDTO>(response);

            var products = productPagedDTO.Products;

            var dbProducts = DbContext.Products.
                                Skip(PagingRequestParams.DEFAULT_PAGE_SIZE).
                                Take(PagingRequestParams.DEFAULT_PAGE_SIZE).
                                ToList();

            Assert.NotEmpty(dbProducts);

            Assert.Equal(20, products.Count);

            for (int i = 0; i < dbProducts.Count; i++)
            {
                var dbProduct = dbProducts[i];
                var product = products[i];

                Assert.Equal(dbProduct.Id, product.Id);
                Assert.Equal(dbProduct.Name, product.Name);
            }
        }

        [Fact]
        public async void GetAllProducts_WithPageTwoPageSzie_Except50Products()
        {
            var response = await Client.GetAsync(testRoute.Paged(testRoute.Product, 2,50));
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var productPagedDTO = await DeSerializeJsonObject<ProductPagedDTO>(response);

            var products = productPagedDTO.Products;

            var dbProducts = DbContext.Products.
                                Skip(50).
                                Take(50).
                                ToList();

            Assert.NotEmpty(dbProducts);

            Assert.Equal(dbProducts.Count, products.Count);

            for (int i = 0; i < dbProducts.Count; i++)
            {
                var dbProduct = dbProducts[i];
                var product = products[i];

                Assert.Equal(dbProduct.Id, product.Id);
                Assert.Equal(dbProduct.Name, product.Name);
            }
        }

        [Fact]
        public async void GetOneProduct_WithId1_ExceptOneProduct()
        {
            var product = DbContext.Products.First();
            Assert.NotNull(product);

            var response = await Client.GetAsync(testRoute.Product + "/" + product.Id);

            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var productDTO = await DeSerializeJsonObject<ProductDTO>(response);

            Assert.Equal(product.Id, productDTO.Id);
            Assert.Equal(product.Name,productDTO.Name);
            Assert.Equal(product.ImageUrl,productDTO.ImageUrl);
            Assert.NotNull(productDTO.Category);
            Assert.Equal(product.CategoryId, productDTO.Category.Id);
        }
    }
}
