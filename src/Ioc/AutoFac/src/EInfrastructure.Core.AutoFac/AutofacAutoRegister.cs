// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EInfrastructure.Core.AutoFac.Modules;
using EInfrastructure.Core.Configuration.Ioc;
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
        [Obsolete("Use the EInfrastructure.Core.AutoFac.AutofacAutoRegister.Use method instead")]
        public virtual IServiceProvider Build(IServiceCollection services,
            Action<ContainerBuilder> action = null)
        {
            return Use(services, action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Use(IServiceCollection services,
            Action<ContainerBuilder> action = null)
        {
            EInfrastructure.Core.StartUp.Run();
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutomaticInjectionModule>();
            action?.Invoke(builder);
            builder.Populate(services);
            var container = builder.Build();
            var servicesProvider = new AutofacServiceProvider(container);
            ServiceComponent.SetServiceProvider(servicesProvider);
            return servicesProvider;
        }
    }
}
