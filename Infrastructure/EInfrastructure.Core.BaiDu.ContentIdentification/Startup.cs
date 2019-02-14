using System;
using EInfrastructure.Core.BaiDu.ContentIdentification.Config;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.BaiDu.ContentIdentification
{
    /// <summary>
    /// 加载百度鉴黄服务
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        public static IServiceCollection AddBaiduContentIdentification(this IServiceCollection serviceCollection,
            Action<BaiDuConfig> action = null)
        {
            action?.Invoke(BaiDuConfig.Get());
            return serviceCollection;
        }
    }
}