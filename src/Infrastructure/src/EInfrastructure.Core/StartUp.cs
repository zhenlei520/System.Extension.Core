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
        /// <param name="enableLog">默认不启用日志</param>
        public static void Run(bool enableLog = false)
        {
            if (!_isStartUp)
            {
                EInfrastructureCoreConfigurations.SetLog(enableLog);
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
            Serialize.Xml.StartUp.Run(EInfrastructureCoreConfigurations.EnableLog);
            Serialize.NewtonsoftJson.StartUp.Run(EInfrastructureCoreConfigurations.EnableLog);
            Tools.StartUp.Run(EInfrastructureCoreConfigurations.EnableLog);
        }

        #endregion
    }
}
