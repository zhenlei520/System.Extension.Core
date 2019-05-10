// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        #region 强制加载服务

        /// <summary>
        /// 强制加载服务
        /// </summary>
        public static void Load()
        {
            Configuration.Startup.Load();
        }

        #endregion
    }
}