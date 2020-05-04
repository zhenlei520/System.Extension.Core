// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage
{
    /// <summary>
    /// 移动文件响应
    /// </summary>
    public class MoveFileResultDto: OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state">操作结果</param>
        /// <param name="key">目标文件key</param>
        /// <param name="msg">提示信息</param>
        public MoveFileResultDto(bool state, string key, string msg) : base(state, msg)
        {
            Key = key;
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; }
    }
}
