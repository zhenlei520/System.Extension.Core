using System;
using System.Linq;
using EInfrastructure.Core.Redis.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Redis
{
    /// <summary>
    /// 加载Redis服务
    /// </summary>
    public static class Startup
    {
        #region 加载Redis服务

        /// <summary>
        /// 加载Redis服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration) service.ImplementationInstance;
            return AddRedis(services, configuration);
        }

        #endregion

        #region 加载Redis服务

        /// <summary>
        /// 加载Redis服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        public static IServiceCollection AddRedis(this IServiceCollection services,
            Action<RedisConfig> action)
        {
            services.Configure(action);
            return services;
        }

        #endregion

        #region 加载Redis服务

        /// <summary>
        /// 加载Redis服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        public static IServiceCollection AddRedis(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<RedisConfig>(configuration);
            return services;
        }

        #endregion
    }
}