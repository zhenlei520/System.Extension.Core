using System;
using EInfrastructure.Core.Redis.Config;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Redis
{
    /// <summary>
    /// 加载Redis服务
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        public static IServiceCollection AddRedis(this IServiceCollection serviceCollection,
            Action<RedisConfig> action)
        {
            action.Invoke(RedisConfig.Get());
            return serviceCollection;
        }
    }
}