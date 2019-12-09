// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Autofac;
using EInfrastructure.Core.MySql;
using Microsoft.Extensions.DependencyInjection;
using System;
using EInfrastructure.Core.Config.EntitiesExtensions;
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
        public override IServiceProvider Build(IServiceCollection services,
            Action<ContainerBuilder> action)
        {
            return base.Build(services, (builder) =>
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

                action(builder);
            });
        }
    }
}
