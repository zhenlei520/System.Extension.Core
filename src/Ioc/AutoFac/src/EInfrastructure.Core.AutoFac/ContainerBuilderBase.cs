// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EInfrastructure.Core.AutoFac.Modules;
using EInfrastructure.Core.Configuration.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac
{
    /// <summary>
    ///
    /// </summary>
    public class ContainerBuilderBase
    {
        /// <summary>
        ///
        /// </summary>
        public ContainerBuilder ContainerBuilder { get; }

        /// <summary>
        ///
        /// </summary>
        public ContainerBuilderBase() : this(new ContainerBuilder())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="containerBuilder"></param>
        public ContainerBuilderBase(ContainerBuilder containerBuilder)
        {
            ContainerBuilder = containerBuilder;
        }

        #region 得到配置容器

        /// <summary>
        /// 得到配置容器
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="typeFinder"></param>
        /// <returns></returns>
        public ContainerBuilder ConfigureContainer(Assembly[] assemblies,
            ITypeFinder typeFinder)
        {
            return ConfigureContainer(new AutomaticInjectionModule(assemblies, typeFinder));
        }

        /// <summary>
        /// 得到配置容器
        /// </summary>
        /// <param name="automaticInjectionModule"></param>
        /// <returns></returns>
        public ContainerBuilder ConfigureContainer(AutomaticInjectionModule automaticInjectionModule)
        {
            this.ContainerBuilder.RegisterModule(automaticInjectionModule);
            return this.ContainerBuilder;
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        public virtual void AddConfigureContainer(Action<ContainerBuilder> action)
        {
            action?.Invoke(this.ContainerBuilder);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        public void Populate(IServiceCollection services)
        {
            this.ContainerBuilder.Populate(services);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Autofac.IContainer Build()
        {
            return this.ContainerBuilder.Build();
        }
    }
}
