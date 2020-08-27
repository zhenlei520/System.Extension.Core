// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EInfrastructure.Core.Configuration.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac
{
    /// <summary>
    /// 自动注入
    /// </summary>
    public class AutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <param name="assemblies"></param>
        /// <param name="typeFinder"></param>
        /// <returns></returns>
        public virtual IServiceProvider Use(IServiceCollection services,
            Action<ContainerBuilder> action = null, Assembly[] assemblies = null,
            ITypeFinder typeFinder = null)
        {
            return Use(services, this.CreateContainerBuilder(action, null, assemblies, typeFinder));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="containerBuilderBase"></param>
        /// <returns></returns>
        public virtual IServiceProvider Use(IServiceCollection services, ContainerBuilderBase containerBuilderBase)
        {
            containerBuilderBase.Populate(services);
            var servicesProvider = new AutofacServiceProvider(containerBuilderBase.Build());
            return servicesProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        /// <param name="containerBuilder"></param>
        /// <param name="assemblies"></param>
        /// <param name="typeFinder"></param>
        /// <returns></returns>
        public virtual ContainerBuilderBase CreateContainerBuilder(Action<ContainerBuilder> action,
            ContainerBuilder containerBuilder = null, Assembly[] assemblies = null,
            ITypeFinder typeFinder = null)
        {
            StartUp.Run();
            ContainerBuilderBase autoRegister = new ContainerBuilderBase(containerBuilder);
            autoRegister.ConfigureContainer(assemblies, typeFinder);
            autoRegister.AddConfigureContainer(action);
            return autoRegister;
        }
    }
}
