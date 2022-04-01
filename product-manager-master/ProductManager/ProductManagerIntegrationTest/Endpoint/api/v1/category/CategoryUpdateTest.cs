using Microsoft.EntityFrameworkCore;
using ProductManager.DTO;
using ProductManager.DTO.Category;
using ProductManager.ModelConfiguration;
using ProductManager.Validators;
using ProductManagerDTO.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.category
{
    [Collection("TestCollection1 #1")]
    public class CategoryUpdateTest : AbstractTest
    {
        [Fact]
        public async void UpdateCategory_WithUnAuthorized_Except401()
        {
            var category = DbContext.Categories.First();

            var categoryUpdateDTO = new CategoryUpdateDTO();
            categoryUpdateDTO.Name = "Updated Name";
            categoryUpdateDTO.ParentId = category.ParentId;

            var response = await Client.PutAsync(testRoute.Single(testRoute.Category, category.Id),
                SerializeJsonObject(categoryUpdateDTO));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void UpdateCategory_WithNoName_Except400()
        {
            var category = DbContext.Categories.First();

            var categoryUpdateDTO = new CategoryUpdateDTO();
            categoryUpdateDTO.ParentId = category.ParentId;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Category, category.Id),
                SerializeJsonObject(categoryUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CategoryCreateDTO.Name))
                , ValidationMessageKey.Required);
        }

        [Fact]
        public async void UpdateCategory_WithLongName_Except400()
        {
            var category = DbContext.Categories.First();

            var categoryUpdateDTO = new CategoryUpdateDTO();
            categoryUpdateDTO.Name = new String('a', CategoryConfiguration.NameMaxLength + 1);
            categoryUpdateDTO.ParentId = category.ParentId;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Category, category.Id),
                SerializeJsonObject(categoryUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CategoryUpdateDTO.Name))
                , ValidationMessageKey.TooLong);
        }

        [Fact]
        public async void UpdateCategory_WithNoParent_Except400()
        {
            var category = DbContext.Categories.First();

            var categoryUpdateDTO = new CategoryUpdateDTO();
            categoryUpdateDTO.Name = category.Name;
            categoryUpdateDTO.ParentId = null;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Category, category.Id),
                SerializeJsonObject(categoryUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CategoryCreateDTO.ParentId))
                , ValidationMessageKey.Required);
        }

        [Fact]
        public async void UpdateCategory_WithNotExistedParentId_Except400()
        {
            var category = DbContext.Categories.First();

            var categoryUpdateDTO = new CategoryUpdateDTO();
            categoryUpdateDTO.Name = category.Name;
            categoryUpdateDTO.ParentId = 999999;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Category, category.Id),
                SerializeJsonObject(categoryUpdateDTO));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorDTO = await DeSerializeJsonObject<ErrorDTO>(response);

            Assert.NotEmpty(errorDTO.errors);

            EnsureErrorValidationCorrect(errorDTO, (nameof(CategoryCreateDTO.ParentId))
                , ValidationMessageKey.NotExists);
        }

        [Fact]
        public async void UpdateCategory_WithValidData_ExceptUpdatedCategory()
        {
            var category = DbContext.Categories.First();
            var newParent = DbContext.Categories.OrderBy(c=>c.Name).First();

            DbContext.Entry(category).State = EntityState.Detached;

            Assert.NotEqual(category.ParentId, newParent.Id);

            var categoryUpdateDTO = new CategoryUpdateDTO();
            categoryUpdateDTO.Name = "Updated Category Name";
            categoryUpdateDTO.ParentId = newParent.Id;

            var response = await AuthorizedClient.PutAsync(testRoute.Single(testRoute.Category, category.Id),
                SerializeJsonObject(categoryUpdateDTO));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updatedCategory = DbContext.Categories.Where(c=>c.Id == category.Id).First();

            Assert.Equal(updatedCategory.Name, categoryUpdateDTO.Name);
            Assert.Equal(updatedCategory.ParentId, categoryUpdateDTO.ParentId);
        }

    }
}
