// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Autofac;
using Autofac.Core;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.AutoFac.Modules
{
    /// <summary>
    /// 自动注入
    /// </summary>
    public class AutomaticInjectionModule : Autofac.Module
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        ///
        /// </summary>
        public AutomaticInjectionModule() : this(AppDomain.CurrentDomain.GetAssemblies())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="assemblies"></param>
        public AutomaticInjectionModule(Assembly[] assemblies)
        {
            _assemblies = assemblies ?? AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleBuilder"></param>
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            var assemblys = this._assemblies;
            var perRequestType = typeof(IPerRequest);
            moduleBuilder.RegisterAssemblyTypes(assemblys)
                .Where(t => perRequestType.IsAssignableFrom(t) && t != perRequestType)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var perDependencyType = typeof(IDependency);
            moduleBuilder.RegisterAssemblyTypes(assemblys)
                .Where(t => perDependencyType.IsAssignableFrom(t) && t != perDependencyType)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            var singleInstanceType = typeof(ISingleInstance);
            moduleBuilder.RegisterAssemblyTypes(assemblys)
                .Where(t => singleInstanceType.IsAssignableFrom(t) && t != singleInstanceType)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentRegistry"></param>
        /// <param name="registration"></param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry,
            IComponentRegistration registration)
        {
        }
    }
}
