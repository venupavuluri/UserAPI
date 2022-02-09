using AutoMapper;
using UserAPI.Logic;
using UserAPI.Repository;

namespace UserAPI
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddTransient<IUserLogic, UserLogic>();

            //Mapping Logic
            var mapperConfig = new MapperConfiguration(cfg => {              
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
