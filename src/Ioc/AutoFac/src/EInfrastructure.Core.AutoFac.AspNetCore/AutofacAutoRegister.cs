// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Autofac;
using EInfrastructure.Core.AspNetCore;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac.AspNetCore
{
    /// <summary>
    /// Mvc自动注入(注入mvc)
    /// </summary>
    public class AutofacAutoRegister : EInfrastructure.Core.AutoFac.AutofacAutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [Obsolete("Use the EInfrastructure.Core.AutoFac.AspNetCore.AutofacAutoRegister.Use method instead")]
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
        /// <returns></returns>
        public new static IServiceProvider Use(IServiceCollection services,
            Action<ContainerBuilder> action = null)
        {
            return Use(services, AssemblyProvider.GetDefaultAssemblyProvider, action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public new static IServiceProvider Use(IServiceCollection services, Assembly[] assemblies,
            Action<ContainerBuilder> action = null)
        {
            return EInfrastructure.Core.AutoFac.AutofacAutoRegister.Use(services, assemblies, (builder) =>
            {
                EInfrastructure.Core.AspNetCore.StartUp.AddMvc(services.AddBasicNetCore()).AddControllersAsServices();
                action?.Invoke(builder);
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyProvider"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public new static IServiceProvider Use(IServiceCollection services, IAssemblyProvider assemblyProvider,
            Action<ContainerBuilder> action = null)
        {
            return EInfrastructure.Core.AutoFac.AutofacAutoRegister.Use(services, assemblyProvider, (builder) =>
            {
                EInfrastructure.Core.AspNetCore.StartUp.AddMvc(services.AddBasicNetCore()).AddControllersAsServices();
                action?.Invoke(builder);
            });
        }
    }
}
