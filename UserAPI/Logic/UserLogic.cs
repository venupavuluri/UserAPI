using UserAPI.Model;

namespace UserAPI.Logic
{
    public class UserLogic : IUserLogic
    {
        public Guid CreateUser(PostUserRequestModel user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public GetUserResponseModel GetUserByEmail(string emailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
