// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Extension.Ioc
{
    /// <summary>
    /// Ioc自动注入
    /// </summary>
    public class AutoRegister
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual IServiceProvider Build(IServiceCollection services)
        {
//            var builder = new ContainerBuilder();
//            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToArray();
//
//            var perRequestType = typeof(IPerRequest);
//            var perRequestList = assemblys
//                .SelectMany(x => x.GetTypes().Where(x => x.GetInterfaces().Contains(perRequestType)))
//                .ToArray();
//            foreach (var item in perRequestList)
//            {
//                
//            }
//            
//
//            builder.RegisterAssemblyTypes(assemblys)
//                .Where(t => perRequestType.IsAssignableFrom(t) && t != perRequestType)
//                .PropertiesAutowired()
//                .AsImplementedInterfaces()
//                .InstancePerLifetimeScope();
//
//            var perDependencyType = typeof(IDependency);
//            builder.RegisterAssemblyTypes(assemblys)
//                .Where(t => perDependencyType.IsAssignableFrom(t) && t != perDependencyType)
//                .PropertiesAutowired()
//                .AsImplementedInterfaces()
//                .InstancePerDependency();
//
//            var singleInstanceType = typeof(ISingleInstance);
//            builder.RegisterAssemblyTypes(assemblys)
//                .Where(t => singleInstanceType.IsAssignableFrom(t) && t != singleInstanceType)
//                .PropertiesAutowired()
//                .AsImplementedInterfaces()
//                .SingleInstance();
//
//            action(builder);
//
//            builder.Populate(services);
//
//            var container = builder.Build();
//
//            var servicesProvider = new AutofacServiceProvider(container);

//            return servicesProvider;
            return null;
        }
    }
}