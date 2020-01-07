// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Config.IdentificationExtensions.Enum
{
    /// <summary>
    /// 违规状态
    /// </summary>
    public enum ViolationStatusEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")] UnKnow = -1,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")] Normal = 0,

        /// <summary>
        /// 违规
        /// </summary>
        [Description("违规")] Violation = 1
    }
}