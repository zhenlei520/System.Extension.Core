using System;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoConfig
{
    /// <summary>
    /// 自动注入config文件
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 只能启动一次
        /// </summary>
        private static bool _startUp = false;

        #region 自动注入配置文件（含热更新与非热更新，不可修改）

        /// <summary>
        /// 自动注入配置文件（含热更新与非热更新，不可修改）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoConfig(this IServiceCollection services,
            bool isCompleteName = false, Action<ConfigAutoRegister> action = null)
        {
            ConfigAutoRegister configAutoRegisterExt = new ConfigAutoRegister();
            if (action == null)
            {
                configAutoRegisterExt.AddSingletonConfig(services, isCompleteName);
                configAutoRegisterExt.AddScopedConfig(services, isCompleteName);
                configAutoRegisterExt.AddTransientConfig(services, isCompleteName);
            }
            else
            {
                action.Invoke(configAutoRegisterExt);
            }

            return services;
        }

        #endregion
    }
}