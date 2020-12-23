// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Configuration;

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

        #region 是否闰年

        /// <summary>
        /// 是否闰年
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }

        #endregion

        #region 返回某年某月最后一天

        /// <summary>
        /// 返回某年某月最后一天
        /// 月份不能为0，可以超过12也可以低于0，如14月，即year+1，month：2
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            CheckDateTime(year, month);
            DateTime lastDay = new DateTime(year, month, GlobalConfigurations.Calendar.GetDaysInMonth(year, month));
            int day = lastDay.Day;
            return day;
        }

        #endregion

        #region 得到指定月份的第一天

        /// <summary>
        /// 得到指定月份的第一天
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public static DateTime GetSpecifyMonthFirstDay(int year, int month)
        {
            CheckDateTime(year, month);
            return new DateTime(year, month, 1);
        }

        #endregion

        #region 得到指定月份的最后一天

        /// <summary>
        /// 得到指定月份的最后一天
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public static DateTime GetSpecifyMonthLastDay(int year, int month)
        {
            CheckDateTime(year, month);
            var lastDay = GetMonthLastDate(year, month);
            return new DateTime(year, month, lastDay);
        }

        #endregion

        #region 获取指定年份春节当日（正月初一）的阳历日期

        /// <summary>
        /// 获取指定年份春节当日（正月初一）的阳历日期
        /// </summary>
        /// <param name="year">指定的年份</param>
        public static DateTime GetLunarNewYearDate(int year)
        {
            DateTime dt = new DateTime(year, 1, 1);
            int cnYear = GlobalConfigurations.ChineseCalendar.GetYear(dt);
            int cnMonth = GlobalConfigurations.ChineseCalendar.GetMonth(dt);

            int num1 = 0;
            int num2 = GlobalConfigurations.ChineseCalendar.IsLeapYear(cnYear) ? 13 : 12;

            while (num2 >= cnMonth)
            {
                num1 += GlobalConfigurations.ChineseCalendar.GetDaysInMonth(cnYear, num2--);
            }

            num1 = num1 - GlobalConfigurations.ChineseCalendar.GetDayOfMonth(dt) + 1;
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

            if (day < 1 || day > GlobalConfigurations.ChineseCalendar.GetDaysInMonth(year, month))
                throw new BusinessException("农历日期输入有误", HttpStatus.Err.Id);

            int num1 = 0, num2 = 0;
            int leapMonth = GlobalConfigurations.ChineseCalendar.GetLeapMonth(year);

            if (((leapMonth == month + 1) && isLeapMonth) || (leapMonth > 0 && leapMonth <= month))
                num2 = month;
            else
                num2 = month - 1;

            while (num2 > 0)
            {
                num1 += GlobalConfigurations.ChineseCalendar.GetDaysInMonth(year, num2--);
            }

            DateTime dt = GetLunarNewYearDate(year);
            return dt.AddDays(num1 + day - 1);
        }

        #endregion

        #region 得到日期

        /// <summary>
        /// 得到日期
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns></returns>
        public static DateTime Get(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        #endregion

        #region 根据出生年份得到生肖信息

        /// <summary>
        /// 根据出生年份得到生肖信息
        /// 超过2099年低于1582年的为null
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

        #region private methods

        #region 检查时间，月份必须在1-12之间

        /// <summary>
        /// 检查时间，月份必须在1-12之间
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        private static void CheckDateTime(int year, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new NotSupportedException("The month must be from January to December till now");
            }
        }

        #endregion

        #endregion
    }
}
