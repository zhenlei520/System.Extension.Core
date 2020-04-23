// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage
{
    /// <summary>
    /// 复制文件响应
    /// </summary>
    public class CopyFileResultDto: OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state">操作结果</param>
        /// <param name="fileId">文件id</param>
        /// <param name="msg">提示信息</param>
        public CopyFileResultDto(bool state, string fileId, string msg) : base(state, msg)
        {
            FileId = fileId;
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string FileId { get; }
    }
}
