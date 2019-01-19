using System;
using EInfrastructure.Core.AutoConfig;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.QiNiu.Storage
{
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名（配置文件需要）</param>
        /// <param name="action"></param>
        public static IServiceCollection AddQiNiuStoreage(this IServiceCollection serviceCollection,
            bool isCompleteName = false,
            Action<ConfigAutoRegister> action = null)
        {
            serviceCollection.AddAutoConfig(isCompleteName, action);
            return serviceCollection;
        }
    }
}