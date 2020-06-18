// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.HelpCommon;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Exception
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Ext
    {
        #region 设置Json配置

        /// <summary>
        /// 设置Json配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action">拦截器，默认无</param>
        public static void SetJsonOption(this IServiceCollection services, Action<MvcOptions> action = null)
        {
            services.AddMvc(options => { action?.Invoke(options); })
                .AddJsonOptions((options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                }));
        }

        #endregion

        #region 使用自定义配置

        /// <summary>
        /// 使用自定义配置
        /// </summary>
        /// <param name="env">环境信息</param>
        /// <param name="useEnvironment">是否使用环境变量</param>
        /// <param name="configPathList">自定义配置列表</param>
        /// <param name="isOptional"></param>
        /// <param name="reloadOnChange"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAppsettings(this IHostingEnvironment env, bool useEnvironment = true,
            List<string> configPathList = null, bool isOptional = true, bool reloadOnChange = true)
        {
            return ConfigurationCommon.CreateConfigurationBuilder(configurationBuilder =>
            {
                List<string> pathList = new List<string>();
                if (useEnvironment)
                {
                    pathList.Add($"appsettings.{env.EnvironmentName}.json");
                }

                pathList.AddRange(configPathList ?? new List<string>());
                configurationBuilder.SetBasePath(env.ContentRootPath)
                    .UseAppsettings(pathList, isOptional, reloadOnChange);
            });
        }

        /// <summary>
        /// 使用自定义配置
        /// </summary>
        /// <param name="env">环境信息</param>
        /// <param name="useEnvironment">是否使用环境变量</param>
        /// <param name="configPathList">自定义配置列表</param>
        /// <param name="isOptional"></param>
        /// <param name="reloadOnChange"></param>
        /// <returns></returns>
        public static IConfigurationRoot UseAppsettings(this IHostingEnvironment env, bool useEnvironment = true,
            List<string> configPathList = null, bool isOptional = true, bool reloadOnChange = true)
        {
            return env.AddAppsettings(useEnvironment, configPathList, isOptional, reloadOnChange).Build();
        }

        #endregion
    }
}
