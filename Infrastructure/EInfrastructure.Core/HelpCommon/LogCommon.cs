using System;
using EInfrastructure.Core.Configuration.Config;
using Microsoft.Extensions.Options;
using NLog;

namespace EInfrastructure.Core.HelpCommon
{
    public class LogCommon
    {
        private static LogConfig _logConfig = new LogConfig()
        {
            Name = "UserLog"
        };

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

        public static void Error(string title, object msg = null)
        {
            Log?.Error(FormatMsg(title, msg));
        }

        public static void Debug(string title, object msg = null)
        {
            Log?.Debug(FormatMsg(title, msg));
        }

        public static void Info(string title, object msg = null)
        {
            Log?.Info(FormatMsg(title, msg));
        }

        public static void Warn(string title, object msg = null)
        {
            Log?.Warn(FormatMsg(title, msg));
        }

        public static void Trace(string title, object msg = null)
        {
            Log?.Trace(FormatMsg(title, msg));
            Console.WriteLine(FormatMsg(title, msg));
        }
    }
}