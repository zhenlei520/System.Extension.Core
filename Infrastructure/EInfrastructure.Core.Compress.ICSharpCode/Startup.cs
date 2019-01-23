using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Compress.ICSharpCode
{
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static IServiceCollection AddSystemsCompress(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}