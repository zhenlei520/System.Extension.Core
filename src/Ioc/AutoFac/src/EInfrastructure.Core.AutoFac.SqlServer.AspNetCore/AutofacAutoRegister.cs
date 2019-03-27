using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac.SqlServer.AspNetCore
{
    /// <summary>
    /// Autofac自动注入（注入mvc以及sqlserver）
    /// </summary>
    public class AutofacAutoRegister : EInfrastructure.Core.AutoFac.SqlServer.AutofacAutoRegister
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
                services.AddMvc().AddControllersAsServices();
                action(builder);
            });
        }
    }
}