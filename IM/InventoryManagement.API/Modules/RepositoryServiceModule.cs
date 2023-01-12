using Autofac;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.Services.Entity;
using InventoryManagement.Core.UnitOfWork;
using InventoryManagement.Repository;
using InventoryManagement.Repository.Repositories;
using InventoryManagement.Repository.UnitOfWork;
using InventoryManagement.Services.Mapping;
using InventoryManagement.Services.Services;
using InventoryManagement.Services.Services.Entity;
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
            builder.RegisterType<CategoryServiceWithDto>().As<ICategoryServiceWithDto>().InstancePerLifetimeScope();
            builder.RegisterType<BrandServiceWithDto>().As<IBrandServiceWithDto>().InstancePerLifetimeScope();
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
