// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage
{
    /// <summary>
    /// 抓取文件响应
    /// </summary>
    public class FetchFileResultDto:OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="extend">扩展信息</param>
        /// <param name="msg">提示信息</param>
        public FetchFileResultDto(bool state,object extend, string msg) : base(state, msg)
        {
            Extend = extend;
        }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public object Extend { get; private set; }
    }
}
