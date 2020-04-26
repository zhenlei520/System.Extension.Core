// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage
{
    /// <summary>
    /// 文件访问权限
    /// </summary>
    public class FilePermissResultInfo : OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="permiss">访问权限</param>
        /// <param name="msg">消息</param>
        public FilePermissResultInfo(bool state, Permiss permiss, string msg) : base(state, msg)
        {
            Permiss = permiss;
        }

        /// <summary>
        /// 访问权限 公开：0 私有：1 公共读写：2
        /// </summary>
        public Permiss Permiss { get; private set; }
    }
}
