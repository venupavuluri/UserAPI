using AutoMapper;
using FluentValidation.AspNetCore;
using System.Reflection;
using UserAPI.Logic;
using UserAPI.Repository;

namespace UserAPI
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {   
            //Mapping Logic
            var mapperConfig = new MapperConfiguration(cfg => {       
                
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddTransient<IUserLogic, UserLogic>();

            return services;
        }
    }
}
