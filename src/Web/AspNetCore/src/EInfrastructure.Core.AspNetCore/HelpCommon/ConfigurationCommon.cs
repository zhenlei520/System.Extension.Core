// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.AspNetCore.HelpCommon
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class ConfigurationCommon
    {
        #region 创建数据源

        /// <summary>
        /// 创建数据源（未设置根目录）
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Microsoft.Extensions.Configuration.IConfigurationBuilder CreateConfigurationBuilder(
            Action<Microsoft.Extensions.Configuration.IConfigurationBuilder> action = null)
        {
            return CreateConfigurationBuilder(true, "", action);
        }

        /// <summary>
        /// 创建数据源（未设置根目录）
        /// </summary>
        /// <param name="isUseEnvironment">是否支持读取环境变量</param>
        /// <param name="prefix">环境变量前缀，默认为空</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Microsoft.Extensions.Configuration.IConfigurationBuilder CreateConfigurationBuilder(
            bool isUseEnvironment, string prefix = "",
            Action<Microsoft.Extensions.Configuration.IConfigurationBuilder> action = null)
        {
            var configurationBuilder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            var builder = configurationBuilder;
            if (isUseEnvironment)
            {
                builder = string.IsNullOrEmpty(prefix)
                    ? (Microsoft.Extensions.Configuration.ConfigurationBuilder) (Microsoft.Extensions.Configuration
                        .EnvironmentVariablesExtensions.AddEnvironmentVariables(configurationBuilder))
                    : (Microsoft.Extensions.Configuration.ConfigurationBuilder) (Microsoft.Extensions.Configuration
                        .EnvironmentVariablesExtensions.AddEnvironmentVariables(configurationBuilder, prefix));
            }

            action?.Invoke(builder);
            return configurationBuilder;
        }

        #endregion

        #region 添加本地配置文件（仅支持json）

        /// <summary>
        /// 添加本地配置文件（仅支持json）
        /// </summary>
        /// <param name="basePath">根目录</param>
        /// <param name="configPath">配置文件</param>
        /// <param name="isOptional">是否必须</param>
        /// <param name="reloadOnChange">是否监听更改</param>
        /// <returns></returns>
        public static Microsoft.Extensions.Configuration.IConfigurationBuilder AddJsonAppsettings(string basePath,
            string configPath, bool isOptional = true, bool reloadOnChange = true)
        {
            return ConfigurationCommon.CreateConfigurationBuilder(configurationBuilder =>
            {
                if (!string.IsNullOrEmpty(configPath))
                {
                    var builder = Microsoft.Extensions.Configuration.FileConfigurationExtensions
                        .SetBasePath(configurationBuilder, basePath);
                    Microsoft.Extensions.Configuration.JsonConfigurationExtensions.AddJsonFile(builder, configPath,
                        isOptional, reloadOnChange);
                }
            });
        }

        /// <summary>
        /// 添加本地配置文件（仅支持json）
        /// </summary>
        /// <param name="isOptional">是否必须</param>
        /// <param name="reloadOnChange">是否监听更改</param>
        /// <returns></returns>
        public static Microsoft.Extensions.Configuration.IConfigurationBuilder AddDefaultJsonAppsettings(
            bool isOptional = true,
            bool reloadOnChange = true)
        {
            return AddJsonAppsettings(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json", isOptional,
                reloadOnChange);
        }

        #endregion
    }
}