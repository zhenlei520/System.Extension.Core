// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 得到文件信息
    /// </summary>
    public class GetFileParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="persistentOps">策略</param>
        public GetFileParam(string key, BasePersistentOps persistentOps= null)
        {
            Key = key;
            PersistentOps = persistentOps??new BasePersistentOps();
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
