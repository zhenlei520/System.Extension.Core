// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Infrastructure;

namespace EInfrastructure.Core.AutoFac.Modules
{
    /// <summary>
    /// 自动注入
    /// </summary>
    public class AutomaticInjectionModule : Autofac.Module
    {
        private readonly Assembly[] _assemblies;
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        ///
        /// </summary>
        public AutomaticInjectionModule() : this(null, null)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="typeFinder"></param>
        public AutomaticInjectionModule(ITypeFinder typeFinder) : this(null, typeFinder)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="typeFinder"></param>
        public AutomaticInjectionModule(Assembly[] assemblies, ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder ?? new TypeFinder();
            _assemblies = assemblies;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleBuilder"></param>
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            var perRequestTypes = _typeFinder.FindClassesOfType<IPerRequest>(this._assemblies).ToArray();
            moduleBuilder.RegisterTypes(perRequestTypes)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var perDependencyTypes = _typeFinder.FindClassesOfType<IDependency>(this._assemblies).ToArray();
            moduleBuilder.RegisterTypes(perDependencyTypes)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            var singleInstanceTypes = _typeFinder.FindClassesOfType<ISingleInstance>(this._assemblies).ToArray();
            moduleBuilder.RegisterTypes(singleInstanceTypes)
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
