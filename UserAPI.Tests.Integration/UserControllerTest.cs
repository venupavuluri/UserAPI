using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Model;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace UserAPI.Tests.Integration
{   
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [Collection("Sequential")]
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _client;                        
        string newUserEmail = "user@mail.com";
        string invalidUserEmail = "IAmNotAnEmailAddress";
        string nonExistUserEmail = "non-exist-user@mail.com";        
        private readonly ITestOutputHelper _output;
        
        public UserControllerTest(WebApplicationFactory<Program> application, ITestOutputHelper testOutputHelper)
        {
            _client = application.CreateClient();            
            _output = testOutputHelper;
        }

        [Fact(DisplayName = "CreateNewUser"), Priority(1)]
        public async Task CreateUserWith200StatusCodeAsync()
        {
            PostUserRequestModel createUserModel = new PostUserRequestModel
            {
                EmailAddress = newUserEmail,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "4251234567",
                MiddleName = "M"
            };

            // Convert to JSON
            var jsonString = JsonConvert.SerializeObject(createUserModel);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var stringUri = _client.BaseAddress + "api/user";
            var uri = new Uri(stringUri);
            var response = await _client.PostAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            _output.WriteLine("Create user running...");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "CreateDuplicateUser409Conflict"), Priority(2)]
        public async Task CreateDuplicateUserWith409ConflictStatusCodeAsync()
        {
            PostUserRequestModel createUserModel = new PostUserRequestModel
            {
                EmailAddress = newUserEmail,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "4251234567",
                MiddleName = "M"
            };

            // Convert to JSON
            var jsonString = JsonConvert.SerializeObject(createUserModel);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var stringUri = _client.BaseAddress + "api/user";
            var uri = new Uri(stringUri);
            var response = await _client.PostAsync(uri, httpContent);
            _output.WriteLine("CreateDuplicateUser running...");
            //var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact(DisplayName = "CreateUser400BadRequest"), Priority(3)]
        public async Task CreateUserWith400BadRequestStatusCodeAsync()
        {
            PostUserRequestModel createUserModel = new PostUserRequestModel
            {
                EmailAddress = invalidUserEmail,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "4251234567",
                MiddleName = "M"
            };

            // Convert to JSON
            var jsonString = JsonConvert.SerializeObject(createUserModel);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var stringUri = _client.BaseAddress + "api/user";
            var uri = new Uri(stringUri);
            var response = await _client.PostAsync(uri, httpContent);            
            //var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "GetNonExistingUserWith204NoContent"), Priority(3)]
        public async Task GetUserNonExistWithStatusCodeNoContentAsync()
        {   
            var response = await _client.GetAsync($"/api/user/{nonExistUserEmail}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);            
        }        

        [Fact(DisplayName ="GetExistingUser200Ok"), Priority(4)]
        public async Task GetExistingUserWith200StatusCodeAsync()
        {         
            var response = await _client.GetAsync($"/api/user/{newUserEmail}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "UpdateUser"), Priority(5)]
        public async Task UpdateUserWith200StatusCodeAsync()
        {
            PostUserRequestModel createUserModel = new PostUserRequestModel
            {
                EmailAddress = newUserEmail,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "3251234567",
                MiddleName = "M"
            };
                        
            // Convert to JSON
            var jsonString = JsonConvert.SerializeObject(createUserModel);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var stringUri = _client.BaseAddress + "api/user";
            var uri = new Uri(stringUri);
            var response = await _client.PutAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName ="DeleteUser"), Priority(6)]
        public async Task DeleteExistingUserWith200StatusCodeAsync()
        {
            var response = await _client.DeleteAsync($"/api/user/{newUserEmail}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}