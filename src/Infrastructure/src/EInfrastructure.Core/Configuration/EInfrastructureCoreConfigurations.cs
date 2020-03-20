// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration
{
    /// <summary>
    /// 基础配置
    /// </summary>
    internal static class EInfrastructureCoreConfigurations
    {
        /// <summary>
        /// 设置是否启用日志
        /// </summary>
        /// <param name="enableLog">启用日志</param>
        /// <returns></returns>
        internal static void SetLog(bool enableLog)
        {
            EnableLog = enableLog;
        }

        /// <summary>
        /// 是否启用日志
        /// </summary>
        internal static bool EnableLog { get; private set; }
    }
}
