// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Enumeration
{
    /// <summary>
    /// 格式化时间类型
    /// </summary>
    public class FormatDateType : SeedWork.Enumeration
    {
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public static FormatDateType Zero = new FormatDateType(0, "yyyy-MM-dd");

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static FormatDateType One = new FormatDateType(1, "yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 格式：yyyy/MM/dd
        /// </summary>
        public static FormatDateType Two = new FormatDateType(2, "yyyy/MM/dd");

        /// <summary>
        /// 格式：yyyy年MM月dd日
        /// </summary>
        public static FormatDateType Three = new FormatDateType(3, "yyyy年MM月dd日");

        /// <summary>
        /// 格式：MM-dd
        /// </summary>
        public static FormatDateType Four = new FormatDateType(4, "MM-dd");

        /// <summary>
        /// 格式：MM/dd
        /// </summary>
        public static FormatDateType Five = new FormatDateType(5, "MM/dd");

        /// <summary>
        /// 格式：MM月dd日
        /// </summary>
        public static FormatDateType Six = new FormatDateType(6, "MM月dd日");

        /// <summary>
        /// 格式：yyyy-MM
        /// </summary>
        public static FormatDateType Seven = new FormatDateType(7, "yyyy-MM");

        /// <summary>
        /// 格式：yyyy/MM
        /// </summary>
        public static FormatDateType Eight = new FormatDateType(8, "yyyy/MM");

        /// <summary>
        /// yyyy年MM月
        /// </summary>
        public static FormatDateType Nine = new FormatDateType(9, "yyyy年MM月");

        public FormatDateType(int id, string name) : base(id, name)
        {
        }
    }
}
