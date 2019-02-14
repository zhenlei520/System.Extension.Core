using System;
using EInfrastructure.Core.UCloud.Storage.Config;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.UCloud.Storage
{
    /// <summary>
    /// 加载UCloud存储服务
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        public static IServiceCollection AddUCloudStorage(this IServiceCollection serviceCollection,
            Action<UCloudConfig> action)
        {
            action.Invoke(UCloudConfig.Get());
            return serviceCollection;
        }
    }
}