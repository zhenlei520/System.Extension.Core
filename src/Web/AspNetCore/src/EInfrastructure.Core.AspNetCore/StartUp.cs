// Copyright (c) zhenlei520 All rights reserved.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore
{
    /// <summary>
    ///
    /// </summary>
    public static class StartUp
    {
        private static bool _isStartUp;

        #region 启用配置

        /// <summary>
        /// 启用配置
        /// </summary>
        [Obsolete("此方法已过时，请更换为services.AddBasicNetCore()")]
        public static void Run()
        {
            if (!_isStartUp)
            {
                _isStartUp = true;
                EInfrastructure.Core.StartUp.Run();
            }
        }

        #endregion

        #region 添加基本的请求信息

        /// <summary>
        /// 添加基本的请求信息
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddBasicNetCore(this IServiceCollection services)
        {
            Run();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        #endregion

        #region 设置Json配置

        /// <summary>
        /// 设置Json配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action">拦截器，默认无</param>
        public static IMvcBuilder AddMvc(this IServiceCollection services, Action<MvcOptions> action = null)
        {
            return Microsoft.Extensions.DependencyInjection.MvcServiceCollectionExtensions.AddMvc(services,
                options => { action?.Invoke(options); });
        }

        /// <summary>
        /// 设置mvc的返回值为Json格式
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mvcOptionAction"></param>
        /// <param name="mvcJsonOptionAction"></param>
        public static IMvcBuilder AddMvcJson(this IServiceCollection services,
            Action<MvcOptions> mvcOptionAction = null,
            Action<MvcJsonOptions> mvcJsonOptionAction = null)
        {
            return AddMvc(services, options => { mvcOptionAction?.Invoke(options); })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    mvcJsonOptionAction?.Invoke(options);
                });
        }

        #endregion
    }
}
