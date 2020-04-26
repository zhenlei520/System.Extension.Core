// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 批量更改存储类型
    /// </summary>
    public class ChangeTypeRangeParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">文件key集合</param>
        /// <param name="type">存储类型</param>
        /// <param name="persistentOps">策略</param>
        public ChangeTypeRangeParam(List<string> keys, int type, BasePersistentOps persistentOps = null)
        {
            Keys = keys??new List<string>();
            Type = type;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key集合
        /// </summary>
        public List<string> Keys { get; }

        /// <summary>
        /// 存储类型
        /// 七牛：0代表普通存储，1：代表低频存储
        /// </summary>
        public int Type { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
