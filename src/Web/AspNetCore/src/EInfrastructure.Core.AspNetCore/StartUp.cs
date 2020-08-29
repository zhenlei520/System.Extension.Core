// Copyright (c) zhenlei520 All rights reserved.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        private static void Run(this IServiceCollection services)
        {
            if (!_isStartUp)
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            services.Run();
            return services;
        }

        #endregion

        #region 设置Json配置

        /// <summary>
        /// 设置Json配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action">拦截器，默认无</param>
        public static IMvcBuilder AddMvc(this IServiceCollection services, Action<Microsoft.AspNetCore.Mvc.MvcOptions> action = null)
        {
            return Microsoft.Extensions.DependencyInjection.MvcServiceCollectionExtensions.AddMvc(services,
                options => { action?.Invoke(options); });
        }

        /// <summary>
        /// 设置mvc的返回值为Json格式
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mvcOptionAction"></param>
        public static IMvcBuilder AddMvcJson(this IServiceCollection services,
            Action<Microsoft.AspNetCore.Mvc.MvcOptions> mvcOptionAction = null)
        {
            var mvcBuilder = AddMvc(services, options => { mvcOptionAction?.Invoke(options); });
#if NETCOREAPP3_1 || NETCOREAPP3_0
            return mvcBuilder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
#else
            return mvcBuilder.AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // mvcOptionAction?.Invoke(options);
            });
#endif
        }

        #endregion
    }
}