// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket
{
    /// <summary>
    /// 防盗链
    /// </summary>
    public class RefererResultDto : OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="isAllowNullReferer">是否允许空Referer</param>
        /// <param name="refererList">Referer白名单。仅允许指定的域名访问OSS资源</param>
        public RefererResultDto(bool isAllowNullReferer, List<string> refererList) : base(true,
            "success")
        {
            IsAllowNullReferer = isAllowNullReferer;
            RefererList = refererList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="msg">异常信息</param>
        public RefererResultDto(string msg) : base(false, msg)
        {
            IsAllowNullReferer = false;
            RefererList = null;
        }

        /// <summary>
        /// 是否允许空Referer
        /// </summary>
        public bool IsAllowNullReferer { get; private set; }

        /// <summary>
        /// Referer白名单。仅允许指定的域名访问OSS资源
        /// </summary>
        public List<string> RefererList { get; private set; }
    }
}
