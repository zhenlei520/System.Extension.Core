using EInfrastructure.Core.AutoConfig;
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
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddBaiduContentIdentification(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddCustomerConfig<BaiDuConfig>(
                configuration.GetSection("EInfrastructure.Core.BaiDu.ContentIdentification.Config.BaiDuConfig"),
                "baiducontentidentification.json");
            return serviceCollection;
        }
    }
}