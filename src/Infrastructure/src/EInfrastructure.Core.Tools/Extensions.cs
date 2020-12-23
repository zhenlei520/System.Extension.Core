// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools.Configuration;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 扩展信息
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        ///
        /// </summary>
        static Extensions()
        {
            InitConstellationData();
            InitDate();
            InitDateTimeProvider();
            InitValidate();
            InitSecurity();
        }

        #region 初始化星座
        /// <summary>
        /// 星座map
        /// </summary>
        private static List<Constellations> _constellationMaps;

        /// <summary>
        /// 初始化星座
        /// </summary>
        private static void InitConstellationData()
        {
            _constellationMaps = new List<Constellations>
            {
                new Constellations
                {
                    Key = Constellation.Aquarius,
                    Value = "水瓶座",
                    MinTime = 1.20F,
                    MaxTime = 2.19F
                },
                new Constellations
                {
                    Key = Constellation.Pisces,
                    Value = "双鱼座",
                    MinTime = 2.19F,
                    MaxTime = 3.21F
                },
                new Constellations
                {
                    Key = Constellation.Aries,
                    Value = "白羊座",
                    MinTime = 3.21F,
                    MaxTime = 4.20F
                },
                new Constellations
                {
                    Key = Constellation.Taurus,
                    Value = "金牛座",
                    MinTime = 4.20F,
                    MaxTime = 5.21F
                },
                new Constellations
                {
                    Key = Constellation.Gemini,
                    Value = "双子座",
                    MinTime = 5.21F,
                    MaxTime = 6.22F
                },
                new Constellations
                {
                    Key = Constellation.Cancer,
                    Value = "巨蟹座",
                    MinTime = 6.22F,
                    MaxTime = 7.23F
                },
                new Constellations
                {
                    Key = Constellation.Leo,
                    Value = "狮子座",
                    MinTime = 7.23F,
                    MaxTime = 8.23F
                },
                new Constellations
                {
                    Key = Constellation.Virgo,
                    Value = "处女座",
                    MinTime = 8.23F,
                    MaxTime = 9.23F
                },
                new Constellations
                {
                    Key = Constellation.Libra,
                    Value = "天秤座",
                    MinTime = 9.23F,
                    MaxTime = 10.24F
                },
                new Constellations
                {
                    Key = Constellation.Scorpio,
                    Value = "天蝎座",
                    MinTime = 10.24F,
                    MaxTime = 11.23F
                },
                new Constellations
                {
                    Key = Constellation.Sagittarius,
                    Value = "射手座",
                    MinTime = 11.23F,
                    MaxTime = 12.22F
                },
                new Constellations
                {
                    Key = Constellation.Capricornus,
                    Value = "魔羯座",
                    MinTime = 12.22F,
                    MaxTime = 12.32F
                },
                new Constellations
                {
                    Key = Constellation.Capricornus,
                    Value = "魔羯座",
                    MinTime = 1.01F,
                    MaxTime = 1.20F
                }
            };
        }

        #endregion

    }
}
