using System;
using System.Linq;
using EInfrastructure.Core.AutoConfig.Interface;
using EInfrastructure.Core.HelpCommon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.AutoConfig
{
    /// <summary>
    /// 自动注入配置
    /// </summary>
    public class ConfigAutoRegister
    {
        #region 自动注入配置文件（项目启动-项目关闭）

        /// <summary>
        /// 自动注入配置文件（项目启动-项目关闭）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="errConfigAction">配置信息错误回调</param>
        /// <returns></returns>
        internal IServiceCollection AddSingletonConfig(IServiceCollection services,
            IConfiguration configuration,
            bool isCompleteName = false,
            Action<string> errConfigAction = null)
        {
            AddConfig<ISingletonConfigModel>(services,
                type =>
                {
                    var config = Get(sectionName =>
                        configuration.GetSection(sectionName).Get(type), type, isCompleteName, errConfigAction);
                    services.AddSingleton(type, config);
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
        /// <param name="errConfigAction">配置信息错误回调</param>
        /// <returns></returns>
        internal IServiceCollection AddScopedConfig(IServiceCollection services,
            bool isCompleteName = false,
            Action<string> errConfigAction = null)
        {
            IConfiguration configuration = null;
            AddConfig<IScopedConfigModel>(services,
                type =>
                {
                    services.AddScoped(type, provider => Get(sectionName =>
                            provider.GetService<IConfiguration>().GetSection(sectionName).Get(type)
                        , type, isCompleteName, errConfigAction));
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
        /// <param name="errConfigAction">配置信息错误回调</param>
        /// <returns></returns>
        internal IServiceCollection AddTransientConfig(IServiceCollection services,
            bool isCompleteName = false,
            Action<string> errConfigAction = null)
        {
            AddConfig<ITransientConfigModel>(services,
                type =>
                {
                    services.AddTransient(type, provider => Get(sectionName =>
                            provider.GetService<IConfiguration>().GetSection(sectionName).Get(type)
                        , type, isCompleteName, errConfigAction));
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
        private static IServiceCollection AddConfig<T>(IServiceCollection services,
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
        /// <param name="func"></param>
        /// <param name="type"></param>
        /// <param name="isCompleteName">是否输入完整的类名，默认：false，为true时则需要输入命名空间+类名</param>
        /// <param name="errConfigAction">配置信息错误回调</param>
        /// <returns></returns>
        private object Get(Func<string, object> func, Type type, bool isCompleteName = false,
            Action<string> errConfigAction = null)
        {
            object result;
            string sectionName;
            if (!isCompleteName)
            {
                sectionName = type.Name;
            }
            else
            {
                sectionName = type.FullName;
            }

            result = func.Invoke(sectionName);
            LogCommon.Debug("自动注入获取配置成功");
            if (result == null && errConfigAction != null)
            {
                errConfigAction.Invoke($"获取{sectionName}节点的信息失败");
                return System.Reflection.Assembly.GetAssembly(type).CreateInstance(type.ToString());
            }

            return result;
        }

        #endregion

        #endregion
    }
}