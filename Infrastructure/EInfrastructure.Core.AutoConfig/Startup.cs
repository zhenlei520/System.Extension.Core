using System;
using EInfrastructure.Core.AutoConfig.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EInfrastructure.Core.AutoConfig
{
    /// <summary>
    /// 自动注入config文件
    /// </summary>
    public static class Startup
    {
        #region 自动注入配置文件（含热更新与非热更新，不可修改）

        /// <summary>
        /// 自动注入配置文件（含热更新与非热更新，不可修改）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="action"></param>
        /// <param name="errConfigAction">配置信息错误回调</param>
        /// <returns></returns>
        public static IServiceCollection AddAutoConfig(this IServiceCollection services,
            bool isCompleteName = false, Action<ConfigAutoRegister> action = null,
            Action<string> errConfigAction = null)
        {
            ConfigAutoRegister configAutoRegisterExt = new ConfigAutoRegister();
            if (action == null)
            {
                configAutoRegisterExt.AddSingletonConfig(services, isCompleteName, errConfigAction);
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

        #region 配置写入文件

        /// <summary>
        /// 配置写入文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="section"></param>
        /// <param name="file"></param>
        /// <typeparam name="T"></typeparam>
        public static IServiceCollection AddCustomerConfig<T>(
            this IServiceCollection services,
            IConfigurationSection section,
            string file) where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IWritableOptions<T>>(provider =>
            {
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new WritableOptions<T>(options, section.Key, file);
            });
            return services;
        }

        #endregion
    }
}