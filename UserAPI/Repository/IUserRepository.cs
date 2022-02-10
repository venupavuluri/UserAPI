namespace UserAPI.Repository
{
    public interface IUserRepository
    {
        public UserEntity? GetUserByEmail(string emailAddress);
        public Guid CreateUser(UserEntity user);
        public bool DeleteUser(string emailAddress);
    }
}
