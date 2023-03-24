using Autofac;
using InventoryManagement.API.Authorization.Decision;
using InventoryManagement.API.Authorization.RPT;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using InventoryManagement.Repository;
using InventoryManagement.Repository.Repositories;
using InventoryManagement.Repository.UnitOfWork;
using InventoryManagement.Services.Mapping;
using InventoryManagement.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Reflection;
using Module = Autofac.Module;


namespace InventoryManagement.API.Modules
{
    public class RepositoryServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Generic Repository
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();


            //Generic Service, TEntity şeklinde donus sağlamak
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();



            //Entity verip, dto şeklinde dönüş sağlamak
            builder.RegisterGeneric(typeof(ServiceWithDto<,>)).As(typeof(IServiceWithDto<,>)).InstancePerLifetimeScope();


            //UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();



            #region Custom Service Dto
            builder.RegisterType<CompanyServiceWithDto>().As<ICompanyServiceWithDto>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryServiceWithDto>().As<ICategoryServiceWithDto>().InstancePerLifetimeScope();
            builder.RegisterType<CategorySubServiceWithDto>().As<ICategorySubServiceWithDto>().InstancePerLifetimeScope();
            builder.RegisterType<BrandServiceWithDto>().As<IBrandServiceWithDto>().InstancePerLifetimeScope();
            builder.RegisterType<ModelServiceWithDto>().As<IModelServiceWithDto>().InstancePerLifetimeScope();
            builder.RegisterType<InventoryServiceWithDto>().As<IInventoryServiceWithDto>().InstancePerLifetimeScope();
            builder.RegisterType<InventoryMovementServiceWithDto>().As<IInventoryMovementServiceWithDto>().InstancePerLifetimeScope();
            #endregion


            #region TEST
            builder.RegisterType<DecisionRequirementHandler>().As<IAuthorizationHandler>().SingleInstance();
            builder.RegisterType<RptRequirementHandler>().As<IAuthorizationHandler>().SingleInstance();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.Register(c => c.Resolve<IOptions<JwtBearerOptions>>().Value).As<JwtBearerOptions>();

            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            //builder.Register(c => c.Resolve<IOptions<JwtBearerOptions>>().Value).As<JwtBearerOptions>();
            #endregion

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAsssembly = Assembly.GetAssembly(typeof(DataContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(AutoMapperProfile));



            builder.RegisterAssemblyTypes(apiAssembly, repoAsssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAsssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
