using EInfrastructure.Core.AutoConfig;
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
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddRedis(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddCustomerConfig<RedisConfig>(
                configuration.GetSection("EInfrastructure.Core.Redis.Config.RedisConfig"), "redis.json");
            return serviceCollection;
        }
    }
}