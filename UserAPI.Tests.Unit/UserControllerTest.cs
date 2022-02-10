using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using UserAPI.Controllers;
using UserAPI.Logic;
using UserAPI.Model;
using Xunit;

namespace UserAPI.Tests.Unit
{
    /// <summary>
    /// User Controller Unit test
    /// </summary>
    public class UserControllerTest
    {
        private readonly Mock<IUserLogic> _userLogicMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        ServiceCollection _serviceCollection;

        public UserControllerTest()
        {
            _serviceCollection = new ServiceCollection();
            _loggerMock = new Mock<ILogger<UserController>>();
            _userLogicMock = new Mock<IUserLogic>();
        }

        private void InitializeControllerMockData()
        {   
            var mockGetUserResponseModel = new GetUserResponseModel 
            { 
                Name = "FullName", 
                EmailAddress = "name@mail.com", 
                UserId = Guid.NewGuid().ToString() 
            };

            _userLogicMock.Setup(res => res.GetUserByEmail(It.IsAny<string>())).Returns(() => { return mockGetUserResponseModel; });
            _userLogicMock.Setup(res => res.CreateUser(It.IsAny<PostUserRequestModel>())).Returns(() => { return Guid.NewGuid(); });
            _serviceCollection.AddSingleton(_userLogicMock.Object);
        }

        [Fact]
        public void GetUserTest()
        {
            //initialize mock objects
            InitializeControllerMockData();

            var controller = new UserController(_serviceCollection.BuildServiceProvider(), _loggerMock.Object);
            var actual = controller.GetUser("john.doe@email.com");

            Assert.Equal(200, ((OkObjectResult)actual).StatusCode);

        }

        [Fact]
        public void CreateUserTest()
        {
            InitializeControllerMockData();
            
            var mockPostUserRequestModel = new PostUserRequestModel 
            { 
                FirstName = "John", 
                LastName = "Doe", 
                EmailAddress = "john.doe@email.com", 
                PhoneNumber = "4251230000" 
            };
            var controller = new UserController(_serviceCollection.BuildServiceProvider(), _loggerMock.Object);
            controller.ModelState.AddModelError("test", "test");

            var actual = controller.Post(mockPostUserRequestModel);

            Assert.Equal(200, ((OkObjectResult)actual).StatusCode);

        }
    }
}