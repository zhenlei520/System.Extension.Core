// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac.SqlServer.AspNetCore
{
    /// <summary>
    /// Autofac自动注入（注入mvc以及sqlserver）
    /// </summary>
    public class AutofacAutoRegister : EInfrastructure.Core.AutoFac.SqlServer.AutofacAutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [Obsolete("Use the EInfrastructure.Core.AutoFac.SqlServer.AspNetCore.AutofacAutoRegister.Use method instead")]
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
            return EInfrastructure.Core.AutoFac.SqlServer.AutofacAutoRegister.Use(services, action);
        }
    }
}
