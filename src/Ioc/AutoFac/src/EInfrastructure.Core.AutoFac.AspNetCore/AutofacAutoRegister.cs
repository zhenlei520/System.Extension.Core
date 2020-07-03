// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Autofac;
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
            Action<ContainerBuilder> action=null)
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
            Action<ContainerBuilder> action=null)
        {
            return EInfrastructure.Core.AutoFac.AutofacAutoRegister.Use(services, (builder) =>
            {
                services.AddMvc().AddControllersAsServices();
                action?.Invoke(builder);
            });
        }
    }
}
