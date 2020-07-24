// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Serialize.NewtonsoftJson
{
    /// <summary>
    ///
    /// </summary>
    public static class StartUp
    {
        /// <summary>
        ///
        /// </summary>
        private static bool _isStartUp;

        /// <summary>
        /// 启用配置
        /// </summary>
        public static void Run()
        {
            if (!_isStartUp)
            {
                _isStartUp = true;
            }
        }
    }
}
