// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Configuration;

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
        private static bool _isStartUp;

        /// <summary>
        /// 启用配置
        /// </summary>
        public static void Run()
        {
            if (!_isStartUp)
            {
                _isStartUp = true;
                Load();
            }
        }

        #region private methods

        /// <summary>
        /// load配置类库
        /// </summary>
        private static void Load()
        {
            Serialize.Xml.StartUp.Run();
            Serialize.NewtonsoftJson.StartUp.Run();
            Tools.StartUp.Run();
        }

        #endregion
    }
}
