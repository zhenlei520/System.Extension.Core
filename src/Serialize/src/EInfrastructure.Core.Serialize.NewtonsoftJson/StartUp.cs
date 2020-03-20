// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Serialize.NewtonsoftJson
{
    /// <summary>
    ///
    /// </summary>
    public static class StartUp
    {
        #region 日志配置

        /// <summary>
        /// 设置是否启用日志
        /// </summary>
        /// <param name="enableLog">启用日志</param>
        /// <returns></returns>
        internal static void SetLog(bool enableLog)
        {
            EnableLog = enableLog;
        }

        /// <summary>
        /// 是否启用日志
        /// </summary>
        internal static bool EnableLog { get; private set; }

        #endregion

        /// <summary>
        ///
        /// </summary>
        private static bool _isStartUp;

        /// <summary>
        /// 启用配置
        /// </summary>
        /// <param name="enableLog">启用日志</param>
        public static void Run(bool enableLog)
        {
            if (!_isStartUp)
            {
                _isStartUp = true;
                SetLog(enableLog);
            }
        }
    }
}
