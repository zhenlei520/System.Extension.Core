// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Config.StorageExtensions.Enumeration
{
    /// <summary>
    /// 上传状态
    /// </summary>
    public class UploadState : EInfrastructure.Core.Configuration.SeedWork.Enumeration
    {
        /// <summary>
        /// 激活
        /// </summary>
        public static UploadState Activated = new UploadState(0, "激活");

        /// <summary>
        /// 激活
        /// </summary>
        public static UploadState Suspended = new UploadState(1, "暂停");

        /// <summary>
        /// 退出
        /// </summary>
        public static UploadState Aborted = new UploadState(2, "退出");

        public UploadState(int id, string name) : base(id, name)
        {
        }
    }
}
