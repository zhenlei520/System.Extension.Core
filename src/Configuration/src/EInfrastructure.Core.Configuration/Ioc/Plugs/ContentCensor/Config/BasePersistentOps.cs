// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Config
{
    /// <summary>
    /// 基本策略
    /// </summary>
    public class BasePersistentOps
    {
        /// <summary>
        ///
        /// </summary>
        public BasePersistentOps()
        {
            CallbackUrl = null;
        }

        /// <summary>
        /// 离线结果回调通知到客户的URL
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// 复制一份新的策略
        /// </summary>
        /// <returns></returns>
        public BasePersistentOps Clone()
        {
            return new BasePersistentOps()
            {
                CallbackUrl = CallbackUrl
            };
        }
    }
}
