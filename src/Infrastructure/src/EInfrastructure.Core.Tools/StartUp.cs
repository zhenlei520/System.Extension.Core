// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    ///
    /// </summary>
    public static class StartUp
    {

        private static bool _isStartUp;

        /// <summary>
        /// 启用配置
        /// <param name="enableLog">默认不启用日志</param>
        /// </summary>
        public static void Run()
        {
            if (!_isStartUp)
            {
                _isStartUp = true;
            }
            Config.Entities.StartUp.Run();
        }
    }
}
