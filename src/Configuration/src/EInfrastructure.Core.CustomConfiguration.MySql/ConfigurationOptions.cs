// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.CustomConfiguration.MySql
{
    /// <summary>
    /// 配置
    /// </summary>
    public class ConfigurationMySqlOptions
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public string Pre { get; set; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public string DbConn { get; set; }
    }
}
