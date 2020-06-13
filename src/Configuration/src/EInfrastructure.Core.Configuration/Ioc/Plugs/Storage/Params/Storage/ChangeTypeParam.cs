// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 更改存储类型
    /// </summary>
    public class ChangeTypeParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="persistentOps"></param>
        public ChangeTypeParam(string key, Enumerations.StorageClass type, BasePersistentOps persistentOps = null)
        {
            Key = key;
            Type = type;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 存储类型
        /// </summary>
        public Enumerations.StorageClass Type { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
