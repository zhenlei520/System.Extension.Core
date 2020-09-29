// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools.Expressions
{
    /// <summary>
    /// 委托扩展
    /// </summary>
    public static class FuncBuilder
    {
        #region 判断时间是否在开始时间与结束时间之前

        /// <summary>
        /// 判断时间是否在开始时间与结束时间之前
        /// </summary>
        /// <param name="time">待判断的时间</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static bool Between(this DateTime time, DateTime startTime, DateTime endTime)
        {
            return time >= startTime && time <= endTime;
        }

        #endregion
    }
}
