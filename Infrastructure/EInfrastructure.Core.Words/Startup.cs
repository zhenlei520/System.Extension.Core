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
        /// <param name="file"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名（配置文件需要）</param>
        /// <param name="action"></param>
        /// <param name="errConfigAction">配置信息错误回调</param>
        public static IServiceCollection AddWords(this IServiceCollection serviceCollection,
            IConfiguration configuration,
            string file = "appsettings.json",
            bool isCompleteName = false,
            Action<ConfigAutoRegister> action = null,
            Action<string> errConfigAction = null)
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