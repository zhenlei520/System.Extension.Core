// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Autofac;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.MySql.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoFac.MySql
{
    /// <summary>
    /// 自动注入扩展
    /// </summary>
    public static class AutofacAutoRegisterExt
    {
        #region 添加多个数据库

        /// <summary>
        /// 添加多个数据库
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddMultDbContext<T>(this ContainerBuilder containerBuilder) where T : IDbContext
        {
            containerBuilder.RegisterType<T>().As<IUnitOfWork<T>>().PropertiesAutowired()
                .InstancePerLifetimeScope();
        }

        #endregion

        #region 添加多个数据库

        /// <summary>
        /// 添加多个数据库
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="T"></typeparam>
        public static IServiceCollection AddMultDbContext<T>(this IServiceCollection services) where T : IDbContext
        {
            services.AddScoped(typeof(IUnitOfWork<T>), typeof(T));
            return services;
        }

        #endregion

        #region 注入mysql数据库

        /// <summary>
        /// 注入mysql数据库
        /// </summary>
        /// <param name="containerBuilder"></param>
        public static void RegisterMySqlRepositoryModule(this ContainerBuilder containerBuilder)
        {
            #region 单数据库查询

            containerBuilder.RegisterGeneric(typeof(QueryBase<,>)).As(typeof(IQuery<,>)).PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepository<,>)).PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<ExecuteBase>().As<IExecute>().PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(SpatialDimensionQuery<,>)).As(typeof(ISpatialDimensionQuery<,>))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            #endregion

            #region 多数据库查询

            containerBuilder.RegisterGeneric(typeof(QueryBase<,,>)).As(typeof(IQuery<,,>)).PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(RepositoryBase<,,>)).As(typeof(IRepository<,,>))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(ExecuteBase<>)).As(typeof(IExecute<>)).PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(SpatialDimensionQuery<,,>)).As(typeof(ISpatialDimensionQuery<,,>))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            #endregion
        }

        #endregion
    }
}
