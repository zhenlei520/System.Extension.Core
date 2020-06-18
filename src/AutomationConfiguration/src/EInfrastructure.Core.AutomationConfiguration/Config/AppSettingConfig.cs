// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.AutomationConfiguration.Config
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class AppSettingConfig
    {
        /// <summary>
        /// 默认配置文件名称（相对路径）
        /// </summary>
        public string DefaultPath { get; set; }

        /// <summary>
        /// 指定路径
        /// key为文件key，value为配置文件路径
        /// 如果未发现文件，则查询默认配置文件
        /// </summary>
        public List<KeyValuePair<string, string>> Maps { get; set; }
    }
}
