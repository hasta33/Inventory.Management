using Autofac;
using Autofac.Extensions.DependencyInjection;
using InventoryManagement.API.Filters;
using InventoryManagement.API.MiddlewaresExtension;
using InventoryManagement.API.Modules;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using InventoryManagement.Repository;
using InventoryManagement.Repository.Repositories;
using InventoryManagement.Repository.UnitOfWork;
using InventoryManagement.Services;
using InventoryManagement.Services.Mapping;
using InventoryManagement.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateFilterAttribute()); //options.Filters.Add(new AuthorizeFilter()); //tum kontrollerde authorize attribute etkinleştirilmis olacak
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull; //db'de null olan değerleri getirme
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();

//Custom FluentValidation
builder.Services.AddCustomApplicationServices();



#region Swagger
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventoryManagement.API", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
//        {
//            new OpenApiSecurityScheme {
//                Reference = new OpenApiReference {
//                    Type = ReferenceType.SecurityScheme,
//                        Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventoryManagement.API", Version = "v1" });

//    //First we define the security scheme
//    c.AddSecurityDefinition("Bearer", //Name the security scheme
//        new OpenApiSecurityScheme
//        {
//            Description = "JWT Authorization header using the Bearer scheme.",
//            Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
//            Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
//        });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
//                    {
//                        new OpenApiSecurityScheme{
//                            Reference = new OpenApiReference{
//                                Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
//                                Type = ReferenceType.SecurityScheme
//                            }
//                        },new List<string>()
//                    }
//                });
//});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IventoryManagement.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.

                }
            },
            new string[] {}
        }
    });
});
#endregion






#region JWT

#endregion




#region Dependencies
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
//var config = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile<AutoMapperProfile>();
//});
//var mapper = config.CreateMapper();
//builder.Services.AddSingleton(mapper);
#endregion



#region Sql Server Connection
builder.Services.AddDbContext<DataContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("IMDbConnection"), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(DataContext)).GetName().Name);
    });
    //x.UseSqlServer(builder.Configuration.GetConnectionString("IMDbConnection"));
});
#endregion

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepositoryServiceModule()));





var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();




app.MapControllers().RequireAuthorization();

//Custom exceptions
app.UseCustomException();

app.Run();
