// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
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
            return CreateConfigurationBuilder(true, "", action);
        }

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="isUseEnvironment">是否支持读取环境变量</param>
        /// <param name="prefix">环境变量前缀，默认为空</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IConfigurationBuilder CreateConfigurationBuilder(bool isUseEnvironment, string prefix = "",
            Action<IConfigurationBuilder> action = null)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var builder = configurationBuilder;
            if (isUseEnvironment)
            {
                builder = string.IsNullOrEmpty(prefix)
                    ? (ConfigurationBuilder) configurationBuilder.AddEnvironmentVariables()
                    : (ConfigurationBuilder) configurationBuilder.AddEnvironmentVariables(prefix);
            }

            action?.Invoke(builder);
            return configurationBuilder;
        }

        #endregion
    }
}
