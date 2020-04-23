// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 更改文件mime
    /// </summary>
    public class ChangeMimeRangeParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">文件key集合</param>
        /// <param name="mimeType">文件mimeType</param>
        /// <param name="persistentOps">策略</param>
        public ChangeMimeRangeParam(List<string> keys, string mimeType, BasePersistentOps persistentOps = null)
        {
            Keys = keys??new List<string>();
            MimeType = mimeType;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key集合
        /// </summary>
        public List<string> Keys { get; private set; }

        /// <summary>
        /// 文件mimeType
        /// </summary>
        public string MimeType { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
