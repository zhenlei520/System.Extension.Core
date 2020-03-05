// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto
{
    /// <summary>
    /// 上传结果
    /// </summary>
    public class UploadResultDto : OperateResultDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="msg"></param>
        public UploadResultDto(bool state, string msg) : base(state, msg)
        {
        }
    }
}
