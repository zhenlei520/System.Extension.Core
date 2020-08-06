// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    ///
    /// </summary>
    public class TimeSpanFormatDateType : Enumeration
    {
        /// <summary>
        /// {0}天{1}小时{2}分钟{3}秒
        /// </summary>
        public static TimeSpanFormatDateType Zero = new TimeSpanFormatDateType(1, "{0}天{1}小时{2}分钟{3}秒");

        /// <summary>
        /// {0}天-{1}小时-{2}分钟-{3}秒
        /// </summary>
        public static TimeSpanFormatDateType One = new TimeSpanFormatDateType(2, "{0}天-{1}小时-{2}分钟-{3}秒");

        /// <summary>
        /// {0}天-{1}小时-{2}分钟-{3}秒
        /// </summary>
        public static TimeSpanFormatDateType Two = new TimeSpanFormatDateType(3, "{0}天/{1}小时/{2}分钟/{3}秒");

        /// <summary>
        /// {0}天-{1}小时-{2}分钟-{3}秒
        /// </summary>
        public static TimeSpanFormatDateType Three = new TimeSpanFormatDateType(4, "{0}天{1}小时{2}分钟{3}秒{4}毫秒");

        /// <summary>
        /// {0}天-{1}小时-{2}分钟-{3}秒-{4}毫秒
        /// </summary>
        public static TimeSpanFormatDateType Four = new TimeSpanFormatDateType(5, "{0}天-{1}小时-{2}分钟-{3}秒-{4}毫秒");

        /// <summary>
        /// {0}天/{1}小时/{2}分钟/{3}秒/{4}毫秒
        /// </summary>
        public static TimeSpanFormatDateType Five = new TimeSpanFormatDateType(6, "{0}天/{1}小时/{2}分钟/{3}秒/{4}毫秒");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public TimeSpanFormatDateType(int id, string name) : base(id, name)
        {
        }
    }
}
