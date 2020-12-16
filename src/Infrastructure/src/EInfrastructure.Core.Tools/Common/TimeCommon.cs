// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Configuration;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 时间扩展
    /// </summary>
    public static class TimeCommon
    {
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
            if (rectificationType.Id == RectificationType.Celling.Id)
            {
                return Convert.ToInt32(Math.Ceiling(mm));
            }

            if (rectificationType.Id == RectificationType.Floor.Id)
            {
                return Convert.ToInt32(Math.Floor(mm));
            }

            throw new BusinessException("不支持的取整方式", HttpStatus.Err.Id);
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

        #region 返回每月的第一天和最后一天

        /// <summary>
        /// 返回每月的第一天和最后一天
        /// </summary>
        /// <param name="year"></param>
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

        #region 获取指定年份春节当日（正月初一）的阳历日期

        /// <summary>
        /// 获取指定年份春节当日（正月初一）的阳历日期
        /// </summary>
        /// <param name="year">指定的年份</param>
        private static DateTime GetLunarNewYearDate(int year)
        {
            DateTime dt = new DateTime(year, 1, 1);
            int cnYear = Extensions.Calendar.GetYear(dt);
            int cnMonth = Extensions.Calendar.GetMonth(dt);

            int num1 = 0;
            int num2 = Extensions.Calendar.IsLeapYear(cnYear) ? 13 : 12;

            while (num2 >= cnMonth)
            {
                num1 += Extensions.Calendar.GetDaysInMonth(cnYear, num2--);
            }

            num1 = num1 - Extensions.Calendar.GetDayOfMonth(dt) + 1;
            return dt.AddDays(num1);
        }

        /// <summary>
        /// 阴历转阳历
        /// </summary>
        /// <param name="year">阴历年</param>
        /// <param name="month">阴历月</param>
        /// <param name="day">阴历日</param>
        /// <param name="isLeapMonth">是否闰月</param>
        internal static DateTime GetLunarYearDate(int year, int month, int day, bool isLeapMonth)
        {
            if (year < 1902 || year > 2100)
                throw new BusinessException("只支持1902～2100期间的农历年", HttpStatus.Err.Id);
            if (month < 1 || month > 12)
                throw new BusinessException("表示月份的数字必须在1～12之间", HttpStatus.Err.Id);

            if (day < 1 || day > Extensions.Calendar.GetDaysInMonth(year, month))
                throw new BusinessException("农历日期输入有误", HttpStatus.Err.Id);

            int num1 = 0, num2 = 0;
            int leapMonth = Extensions.Calendar.GetLeapMonth(year);

            if (((leapMonth == month + 1) && isLeapMonth) || (leapMonth > 0 && leapMonth <= month))
                num2 = month;
            else
                num2 = month - 1;

            while (num2 > 0)
            {
                num1 += Extensions.Calendar.GetDaysInMonth(year, num2--);
            }

            DateTime dt = GetLunarNewYearDate(year);
            return dt.AddDays(num1 + day - 1);
        }

        #endregion

        #region 根据出生年份得到生肖信息

        /// <summary>
        /// 根据出生年份得到生肖信息
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static Animal GetAnimal(int year)
        {
            if (year < 1582 || year > 2099)
            {
                return null;
            }

            var index = (year - 3) % 12;
            if (index == 0)
            {
                index = 12;
            }

            return Animal.GetAll<Animal>().FirstOrDefault(x => x.Id == index);
        }

        #endregion

        #region 得到月初/月末/本周一/本周日/本季初/本季末/年初/年末时间

        /// <summary>
        /// 得到月初/月末/本周一/本周日/本季初/本季末/年初/年末时间
        /// </summary>
        /// <param name="timeKey">时间Key</param>
        /// <param name="dateTime">指定时间，默认当前时间</param>
        /// <returns></returns>
        public static DateTime Get(TimeType timeKey, DateTime? dateTime = null)
        {
            return (dateTime ?? DateTime.Now).Get(timeKey);
        }

        #endregion

        #region 得到耗时时间

        /// <summary>
        /// 得到耗时时间
        /// </summary>
        /// <param name="action">委托方法</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static TimeElapsed GetElapsedTime<T>(Action action)
        {
            return new TimeElapsed(action);
        }

        /// <summary>
        /// 得到耗时时间
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static KeyValuePair<T, TimeElapsed> GetElapsedTime<T>(Func<T> func)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            T res = func.Invoke();
            stopwatch.Stop();
            return new KeyValuePair<T, TimeElapsed>(res, new TimeElapsed(stopwatch.ElapsedTicks));
        }

        #endregion
    }
}
