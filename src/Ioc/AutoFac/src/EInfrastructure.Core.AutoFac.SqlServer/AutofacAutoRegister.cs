// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.SqlServer.Repository;

namespace EInfrastructure.Core.AutoFac.SqlServer
{
    /// <summary>
    /// Autofac自动注入（注入sqlserver）
    /// </summary>
    public class AutofacAutoRegister : EInfrastructure.Core.AutoFac.AutofacAutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [Obsolete("Use the EInfrastructure.Core.AutoFac.SqlServer.AutofacAutoRegister.Use method instead")]
        public override IServiceProvider Build(IServiceCollection services,
            Action<ContainerBuilder> action=null)
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
            Action<ContainerBuilder> action=null)
        {
            return EInfrastructure.Core.AutoFac.AutofacAutoRegister.Use(services, (builder) =>
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
