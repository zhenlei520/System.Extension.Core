using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.AutoConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AliYun.DaYu
{
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddAliDaYu(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddCustomerConfig<SmsConfig>(
                configuration.GetSection("EInfrastructure.Core.AliYun.DaYu.Config.SmsConfig"), "alidayu.json");
            return serviceCollection;
        }
    }
}