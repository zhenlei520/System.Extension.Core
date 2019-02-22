using System;
using EInfrastructure.Core.QiNiu.Storage.Config;
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
        /// <param name="action">委托</param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection serviceCollection,
            Action<QiNiuConfig> action )
        {
            var qiNiuConfig = QiNiuConfig.Get();
            action.Invoke(qiNiuConfig);
            qiNiuConfig.Set();//不执行也可
            return serviceCollection;
        }
    }
}