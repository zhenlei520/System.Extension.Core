// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 格式化时间类型
    /// </summary>
    public class FormatDateType : Enumeration
    {
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public static FormatDateType Zero = new FormatDateType(1, "yyyy-MM-dd");

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static FormatDateType One = new FormatDateType(2, "yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 格式：yyyy/MM/dd
        /// </summary>
        public static FormatDateType Two = new FormatDateType(3, "yyyy/MM/dd");

        /// <summary>
        /// 格式：yyyy年MM月dd日
        /// </summary>
        public static FormatDateType Three = new FormatDateType(4, "yyyy年MM月dd日");

        /// <summary>
        /// 格式：MM-dd
        /// </summary>
        public static FormatDateType Four = new FormatDateType(5, "MM-dd");

        /// <summary>
        /// 格式：MM/dd
        /// </summary>
        public static FormatDateType Five = new FormatDateType(6, "MM/dd");

        /// <summary>
        /// 格式：MM月dd日
        /// </summary>
        public static FormatDateType Six = new FormatDateType(7, "MM月dd日");

        /// <summary>
        /// 格式：yyyy-MM
        /// </summary>
        public static FormatDateType Seven = new FormatDateType(8, "yyyy-MM");

        /// <summary>
        /// 格式：yyyy/MM
        /// </summary>
        public static FormatDateType Eight = new FormatDateType(9, "yyyy/MM");

        /// <summary>
        /// yyyy年MM月
        /// </summary>
        public static FormatDateType Nine = new FormatDateType(10, "yyyy年MM月");

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        public static FormatDateType Ten = new FormatDateType(11, "yyyy-MM-dd HH:mm:ss.fff");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        public FormatDateType(int id, string name) : base(id, name)
        {
        }
    }
}
