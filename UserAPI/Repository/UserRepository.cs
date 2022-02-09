namespace UserAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        public Guid CreateUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserByEmail(string emailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
