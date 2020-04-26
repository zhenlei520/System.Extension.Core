// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    ///
    /// </summary>
    public class RemoveRangeParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">文件key集合</param>
        /// <param name="persistentOps"></param>
        public RemoveRangeParam(List<string> keys, BasePersistentOps persistentOps = null)
        {
            Keys = keys??new List<string>();
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key集合
        /// </summary>
        public List<string> Keys { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
