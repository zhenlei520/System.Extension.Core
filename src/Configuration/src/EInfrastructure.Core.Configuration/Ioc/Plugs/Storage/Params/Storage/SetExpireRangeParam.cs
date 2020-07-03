// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 批量设置文件过期时间
    /// </summary>
    public class SetExpireRangeParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">文件key集合</param>
        /// <param name="expire">过期时间，默认7天</param>
        /// <param name="persistentOps">策略</param>
        public SetExpireRangeParam(List<string> keys, int expire = 7, BasePersistentOps persistentOps = null)
        {
            Keys = keys??new List<string>();
            Expire = expire;
            PersistentOps = persistentOps??new BasePersistentOps();
        }

        /// <summary>
        /// 文件key集合
        /// </summary>
        public List<string> Keys { get; }

        /// <summary>
        /// 过期时间 单位：天
        /// </summary>
        public int Expire { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
