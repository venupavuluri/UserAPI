using UserAPI.Model;

namespace UserAPI.Logic
{
    public interface IUserLogic
    {
        public GetUserResponseModel GetUserByEmail(string emailAddress);
        public Guid CreateUser(PostUserRequestModel user);
        public bool DeleteUser(string emailAddress);
    }
}
