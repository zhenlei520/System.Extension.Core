using System;
using EInfrastructure.Core.Words.Config.PinYin;
using EInfrastructure.Core.Words.Config.Text;
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
        /// <param name="action"></param>
        public static IServiceCollection AddWords(this IServiceCollection serviceCollection,
            Action<DictPinYinPathConfig, DictTextPathConfig> action = null)
        {
            DictPinYinPathConfig dictPinYinPathConfig = DictPinYinPathConfig.Get();
            DictTextPathConfig dictTextPathConfig = DictTextPathConfig.Get();
            action?.Invoke(dictPinYinPathConfig, dictTextPathConfig);
            return serviceCollection;
        }
    }
}