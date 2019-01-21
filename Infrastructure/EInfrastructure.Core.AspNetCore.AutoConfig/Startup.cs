/**
 * 使用实例
 * Startup.cs上增加
 * services.AutoRegister(Configuration, "appsettings.json", false);
 * 控制器上增加：
 * private readonly IWritableOptions<object> _options;
 * public ValuesController(IWritableOptions<object> options){_options=options}
 * 方法中调用更改方法
 * _options.Update<T>(opt =>{ });//其中T为Class类
 * 
 */

using System;
using System.Linq;
using EInfrastructure.Core.AspNetCore.AutoConfig.Extension;
using EInfrastructure.Core.AutoConfig.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EInfrastructure.Core.AspNetCore.AutoConfig
{
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
                var environment = provider.GetService<IHostingEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new WritableOptions<T>(environment, options, section.Key, file);
            });
            return services;
        }

        #endregion

        #region 自动注入可修改配置文件

        /// <summary>
        /// 自动注入可修改配置文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="configuration"></param>
        /// <param name="file">配置文件地址</param>
        public static IServiceCollection AddAutoUpdateConfig(this IServiceCollection services,
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
                var environment = provider.GetService<IHostingEnvironment>();
                var options = provider.GetService<IOptionsMonitor<object>>();
                return new WritableOptions<object>(environment, options, section.Key, file);
            });
        }

        #endregion

        #endregion

        #region 环境变量

        #region 添加环境变量

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddHostingEnvironment(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoUpdateConfig<HostingEnvironmentConfigs>(
                configuration.GetSection("EInfrastructure.Core.AutoConfig.Config.HostingEnvironmentConfigs"),
                "environment.json");
            return services;
        }

        #endregion

        #region 使用HostingEnvironmentConfigs配置信息

        /// <summary>
        /// 使用HostingEnvironmentConfigs配置信息
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHostingEnvironment(this IApplicationBuilder app,
            IServiceProvider serviceProvider)
        {
            if (app == null)
                throw new ArgumentNullException(nameof (app));
            var hostingEnvironment = serviceProvider.GetService<IHostingEnvironment>();
            serviceProvider.GetService<IWritableOptions<HostingEnvironmentConfigs>>().Update(item =>
            {
                item.EnvironmentName = hostingEnvironment.EnvironmentName;
                item.ApplicationName = hostingEnvironment.ApplicationName;
                item.WebRootPath = hostingEnvironment.WebRootPath;
                item.ContentRootPath = hostingEnvironment.ContentRootPath;
            });
            return app;
        }

        #endregion

        #endregion
    }
}