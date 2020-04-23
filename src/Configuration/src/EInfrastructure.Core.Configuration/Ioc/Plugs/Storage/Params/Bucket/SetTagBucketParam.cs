// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 设置空间标签
    /// </summary>
    public class SetTagBucketParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="tags">标签</param>
        /// <param name="persistentOps">策略（如果修改的空间不是当前配置中的默认域，则修改此对象的Bucket属性）</param>
        public SetTagBucketParam(List<KeyValuePair<string, string>> tags, BasePersistentOps persistentOps = null)
        {
            Tags = tags??new List<KeyValuePair<string, string>>();
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 标签
        /// </summary>
        public List<KeyValuePair<string, string>> Tags { get; set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
