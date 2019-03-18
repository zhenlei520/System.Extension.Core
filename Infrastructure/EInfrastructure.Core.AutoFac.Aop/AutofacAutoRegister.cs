using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac.Aop
{
    public class AutofacAutoRegister : AutoFac.AutofacAutoRegister
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
                action(builder);
            });
        }
    }
}