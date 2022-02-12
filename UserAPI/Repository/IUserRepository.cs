namespace UserAPI.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserEntity>> GetUserByEmail(string emailAddress);
        public Task<Guid> CreateUser(UserEntity user);
        public Task<bool> DeleteUser(string emailAddress);
    }
}
