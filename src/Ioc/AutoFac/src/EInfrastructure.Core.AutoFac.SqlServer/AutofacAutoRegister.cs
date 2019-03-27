using Autofac;
using EInfrastructure.Core.Ddd;
using Microsoft.Extensions.DependencyInjection;
using System;
using EInfrastructure.Core.SqlServer;

namespace EInfrastructure.Core.AutoFac.SqlServer
{
    /// <summary>
    /// Autofac自动注入（注入sqlserver）
    /// </summary>
    public class AutofacAutoRegister : EInfrastructure.Core.AutoFac.AutofacAutoRegister
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public override IServiceProvider Build(IServiceCollection services,
            Action<ContainerBuilder> action)
        {
            return base.Build(services, (builder) =>
            {
                builder.RegisterGeneric(typeof(QueryBase<,>)).As(typeof(IQuery<,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepository<,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterType<ExecuteBase>().As<IExecute>().PropertiesAutowired()
                    .InstancePerLifetimeScope();
                action(builder);
            });
        }
    }
}