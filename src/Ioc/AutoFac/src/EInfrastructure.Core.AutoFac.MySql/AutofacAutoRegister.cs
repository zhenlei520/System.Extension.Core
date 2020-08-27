// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.AutoFac.MySql
{
    /// <summary>
    /// Autofac自动注入(注入mysql)
    /// </summary>
    public class AutofacAutoRegister : AutoFac.AutofacAutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [Obsolete("Use the EInfrastructure.Core.AutoFac.MySql.AutofacAutoRegister.Use method instead")]
        public override IServiceProvider Build(IServiceCollection services,
            Action<ContainerBuilder> action = null)
        {
            return Use(services, action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <param name="assemblies"></param>
        /// <param name="typeFinder"></param>
        /// <returns></returns>
        public new static IServiceProvider Use(IServiceCollection services,
            Action<ContainerBuilder> action = null, Assembly[] assemblies = null, ITypeFinder typeFinder = null)
        {
            return new EInfrastructure.Core.AutoFac.MySql.AutoRegister().Use(services, action, assemblies, typeFinder);
        }
    }
}
