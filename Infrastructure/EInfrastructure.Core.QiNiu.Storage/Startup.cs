using EInfrastructure.Core.AutoConfig;
using EInfrastructure.Core.QiNiu.Storage.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 加载七牛云存储服务
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddCustomerConfig<QiNiuConfig>(configuration.GetSection("EInfrastructure.Core.QiNiu.Storage.Config.QiNiuConfig"), "qiniucloud.json");
            return serviceCollection;
        }
    }
}