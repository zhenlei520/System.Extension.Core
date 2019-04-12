// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Configuration.Enum
{
    /// <summary>
    /// 星期
    /// </summary>
    public enum WeekEnum
    {
        /// <summary>
        /// Sunday
        /// </summary>
        [Description("周日")] Sunday = 0,

        /// <summary>
        /// Monday
        /// </summary>
        [Description("周一")] Monday = 1,

        /// <summary>
        /// Tuesday
        /// </summary>
        [Description("周二")] Tuesday = 2,

        /// <summary>
        /// Wednesday
        /// </summary>
        [Description("周三")] Wednesday = 3,

        /// <summary>
        /// Thursday
        /// </summary>
        [Description("周四")] Thursday = 4,

        /// <summary>
        /// Friday
        /// </summary>
        [Description("周五")] Friday = 5,

        /// <summary>
        /// Saturday
        /// </summary>
        [Description("周六")] Saturday = 6
    }
}
