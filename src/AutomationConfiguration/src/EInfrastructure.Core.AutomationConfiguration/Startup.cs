using System;
using System.Linq;
using EInfrastructure.Core.AutomationConfiguration.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EInfrastructure.Core.AutomationConfiguration
{
    /// <summary>
    /// 自动注入config文件
    /// </summary>
    public static class Startup
    {
        #region 注入可修改配置文件

        #region 配置写入文件

        /// <summary>
        /// 配置写入文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="section"></param>
        /// <param name="file"></param>
        /// <typeparam name="T"></typeparam>
        public static IServiceCollection AddAutoUpdateConfig<T>(
            this IServiceCollection services,
            IConfigurationSection section,
            string file = "appsettings.json") where T : class, new()
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

        #endregion

        #region 自动注入配置文件（含热更新与非热更新，不可修改）

        /// <summary>
        /// 自动注入配置文件（含热更新与非热更新，不可修改）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="file">配置文件名称</param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="action"></param>
        /// <param name="errConfigAction">配置信息错误回调</param>
        /// <returns></returns>
        public static IServiceCollection AddAutoConfig(this IServiceCollection services,
            IConfiguration configuration,
            string file = "appsettings.json",
            bool isCompleteName = false, Action<ConfigAutoRegister> action = null,
            Action<string> errConfigAction = null, bool isUpdate = false)
        {
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

            if (isUpdate)
            {
                services.AddAutoUpdateConfig(configuration, file, isCompleteName);
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

        #region private methods

        #region 自动注入可修改配置文件

        /// <summary>
        /// 自动注入可修改配置文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="configuration"></param>
        /// <param name="file">配置文件地址</param>
        private static IServiceCollection AddAutoUpdateConfig(this IServiceCollection services,
            IConfiguration configuration,
            string file = "appsettings.json",
            bool isCompleteName = false)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t =>
                    t.GetInterfaces().Contains(typeof(IWritableConfigModel))))
                .ToArray();
            foreach (var type in types)
            {
                ConfigureWritable(services, type, configuration, isCompleteName, file);
            }

            return services;
        }


        /// <summary>
        /// 配置写入文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <param name="configuration"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="file"></param>
        private static void ConfigureWritable(
            this IServiceCollection services,
            Type type,
            IConfiguration configuration,
            bool isCompleteName = false,
            string file = "appsettings.json")
        {
            IConfigurationSection section;
            section = configuration
                .GetSection(!isCompleteName ? type.Name : type.FullName);
            services.Configure<object>(section);
            services.AddTransient<IWritableOptions<object>>(provider =>
            {
                var options = provider.GetService<IOptionsMonitor<object>>();
                return new WritableOptions<object>(options, section.Key, file);
            });
        }

        #endregion

        #endregion
    }
}