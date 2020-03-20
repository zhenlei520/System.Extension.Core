// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Serialize.Xml
{
    /// <summary>
    ///
    /// </summary>
    public static class StartUp
    {
        /// <summary>
        ///
        /// </summary>
        private static bool IsStartUp;

        /// <summary>
        /// 启用配置
        /// </summary>
        /// <param name="enableLog">默认不启用日志</param>
        public static void Run(bool enableLog = false)
        {
            if (!IsStartUp)
            {
                IsStartUp = true;
            }
        }
    }
}
