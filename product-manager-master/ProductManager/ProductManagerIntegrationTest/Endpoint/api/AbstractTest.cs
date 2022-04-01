using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProductManager;
using ProductManager.Helpers;
using ProductManager.Models;
using ProductManager.Validators;
using ProductManagerDTO.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Resources;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1
{
    public abstract class AbstractTest
    {
        private static CustomWebApplicationFactory<Startup> _factory;

        private CustomWebApplicationFactory<Startup> Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new();
                }

                return _factory;
            }
        }

        protected readonly TestRoute testRoute;

        public HttpClient Client { get => Factory.CreateClient(); }

        public ProductManagerDBContext DbContext
        {
            get => (ProductManagerDBContext)Factory
                    .Services.GetService(typeof(ProductManagerDBContext));
        }

        public UserManager<ApiUser> UserService
        {
            get => (UserManager<ApiUser>)Factory
                    .Services.GetService(typeof(UserManager<ApiUser>));
        }

        public IMapper Mapper
        {
            get => (IMapper)Factory
                    .Services.GetService(typeof(IMapper));
        }

        public HttpClient AuthorizedClient
        {
            get
            {
                var client = Factory.CreateClient();
                var claims = new[] { new Claim(ClaimTypes.Name, "Admin") }; // Todo harcoded Admin and Bearrer
                client.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", MockJwtTokens.GenerateJwtToken(claims));
                return client;
            }
        }

        public AbstractTest()
        {
            testRoute = new V1TestRoute();
        }

        public void EnsureContentTypeIsUtf8AppJson(HttpResponseMessage response)
        {
            Assert.Equal(MediaTypeNames.Application.Json,
          response.Content.Headers.ContentType.MediaType);

            Assert.Equal(Encoding.UTF8.WebName,
         response.Content.Headers.ContentType.CharSet);
        }

        public void EnsureErrorValidationCorrect(ErrorDTO dto, String key, ValidationMessageKey messageKey)
        {
            var resourceManager = new ResourceManager(typeof(ValidationResource));
            var str = resourceManager.GetString(messageKey.ToString());

            Assert.Equal(string.Format(str, key), dto.FirstNamedError(key));
        }

        public void EnsureErrorValidationContains(ErrorDTO dto, String key, ValidationMessageKey messageKey)
        {
            var resourceManager = new ResourceManager(typeof(ValidationResource));

            var str = resourceManager.GetString(messageKey.ToString());
            var message = (string.Format(str, key));

            if(dto.errors[key] == null){
                Assert.True(false, "Does not contains validation message= " +message);
                return;
            }

            JsonElement arr = (JsonElement)dto.errors[key];

            bool flag = false;
            foreach (var s in arr.EnumerateArray())
            {
                if (message == (s.ToString()))
                {
                    flag = true;
                }
            }
            if(!flag)
                Assert.True(false, "Does not contains validation message= " + message);
        }

        public async Task<T> DeSerializeJsonObject<T>(HttpResponseMessage response)
        {

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<T>(stream,
                new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true
                });
        }

        public ObjectContent<T> SerializeJsonObject<T>(T dto)
        {
            return new ObjectContent<T>(dto, new JsonMediaTypeFormatter());
        }
    }
}
