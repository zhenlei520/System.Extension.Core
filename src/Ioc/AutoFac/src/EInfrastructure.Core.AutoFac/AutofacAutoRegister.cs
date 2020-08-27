// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Autofac;
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
        /// <param name="assemblies"></param>
        /// <param name="typeFinder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Use(IServiceCollection services,
            Action<ContainerBuilder> action = null, Assembly[] assemblies = null, ITypeFinder typeFinder = null)
        {
            return new AutoRegister().Use(services, action, assemblies, typeFinder);
        }
    }
}
