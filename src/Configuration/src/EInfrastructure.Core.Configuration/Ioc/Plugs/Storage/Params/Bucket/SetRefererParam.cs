// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 设置防盗链
    /// 七牛不支持Api设置防盗链
    /// </summary>
    public class SetRefererParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="isAllowNullReferer">是否允许空Referer</param>
        /// <param name="refererList">Referer白名单。仅允许指定的域名访问OSS资源</param>
        /// <param name="persistentOps">策略</param>
        public SetRefererParam(bool isAllowNullReferer = true, List<string> refererList = null,
            BasePersistentOps persistentOps = null)
        {
            IsAllowNullReferer = isAllowNullReferer;
            RefererList = refererList ?? new List<string>();
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 是否允许空Referer
        /// </summary>
        public bool IsAllowNullReferer { get; private set; }

        /// <summary>
        /// Referer白名单。仅允许指定的域名访问OSS资源
        /// </summary>
        public List<string> RefererList { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
