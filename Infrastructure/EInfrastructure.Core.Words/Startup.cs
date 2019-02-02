using System;
using EInfrastructure.Core.AutoConfig;
using EInfrastructure.Core.Words.Config.PinYin;
using EInfrastructure.Core.Words.Config.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Words
{
    /// <summary>
    /// 启动单词服务
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddWords(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddCustomerConfig<DictPinYinPathConfig>(
                configuration.GetSection("EInfrastructure.Core.Words.Config.PinYin.DictPinYinPathConfig"),
                "words.json");
            serviceCollection.AddCustomerConfig<DictTextPathConfig>(
                configuration.GetSection("EInfrastructure.Core.Words.Config.PinYin.DictTextPathConfig"), "words.json");
            return serviceCollection;
        }
    }
}