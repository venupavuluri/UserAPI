using Cassandra.Mapping;

namespace UserAPI.Repository
{
    public class UserRepository : IUserRepository
    {   
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public UserRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            var sessionFacade = _serviceProvider?.GetRequiredService<ICassandraService>();
            _mapper = new Mapper(sessionFacade.GetSession());
        }

        /// <summary>
        /// Get User by email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<UserEntity> GetUserByEmail(string emailAddress)
        {   
            var cql = @"select * from users where email = ?;";
            return await _mapper.SingleOrDefaultAsync<UserEntity>(cql, emailAddress);
        }

        /// <summary>
        /// Create user record, if user exist throw user already exist
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Guid> CreateUser(UserEntity user)
        {   
            var appliedInfo = await _mapper.InsertIfNotExistsAsync(user);
            return appliedInfo.Applied ? user.Id : throw new Exception("UserAlreadyExist");
        }

        public async Task<bool> UpdateUser(UserEntity user)
        {   
            var cql = @"set first_name = ?, last_name = ?, middle_name = ?, phone_number = ? where email = ? IF EXISTS;";
            var appliedInfo = await _mapper.UpdateIfAsync<UserEntity>(Cql.New(cql, user.FName, user.LName, user.MName, user.Phone, user.Email));
            return appliedInfo.Applied ? true : throw new Exception("UpdateUserFailed");
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string emailAddress)
        {   
            var cql = @"where email = ? IF EXISTS;";
            var appliedInfo = await _mapper.DeleteIfAsync<UserEntity>(Cql.New(cql, emailAddress));
            return appliedInfo.Applied ? true : throw new Exception("UserNotExist");
        }
    }
}
