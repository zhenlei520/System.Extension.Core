// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Interface.Storage.Enum
{
    /// <summary>
    /// 上传状态
    /// </summary>
    public enum UploadStateEnum
    {
        /// <summary>
        /// 任务状态:激活
        /// </summary>
        [Description("激活")] Activated,

        /// <summary>
        /// 任务状态:暂停
        /// </summary>
        [Description("暂停")]Suspended,

        /// <summary>
        /// 任务状态:退出
        /// </summary>
        [Description("退出")]  Aborted
    }
}