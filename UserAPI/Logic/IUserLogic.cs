using UserAPI.Model;
using UserAPI.Repository;

namespace UserAPI.Logic
{
    public interface IUserLogic
    {
        public GetUserResponseModel GetUserByEmail(string emailAddress);
        public Guid CreateUser(PostUserRequestModel user);
        public bool DeleteUser(string emailAddress);
    }
}
