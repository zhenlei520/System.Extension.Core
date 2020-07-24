// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 可计算方法总共耗时情况
    /// </summary>
    public class TimeDifferenceCommon
    {
        private readonly Action _action;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private long _ticks;

        /// <summary>
        ///
        /// </summary>
        private TimeSpan _timeSpan;

        /// <summary>
        ///
        /// </summary>
        /// <param name="action">委托方法</param>
        public TimeDifferenceCommon(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// 总共运行多少毫秒
        /// </summary>
        public double ElapsedMilliseconds => GetTimeSpan().TotalMilliseconds;

        /// <summary>
        /// 总共运行多少秒
        /// </summary>
        public double ElapsedSeconds => GetTimeSpan().TotalSeconds;

        /// <summary>
        /// 总共运行多少分钟
        /// </summary>
        public double ElapsedMinutes => GetTimeSpan().TotalMinutes;

        /// <summary>
        /// 总共运行多少小时
        /// </summary>
        public double ElapsedHours => GetTimeSpan().TotalHours;

        /// <summary>
        /// 总共运行多少天
        /// </summary>
        public double ElapsedDays => GetTimeSpan().TotalDays;

        /// <summary>
        /// 总共运行多少Ticks
        /// </summary>
        public long ElapsedTicks => GetTotalTime();

        /// <summary>
        /// 总共运行多少时间
        /// </summary>
        public TimeSpan ElapsedTimeSpan => GetTimeSpan();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(TimeSpanFormatDateType.Zero);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string ToString(TimeSpanFormatDateType timeSpanFormatDateType)
        {
            return ElapsedTimeSpan.FormatDate(timeSpanFormatDateType);
        }

        #region private methods

        /// <summary>
        /// 得到耗时（Ticks）
        /// </summary>
        /// <returns></returns>
        private long GetTotalTime()
        {
            if (_ticks == 0)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                _action?.Invoke();
                stopwatch.Stop();
                _ticks = stopwatch.ElapsedTicks;
            }

            return _ticks;
        }

        /// <summary>
        /// 得到 TimeSpan
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetTimeSpan()
        {
            if (_timeSpan.Equals(default))
            {
                _timeSpan = new TimeSpan(GetTotalTime());
            }

            return _timeSpan;
        }

        #endregion
    }
}
