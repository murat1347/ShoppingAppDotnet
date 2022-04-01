using ProductManager.DTO;
using ProductManager.Models;
using ProductManagerDTO.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.account
{
    [Collection("TestCollection1 #1")]
    public class AccountTest : AbstractTest
    {
        private async Task<int> createTestUser(){

            ApiUser user = new ApiUser
            {
                UserName = "test2@test.com",
                Email = "test2@test.com"
            };

            var res = await UserService.CreateAsync(
              user
             , "Test$123456");

            return 0;
        }

        [Fact]
        public async void Login_WithEmailPassword_ExceptToken()
        {
            await createTestUser();

            var userLoginDTO = new LoginUserDTO{
                Email = "test2@test.com",
                Password = "Test$123456"
            };

            var response = await Client.PostAsync(testRoute.Account + "/login",
                SerializeJsonObject(userLoginDTO));

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }
    }
}
