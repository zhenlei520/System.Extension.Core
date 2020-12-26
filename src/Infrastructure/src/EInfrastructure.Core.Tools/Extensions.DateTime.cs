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
using EInfrastructure.Core.Tools.Configuration;
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
        private static List<WeekHolidayStruct> _weekHolidayStructs;

        /// <summary>
        /// 初始化节日
        /// </summary>
        private static void InitDate()
        {
            _china = new ChineseLunisolarCalendar();

            //公历节日
            _gHoliday = new Hashtable
            {
                {"0101", "元旦"},
                {"0202", "世界湿地日"},
                {"0207", "国际声援南非日"},
                {"0210", "国际气象节"},
                {"0214", "情人节"},
                {"0301", "国际海豹日"},
                {"0303", "全国爱耳日"},
                {"0305", "雷锋日"},
                {"0308", "妇女节"},
                {"0312", "植树节 孙中山逝世纪念日"},
                {"0314", "国际警察日"},
                {"0315", "国际消费者权益日"},
                {"0317", "中国国医节 国际航海日"},
                {"0321", "世界森林日 消除种族歧视国际日 世界儿歌日"},
                {"0322", "世界水日"},
                {"0323", "世界气象日"},
                {"0324", "世界防治结核病日"},
                {"0325", "全国中小学生安全教育日"},
                {"0330", "巴勒斯坦国土日"},
                {"0401", "愚人节 全国爱国卫生运动月(四月) 税收宣传月(四月)"},
                {"0405", "清明节"},
                {"0407", "世界卫生日"},
                {"0422", "世界地球日"},
                {"0423", "世界图书和版权日"},
                {"0424", "亚非新闻工作者日"},
                {"0501", "劳动节"},
                {"0504", "青年节"},
                {"0505", "碘缺乏病防治日"},
                {"0508", "世界红十字日"},
                {"0512", "国际护士节"},
                {"0515", "国际家庭日"},
                {"0517", "世界电信日"},
                {"0518", "国际博物馆日"},
                {"0520", "全国学生营养日"},
                {"0523", "国际牛奶日"},
                {"0531", "世界无烟日"},
                {"0601", "国际儿童节"},
                {"0605", "世界环境保护日"},
                {"0606", "全国爱眼日"},
                {"0617", "防治荒漠化和干旱日"},
                {"0623", "国际奥林匹克日"},
                {"0625", "全国土地日"},
                {"0626", "国际禁毒日"},
                {"0701", "建党节 香港回归纪念 世界建筑日"},
                {"0702", "国际体育记者日"},
                {"0707", "中国人民抗日战争纪念日"},
                {"0711", "世界人口日"},
                {"0730", "非洲妇女日"},
                {"0801", "建军节"},
                {"0808", "中国男子节 父亲节"},
                {"0815", "抗日战争胜利纪念"},
                {"0908", "国际扫盲日 国际新闻工作者日"},
                {"0909", "毛泽东逝世纪念"},
                {"0910", "教师节"},
                {"0914", "世界清洁地球日"},
                {"0916", "国际臭氧层保护日"},
                {"0918", "九·一八事变纪念日"},
                {"0920", "国际爱牙日"},
                {"0921", "国际和平日"},
                {"0927", "世界旅游日"},
                {"0928", "孔子诞辰"},
                {"1001", "国庆节 国际音乐日 国际老人节"},
                {"1002", "国际和平与民主自由斗争日"},
                {"1004", "世界动物日"},
                {"1008", "全国高血压日 世界视觉日"},
                {"1009", "世界邮政日 万国邮联日"},
                {"1010", "辛亥革命纪念日 世界精神卫生日"},
                {"1013", "世界保健日 国际教师节"},
                {"1014", "世界标准日"},
                {"1015", "国际盲人节(白手杖节)"},
                {"1016", "世界粮食日"},
                {"1017", "世界消除贫困日"},
                {"1022", "世界传统医药日"},
                {"1024", "联合国日 世界发展信息日"},
                {"1031", "世界勤俭日"},
                {"1107", "十月社会主义革命纪念日"},
                {"1108", "中国记者日"},
                {"1109", "全国消防安全宣传教育日"},
                {"1110", "世界青年节"},
                {"1111", "国际科学与和平周(本日所属的一周)"},
                {"1112", "孙中山诞辰纪念"},
                {"1114", "世界糖尿病日"},
                {"1117", "国际大学生节 世界学生节"},
                {"1121", "世界问候日 世界电视日"},
                {"1129", "国际声援巴勒斯坦人民国际日"},
                {"1201", "世界艾滋病日"},
                {"1203", "世界残疾人日"},
                {"1205", "国际经济和社会发展志愿人员日"},
                {"1208", "国际儿童电视日"},
                {"1209", "世界足球日"},
                {"1210", "世界人权日"},
                {"1212", "西安事变纪念日"},
                {"1213", "南京大屠杀(1937年)纪念日！紧记血泪史！"},
                {"1220", "澳门回归纪念"},
                {"1221", "国际篮球日"},
                {"1224", "平安夜"},
                {"1225", "圣诞节"},
                {"1226", "毛主席诞辰"},
                {"1229", "国际生物多样性日"},
            };

            //农历节日
            _nHoliday = new Hashtable
            {
                {"0101", "春节"},
                {"0115", "元宵节"},
                {"0505", "端午节"},
                {"0707", "七夕情人节"},
                {"0715", "中元节"},
                {"0815", "中秋节"},
                {"0909", "重阳节"},
                {"1208", "腊八节"},
                {"1223", "北方小年(扫房)"},
                {"1224", "南方小年(掸尘)"},
            };

            _weekHolidayStructs = new List<WeekHolidayStruct>()
            {
                new WeekHolidayStruct(5, 2, 7, "母亲节"),
                new WeekHolidayStruct(5, 3, 7, "全国助残日"),
                new WeekHolidayStruct(6, 3, 7, "父亲节"),
                new WeekHolidayStruct(9, 4, 7, "国际聋人节"),
                new WeekHolidayStruct(10, 1, 1, "国际住房日"),
                new WeekHolidayStruct(10, 2, 3, "国际减轻自然灾害日"),
                new WeekHolidayStruct(11, 4, 4, "感恩节")
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
        /// <param name="dateTime">指定时间，如果为null，则默认当前时间</param>
        /// <param name="timeKey">时间Key</param>
        /// <returns></returns>
        public static DateTime Get(this DateTime? dateTime, TimeType timeKey)
        {
            return (dateTime ?? DateTime.Now).Get(timeKey);
        }

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

            if (span.TotalSeconds < 60)
            {
                return "刚刚";
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
        /// <param name="date">较小一点的时间</param>
        /// <returns></returns>
        public static string GetAccordingToCurrent(this DateTime date)
        {
            return DateTime.Now.GetAccordingToCurrent(date);
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

        #region 获取公历(阳历)节日

        /// <summary>
        /// 获取公历(阳历)节日
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
            else
            {
                var indexOfMonth = dateTime.GetWeekIndexOfMonth();
                var dayOfWeek = dateTime.GetDayOfWeek(Nationality.China);

                var weekHolidayStruct = _weekHolidayStructs.FirstOrDefault(x =>
                    x.Month == dateTime.Month && x.WeekAtMonth == indexOfMonth && x.WeekDay == dayOfWeek);
                if (weekHolidayStruct != null)
                {
                    return weekHolidayStruct.HolidayName;
                }
            }

            return strReturn;
        }

        #endregion

        #region 获取农历（阴历）节日

        /// <summary>
        /// 获取农历（阴历）节日
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

        #region 阴历转阳历

        /// <summary>
        /// 阴历转阳历
        /// </summary>
        /// <param name="dateTime">阴历日期</param>
        public static DateTime GetLunarYearDate(this DateTime dateTime)
        {
            return TimeCommon.GetLunarYearDate(dateTime.Year, dateTime.Month, dateTime.Day,
                TimeCommon.IsLeapYear(dateTime.Year));
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

        #region 得到dateTime是当月的第几周

        /// <summary>
        /// 得到dateTime是当月的第几周，如果习惯周一到周日为一周，则nationality传Nationality.China
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="nationality">国家，默认为美国</param>
        /// <returns></returns>
        public static int GetWeekIndexOfMonth(this DateTime dateTime, Nationality nationality = null)
        {
            if (nationality == null)
            {
                nationality = Nationality.Usa;
            }

            int i = dateTime.GetDayOfWeek(Nationality.China);
            if (nationality.Equals(Nationality.China))
            {
                return (dateTime.Date.Day + i - 2) / 7 + 1;
            }

            return (dateTime.Date.Day + i - 1) / 7;
        }

        #endregion

        #region 获取当前是周几

        /// <summary>
        /// 获取当前是周几
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Week GetDayName(this DateTime dateTime)
        {
            var dayOfWeek = dateTime.GetDayOfWeek(Nationality.China);
            return Week.GetAll<Week>()
                .FirstOrDefault(x => x.Id == dayOfWeek);
        }

        #endregion

        #region 根据日期得到序号，支持国家

        /// <summary>
        /// 根据日期得到序号，支持国家
        /// 目前除中国外，周一是1，周日是7
        /// 其他国家为：周日是0，周六是7
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="nationality">国家，默认英国等</param>
        /// <returns></returns>
        public static int GetDayOfWeek(this DateTime dateTime, Nationality nationality = null)
        {
            return dateTime.DayOfWeek.GetDayOfWeek(nationality);
        }

        /// <summary>
        /// 根据DayOfWeek得到序号，支持国家
        /// 目前除中国外，周一是1，周日是7
        /// 其他国家为：周日是0，周六是7
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="nationality">国家,默认是美国</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static int GetDayOfWeek(this DayOfWeek dayOfWeek, Nationality nationality = null)
        {
            if (nationality == null)
            {
                nationality = Nationality.Usa;
            }

            int? num = dayOfWeek.ToString("d").ConvertToInt();
            if (num == null)
            {
                throw new NotSupportedException(nameof(dayOfWeek));
            }

            if (nationality.Equals(Nationality.China))
            {
                if (num == 0)
                {
                    return 7;
                }
            }

            return num.Value;
        }

        #endregion

        #region 判断是同一年

        /// <summary>
        /// 判断是同一年
        /// </summary>
        /// <param name="dateTime">时间1</param>
        /// <param name="dateTime2">时间2</param>
        /// <returns></returns>
        public static bool IsInSameYear(this DateTime dateTime, DateTime dateTime2)
        {
            return dateTime.Year == dateTime2.Year;
        }

        #endregion

        #region 判断是同一年的同一月

        /// <summary>
        /// 判断是同一年的同一月
        /// </summary>
        /// <param name="dateTime">时间1</param>
        /// <param name="dateTime2">时间2</param>
        /// <returns></returns>
        public static bool IsInSameMonth(this DateTime dateTime, DateTime dateTime2)
        {
            return dateTime.Year == dateTime2.Year && dateTime.Month == dateTime2.Month;
        }

        #endregion

        #region 判断是同一年的同一月的同一周

        /// <summary>
        /// 判断是同一年的同一月的同一周
        /// 目前除中国外，周一是1，周日是7
        /// 其他国家为：周日是0，周六是7
        /// </summary>
        /// <param name="dateTime">时间1</param>
        /// <param name="dateTime2">时间2</param>
        /// <param name="nationality">国家,默认是美国</param>
        /// <returns></returns>
        public static bool IsInSameWeek(this DateTime dateTime, DateTime dateTime2, Nationality nationality = null)
        {
            return dateTime.AddDays(-(int) dateTime.GetDayOfWeek(nationality)).Date ==
                   dateTime2.AddDays(-(int) dateTime2.GetDayOfWeek(nationality)).Date;
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
