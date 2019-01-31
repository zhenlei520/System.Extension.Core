using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.MemoryCache
{
    /// <summary>
    /// 加载MemoryCache服务
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static IServiceCollection AddMemoryCache(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}