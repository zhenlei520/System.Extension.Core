// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using EInfrastructure.Core.Words.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Words
{
    /// <summary>
    /// 启动单词服务
    /// </summary>
    public static class Startup
    {
        #region 加载单词服务

        /// <summary>
        /// 加载单词服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddWords(this IServiceCollection services)
        {
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration) service.ImplementationInstance;
            return AddWords(services, configuration);
        }

        #endregion

        #region 加载单词服务

        /// <summary>
        /// 加载单词服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="func"></param>
        public static IServiceCollection AddWords(this IServiceCollection services,
            Func<EWordConfig> func)
        {
            services.AddSingleton(func?.Invoke());
            return services;
        }

        #endregion

        #region 加载单词服务

        /// <summary>
        /// 加载单词服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddWords(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddWords(() => configuration.GetSection(nameof(EWordConfig)).Get<EWordConfig>());
        }

        #endregion
    }
}
