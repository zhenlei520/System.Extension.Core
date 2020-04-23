// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 移除文件
    /// </summary>
    public class RemoveParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="persistentOps"></param>
        public RemoveParam(string key, BasePersistentOps persistentOps = null)
        {
            Key = key;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
