using System;
using System.Linq;
using EInfrastructure.Core.AutoConfig.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoConfig
{
    /// <summary>
    /// 自动注入config文件
    /// </summary>
    public static class ConfigAutoRegister
    {
        #region 自动注入配置文件（含热更新与非热更新）

        /// <summary>
        /// 自动注入配置文件（含热更新与非热更新）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <returns></returns>
        public static IServiceCollection AutoRegister(this IServiceCollection services,
            bool isCompleteName = false)
        {
            AddSingletonConfig(services, isCompleteName);
            AddScopedConfig(services, isCompleteName);
            AddTransientConfig(services, isCompleteName);
            return services;
        }

        #endregion

        #region 自动注入配置文件（项目启动-项目关闭）

        /// <summary>
        /// 自动注入配置文件（项目启动-项目关闭）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <returns></returns>
        public static IServiceCollection AddSingletonConfig(this IServiceCollection services,
            bool isCompleteName = false)
        {
            services.AddConfig<ISingletonConfigModel>((type) =>
            {
                services.AddSingleton(type, provider => provider.Get(type, isCompleteName));
            });
            return services;
        }

        #endregion

        #region  自动注入配置文件（请求开始-请求结束）

        /// <summary>
        /// 自动注入配置文件（请求开始-请求结束）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <returns></returns>
        public static IServiceCollection AddScopedConfig(this IServiceCollection services, bool isCompleteName = false)
        {
            services.AddConfig<IScopedConfigModel>((type) =>
            {
                services.AddScoped(type, provider => provider.Get(type, isCompleteName));
            });
            return services;
        }

        #endregion

        #region  自动注入配置文件（请求开始-请求结束）

        /// <summary>
        /// 自动注入配置文件（请求获取-（GC回收-主动释放），每一次获取的对象都不是同一个）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <returns></returns>
        public static IServiceCollection AddTransientConfig(this IServiceCollection services,
            bool isCompleteName = false)
        {
            services.AddConfig<ITransientConfigModel>((type) =>
            {
                services.AddTransient(type, provider => provider.Get(type, isCompleteName));
            });
            return services;
        }

        #endregion

        #region private methods

        #region 注册配置文件信息

        /// <summary>
        /// 注册配置文件信息
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IServiceCollection AddConfig<T>(this IServiceCollection services,
            Action<Type> action)
            where T : IConfigModel

        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(T))))
                .ToArray();

            foreach (var type in types)
            {
                action.Invoke(type);
            }

            return services;
        }

        #endregion

        #region 得到配置文件信息

        /// <summary>
        /// 得到配置文件信息
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="type"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <returns></returns>
        private static object Get(this IServiceProvider provider, Type type, bool isCompleteName = false)
        {
            if (!isCompleteName)
            {
                return provider.GetService<IConfiguration>().GetSection(type.Name).Get(type);
            }

            return provider.GetService<IConfiguration>().GetSection(type.FullName).Get(type);
        }

        #endregion

        #endregion
    }
}