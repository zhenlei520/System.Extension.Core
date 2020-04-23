// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 得到多个文件信息
    /// </summary>
    public class GetFileRangeParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">文件key集合</param>
        /// <param name="persistentOps">策略</param>
        public GetFileRangeParam(List<string> keys, BasePersistentOps persistentOps = null)
        {
            Keys = keys ?? new List<string>();
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key集合
        /// </summary>
        public List<string> Keys { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
