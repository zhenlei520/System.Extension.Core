// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 得到公用空间的地址
    /// </summary>
    public class GetVisitUrlParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="permiss">访问权限 公开：0 私有：1 公共读写：2（如果不清楚，可以为null，会自动判断处理，但会影响性能）</param>
        /// <param name="persistentOps"></param>
        public GetVisitUrlParam(string key, Permiss permiss = null, BasePersistentOps persistentOps = null)
        {
            Key = key;
            Permiss = permiss;
            PersistentOps = persistentOps ?? new BasePersistentOps();
            Expire = 3600;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="permiss">访问权限 公开：0 私有：1 公共读写：2（如果不清楚，可以为null，会自动判断处理，但会影响性能）</param>
        /// <param name="expire">过期时间，默认3600s</param>
        /// <param name="persistentOps"></param>
        public GetVisitUrlParam(string key, Permiss permiss, int expire = 3600,
            BasePersistentOps persistentOps = null) : this(
            key,
            permiss, persistentOps)
        {
            Expire = expire;
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 访问权限 公开：0 私有：1 公共读写：2
        /// 如果不清楚，可以为null，会自动判断处理，但会影响性能
        /// </summary>
        public Permiss Permiss { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }

        /// <summary>
        /// 过期时间 单位：秒（s）
        /// 默认3600s
        /// </summary>
        public int Expire { get; set; }
    }
}
