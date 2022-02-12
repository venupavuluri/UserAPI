using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Model;
using Xunit;

namespace UserAPI.Tests.Integration
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _client;

        public UserControllerTest(WebApplicationFactory<Program> application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task GetUserNonExistWithStatusCodeNoContentAsync()
        {
            var response = await _client.GetAsync("/api/user/emailnotexist@email.com");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);            
        }


        [Fact]
        public async Task CreateUserWith200StatusCodeAsync()
        {
            PostUserRequestModel createUserModel = new PostUserRequestModel 
            { 
                EmailAddress = "testmail.com", 
                FirstName = "John", 
                LastName="Doe", 
                PhoneNumber = "42512345678",
                MiddleName = "M"
            };

            // Convert to JSON
            var jsonString = JsonConvert.SerializeObject(createUserModel);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var stringUri = _client.BaseAddress + "api/user";
            var uri = new Uri(stringUri);
            var response = await _client.PostAsync(uri, httpContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}