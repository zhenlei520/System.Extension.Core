// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket
{
    /// <summary>
    /// 域名响应
    /// </summary>
    public class DomainResultDto : OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        /// <param name="host">域名空间</param>
        /// <param name="msg"></param>
        public DomainResultDto(bool state, string host, string msg) : base(state, msg)
        {
            Host = host;
        }

        /// <summary>
        /// 域名
        /// </summary>
        public string Host { get; set; }
    }
}
