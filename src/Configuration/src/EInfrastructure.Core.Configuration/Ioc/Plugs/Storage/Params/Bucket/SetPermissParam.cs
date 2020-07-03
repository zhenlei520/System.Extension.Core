// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 设置空间访问权限
    /// 公有或者私有
    /// </summary>
    public class SetPermissParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="permiss">空间访问权限 公开读：0 私有：1 公共读写：2</param>
        /// <param name="persistentOps">策略（如果修改的空间不是当前配置中的默认域，则修改此对象的Bucket属性）</param>
        public SetPermissParam(Permiss permiss, BasePersistentOps persistentOps = null)
        {
            Permiss = permiss;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 空间访问权限 公开：0 私有：1 公共读写：2
        /// </summary>
        public Permiss Permiss { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
