// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 获取防盗链配置
    /// </summary>
    public class GetRefererParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="persistentOps">如果获取的空间与默认空间不一致，则修改此对象的bucket属性</param>
        public GetRefererParam(BasePersistentOps persistentOps = null)
        {
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
