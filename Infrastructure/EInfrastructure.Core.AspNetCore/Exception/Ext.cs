using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
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
        public static void SetJsonOption(this IServiceCollection services)
        {
            services.AddMvc()
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
        /// <returns></returns>
        public static IConfigurationRoot UseAppsettings(this IHostingEnvironment env, bool useEnvironment = true,
            List<string> configPathList = null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            if (useEnvironment)
            {
                builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();
            }

            if (configPathList == null)
            {
                configPathList = new List<string>();
            }

            configPathList.ForEach(configPath => { builder.AddJsonFile(configPath, true, true); });

            var config = builder.Build();

            return config;
        }

        #endregion
    }
}