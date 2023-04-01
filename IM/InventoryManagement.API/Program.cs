using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityModel.Client;
using InventoryManagement.API.Authentication;
using InventoryManagement.API.Authorization.Decision;
using InventoryManagement.API.Authorization.RPT;
using InventoryManagement.API.Filters;
using InventoryManagement.API.MiddlewaresExtension;
using InventoryManagement.API.Modules;
using InventoryManagement.API.Services;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using InventoryManagement.Repository;
using InventoryManagement.Repository.Repositories;
using InventoryManagement.Repository.UnitOfWork;
using InventoryManagement.Services;
using InventoryManagement.Services.Mapping;
using InventoryManagement.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventoryManagement.API", Version = "v1" });

    ////First we define the security scheme
    //c.AddSecurityDefinition("Bearer", //Name the security scheme
    //    new OpenApiSecurityScheme
    //    {
    //        Description = "JWT Authorization header using the Bearer scheme.",
    //        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
    //        Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
    //    });

    //c.AddSecurityRequirement(new OpenApiSecurityRequirement{
    //                {
    //                    new OpenApiSecurityScheme{
    //                        Reference = new OpenApiReference{
    //                            Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
    //                            Type = ReferenceType.SecurityScheme
    //                        }
    //                    },new List<string>()
    //                }
    //            });
});
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



#region JWT
var jwtOptions = builder.Configuration.GetSection("JwtBearer").Get<JwtBearerOptions>();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = jwtOptions.Authority;
        options.Audience = jwtOptions.Audience;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "preferred_username",
            RoleClaimType = "role" //RoleClaimType
        };
    });

builder.Services.AddTransient<IClaimsTransformation>(_ => new KeycloakRolesClaimsTransformation("role", jwtOptions.Audience));

builder.Services.AddAuthorization(options =>
{
    #region Company Permissions
    options.AddPolicy("company#get", builder => builder.AddRequirements(new RptRequirement("company", "get")));
    options.AddPolicy("company#create", builder => builder.AddRequirements(new RptRequirement("company", "create")));
    options.AddPolicy("company#update", builder => builder.AddRequirements(new RptRequirement("company", "update")));
    options.AddPolicy("company#delete", builder => builder.AddRequirements(new RptRequirement("company", "delete")));
    #endregion

    #region Brand Permissions
    options.AddPolicy("brand#get", builder => builder.AddRequirements(new RptRequirement("brand", "get")));
    options.AddPolicy("brand#create", builder => builder.AddRequirements(new RptRequirement("brand", "create")));
    options.AddPolicy("brand#update", builder => builder.AddRequirements(new RptRequirement("brand", "update")));
    options.AddPolicy("brand#delete", builder => builder.AddRequirements(new RptRequirement("brand", "delete")));
    #endregion
});

builder.Services.AddHttpClient<KeycloakServiceTest>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["KeycloakResourceUrl"]); //new Uri(Configuration["KeycloakResourceUrl"]); KeycloakResourceUrl
});
builder.Services.AddHttpClient<IdentityModel.Client.TokenClient>();
builder.Services.AddSingleton(builder.Configuration.GetSection("ClientCredentialsTokenRequest").Get<ClientCredentialsTokenRequest>());
#endregion


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();




app.MapControllers(); //.RequireAuthorization();

//Custom exceptions
app.UseCustomException();

app.Run();
