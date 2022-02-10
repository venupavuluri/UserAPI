namespace UserAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        List<UserEntity> userEntities;

        public UserRepository()
        {
            userEntities = new List<UserEntity>();            
        }

        /// <summary>
        /// upsert user (Create/Update)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Guid CreateUser(UserEntity user)
        {
            //check item existance
            UserEntity? item = GetUser(user.Email);

            //add item to the list
            if (item == null)
            {
                userEntities.Add(user);
                return user.Id;
            }

            //update item
            UpdateUserItem(user, item);

            return item.Id;
        }
              
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public bool DeleteUser(string emailAddress)
        {
            UserEntity? item = GetUser(emailAddress);

            if (item != null)
                return userEntities.Remove(item);

            return false;
        }        

        /// <summary>
        /// Get User by email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public UserEntity? GetUserByEmail(string emailAddress)
        {
            UserEntity? item = GetUser(emailAddress);
            return item;            
        }

        #region HelperMethods
        private static void UpdateUserItem(UserEntity user, UserEntity item)
        {
            item.FName = user.FName;
            item.LName = user.LName;
            item.MName = user.MName;
            item.Phone = user.Phone;
        }

        private UserEntity? GetUser(string emailAddress)
        {
            return userEntities.FirstOrDefault(item => item.Email.Equals(emailAddress.ToLower().Trim(), StringComparison.OrdinalIgnoreCase));
        }
        #endregion
    }
}
