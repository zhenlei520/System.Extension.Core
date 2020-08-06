// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EInfrastructure.Core.AutoFac.Modules;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Infrastructure;
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
            return Use(services, AssemblyProvider.GetDefaultAssemblyProvider.GetAssemblies().ToArray(), action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Use(IServiceCollection services, Assembly[] assemblies,
            Action<ContainerBuilder> action = null)
        {
            return Use(services, assemblies, null, action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="typeFinder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Use(IServiceCollection services, Assembly[] assemblies, ITypeFinder typeFinder,
            Action<ContainerBuilder> action = null)
        {
            StartUp.Run();
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutomaticInjectionModule(assemblies, typeFinder));
            action?.Invoke(builder);
            builder.Populate(services);
            var container = builder.Build();
            var servicesProvider = new AutofacServiceProvider(container);
            return servicesProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="typeFinder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Use(IServiceCollection services, ITypeFinder typeFinder,
            Action<ContainerBuilder> action = null)
        {
            return Use(services, null, typeFinder, action);
        }
    }
}
