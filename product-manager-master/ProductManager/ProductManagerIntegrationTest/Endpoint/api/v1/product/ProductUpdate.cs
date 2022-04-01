using Microsoft.EntityFrameworkCore;
using ProductManager.DTO;
using ProductManager.DTO.Product;
using ProductManager.ModelConfiguration;
using ProductManager.Validators;
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
    public class ProductUpdate : AbstractTest
    {
        [Fact]
        public async Task UpdateProduct_WithUnAuthorized_Except401()
        {
            var productUpdateDTO = new ProductUpdateDTO();

            var response = await Client.PutAsync(testRoute.Single(testRoute.Product,1),
                SerializeJsonObject(productUpdateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_WithNoName_Except400()
        {
            var product = DbContext.Products.First();
           
            var productUpdateDTO = Mapper.Map<ProductUpdateDTO>(product);

            productUpdateDTO.Name = null;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Product, product.Id),
                SerializeJsonObject(productUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.Name))
                , ValidationMessageKey.Required);
        }

        [Fact]
        public async Task UpdateProduct_WithLongName_Except400()
        {
            var product = DbContext.Products.First();

            var productUpdateDTO = Mapper.Map<ProductUpdateDTO>(product);

            productUpdateDTO.Name = new String('a', ProductConfiguration.NameMaxLength + 1);

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Product, product.Id),
                SerializeJsonObject(productUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.Name))
                , ValidationMessageKey.TooLong);
        }

        [Fact]
        public async Task UpdateProduct_WithNoImageUrl_Except400()
        {
            var product = DbContext.Products.First();

            var productUpdateDTO = Mapper.Map<ProductUpdateDTO>(product);

            productUpdateDTO.ImageUrl = null;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Product, product.Id),
                SerializeJsonObject(productUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.ImageUrl))
                , ValidationMessageKey.Required);
        }

        [Fact]
        public async Task UpdateProduct_WithLongImageUrl_Except400()
        {
            var product = DbContext.Products.First();

            var productUpdateDTO = Mapper.Map<ProductUpdateDTO>(product);

            productUpdateDTO.ImageUrl = new String('a', ProductConfiguration.ImageUrlMaxLength + 1);

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Product, product.Id),
                SerializeJsonObject(productUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.ImageUrl))
                , ValidationMessageKey.TooLong);
        }

        [Fact]
        public async Task UpdateProduct_WithNoCategoryId_Except400()
        {
            var product = DbContext.Products.First();

            var productUpdateDTO = Mapper.Map<ProductUpdateDTO>(product);

            productUpdateDTO.CategoryId = null;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Product, product.Id),
                SerializeJsonObject(productUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.CategoryId))
                , ValidationMessageKey.Required);
        }

        [Fact]
        public async Task UpdateProduct_WithNotExistCategoryId_Except400()
        {
            var product = DbContext.Products.First();

            var productUpdateDTO = Mapper.Map<ProductUpdateDTO>(product);

            productUpdateDTO.CategoryId = 9999999;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Product, product.Id),
                SerializeJsonObject(productUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(ProductCreateDTO.CategoryId))
                , ValidationMessageKey.NotExists);
        }

        [Fact]
        public async Task UpdateProduct_WithValidData_ExceptUpdated()
        {
            var product = DbContext.Products.First();
            DbContext.Entry(product).State = EntityState.Detached;

            var category = DbContext.Categories.OrderBy(c => c.Name).First();

            Assert.NotEqual(product.CategoryId, category.Id);

            var productUpdateDTO = Mapper.Map<ProductUpdateDTO>(product);

            productUpdateDTO.Name = "Updated Name";
            productUpdateDTO.ImageUrl = "updated.jpg";
            productUpdateDTO.CategoryId = category.Id;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Product, product.Id),
                SerializeJsonObject(productUpdateDTO));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var dBProduct = DbContext.Products.Find(product.Id);

            Assert.Equal(dBProduct.Id, product.Id);
            Assert.Equal(dBProduct.Name, productUpdateDTO.Name);
            Assert.Equal(dBProduct.ImageUrl, productUpdateDTO.ImageUrl);
            Assert.NotEqual(dBProduct.CategoryId, product.CategoryId);
            Assert.Equal(dBProduct.CategoryId, productUpdateDTO.CategoryId);
        }
    }
}
