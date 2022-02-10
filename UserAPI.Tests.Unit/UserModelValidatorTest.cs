using UserAPI.Model;
using UserAPI.ModelValidator;
using Xunit;

namespace UserAPI.Tests.Unit
{
    public class UserModelValidatorTest
    {   
        private UserModelValidator validator { get; }
        public UserModelValidatorTest()
        {
            validator = new UserModelValidator();
        }

        [Fact]
        public void NotAllowInvalidEmail()
        {
            var mockPostUserRequestInvalidEmailModel = new PostUserRequestModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "4251230000",
                EmailAddress = "invalidEmail"
            };
            
            var result = validator.Validate(mockPostUserRequestInvalidEmailModel).IsValid;
            //Fluent validator should return false for invalid email address
            Assert.False(result);
        }
    }
}
