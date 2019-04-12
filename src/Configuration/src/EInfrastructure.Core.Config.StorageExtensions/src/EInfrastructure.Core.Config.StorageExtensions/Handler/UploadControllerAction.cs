// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Config.StorageExtensions.Handler
{
    /// <summary>
    /// 上传任务的状态
    /// </summary>
    public enum UploadControllerAction
    {
        /// <summary>
        /// 任务状态:激活
        /// </summary>
        [Description("激活")] Activated,

        /// <summary>
        /// 任务状态:暂停
        /// </summary>
        [Description("暂停")] Suspended,

        /// <summary>
        /// 任务状态:退出
        /// </summary>
        [Description("退出")] Aborted
    };

    /// <summary>
    /// 上传任务的控制函数
    /// </summary>
    /// <returns></returns>
    public delegate UploadControllerAction UploadController();
}