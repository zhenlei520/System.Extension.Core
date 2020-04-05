using System;
using System.Linq;
using EInfrastructure.Core.Redis.Config;
using EInfrastructure.Core.Redis.Internal;
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
        /// <param name="func"></param>
        public static IServiceCollection AddRedis(this IServiceCollection services,
            Func<RedisConfig> func)
        {
            EInfrastructure.Core.StartUp.Run();
            services.AddSingleton(func.Invoke());
            return services;
        }

        /// <summary>
        /// 加载Redis服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        public static IServiceCollection AddRedis(this IServiceCollection services,
            Action<RedisConfig> action)
        {
            RedisConfig redisConfig = new RedisConfig();
            action.Invoke(redisConfig);
            return services.AddRedis(() => redisConfig);
        }

        #endregion

        #region 加载Redis服务

        /// <summary>
        /// 加载Redis服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddRedis(this IServiceCollection services,
            IConfiguration configuration)
        {
            EInfrastructure.Core.StartUp.Run();
            services.AddHostedService<Bootstrapper>();
            return services.AddRedis(() => configuration.GetSection(nameof(RedisConfig)).Get<RedisConfig>());
        }

        #endregion
    }
}
