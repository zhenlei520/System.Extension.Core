// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Config;
using Microsoft.Extensions.Options;
using NLog;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogCommon
    {
        private static LogConfig _logConfig = new LogConfig()
        {
            Name = "UserLog"
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logConfig"></param>
        public LogCommon(IOptionsSnapshot<LogConfig> logConfig)
        {
            _logConfig = logConfig.Value;
        }


        private static readonly Logger Log = LogManager.GetLogger(_logConfig.Name);

        private static string FormatMsg(string title, object msg)
        {
            if (msg == null)
            {
                return title;
            }

            return title + "\r\n" + msg;
        }

        #region 增加error文件日志

        /// <summary>
        /// 增加error文件日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        public static void Error(string title, object msg = null)
        {
            Log?.Error(FormatMsg(title, msg));
        }

        #endregion

        #region 增加debug文件日志

        /// <summary>
        /// 增加debug文件日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        public static void Debug(string title, object msg = null)
        {
            Log?.Debug(FormatMsg(title, msg));
        }

        #endregion

        #region 增加info文件日志

        /// <summary>
        /// 增加info文件日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        public static void Info(string title, object msg = null)
        {
            Log?.Info(FormatMsg(title, msg));
        }

        #endregion

        #region 增加warn文件日志

        /// <summary>
        /// 增加warn文件日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        public static void Warn(string title, object msg = null)
        {
            Log?.Warn(FormatMsg(title, msg));
        }

        #endregion

        #region 增加trace文件日志

        /// <summary>
        /// 增加trace文件日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        public static void Trace(string title, object msg = null)
        {
            Log?.Trace(FormatMsg(title, msg));
            Console.WriteLine(FormatMsg(title, msg));
        }

        #endregion
    }
}