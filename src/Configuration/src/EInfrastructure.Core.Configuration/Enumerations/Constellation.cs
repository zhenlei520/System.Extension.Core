// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 星座
    /// </summary>
    public class Constellation : Enumeration
    {
        /// <summary>
        /// 白羊座
        /// </summary>
        public static Constellation Aries = new Constellation(1, "白羊座");

        /// <summary>
        /// 金牛座
        /// </summary>
        public static Constellation Taurus = new Constellation(2, "金牛座");

        /// <summary>
        /// 双子座
        /// </summary>
        public static Constellation Gemini = new Constellation(3, "双子座");

        /// <summary>
        /// 巨蟹座
        /// </summary>
        public static Constellation Cancer = new Constellation(4, "巨蟹座");

        /// <summary>
        /// 狮子座
        /// </summary>
        public static Constellation Leo = new Constellation(5, "狮子座");

        /// <summary>
        /// 处女座
        /// </summary>
        public static Constellation Virgo = new Constellation(6, "处女座");

        /// <summary>
        /// 天秤座
        /// </summary>
        public static Constellation Libra = new Constellation(7, "天秤座");

        /// <summary>
        /// 天蝎座
        /// </summary>
        public static Constellation Scorpio = new Constellation(8, "天蝎座");

        /// <summary>
        /// 射手座
        /// </summary>
        public static Constellation Sagittarius = new Constellation(9, "射手座");

        /// <summary>
        /// 摩羯座
        /// </summary>
        public static Constellation Capricornus = new Constellation(10, "摩羯座");

        /// <summary>
        /// 水瓶座
        /// </summary>
        public static Constellation Aquarius = new Constellation(11, "水瓶座");

        /// <summary>
        /// 双鱼座
        /// </summary>
        public static Constellation Pisces = new Constellation(12, "双鱼座");

        /// <summary>
        /// 未知
        /// </summary>
        public static Constellation Unknow = new Constellation(13, "未知");


        /// <summary>
        ///
        /// </summary>
        /// <param name="id">星座id</param>
        /// <param name="name">星座描述</param>
        public Constellation(int id, string name) : base(id, name)
        {
        }
    }
}
