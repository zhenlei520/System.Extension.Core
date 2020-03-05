// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core
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
        public static void Run()
        {
            if (!IsStartUp)
            {
                IsStartUp = true;
                Load();
            }
        }

        #region private methods

        /// <summary>
        /// load配置类库
        /// </summary>
        private static void Load()
        {
            Config.IdentificationExtensions.StartUp.Run();
            Tools.StartUp.Run();
        }

        #endregion
    }
}
