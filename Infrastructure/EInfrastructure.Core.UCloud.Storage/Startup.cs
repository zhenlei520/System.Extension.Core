using EInfrastructure.Core.AutoConfig;
using EInfrastructure.Core.UCloud.Storage.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.UCloud.Storage
{
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddUCloudStorage(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddCustomerConfig<UCloudConfig>(configuration.GetSection("EInfrastructure.Core.UCloud.Storage.Config.UCloudConfig"), "ucloud.json");
            return serviceCollection;
        }
    }
}