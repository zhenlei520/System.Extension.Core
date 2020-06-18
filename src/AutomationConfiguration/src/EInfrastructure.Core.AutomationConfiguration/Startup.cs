using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutomationConfiguration
{
    /// <summary>
    /// 自动注入config文件
    /// </summary>
    public static class Startup
    {
        #region 自动注入配置文件

        /// <summary>
        /// 自动注入配置文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="action"></param>
        /// <param name="errConfigAction">配置信息错误回调</param>
        /// <returns></returns>
        public static IServiceCollection AddAutoConfig(this IServiceCollection services,
            IConfiguration configuration,
            bool isCompleteName = false, Action<ConfigAutoRegister> action = null,
            Action<string> errConfigAction = null)
        {
            StartUp.Run();
            ConfigAutoRegister configAutoRegisterExt = new ConfigAutoRegister();
            if (action == null)
            {
                configAutoRegisterExt.AddSingletonConfig(services, configuration, isCompleteName, errConfigAction);
                configAutoRegisterExt.AddScopedConfig(services, isCompleteName, errConfigAction);
                configAutoRegisterExt.AddTransientConfig(services, isCompleteName, errConfigAction);
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
