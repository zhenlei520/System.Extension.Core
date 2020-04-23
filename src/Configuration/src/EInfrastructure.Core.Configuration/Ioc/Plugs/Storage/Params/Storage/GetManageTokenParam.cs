// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 得到管理凭证
    /// </summary>
    public class GetManageTokenParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="persistentOps">策略</param>
        public GetManageTokenParam(string url, BasePersistentOps persistentOps = null)
        {
            Url = url;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="body">内容</param>
        /// <param name="persistentOps">策略</param>
        public GetManageTokenParam(string url, byte[] body, BasePersistentOps persistentOps = null) : this(url,
            persistentOps)
        {
            Body = body;
        }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        public byte[] Body { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
