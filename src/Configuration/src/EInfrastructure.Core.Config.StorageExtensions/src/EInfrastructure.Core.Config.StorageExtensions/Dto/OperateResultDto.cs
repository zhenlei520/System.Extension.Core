// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Config.StorageExtensions.Dto
{
    /// <summary>
    /// 存储操作响应信息
    /// </summary>
    public class OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state">操作结果</param>
        /// <param name="msg">提示信息</param>
        public OperateResultDto(bool state, string msg)
        {
            State = state;
            Msg = msg;
        }

        /// <summary>
        /// 操作结果
        /// </summary>
        public bool State { get; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; }
    }
}
