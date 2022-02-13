namespace UserAPI.Repository
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserByEmail(string emailAddress);
        public Task<Guid> CreateUser(UserEntity user);
        public Task<bool> UpdateUser(UserEntity user);
        public Task<bool> DeleteUser(string emailAddress);
    }
}
