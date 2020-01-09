// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using EInfrastructure.Core.Config.EnumerationExtensions;
using EInfrastructure.Core.Config.ExceptionExtensions;
using TimeType = EInfrastructure.Core.Tools.Enumerations.TimeType;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public static class TimeCommon
    {
        static TimeCommon()
        {
            ChinaDate();
        }

        #region 格式化时间

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="time"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Format(this DateTime? time, string format = "yyyy-MM-dd")
        {
            if (time != null &&
                time != DateTime.MinValue && time != DateTime.MaxValue)
            {
                return time.Value.ToString(format);
            }

            return "";
        }

        #endregion

        #region 将时间格式化成 年月日 的形式,如果时间为null，返回当前系统时间

        /// <summary>
        /// 将时间格式化成 年月日 的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dateTime">年月日分隔符</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetFormatDate(this DateTime dateTime, char separator)
        {
            string tem = $"yyyy{separator}MM{separator}dd";
            return dateTime.ToString(tem);
        }

        /// <summary>
        /// 将时间格式化成 年月日 的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dateTime">年月日分隔符</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetFormatDate(this DateTime? dateTime, char separator)
        {
            return dateTime?.GetFormatDate(separator);
        }

        #endregion

        #region 把秒转换成分钟

        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <param name="second">秒</param>
        /// <param name="rectificationType">取整方式</param>
        /// <returns></returns>
        public static int SecondToMinute(int second, RectificationType rectificationType)
        {
            decimal mm = (decimal) second / 60;
            if (rectificationType == RectificationType.Celling)
            {
                return Convert.ToInt32(Math.Ceiling(mm));
            }

            if (rectificationType == RectificationType.Floor)
            {
                return Convert.ToInt32(Math.Floor(mm));
            }

            throw new BusinessException("不支持的取整方式");
        }

        #endregion

        #region 返回某年某月最后一天

        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new GregorianCalendar().GetDaysInMonth(year, month));
            int day = lastDay.Day;
            return day;
        }

        #endregion

        #region 返回时间差

        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(this DateTime dateTime1, DateTime dateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = dateTime2 - dateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = dateTime1.Month + "月" + dateTime1.Day + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes + "分钟前";
                    }
                }
            }
            catch
            {
                // ignored
            }

            return dateDiff;
        }

        #endregion

        #region 获得两个日期的间隔

        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="dateTime1">日期一(较大一点的时间)。</param>
        /// <param name="dateTime2">日期二(较小一点的时间)。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff2(this DateTime dateTime1, DateTime dateTime2)
        {
            TimeSpan ts1 = new TimeSpan(dateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(dateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }

        #endregion

        #region 格式化日期时间

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime1">日期时间</param>
        /// <param name="dateMode">显示模式</param>
        /// <returns>0-9种模式的日期</returns>
        public static string FormatDate(this DateTime dateTime1, FormatDateType dateMode = null)
        {
            if (dateMode == null)
            {
                dateMode = FormatDateType.One;
            }

            return dateTime1.ToString(dateMode.Name);
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

        #region 返回每月的第一天和最后一天

        /// <summary>
        /// 返回每月的第一天和最后一天
        /// </summary>
        /// <param name="year1"></param>
        /// <param name="month"></param>
        /// <param name="firstDay"></param>
        /// <param name="lastDay"></param>
        public static void ReturnDateFormat(int year, int month, out string firstDay, out string lastDay)
        {
            year = year + month / 12;
            if (month != 12)
            {
                month = month % 12;
                month = Math.Abs(month);
            }

            switch (month)
            {
                case 1:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 2:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.IsLeapYear(year)
                        ? DateTime.Now.ToString(year + "-0" + month + "-29")
                        : DateTime.Now.ToString(year + "-0" + month + "-28");
                    break;
                case 3:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString("yyyy-0" + month + "-31");
                    break;
                case 4:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 5:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 6:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 7:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 8:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 9:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 10:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
                    break;
                case 11:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-30");
                    break;
                default:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
                    break;
            }
        }

        #endregion

        #region 得到月初与月末时间

        /// <summary>
        /// 得到月初/月末/本周一/本周日/本季初/本季末/年初/年末时间
        /// </summary>
        /// <param name="timeKey">时间Key</param>
        /// <param name="dateTime">指定时间</param>
        /// <returns></returns>
        public static DateTime Get(this DateTime? dateTime, TimeType timeKey)
        {
            DateTime dateNow = dateTime ?? DateTime.Now.Date; //当前时间
            if (timeKey == TimeType.StartYear)
            {
                return new DateTime(dateNow.Year, 1, 1); //本年年初
            }

            if (timeKey == TimeType.EndYear)
            {
                return new DateTime(dateNow.Year, 12, 31); //本年年末
            }

            if (timeKey == TimeType.StartQuarter)
            {
                return dateNow.AddMonths(0 - (dateNow.Month - 1) % 3).AddDays(1 - dateNow.Day); //本季度初;
            }

            if (timeKey == TimeType.EndQuarter)
            {
                return dateNow.AddMonths(0 - (dateNow.Month - 1) % 3).AddDays(1 - dateNow.Day).AddMonths(3)
                    .AddDays(-1); //本季度末
            }

            if (timeKey == TimeType.StartMonth)
            {
                return dateNow.AddDays(1 - dateNow.Day); //本月月初
            }

            if (timeKey == TimeType.EndMonth)
            {
                return dateNow.AddDays(1 - dateNow.Day).AddMonths(1).AddDays(-1); //本月月末
            }

            if (timeKey == TimeType.StartWeek)
            {
                int count = dateNow.DayOfWeek - DayOfWeek.Monday;
                if (count == -1) count = 6;
                return new DateTime(dateNow.Year, dateNow.Month, dateNow.Day).AddDays(-count); //本周周一
            }

            if (timeKey == TimeType.EndWeek)
            {
                int count = dateNow.DayOfWeek - DayOfWeek.Sunday;
                if (count != 0) count = 7 - count;

                return new DateTime(dateNow.Year, dateNow.Month, dateNow.Day).AddDays(count); //本周周日
            }

            return dateNow;
        }

        #endregion

        #region 得到前N秒的时间

        /// <summary>
        /// 得到前N秒的时间
        /// </summary>
        /// <param name="time">时间(单位s)，默认300s</param>
        /// <returns></returns>
        public static DateTime FindASecondsAgo(int time = 300)
        {
            return DateTime.Now.AddSeconds(-time);
        }

        #endregion

        #region 得到距离当前多远时间

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

        private static readonly ChineseLunisolarCalendar China = new ChineseLunisolarCalendar();
        private static readonly Hashtable GHoliday = new Hashtable();
        private static readonly Hashtable NHoliday = new Hashtable();

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

        /// <summary>
        /// 中国节日
        /// </summary>
        public static void ChinaDate()
        {
            //公历节日
            GHoliday.Add("0101", "元旦");
            GHoliday.Add("0214", "情人节");
            GHoliday.Add("0305", "雷锋日");
            GHoliday.Add("0308", "妇女节");
            GHoliday.Add("0312", "植树节");
            GHoliday.Add("0315", "消费者权益日");
            GHoliday.Add("0401", "愚人节");
            GHoliday.Add("0501", "劳动节");
            GHoliday.Add("0504", "青年节");
            GHoliday.Add("0601", "儿童节");
            GHoliday.Add("0701", "建党节");
            GHoliday.Add("0801", "建军节");
            GHoliday.Add("0910", "教师节");
            GHoliday.Add("1001", "国庆节");
            GHoliday.Add("1224", "平安夜");
            GHoliday.Add("1225", "圣诞节");

            //农历节日
            NHoliday.Add("0101", "春节");
            NHoliday.Add("0115", "元宵节");
            NHoliday.Add("0505", "端午节");
            NHoliday.Add("0815", "中秋节");
            NHoliday.Add("0909", "重阳节");
            NHoliday.Add("1208", "腊八节");
        }

        #endregion

        #region 阳历转阴历(农历)

        /// <summary>
        /// 阳历转阴历(农历)
        /// </summary>
        /// <param name="dt">待转换的日期</param>
        /// <returns></returns>
        public static string ConvertToLunar(this DateTime dt)
        {
            if (dt > China.MaxSupportedDateTime || dt < China.MinSupportedDateTime)
            {
                //日期范围：1901 年 2 月 19 日 - 2101 年 1 月 28 日
                throw new System.Exception(
                    $"日期超出范围！必须在{China.MinSupportedDateTime:yyyy-MM-dd}到{China.MaxSupportedDateTime.ToString($"yyyy-MM-dd")}之间！");
            }

            string str = $"{GetYear(dt)} {GetMonth(dt)}{GetDay(dt)}";
            string strJq = GetSolarTerm(dt);
            if (strJq != "")
            {
                str += " (" + strJq + ")";
            }

            string strHoliday = GetHoliday(dt);
            if (strHoliday != "")
            {
                str += " " + strHoliday;
            }

            string strChinaHoliday = GetChinaHoliday(dt);
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
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetYear(this DateTime dt)
        {
            int yearIndex = China.GetSexagenaryYear(dt);
            string yearTG = " 甲乙丙丁戊己庚辛壬癸";
            string yearDZ = " 子丑寅卯辰巳午未申酉戌亥";
            string yearSX = " 鼠牛虎兔龙蛇马羊猴鸡狗猪";
            int year = China.GetYear(dt);
            int yTg = China.GetCelestialStem(yearIndex);
            int yDz = China.GetTerrestrialBranch(yearIndex);

            string str = string.Format("[{1}]{2}{3}{0}", year, yearSX[yDz], yearTG[yTg], yearDZ[yDz]);
            return str;
        }

        #endregion

        #region 获取农历月份

        /// <summary>
        /// 获取农历月份
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetMonth(this DateTime dt)
        {
            int year = China.GetYear(dt);
            int iMonth = China.GetMonth(dt);
            int leapMonth = China.GetLeapMonth(year);
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
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDay(this DateTime dt)
        {
            int iDay = China.GetDayOfMonth(dt);
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
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetSolarTerm(this DateTime dt)
        {
            DateTime dtBase = new DateTime(1900, 1, 6, 2, 5, 0);
            string strReturn = "";

            var y = dt.Year;
            for (int i = 1; i <= 24; i++)
            {
                var num = 525948.76 * (y - 1900) + JqData[i - 1];
                var dtNew = dtBase.AddMinutes(num);
                if (dtNew.DayOfYear == dt.DayOfYear)
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
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetHoliday(this DateTime dt)
        {
            string strReturn = "";
            object g = GHoliday[dt.Month.ToString("00") + dt.Day.ToString("00")];
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
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetChinaHoliday(this DateTime dt)
        {
            string strReturn = "";
            int year = China.GetYear(dt);
            int iMonth = China.GetMonth(dt);
            int leapMonth = China.GetLeapMonth(year);
            int iDay = China.GetDayOfMonth(dt);
            if (China.GetDayOfYear(dt) == China.GetDaysInYear(year))
            {
                strReturn = "除夕";
            }
            else if (leapMonth != iMonth)
            {
                if (leapMonth != 0 && iMonth >= leapMonth)
                {
                    iMonth--;
                }

                object n = NHoliday[iMonth.ToString("00") + iDay.ToString("00")];
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

        private static ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();

        #region 阴历转阳历

        /// <summary>
        /// 获取指定年份春节当日（正月初一）的阳历日期
        /// </summary>
        /// <param name="year">指定的年份</param>
        private static DateTime GetLunarNewYearDate(int year)
        {
            DateTime dt = new DateTime(year, 1, 1);
            int cnYear = calendar.GetYear(dt);
            int cnMonth = calendar.GetMonth(dt);

            int num1 = 0;
            int num2 = calendar.IsLeapYear(cnYear) ? 13 : 12;

            while (num2 >= cnMonth)
            {
                num1 += calendar.GetDaysInMonth(cnYear, num2--);
            }

            num1 = num1 - calendar.GetDayOfMonth(dt) + 1;
            return dt.AddDays(num1);
        }

        /// <summary>
        /// 阴历转阳历
        /// </summary>
        /// <param name="year">阴历年</param>
        /// <param name="month">阴历月</param>
        /// <param name="day">阴历日</param>
        /// <param name="IsLeapMonth">是否闰月</param>
        private static DateTime GetLunarYearDate(int year, int month, int day, bool IsLeapMonth)
        {
            if (year < 1902 || year > 2100)
                throw new BusinessException("只支持1902～2100期间的农历年");
            if (month < 1 || month > 12)
                throw new BusinessException("表示月份的数字必须在1～12之间");

            if (day < 1 || day > calendar.GetDaysInMonth(year, month))
                throw new BusinessException("农历日期输入有误");

            int num1 = 0, num2 = 0;
            int leapMonth = calendar.GetLeapMonth(year);

            if (((leapMonth == month + 1) && IsLeapMonth) || (leapMonth > 0 && leapMonth <= month))
                num2 = month;
            else
                num2 = month - 1;

            while (num2 > 0)
            {
                num1 += calendar.GetDaysInMonth(year, num2--);
            }

            DateTime dt = GetLunarNewYearDate(year);
            return dt.AddDays(num1 + day - 1);
        }

        /// <summary>
        /// 阴历转阳历
        /// </summary>
        /// <param name="date">阴历日期</param>
        public static DateTime GetLunarYearDate(this DateTime date)
        {
            return GetLunarYearDate(date.Year, date.Month, date.Day, DateTime.IsLeapYear(date.Year));
        }

        #endregion

        #region 阳历转为阴历

        /// <summary>
        /// 阳历转为阴历
        /// </summary>
        /// <param name="dt">公历日期</param>
        /// <returns>农历的日期</returns>
        public static DateTime GetSunYearDate(this DateTime dt)
        {
            int year = China.GetYear(dt);
            int iMonth = China.GetMonth(dt);
            int iDay = China.GetDayOfMonth(dt);
            int leapMonth = China.GetLeapMonth(year);
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

        #region 获取时间戳

        #region DateTime时间格式转换为10位不带毫秒的Unix时间戳

        /// <summary>
        /// DateTime时间格式转换为10位不带毫秒的Unix时间戳
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <param name="dateTimeKind"></param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(this DateTime time, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            DateTime startTime =
                TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, dateTimeKind), TimeZoneInfo.Local);
            return (int) (time - startTime).TotalSeconds;
        }

        #endregion

        #region 得到13位时间戳

        /// <summary>
        /// 得到13位时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static long GetTimeSpan(this DateTime? time, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            if (time == null)
                time = DateTime.Now;
            return GetTimeSpan(time.Value, dateTimeKind);
        }

        /// <summary>
        /// 得到13位时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static long GetTimeSpan(this DateTime time, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            var startTime =
                TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0, dateTimeKind), TimeZoneInfo.Local);
            long t = (time.Ticks - startTime.Ticks) / 10000; //除10000调整为13位
            return t;
        }

        #endregion

        #endregion

        #region 时间戳转时间

        #region 将10位时间戳转时间

        /// <summary>
        /// 将10位时间戳转时间
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp,
            DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, dateTimeKind);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        #endregion

        #region 将13位时间戳转为时间

        /// <summary>
        /// 将13位时间戳转为时间
        /// </summary>
        /// <param name="javaTimeStamp"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTime JsTimeStampToDateTime(double javaTimeStamp, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, dateTimeKind);
            dtDateTime = dtDateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        #endregion

        #endregion

        #region 获得总秒数

        /// <summary>
        /// 获得总秒数
        /// </summary>
        /// <param name="target"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static long CurrentTimeMillis(this DateTime target, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            return (long) (TimeZoneInfo.ConvertTimeToUtc(target) - new DateTime(1970, 1, 1, 0, 0, 0, dateTimeKind)).TotalSeconds;
        }

        #endregion

        #region 将当前Utc时间转换为总毫秒数

        /// <summary>
        /// 将当前Utc时间转换为总毫秒数
        /// </summary>
        /// <param name="target"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime target, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(target) -
                                    new DateTime(1970, 1, 1, 0, 0, 0, 0, dateTimeKind)).TotalMilliseconds.ConvertToLong(0);
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
    }
}
