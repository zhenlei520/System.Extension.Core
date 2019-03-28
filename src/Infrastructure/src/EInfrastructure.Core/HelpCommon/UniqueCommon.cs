// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 唯一方法实现
    /// </summary>
    public class UniqueCommon
    {
        #region 全局唯一Guid

        /// <summary>
        /// 全局唯一Guid
        /// </summary>
        public static string Guids()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        #endregion
    }
}