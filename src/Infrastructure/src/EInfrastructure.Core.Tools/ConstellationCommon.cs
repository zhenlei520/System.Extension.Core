// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.EnumerationExtensions;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 星座帮助类
    /// </summary>
    public class ConstellationCommon
    {
        /// <summary>
        /// 星座map
        /// </summary>
        private static readonly List<ConstellationMap> ConstellationMaps = new List<ConstellationMap>
        {
            new ConstellationMap
            {
                Key = Constellation.Aquarius,
                Value = "水瓶座",
                MinTime = 1.20F,
                MaxTime = 2.19F
            },
            new ConstellationMap
            {
                Key = Constellation.Pisces,
                Value = "双鱼座",
                MinTime = 2.19F,
                MaxTime = 3.21F
            },
            new ConstellationMap
            {
                Key = Constellation.Aries,
                Value = "白羊座",
                MinTime = 3.21F,
                MaxTime = 4.20F
            },
            new ConstellationMap
            {
                Key = Constellation.Taurus,
                Value = "金牛座",
                MinTime = 4.20F,
                MaxTime = 5.21F
            },
            new ConstellationMap
            {
                Key = Constellation.Gemini,
                Value = "双子座",
                MinTime = 5.21F,
                MaxTime = 6.22F
            },
            new ConstellationMap
            {
                Key = Constellation.Cancer,
                Value = "巨蟹座",
                MinTime = 6.22F,
                MaxTime = 7.23F
            },
            new ConstellationMap
            {
                Key = Constellation.Leo,
                Value = "狮子座",
                MinTime = 7.23F,
                MaxTime = 8.23F
            },
            new ConstellationMap
            {
                Key = Constellation.Virgo,
                Value = "处女座",
                MinTime = 8.23F,
                MaxTime = 9.23F
            },
            new ConstellationMap
            {
                Key = Constellation.Libra,
                Value = "天秤座",
                MinTime = 9.23F,
                MaxTime = 10.24F
            },
            new ConstellationMap
            {
                Key = Constellation.Scorpio,
                Value = "天蝎座",
                MinTime = 10.24F,
                MaxTime = 11.23F
            },
            new ConstellationMap
            {
                Key = Constellation.Sagittarius,
                Value = "射手座",
                MinTime = 11.23F,
                MaxTime = 12.22F
            },
            new ConstellationMap
            {
                Key = Constellation.Capricornus,
                Value = "魔羯座",
                MinTime = 12.22F,
                MaxTime = 12.32F
            },
            new ConstellationMap
            {
                Key = Constellation.Capricornus,
                Value = "魔羯座",
                MinTime = 1.01F,
                MaxTime = 1.20F
            }
        };

        #region 根据日期得到星座名称

        /// <summary>
        /// 根据日期得到星座名称
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <returns></returns>
        public static string GetConstellationFromBirthday(DateTime? birthday)
        {
            if (birthday == null)
            {
                return "未知";
            }

            float fBirthDay = Convert.ToSingle(birthday.Value.ToString("M.dd"));

            return ConstellationMaps.Where(x => fBirthDay >= x.MinTime && fBirthDay < x.MaxTime).Select(x => x.Value)
                .FirstOrDefault();
        }

        #endregion

        #region 根据日期得到星座枚举

        /// <summary>
        /// 根据日期得到星座枚举
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <returns></returns>
        public static Constellation GetConstellationEnumFromBirthday(DateTime? birthday)
        {
            if (birthday == null)
            {
                return Constellation.Unknow;
            }

            float fBirthDay = Convert.ToSingle(birthday.Value.ToString("M.dd"));
            return ConstellationMaps.Where(x => fBirthDay >= x.MinTime && fBirthDay < x.MaxTime)
                .Select(x => x.Key).FirstOrDefault();
        }

        #endregion

        /// <summary>
        /// 星座
        /// </summary>
        private class ConstellationMap
        {
            /// <summary>
            /// 内容
            /// </summary>
            public string Value { get; set; }

            /// <summary>
            /// 星座
            /// </summary>
            public Constellation Key { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public float MinTime { get; set; }

            /// <summary>
            /// 截至时间
            /// </summary>
            public float MaxTime { get; set; }
        }
    }
}
