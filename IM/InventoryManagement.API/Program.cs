using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using InventoryManagement.API.Filters;
using InventoryManagement.API.MiddlewaresExtension;
using InventoryManagement.API.Modules;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services.Entity;
using InventoryManagement.Core.UnitOfWork;
using InventoryManagement.Repository;
using InventoryManagement.Repository.Repositories;
using InventoryManagement.Repository.UnitOfWork;
using InventoryManagement.Services.Mapping;
using InventoryManagement.Services.Services.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHealthChecks();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateFilterAttribute()); //options.Filters.Add(new AuthorizeFilter()); //tum kontrollerde authorize attribute etkinle�tirilmis olacak
}).AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull; //db'de null olan de�erleri getirme
});



builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();

#region Swagger
builder.Services.AddSwaggerGen();
#endregion


#region Dependencies
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); //IService yap�labilir herhangi bir sorunda
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
app.UseAuthorization();
app.MapControllers();

//Custom exceptions
app.UseCustomException();

#region Health check
app.UseHealthChecks("/health");
#endregion


app.Run();
