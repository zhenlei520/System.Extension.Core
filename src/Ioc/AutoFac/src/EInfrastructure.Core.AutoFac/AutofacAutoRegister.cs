using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EInfrastructure.Core.Interface.IOC;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac
{
    /// <summary>
    /// Autofac自动注入
    /// </summary>
    public class AutofacAutoRegister
    {
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual IServiceProvider Build(IServiceCollection services,
            Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToArray();
            
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