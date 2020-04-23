// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 得到空间标签
    /// </summary>
    public class GetTagsBucketParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="persistentOps">策略（如果希望得到的空间不是默认配置的空间，则需要修改此对象的Bucket属性）</param>
        public GetTagsBucketParam(BasePersistentOps persistentOps = null)
        {
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
