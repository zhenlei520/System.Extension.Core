// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using EInfrastructure.Core.BaiDu.ContentIdentification.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.BaiDu.ContentIdentification
{
    /// <summary>
    /// 加载百度鉴黄服务
    /// </summary>
    public static class Startup
    {
        #region 加载百度鉴黄服务

        /// <summary>
        /// 加载七牛云存储
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddBaiduContentIdentification(this IServiceCollection services)
        {
            EInfrastructure.Core.StartUp.Run();
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration) service.ImplementationInstance;
            return AddBaiduContentIdentification(services, configuration);
        }

        #endregion

        #region 加载百度鉴黄服务

        /// <summary>
        /// 加载百度鉴黄服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="func"></param>
        public static IServiceCollection AddBaiduContentIdentification(this IServiceCollection services,
            Func<BaiDuConfig> func)
        {
            EInfrastructure.Core.StartUp.Run();
            services.AddSingleton(func?.Invoke());
            return services;
        }

        #endregion

        #region 加载百度鉴黄服务

        /// <summary>
        /// 加载百度鉴黄服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddBaiduContentIdentification(this IServiceCollection services,
            IConfiguration configuration)
        {
            EInfrastructure.Core.StartUp.Run();
            services.Configure<BaiDuConfig>(configuration);
            return services;
        }

        #endregion
    }
}
