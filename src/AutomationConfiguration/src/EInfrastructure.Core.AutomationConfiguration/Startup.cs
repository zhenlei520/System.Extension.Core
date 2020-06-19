using System;
using EInfrastructure.Core.AutomationConfiguration.Config;
using EInfrastructure.Core.AutomationConfiguration.Extension;
using EInfrastructure.Core.AutomationConfiguration.Interface;
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
        /// 自动注入配置文件（调用方法前，需要将配置信息注入IConfiguration中）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="appSettingConfig">默认更改或者读取的文件配置，如果不设置，则默认读取根目录的appsettings.json</param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="action"></param>
        /// <param name="errConfigAction">配置信息错误回调</param>
        /// <returns></returns>
        public static IServiceCollection AddAutoConfig(this IServiceCollection services,
            IConfiguration configuration,
            AppSettingConfig appSettingConfig,
            bool isCompleteName = false, Action<ConfigAutoRegister> action = null,
            Action<string> errConfigAction = null)
        {
            Load();
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

            if (appSettingConfig == null)
            {
                appSettingConfig = new AppSettingConfig()
                {
                    DefaultPath = "appsettings.json"
                };
            }

            services.AddTransient(provider => appSettingConfig);
            services.AddTransient(typeof(IWritableOptions<>), typeof(WritableOptions<>));
            return services;
        }

        /// <summary>
        /// 自动注入配置文件（调用方法前，需要将配置信息注入IConfiguration中）
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
            return services.AddAutoConfig(configuration, null, isCompleteName, action,
                errConfigAction);
        }

        #endregion

        #region private methods

        #region 加载必要服务

        /// <summary>
        ///加载必要服务
        /// </summary>
        private static void Load()
        {
            StartUp.Run();
        }

        #endregion

        #endregion
    }
}
