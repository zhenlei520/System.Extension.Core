using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.Interface.IOC;
using Microsoft.Extensions.DependencyInjection;
using EInfrastructure.Core.Ddd;
using EInfrastructure.Core.MySql;

namespace EInfrastructure.Core.AutoFac
{
  public class MvcAutoRegister
  {
    public IServiceProvider Build<T>(IServiceCollection services,
        Action<ContainerBuilder> action) where T : struct
    {
      services.AddMvc().AddControllersAsServices();

      var builder = new ContainerBuilder();


      var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToArray();

      LogCommon.Debug("已加载程的序集", new JsonCommon().Serializer(assemblys.Select(t => t.FullName), true));

      var perRequestType = typeof(IPerRequest);
      builder.RegisterAssemblyTypes(assemblys)
          .Where(t => perRequestType.IsAssignableFrom(t) && t != perRequestType)
          .PropertiesAutowired()
          .AsImplementedInterfaces()
          .InstancePerLifetimeScope();

      var perDependencyType = typeof(IDependency);
      builder.RegisterAssemblyTypes(assemblys)
          .Where(t => perDependencyType.IsAssignableFrom(t) && t != perDependencyType)
          .PropertiesAutowired()
          .AsImplementedInterfaces()
          .InstancePerDependency();

      var singleInstanceType = typeof(ISingleInstance);
      builder.RegisterAssemblyTypes(assemblys)
          .Where(t => singleInstanceType.IsAssignableFrom(t) && t != singleInstanceType)
          .PropertiesAutowired()
          .AsImplementedInterfaces()
          .SingleInstance();

      var temp = typeof(IQuery<IEntity<Guid>,Guid>);
      builder.RegisterGeneric(typeof(QueryBase<Entity<T>, T>)).As(typeof(IQuery<IEntity<T>, T>)).PropertiesAutowired()
          .InstancePerLifetimeScope();

      builder.RegisterGeneric(typeof(RepositoryBase<AggregateRoot<T>, T>)).As(typeof(IRepository<IAggregateRoot<T>, T>)).PropertiesAutowired()
          .InstancePerLifetimeScope();

      action(builder);

      builder.Populate(services);

      var container = builder.Build();

      var servicesProvider = new AutofacServiceProvider(container);

      return servicesProvider;

    }
  }

}