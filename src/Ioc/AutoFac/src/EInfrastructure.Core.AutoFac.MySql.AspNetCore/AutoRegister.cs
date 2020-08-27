// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Autofac;
using EInfrastructure.Core.AspNetCore;
using EInfrastructure.Core.Configuration.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac.MySql.AspNetCore
{
    /// <summary>
    ///
    /// </summary>
    public class AutoRegister : EInfrastructure.Core.AutoFac.MySql.AutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <param name="typeFinder"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public override IServiceProvider Use(IServiceCollection services,
            Action<ContainerBuilder> action = null, Assembly[] assemblies = null, ITypeFinder typeFinder = null)
        {
            EInfrastructure.Core.AspNetCore.StartUp.AddMvc(services.AddBasicNetCore())
                .AddControllersAsServices();
            return base.Use(services, builder => { action?.Invoke(builder); }, assemblies,
                typeFinder);
        }
    }
}
