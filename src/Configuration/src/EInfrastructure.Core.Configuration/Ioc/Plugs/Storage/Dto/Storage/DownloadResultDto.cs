// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage
{
    /// <summary>
    /// 下载结果
    /// </summary>
    public class DownloadResultDto : OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="msg"></param>
        /// <param name="extend"></param>
        public DownloadResultDto(bool state, string msg, object extend = null) : base(state, msg)
        {
            Extend = extend;
        }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public object Extend { get; set; }
    }
}
