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
            StartUp.Run();
            ContainerBuilderBase autoRegister = new ContainerBuilderBase();
            autoRegister.ConfigureContainer(assemblies, typeFinder);
            autoRegister.AddConfigureContainer(action);
            autoRegister.Populate(services);
            var servicesProvider = new AutofacServiceProvider(autoRegister.Build());
            return servicesProvider;
        }
    }
}
