// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Tools
{
    public static class StartUp
    {
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
            EInfrastructure.Core.Config.Entities.StartUp.Run();
        }
    }
}
