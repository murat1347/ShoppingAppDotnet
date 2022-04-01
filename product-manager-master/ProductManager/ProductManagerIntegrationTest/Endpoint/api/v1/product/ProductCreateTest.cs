using ProductManager.DTO;
using ProductManager.ModelConfiguration;
using ProductManager.Validators;
using ProductManagerDTO.DTO.Product;
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
    public class ProductCreateTest : AbstractTest
    {
        [Fact]
        public async void CreateProduct_WithUnAuthorized_Except401()
        {
            var productCreateDTO = new ProductCreateDTO();

            var response = await Client.PostAsync(testRoute.Product,
                SerializeJsonObject(productCreateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void CreateProduct_WithUpperLimit_Except400()
        {
            var category = DbContext.Categories.First();
            Assert.NotNull(category);

            var productCreateDTO = new ProductCreateDTO()
            {
                Name = new String('a', ProductConfiguration.NameMaxLength + 1),
                ImageUrl = new String('a', ProductConfiguration.ImageUrlMaxLength + 1),
                CategoryId = category.Id
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Product,
                SerializeJsonObject(productCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.Name))
                ,ValidationMessageKey.TooLong);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.ImageUrl))
                ,ValidationMessageKey.TooLong);
        }

        [Fact]
        public async void CreateProduct_WithRequired_Except400()
        {
            var category = DbContext.Categories.First();
            Assert.NotNull(category);

            var productCreateDTO = new ProductCreateDTO();

            var response = await AuthorizedClient.PostAsync(testRoute.Product,
                SerializeJsonObject(productCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationContains(errorDTO, (nameof(ProductCreateDTO.Name))
             ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(ProductCreateDTO.ImageUrl))
             ,ValidationMessageKey.Required);

            EnsureErrorValidationContains(errorDTO, (nameof(ProductCreateDTO.CategoryId))
             ,ValidationMessageKey.Required);
        }

        [Fact]
        public async void CreateProduct_WithNotExistCategoryId_Except400()
        {
            var productCreateDTO = new ProductCreateDTO()
            {
                Name = "New Product",
                ImageUrl = "image.jpg",
                CategoryId = 10000,
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Product,
                SerializeJsonObject(productCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.CategoryId))
                ,ValidationMessageKey.NotExists);
        }

        [Fact]
        public async void CreateProduct_WithValidData_ExceptCreateResult()
        {
            var category = DbContext.Categories.First();
            Assert.NotNull(category);

            var productCreateDTO = new ProductCreateDTO()
            {
                Name = "New Product",
                ImageUrl = "image.jpg",
                CategoryId = category.Id,
            };

            var response = await AuthorizedClient.PostAsync(testRoute.Product,
                SerializeJsonObject(productCreateDTO));

            response.EnsureSuccessStatusCode();

            var productDTO = await DeSerializeJsonObject<ProductSingleResultDTO>(response);

            Assert.Equal(productCreateDTO.Name, productDTO.Name);
            Assert.Equal(productCreateDTO.ImageUrl, productDTO.ImageUrl);
            Assert.Equal(productCreateDTO.CategoryId, productDTO.CategoryId);
        }
    }
}
