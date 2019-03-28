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
        public override IServiceProvider Build(IServiceCollection services,
           Action<ContainerBuilder> action)
        {
            return base.Build(services, (builder) =>
            {
                services.AddMvc().AddControllersAsServices();
                action(builder);
            });
        }
    }
}
