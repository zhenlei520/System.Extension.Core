// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Configuration
{
    public static class StartUp
    {
        private static bool IsStartUp;

        /// <summary>
        /// 启用配置
        /// </summary>
        public static void Run()
        {
            if (!IsStartUp)
            {
                IsStartUp = true;
            }
        }
    }
}
