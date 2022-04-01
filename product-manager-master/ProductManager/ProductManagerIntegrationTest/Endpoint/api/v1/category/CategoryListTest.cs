using Microsoft.AspNetCore.Mvc.Testing;
using ProductManager;
using ProductManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.category
{
    [Collection("TestCollection1 #1")]
    public class CategoryListTest : AbstractTest
    {
        [Fact]
        public async void GetAllCategories_WithNoParameter_ExceptAllCategories()
        {

            var response = await Client.GetAsync(testRoute.Category);
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var categories = await DeSerializeJsonObject<List<CategoryDTO>>(response);
            
            var dbCategories = DbContext.Categories.ToList();

            Assert.NotEmpty(dbCategories);

            Assert.Equal(dbCategories.Count, categories.Count);

            for (int i = 0; i < dbCategories.Count; i++)
            {
                var cat = dbCategories[i];
                var catDTO = categories[i];

                Assert.Equal(cat.Id, catDTO.Id);
                Assert.Equal(cat.Name, catDTO.Name);

                if (cat.Parent != null)
                {
                    Assert.Equal(cat.Parent.Id, catDTO.ParentId);
                }
            }
        }

        [Fact]
        public async void GetOneCategory_WithId1_ExceptOneCategory()
        {

            var cat = DbContext.Categories.First();

            var response = await Client.GetAsync(testRoute.Category + "/" + cat.Id);
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var catDTO = await DeSerializeJsonObject<CategoryDTO>(response);

            Assert.Equal(cat.Id, catDTO.Id);
            Assert.Equal(cat.Name, catDTO.Name);
            if (cat.Parent != null)
            {
                Assert.Equal(cat.Parent.Id, catDTO.ParentId);
            }
        }
    }
}
