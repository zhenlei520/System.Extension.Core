// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.CustomConfiguration.Core.Internal;

namespace EInfrastructure.Core.CustomConfiguration.MySql
{
    /// <summary>
    ///
    /// </summary>
    public static class CustomConfigurationExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="pre">数据库表前缀</param>
        /// <returns></returns>
        public static CustomConfigurationOptions UseMySql(this CustomConfigurationOptions options,
            string connectionString, string pre = "")
        {
            return options.UseMySql(opt =>
            {
                opt.DbConn = connectionString;
                opt.Pre = pre;
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static CustomConfigurationOptions UseMySql(this CustomConfigurationOptions options,
            Action<ConfigurationMySqlOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            options.RegisterExtension(new MySqlCustomConfigurationOptionsExtension(configure));

            return options;
        }
    }
}
