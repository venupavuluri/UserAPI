namespace UserAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        List<UserEntity> userEntities;

        public UserRepository()
        {
            userEntities = new List<UserEntity>();
            userEntities.Add(new UserEntity { FName = "Venu", LName = "Pavuluri", MName = "B", Email = "venu@mail.com", Id = Guid.NewGuid(), Phone = "4256158900" });
        }

        public Guid CreateUser(UserEntity user)
        {
            userEntities.Add(user);
            return user.Id;
        }

        public bool DeleteUser(string emailAddress)
        {
            UserEntity? item = GetUser(emailAddress);

            if (item != null)
                return userEntities.Remove(item);

            return false;
        }        

        public UserEntity? GetUserByEmail(string emailAddress)
        {
            UserEntity? item = GetUser(emailAddress);
            return item;            
        }

        #region HelperMethods
        private UserEntity? GetUser(string emailAddress)
        {
            return userEntities.FirstOrDefault(item => item.Email.Equals(emailAddress.ToLower().Trim(), StringComparison.OrdinalIgnoreCase));
        }
        #endregion
    }
}
