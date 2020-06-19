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
        /// 使用自定义配置（默认读取json，不支持其他格式文件）
        /// </summary>
        /// <param name="env">环境信息</param>
        /// <param name="configPath">配置文件</param>
        /// <param name="isOptional">文件必须存在</param>
        /// <param name="reloadOnChange">监听文件更改</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAppsettings(this IHostingEnvironment env,
            string configPath, bool isOptional = true, bool reloadOnChange = true)
        {
            return ConfigurationCommon.CreateConfigurationBuilder(configurationBuilder =>
            {
                if (!string.IsNullOrEmpty(configPath))
                {
                    configurationBuilder.SetBasePath(env.ContentRootPath)
                        .AddJsonFile(configPath, isOptional, reloadOnChange);
                }
            });
        }

        /// <summary>
        /// 使用默认环境配置，appsettings.EnvironmentName.json
        /// </summary>
        /// <param name="env">环境信息</param>
        /// <param name="isOptional">文件必须存在</param>
        /// <param name="reloadOnChange">监听文件更改</param>
        /// <returns></returns>
        public static IConfigurationRoot UseDefaultAppsettings(this IHostingEnvironment env, bool isOptional = true,
            bool reloadOnChange = true)
        {
            return env.AddAppsettings($"appsettings.{env.EnvironmentName}.json", isOptional, reloadOnChange)
                .Build();
        }

        #endregion
    }
}
