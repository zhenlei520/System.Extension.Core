// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore
{
    /// <summary>
    /// 加载Consul注册服务
    /// </summary>
    public static class Startup
    {
        #region 加载Consul服务

        /// <summary>
        /// 加载Consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration) service.ImplementationInstance;
            return AddConsul(services, configuration);
        }

        #endregion

        #region 加载Consul服务

        /// <summary>
        /// 加载Consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="func">Consul 配置</param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services,
            Func<ConsulConfig> func)
        {
            EInfrastructure.Core.StartUp.Run();
            services.AddSingleton(func?.Invoke());
            return services;
        }

        /// <summary>
        /// 加载Consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action">Consul 配置</param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services,
            Action<List<ConsulConfig>> action)
        {
            List<ConsulConfig> consulConfigs = new List<ConsulConfig>();
            action.Invoke(consulConfigs);
            return services.AddConsul(() => consulConfigs);
        }

        /// <summary>
        /// 加载Consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action">Consul 配置</param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services,
            Func<List<ConsulConfig>> action)
        {
            EInfrastructure.Core.StartUp.Run();
            services.AddSingleton(action?.Invoke());
            return services;
        }

        #endregion

        #region 加载Consul服务

        /// <summary>
        /// 加载Consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Consul 配置</param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services,
            IConfiguration configuration)
        {
            EInfrastructure.Core.StartUp.Run();

            services.Configure<ConsulConfig>(configuration);
            return services;
        }

        #endregion
    }
}
