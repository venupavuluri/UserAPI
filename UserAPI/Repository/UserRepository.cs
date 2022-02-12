using Cassandra.Mapping;

namespace UserAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        List<UserEntity> userEntities;                
        readonly IServiceProvider _serviceProvider;
        public UserRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            userEntities = new List<UserEntity>();            
        }

        /// <summary>
        /// Create user record, if user exist throw user already exist
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Guid> CreateUser(UserEntity user)
        {
            var sessionFacade = _serviceProvider?.GetRequiredService<ICassandraService>();
            var mapper = new Mapper(sessionFacade.GetSession());            
            var appliedInfo = await mapper.InsertIfNotExistsAsync(user);
            return appliedInfo.Applied ? user.Id : throw new Exception("UserAlreadyExist");
        }
              
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string emailAddress)
        {
            var sessionFacade = _serviceProvider?.GetRequiredService<ICassandraService>();
            var mapper = new Mapper(sessionFacade.GetSession());
            var cql = @"delete from users where email = ?";
            var appliedInfo = await mapper.DeleteIfAsync<UserEntity>(cql, emailAddress);
            return appliedInfo.Applied ? true : throw new Exception("UserNotExist");
        }        

        /// <summary>
        /// Get User by email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> GetUserByEmail(string emailAddress)
        {
            return await GetUser(emailAddress);            
        }

        #region HelperMethods
        private static void UpdateUserItem(UserEntity user, UserEntity item)
        {
            item.FName = user.FName;
            item.LName = user.LName;
            item.MName = user.MName;
            item.Phone = user.Phone;
        }

        private async Task<IEnumerable<UserEntity>> GetUser(string emailAddress)
        {
            var sessionFacade = _serviceProvider?.GetRequiredService<ICassandraService>();
            var mapper = new Mapper(sessionFacade.GetSession());
            var cql = @"select * from users where email = ?;";
            return await mapper.FetchAsync<UserEntity>(cql,emailAddress);
        }
        #endregion
    }
}
