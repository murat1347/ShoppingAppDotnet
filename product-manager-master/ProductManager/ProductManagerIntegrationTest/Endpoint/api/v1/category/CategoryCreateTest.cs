using ProductManager.DTO;
using ProductManager.ModelConfiguration;
using ProductManager.Validators;
using ProductManagerDTO.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.category
{
    [Collection("TestCollection1 #1")]
    public class CategoryCreateTest : AbstractTest
    {
        [Fact]
        public async void CreateCategory_WithUnAuthorized_Except401()
        {
            var categoryCreateDTO = new CategoryCreateDTO();
            categoryCreateDTO.Name = "dogukan";

            var response = await Client.PostAsync(testRoute.Category,
                SerializeJsonObject(categoryCreateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void CreateCategory_WithNoNameNoParent_Except400()
        {
            var categoryCreateDTO = new CategoryCreateDTO();

            var response = await AuthorizedClient.PostAsync(testRoute.Category,
                SerializeJsonObject(categoryCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CategoryCreateDTO.Name))
                , ValidationMessageKey.Required);
        }

        [Fact]
        public async void CreateCategory_WithLongNameNoParent_Except400()
        {
            var categoryCreateDTO = new CategoryCreateDTO();
            categoryCreateDTO.Name = "This is a very looooooooooooooooooon category name";

            Assert.True(categoryCreateDTO.Name.Length > CategoryConfiguration.NameMaxLength);

            var response = await AuthorizedClient.PostAsync(testRoute.Category,
                SerializeJsonObject(categoryCreateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CategoryCreateDTO.Name))
                , ValidationMessageKey.TooLong);
        }

        [Fact]
        public async void CreateCategory_WithValidNameNoParent_ExceptCreatedCategory()
        {
            var categoryCreateDTO = new CategoryCreateDTO();
            categoryCreateDTO.Name = "New Category Name";

            var response = await AuthorizedClient.PostAsync(testRoute.Category,
                SerializeJsonObject(categoryCreateDTO));

            response.EnsureSuccessStatusCode();
            EnsureContentTypeIsUtf8AppJson(response);

            var catDTO = await DeSerializeJsonObject<CategoryDTO>(response);

            Assert.Equal(categoryCreateDTO.Name, catDTO.Name);
            if (categoryCreateDTO.ParentId != null)
            {
                Assert.Equal(categoryCreateDTO.ParentId, catDTO.ParentId);
            }
        }

        [Fact]
        public async void CreateCategory_WithValidNameParent_ExceptCreatedCategory()
        {
            var parent = DbContext.Categories.First();

            Assert.NotNull(parent);

            var categoryCreateDTO = new CategoryCreateDTO();
            categoryCreateDTO.Name = "New Category Name";
            categoryCreateDTO.ParentId = parent.Id;

            var response = await AuthorizedClient.PostAsync(testRoute.Category,
                SerializeJsonObject(categoryCreateDTO));

            response.EnsureSuccessStatusCode();
            EnsureContentTypeIsUtf8AppJson(response);

            var catDTO = await DeSerializeJsonObject<CategoryDTO>(response);

            Assert.Equal(categoryCreateDTO.Name, catDTO.Name);
            if (categoryCreateDTO.ParentId != null)
            {
                Assert.Equal(categoryCreateDTO.ParentId, catDTO.ParentId);
            }
        }
    }
}
