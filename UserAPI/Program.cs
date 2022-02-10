
using AutoMapper;
using FluentValidation.AspNetCore;
using UserAPI.Filters;
using UserAPI.Logic;
using UserAPI.Model;
using UserAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserLogic, UserLogic>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

//auto mapper initialization
var mapperConfig = new MapperConfiguration(cfg => {
    cfg.CreateMap<UserEntity, GetUserResponseModel>()
      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.MName) ?
                                                                  string.Join(" ", src.FName, src.LName) : string.Join(" ", src.FName, src.MName, src.LName)))
      .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email))
      .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));

    cfg.CreateMap<PostUserRequestModel, UserEntity>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
        .ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.MName, opt => opt.MapFrom(src => src.LastName))
        .ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.MiddleName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress.ToLower().Trim()))
        .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddMvc(opt =>
{
    opt.Filters.Add(typeof(ValidatorActionFilter));
}).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Program>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
