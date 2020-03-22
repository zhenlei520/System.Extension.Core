// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac.MySql.AspNetCore
{
    /// <summary>
    /// Autofac自动注入（注入mvc以及mysql）
    /// </summary>
    public class AutofacAutoRegister : EInfrastructure.Core.AutoFac.MySql.AutofacAutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [Obsolete("Use the EInfrastructure.Core.AutoFac.MySql.AspNetCore.AutofacAutoRegister.Use method instead")]
        public override IServiceProvider Build(IServiceCollection services,
            Action<ContainerBuilder> action)
        {
            return AutofacAutoRegister.Use(services, action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Use(IServiceCollection services,
            Action<ContainerBuilder> action)
        {
            return EInfrastructure.Core.AutoFac.MySql.AutofacAutoRegister.Use(services, (builder) =>
            {
                services.AddMvc().AddControllersAsServices();
                action?.Invoke(builder);
            });
        }
    }
}
