// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage
{
    /// <summary>
    /// 下载文件流
    /// </summary>
    public class DownloadStreamResultDto : OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="msg"></param>
        public DownloadStreamResultDto(string msg) : base(false, msg)
        {

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="msg"></param>
        /// <param name="fileStream">文件流</param>
        /// <param name="extend">扩展信息</param>
        public DownloadStreamResultDto(bool state, string msg, Stream fileStream, object extend) : base(state, msg)
        {
            FileStream = fileStream;
            Extend = extend;
        }

        /// <summary>
        /// 文件流
        /// </summary>
        public Stream FileStream { get; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public object Extend { get; }
    }
}
