using UserAPI.Model;
using UserAPI.Repository;

namespace UserAPI.Logic
{
    public interface IUserLogic
    {
        public GetUserResponseModel GetUserByEmail(string emailAddress);
        public Task<Guid> CreateUser(PostUserRequestModel user);
        public Task<bool> DeleteUser(string emailAddress);
    }
}
