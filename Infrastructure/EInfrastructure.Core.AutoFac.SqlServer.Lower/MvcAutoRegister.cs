using Autofac;
using EInfrastructure.Core.Ddd;
using EInfrastructure.Core.SqlServer.Lower;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EInfrastructure.Core.AutoFac.SqlServer.Lower
{
    public class MvcAutoRegister : EInfrastructure.Core.AutoFac.MvcAutoRegister
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
                action(builder);
            });
        }
    }
}
