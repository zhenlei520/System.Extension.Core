using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.Interface.IOC;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac
{
    public class MvcAutoRegister
    {
        public virtual IServiceProvider Build(IServiceCollection services,
            Action<ContainerBuilder> action)
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

            action(builder);

            builder.Populate(services);

            var container = builder.Build();

            var servicesProvider = new AutofacServiceProvider(container);

            return servicesProvider;
        }
    }
}