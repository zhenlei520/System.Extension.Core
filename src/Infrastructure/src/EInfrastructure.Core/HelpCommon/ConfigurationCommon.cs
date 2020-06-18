// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using Microsoft.Extensions.Configuration;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class ConfigurationCommon
    {
        #region 创建数据源

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IConfigurationBuilder CreateConfigurationBuilder(Action<IConfigurationBuilder> action = null)
        {
            return CreateConfigurationBuilder(true, action);
        }

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="isUseEnvironment">是否支持读取环境变量</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IConfigurationBuilder CreateConfigurationBuilder(bool isUseEnvironment,
            Action<IConfigurationBuilder> action = null)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var builder = isUseEnvironment ? configurationBuilder.AddEnvironmentVariables() : configurationBuilder;
            action?.Invoke(builder);
            return configurationBuilder;
        }

        #endregion

        #region 使用自定义配置读取json配置（自行设置根目录）

        /// <summary>
        /// 使用自定义配置读取json配置（自行设置根目录）
        /// </summary>
        /// <param name="configurationBuilder"></param>
        /// <param name="configPathList">自定义配置列表</param>
        /// <param name="isOptional">文件是否可选</param>
        /// <param name="reloadOnChange">修改后重载</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseAppsettings(this IConfigurationBuilder configurationBuilder,
            List<string> configPathList = null, bool isOptional = true,
            bool reloadOnChange = true)
        {
            if (configPathList == null)
            {
                configPathList = new List<string>();
            }

            if (configPathList.Count == 0)
            {
                throw new BusinessException<string>("配置文件不能为空", HttpStatus.NoFind.Name);
            }

            configPathList.ForEach(configPath =>
            {
                configurationBuilder.AddJsonFile(configPath, isOptional, reloadOnChange);
            });
            return configurationBuilder;
        }

        #endregion
    }
}
