// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using EInfrastructure.Core.QiNiu.Storage.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 加载七牛云存储服务
    /// </summary>
    public static class Startup
    {
        #region 加载七牛云存储

        /// <summary>
        /// 加载七牛云存储
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection services)
        {
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration) service.ImplementationInstance;
            return AddQiNiuStorage(services, configuration);
        }

        #endregion

        #region 加载七牛云存储

        /// <summary>
        /// 加载七牛云存储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action">委托</param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection services,
            Action<QiNiuStorageConfig> action)
        {
            services.Configure(action);
            return services;
        }

        #endregion

        #region 加载七牛云存储

        /// <summary>
        /// 加载七牛云存储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<QiNiuStorageConfig>(configuration);
            return services;
        }

        #endregion
    }
}
