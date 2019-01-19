using System;
using EInfrastructure.Core.AutoConfig;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Words
{
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名（配置文件需要）</param>
        /// <param name="action"></param>
        /// <param name="errConfigAction">配置信息错误回调</param>
        public static IServiceCollection AddWords(this IServiceCollection serviceCollection,
            bool isCompleteName = false,
            Action<ConfigAutoRegister> action = null,
            Action<string> errConfigAction = null)
        {
            serviceCollection.AddAutoConfig(isCompleteName, action,errConfigAction);
            return serviceCollection;
        }
    }
}