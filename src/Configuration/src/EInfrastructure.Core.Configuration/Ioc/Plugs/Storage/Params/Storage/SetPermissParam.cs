// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 设置文件访问权限
    /// </summary>
    public class SetPermissParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="permiss">访问权限 公开读：0 私有：1 公共读写：2</param>
        /// <param name="persistentOps">策略（如果修改的空间不是当前配置中的默认域，则修改此对象的Bucket属性）</param>
        public SetPermissParam(string key, Permiss permiss = null, BasePersistentOps persistentOps = null)
        {
            Key = key;
            Permiss = permiss;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 访问权限 公开：0 私有：1 公共读写：2
        /// </summary>
        public Permiss Permiss { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
