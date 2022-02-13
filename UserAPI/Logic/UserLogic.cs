using AutoMapper;
using UserAPI.Model;
using UserAPI.Repository;

namespace UserAPI.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public UserLogic(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userRepository =  _serviceProvider.GetService<IUserRepository>();
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<Guid> CreateUser(PostUserRequestModel user)
        {   
            return await _userRepository.CreateUser(_mapper.Map<UserEntity>(user));
        }

        public async Task<bool> DeleteUser(string emailAddress)
        {
            return await _userRepository.DeleteUser(emailAddress);
        }

        public async Task<GetUserResponseModel> GetUserByEmail(string emailAddress)
        {
            var result = await _userRepository.GetUserByEmail(emailAddress);
            return _mapper.Map<GetUserResponseModel>(result);
        }

        public async Task<bool> UpdateUser(PostUserRequestModel user)
        {
            return await _userRepository.UpdateUser(_mapper.Map<UserEntity>(user));
        }
    }
}
