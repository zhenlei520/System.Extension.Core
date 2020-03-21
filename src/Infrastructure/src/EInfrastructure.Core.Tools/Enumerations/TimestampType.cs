// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Tools.Enumerations
{
    /// <summary>
    /// 时间戳类型
    /// </summary>
    public class TimestampType : Enumeration
    {
        /// <summary>
        /// 十位时间戳 秒
        /// </summary>
        public static TimestampType Second = new TimestampType(1, "十位时间戳");

        /// <summary>
        /// 十三位时间戳 毫秒
        /// </summary>
        public static TimestampType Millisecond = new TimestampType(2, "十三位时间戳");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public TimestampType(int id, string name) : base(id, name)
        {
        }
    }
}
