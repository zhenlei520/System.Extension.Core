// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Interface.Storage.Enum
{
    /// <summary>
    /// 回调内容类型
    /// </summary>
    public enum CallbackBodyTypeEnum
    {
        /// <summary>
        /// application/json
        /// </summary>
        [Description("application/json")] Json = 1,

        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        [Description("application/x-www-form-urlencoded")]
        Urlencoded = 2
    }
}