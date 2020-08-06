// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Infrastructure;
using EInfrastructure.Core.MySql.Repository;

namespace EInfrastructure.Core.AutoFac.MySql
{
    /// <summary>
    /// Autofac自动注入(注入mysql)
    /// </summary>
    public class AutofacAutoRegister : AutoFac.AutofacAutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [Obsolete("Use the EInfrastructure.Core.AutoFac.MySql.AutofacAutoRegister.Use method instead")]
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
            return Use(services, AssemblyProvider.GetDefaultAssemblyProvider.GetAssemblies().ToArray(), action);
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
            return AutoFac.AutofacAutoRegister.Use(services, assemblies, (builder) =>
            {
                #region 单数据库查询

                builder.RegisterGeneric(typeof(QueryBase<,>)).As(typeof(IQuery<,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepository<,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterType<ExecuteBase>().As<IExecute>().PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(SpatialDimensionQuery<,>)).As(typeof(ISpatialDimensionQuery<,>))
                    .PropertiesAutowired()
                    .InstancePerLifetimeScope();

                #endregion

                #region 多数据库查询

                builder.RegisterGeneric(typeof(QueryBase<,,>)).As(typeof(IQuery<,,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(RepositoryBase<,,>)).As(typeof(IRepository<,,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(ExecuteBase<>)).As(typeof(IExecute<>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(SpatialDimensionQuery<,,>)).As(typeof(ISpatialDimensionQuery<,,>))
                    .PropertiesAutowired()
                    .InstancePerLifetimeScope();

                #endregion

                action?.Invoke(builder);
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="typeFinder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public new static IServiceProvider Use(IServiceCollection services,
            ITypeFinder typeFinder,
            Action<ContainerBuilder> action = null)
        {
            return AutoFac.AutofacAutoRegister.Use(services, typeFinder, (builder) =>
            {
                #region 单数据库查询

                builder.RegisterGeneric(typeof(QueryBase<,>)).As(typeof(IQuery<,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepository<,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterType<ExecuteBase>().As<IExecute>().PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(SpatialDimensionQuery<,>)).As(typeof(ISpatialDimensionQuery<,>))
                    .PropertiesAutowired()
                    .InstancePerLifetimeScope();

                #endregion

                #region 多数据库查询

                builder.RegisterGeneric(typeof(QueryBase<,,>)).As(typeof(IQuery<,,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(RepositoryBase<,,>)).As(typeof(IRepository<,,>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(ExecuteBase<>)).As(typeof(IExecute<>)).PropertiesAutowired()
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(SpatialDimensionQuery<,,>)).As(typeof(ISpatialDimensionQuery<,,>))
                    .PropertiesAutowired()
                    .InstancePerLifetimeScope();

                #endregion

                action?.Invoke(builder);
            });
        }
    }
}
