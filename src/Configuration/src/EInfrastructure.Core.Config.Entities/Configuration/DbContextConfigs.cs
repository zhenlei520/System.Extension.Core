// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Config.Entities.Configuration
{
    /// <summary>
    /// 配置
    /// </summary>
    public class DbContextConfigs
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        public List<DbContextConfigItemOptions> Configurations { get; set; }
    }

    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbContextConfigItemOptions : DbContextConfigOptions
    {
        /// <summary>
        /// 数据库完成类名
        /// </summary>
        public string FullName { get; set; }
    }
}
