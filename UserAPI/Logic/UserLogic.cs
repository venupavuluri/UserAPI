﻿using AutoMapper;
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

        public Guid CreateUser(PostUserRequestModel user)
        {   
            return _userRepository.CreateUser(_mapper.Map<UserEntity>(user));
        }

        public bool DeleteUser(string emailAddress)
        {
            return _userRepository.DeleteUser(emailAddress);
        }

        public GetUserResponseModel GetUserByEmail(string emailAddress)
        {
            var result = _userRepository.GetUserByEmail(emailAddress);
            return _mapper.Map<GetUserResponseModel>(result);
        }
    }
}
