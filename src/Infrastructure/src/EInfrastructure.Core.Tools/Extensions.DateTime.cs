// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Common;
using EInfrastructure.Core.Tools.Component;
using EInfrastructure.Core.Tools.Enumerations;
using EInfrastructure.Core.Tools.Internal;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 时间扩展
    /// </summary>
    public partial class Extensions
    {
        #region 初始化

        #region 初始化中国节日

        private static ChineseLunisolarCalendar _china;
        private static Hashtable _gHoliday;
        private static Hashtable _nHoliday;

        /// <summary>
        /// 初始化中国节日
        /// </summary>
        private static void InitChinaDate()
        {
            _china = new ChineseLunisolarCalendar();

            //公历节日
            _gHoliday = new Hashtable
            {
                {"0101", "元旦"},
                {"0214", "情人节"},
                {"0305", "雷锋日"},
                {"0308", "妇女节"},
                {"0312", "植树节"},
                {"0315", "消费者权益日"},
                {"0401", "愚人节"},
                {"0501", "劳动节"},
                {"0504", "青年节"},
                {"0601", "儿童节"},
                {"0701", "建党节"},
                {"0801", "建军节"},
                {"0910", "教师节"},
                {"1001", "国庆节"},
                {"1224", "平安夜"},
                {"1225", "圣诞节"}
            };

            //农历节日
            _nHoliday = new Hashtable
            {
                {"0101", "春节"},
                {"0115", "元宵节"},
                {"0505", "端午节"},
                {"0815", "中秋节"},
                {"0909", "重阳节"},
                {"1208", "腊八节"}
            };
        }

        #endregion

        #region 初始化星期几

        private static List<KeyValuePair<DayOfWeek, int>> _weekMaps;

        /// <summary>
        /// 初始化星期几
        /// </summary>
        private static void InitWeek()
        {
            _weekMaps = new List<KeyValuePair<DayOfWeek, int>>()
            {
                new KeyValuePair<DayOfWeek, int>(DayOfWeek.Sunday, 1),
                new KeyValuePair<DayOfWeek, int>(DayOfWeek.Monday, 2),
                new KeyValuePair<DayOfWeek, int>(DayOfWeek.Tuesday, 3),
                new KeyValuePair<DayOfWeek, int>(DayOfWeek.Wednesday, 4),
                new KeyValuePair<DayOfWeek, int>(DayOfWeek.Thursday, 5),
                new KeyValuePair<DayOfWeek, int>(DayOfWeek.Friday, 6),
                new KeyValuePair<DayOfWeek, int>(DayOfWeek.Saturday, 7)
            };
        }

        #endregion

        #region 初始化提供者

        private static IEnumerable<IDateTimeProvider> _dateTimeProviders;

        private static IEnumerable<ISpecifiedTimeAfterProvider> _specifiedTimeAfterProviders;

        /// <summary>
        /// 初始化提供者
        /// </summary>
        private static void InitDateTimeProvider()
        {
            _dateTimeProviders = ServiceProvider.GetServiceProvider().GetServices<IDateTimeProvider>();
            _specifiedTimeAfterProviders =
                ServiceProvider.GetServiceProvider().GetServices<ISpecifiedTimeAfterProvider>();
            Calendar = new ChineseLunisolarCalendar();
        }

        #endregion

        #endregion

        #region 获得两个日期的间隔

        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="dateTime1">日期一(较大一点的时间)。</param>
        /// <param name="dateTime2">日期二(较小一点的时间)。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff(this DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1 - dateTime2;
        }

        #endregion

        #region 格式化日期时间

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="dateMode">显示模式</param>
        /// <returns>0-9种模式的日期</returns>
        public static string FormatDate(this DateTime dateTime, FormatDateType dateMode = null)
        {
            if (dateMode == null)
            {
                dateMode = FormatDateType.One;
            }

            return dateTime.ToString(dateMode.Name);
        }

        #endregion

        #region 得到随机日期

        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(this DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime;

            var ts = new TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds;

            if (dTotalSecontds > int.MaxValue)
            {
                iTotalSecontds = int.MaxValue;
            }
            else if (dTotalSecontds < int.MinValue)
            {
                iTotalSecontds = int.MinValue;
            }
            else
            {
                iTotalSecontds = (int) dTotalSecontds;
            }


            if (iTotalSecontds > 0)
            {
                minTime = time2;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= int.MinValue)
                maxValue = int.MinValue + 1;

            int i = random.Next(Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }

        #endregion

        #region 得到月初/月末/本周一/本周日/本季初/本季末/年初/年末时间

        /// <summary>
        /// 得到月初/月末/本周一/本周日/本季初/本季末/年初/年末时间
        /// </summary>
        /// <param name="dateTime">指定时间</param>
        /// <param name="timeKey">时间Key</param>
        /// <returns></returns>
        public static DateTime Get(this DateTime dateTime, TimeType timeKey)
        {
            var provider = _dateTimeProviders.Where(x => x.Type.Equals(timeKey)).OrderByDescending(x => x.GetWeights())
                .FirstOrDefault();

            if (provider != null)
            {
                return provider.GetResult(dateTime);
            }

            throw new NotSupportedException(nameof(timeKey));
        }

        #endregion

        #region 得到指定的时间后

        /// <summary>
        /// 得到指定的时间后
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="timeType">时间类型</param>
        /// <param name="duration">时长，允许为负数,为正时：指定时间后持续时间，为负时：指定时间前持续时间</param>
        /// <returns></returns>
        public static DateTimeOffset GetSpecifiedTimeAfter(this DateTime? dateTime, DurationType timeType, int duration)
        {
            DateTime date = dateTime ?? DateTime.Now.Date; //当前时间

            var provider = _specifiedTimeAfterProviders.Where(x => x.Type.Equals(timeType))
                .OrderByDescending(x => x.GetWeights()).FirstOrDefault();

            if (provider != null)
            {
                var res = provider.GetResult(date, duration);
                return res.ConvertToDateTimeOffset();
            }

            throw new NotSupportedException(nameof(timeType));
        }

        #endregion

        #region 转换为DateTimeOffset

        /// <summary>
        /// 转换为DateTimeOffset
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ConvertToDateTimeOffset(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime() <= DateTimeOffset.MinValue.UtcDateTime
                ? DateTimeOffset.MinValue
                : new DateTimeOffset(dateTime);
        }

        /// <summary>
        /// 转换为DateTimeOffset
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ConvertToDateTimeOffset(this DateTime? dateTime)
        {
            return (dateTime ?? DateTime.Now).ConvertToDateTimeOffset();
        }

        #endregion

        #region 得到距离当前多远时间

        /// <summary>
        /// 得到两个时间间隔差多远
        /// </summary>
        /// <param name="dateTime1">日期一(较大一点的时间)。</param>
        /// <param name="dateTime2">日期二(较小一点的时间)。</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetAccordingToCurrent(this DateTime dateTime1, DateTime dateTime2)
        {
            TimeSpan span = DateDiff(dateTime1, dateTime2);
            if (span == null)
            {
                throw new ArgumentNullException(nameof(span));
            }

            if (span.TotalMinutes < 1)
            {
                return "刚刚";
            }

            if (span.TotalSeconds < 60)
            {
                return "1分钟之前";
            }

            if (span.TotalMinutes < 60)
            {
                return Math.Ceiling(span.TotalMinutes) + "分钟之前";
            }

            if (span.TotalHours < 24)
            {
                return Math.Ceiling(span.TotalHours) + "小时之内";
            }

            if (span.TotalDays < 7)
            {
                return Math.Ceiling(span.TotalDays) + "天之内";
            }

            if (span.TotalDays < 30)
            {
                return Math.Ceiling(span.TotalDays / 7) + "周之内";
            }

            if (span.TotalDays < 180)
            {
                return Math.Ceiling(span.TotalDays / 30) + "月之内";
            }

            return Math.Ceiling(span.TotalDays / 360) + "年之内";
        }

        /// <summary>
        /// 得到据当前多远时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetAccordingToCurrent(this DateTime date)
        {
            TimeSpan span = DateTime.Now - date;
            if (span.TotalMinutes < 1)
            {
                return "刚刚";
            }

            if (span.TotalSeconds < 60)
            {
                return "1分钟之前";
            }

            if (span.TotalMinutes < 60)
            {
                return Math.Ceiling(span.TotalMinutes) + "分钟之前";
            }

            if (span.TotalHours < 24)
            {
                return Math.Ceiling(span.TotalHours) + "小时之内";
            }

            if (span.TotalDays < 7)
            {
                return Math.Ceiling(span.TotalDays) + "天之内";
            }

            if (span.TotalDays < 30)
            {
                return Math.Ceiling(span.TotalDays / 7) + "周之内";
            }

            if (span.TotalDays < 180)
            {
                return Math.Ceiling(span.TotalDays / 30) + "月之内";
            }

            return Math.Ceiling(span.TotalDays / 360) + "年之内";
        }

        /// <summary>
        /// 得到据当前多远时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetAccordingToCurrent(this DateTime? date)
        {
            if (date == null)
                return "未知";
            return GetAccordingToCurrent((DateTime) date);
        }

        #endregion

        #region 阳历转阴历(农历)

        #region 农历信息获取

        private static readonly string[] Jq =
        {
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", "白露", "秋分",
            "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
        };

        private static readonly int[] JqData =
        {
            0, 21208, 43467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989,
            308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758
        };

        #endregion

        #region 阳历转阴历(农历)

        /// <summary>
        /// 阳历转阴历(农历)
        /// </summary>
        /// <param name="dateTime">待转换的日期</param>
        /// <returns></returns>
        public static string ConvertToLunar(this DateTime dateTime)
        {
            if (dateTime > _china.MaxSupportedDateTime || dateTime < _china.MinSupportedDateTime)
            {
                //日期范围：1901 年 2 月 19 日 - 2101 年 1 月 28 日
                throw new Exception(
                    $"日期超出范围！必须在{_china.MinSupportedDateTime:yyyy-MM-dd}到{_china.MaxSupportedDateTime.ToString($"yyyy-MM-dd")}之间！");
            }

            string str = $"{GetYear(dateTime)} {GetMonth(dateTime)}{GetDay(dateTime)}";
            string strJq = GetSolarTerm(dateTime);
            if (strJq != "")
            {
                str += " (" + strJq + ")";
            }

            string strHoliday = GetHoliday(dateTime);
            if (strHoliday != "")
            {
                str += " " + strHoliday;
            }

            string strChinaHoliday = GetChinaHoliday(dateTime);
            if (strChinaHoliday != "")
            {
                str += " " + strChinaHoliday;
            }

            return str;
        }

        #endregion

        #region 获取农历年份

        /// <summary>
        /// 获取农历年份
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetYear(this DateTime dateTime)
        {
            int yearIndex = _china.GetSexagenaryYear(dateTime);
            string yearTG = " 甲乙丙丁戊己庚辛壬癸";
            string yearDZ = " 子丑寅卯辰巳午未申酉戌亥";
            string yearSX = " 鼠牛虎兔龙蛇马羊猴鸡狗猪";
            int year = _china.GetYear(dateTime);
            int yTg = _china.GetCelestialStem(yearIndex);
            int yDz = _china.GetTerrestrialBranch(yearIndex);

            string str = string.Format("[{1}]{2}{3}{0}", year, yearSX[yDz], yearTG[yTg], yearDZ[yDz]);
            return str;
        }

        #endregion

        #region 获取农历月份

        /// <summary>
        /// 获取农历月份
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetMonth(this DateTime dateTime)
        {
            int year = _china.GetYear(dateTime);
            int iMonth = _china.GetMonth(dateTime);
            int leapMonth = _china.GetLeapMonth(year);
            bool isLeapMonth = iMonth == leapMonth;
            if (leapMonth != 0 && iMonth >= leapMonth)
            {
                iMonth--;
            }

            string szText = "正二三四五六七八九十";
            string strMonth = isLeapMonth ? "闰" : "";
            if (iMonth <= 10)
            {
                strMonth += szText.Substring(iMonth - 1, 1);
            }
            else if (iMonth == 11)
            {
                strMonth += "十一";
            }
            else
            {
                strMonth += "腊";
            }

            return strMonth + "月";
        }

        #endregion

        #region 获取农历日期

        /// <summary>
        /// 获取农历日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDay(this DateTime dateTime)
        {
            int iDay = _china.GetDayOfMonth(dateTime);
            string szText1 = "初十廿三";
            string szText2 = "一二三四五六七八九十";
            string strDay;
            if (iDay == 20)
            {
                strDay = "二十";
            }
            else if (iDay == 30)
            {
                strDay = "三十";
            }
            else
            {
                strDay = szText1.Substring((iDay - 1) / 10, 1);
                strDay = strDay + szText2.Substring((iDay - 1) % 10, 1);
            }

            return strDay;
        }

        #endregion

        #region 获取节气

        /// <summary>
        /// 获取节气
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetSolarTerm(this DateTime dateTime)
        {
            DateTime dtBase = new DateTime(1900, 1, 6, 2, 5, 0);
            string strReturn = "";

            var y = dateTime.Year;
            for (int i = 1; i <= 24; i++)
            {
                var num = 525948.76 * (y - 1900) + JqData[i - 1];
                var dtNew = dtBase.AddMinutes(num);
                if (dtNew.DayOfYear == dateTime.DayOfYear)
                {
                    strReturn = Jq[i - 1];
                }
            }

            return strReturn;
        }

        #endregion

        #region 获取公历(阴历)节日

        /// <summary>
        /// 获取公历(阴历)节日
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetHoliday(this DateTime dateTime)
        {
            string strReturn = "";
            object g = _gHoliday[dateTime.Month.ToString("00") + dateTime.Day.ToString("00")];
            if (g != null)
            {
                strReturn = g.ToString();
            }

            return strReturn;
        }

        #endregion

        #region 获取农历节日

        /// <summary>
        /// 获取农历节日
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetChinaHoliday(this DateTime dateTime)
        {
            string strReturn = "";
            int year = _china.GetYear(dateTime);
            int iMonth = _china.GetMonth(dateTime);
            int leapMonth = _china.GetLeapMonth(year);
            int iDay = _china.GetDayOfMonth(dateTime);
            if (_china.GetDayOfYear(dateTime) == _china.GetDaysInYear(year))
            {
                strReturn = "除夕";
            }
            else if (leapMonth != iMonth)
            {
                if (leapMonth != 0 && iMonth >= leapMonth)
                {
                    iMonth--;
                }

                object n = _nHoliday[iMonth.ToString("00") + iDay.ToString("00")];
                if (n != null)
                {
                    if (strReturn == "")
                    {
                        strReturn = n.ToString();
                    }
                    else
                    {
                        strReturn += " " + n;
                    }
                }
            }

            return strReturn;
        }

        #endregion

        #region 阴历-阳历-转换

        /// <summary>
        ///
        /// </summary>
        internal static ChineseLunisolarCalendar Calendar;

        #region 阴历转阳历

        /// <summary>
        /// 阴历转阳历
        /// </summary>
        /// <param name="dateTime">阴历日期</param>
        public static DateTime GetLunarYearDate(this DateTime dateTime)
        {
            return TimeCommon.GetLunarYearDate(dateTime.Year, dateTime.Month, dateTime.Day,
                DateTime.IsLeapYear(dateTime.Year));
        }

        #endregion

        #region 阳历转为阴历

        /// <summary>
        /// 阳历转为阴历
        /// </summary>
        /// <param name="dateTime">公历日期</param>
        /// <returns>农历的日期</returns>
        public static DateTime GetSunYearDate(this DateTime dateTime)
        {
            int year = _china.GetYear(dateTime);
            int iMonth = _china.GetMonth(dateTime);
            int iDay = _china.GetDayOfMonth(dateTime);
            int leapMonth = _china.GetLeapMonth(year);
            if (leapMonth != 0 && iMonth >= leapMonth)
            {
                iMonth--;
            }

            string str = $"{year}-{iMonth}-{iDay}";
            DateTime dtNew = DateTime.Now;
            try
            {
                dtNew = Convert.ToDateTime(str); //防止出现2月份时，会出现超过时间，出现“2015-02-30”这种错误日期
            }
            catch
            {
                // ignored
            }

            return dtNew;
        }

        #endregion

        #endregion

        #endregion

        #region 生成时间戳

        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <param name="target">待转换的时间</param>
        /// <param name="timestampType">时间戳类型：10位或者13位</param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime target, TimestampType timestampType,
            DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            if (timestampType.Id == TimestampType.Millisecond.Id)
            {
                return (TimeZoneInfo.ConvertTimeToUtc(target).Ticks - TimeZoneInfo
                           .ConvertTimeToUtc(new DateTime(1970, 1, 1, 0, 0, 0, 0, dateTimeKind)).Ticks) /
                       10000; //除10000调整为13位
            }

            if (timestampType.Id == TimestampType.Second.Id)
            {
                return (long) (TimeZoneInfo.ConvertTimeToUtc(target) - new DateTime(1970, 1, 1, 0, 0, 0, dateTimeKind))
                    .TotalSeconds;
            }

            throw new BusinessException("不支持的类型", HttpStatus.Err.Id);
        }

        #endregion

        #region 获取当前是周几

        /// <summary>
        /// 获取当前是周几
        /// </summary>
        /// <param name="date"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static string GetDayName(this DateTime date,
            string[] days)
        {
            if (days == null || days.Length != 7)
            {
                return "";
            }

            return days[Convert.ToInt32(date.DayOfWeek.ToString("d"))];
        }

        #endregion

        #region 获取当前是周几

        /// <summary>
        /// 获取当前是周几
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Week GetDayName(this DateTime date)
        {
            return Week.GetAll<Week>()
                .FirstOrDefault(x => x.Id == Convert.ToInt32(date.DayOfWeek.ToString("d")));
        }

        #endregion

        #region 获取当前是周几

        /// <summary>
        /// 获取当前是周几
        /// </summary>
        /// <param name="date"></param>
        /// <param name="days">从周日到周六</param>
        /// <returns></returns>
        public static Enum GetDayName(this DateTime date,
            Enum[] days)
        {
            return days[Convert.ToInt32(date.DayOfWeek.ToString("d"))];
        }

        #endregion

        #region 根据日期获取当前星期几

        /// <summary>
        /// 得到指定的日期是星期几
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetDayOfWeek(this DateTime date)
        {
            return date.DayOfWeek.GetDayOfWeek();
        }

        /// <summary>
        /// 将星期几转成数字表示
        /// </summary>
        public static int GetDayOfWeek(this DayOfWeek dayOfWeek)
        {
            return _weekMaps.Where(x => x.Key == dayOfWeek).Select(x => x.Value).FirstOrDefault();
        }

        #endregion

        #region 根据日期得到星座信息

        /// <summary>
        /// 根据日期得到星座信息
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <returns></returns>
        public static Constellation GetConstellationFromBirthday(this DateTime? birthday)
        {
            if (birthday == null)
            {
                return Constellation.Unknow;
            }

            return birthday.Value.GetConstellationFromBirthday();
        }

        /// <summary>
        /// 根据日期得到星座信息
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <returns></returns>
        public static Constellation GetConstellationFromBirthday(this DateTime birthday)
        {
            float fBirthDay = Convert.ToSingle(birthday.ToString("M.dd"));
            return _constellationMaps.Where(x => fBirthDay >= x.MinTime && fBirthDay < x.MaxTime)
                .Select(x => x.Key).FirstOrDefault();
        }

        #endregion
    }
}
