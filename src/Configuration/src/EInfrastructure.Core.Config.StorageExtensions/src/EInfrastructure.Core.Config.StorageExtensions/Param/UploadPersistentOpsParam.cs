// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.StorageExtensions.Config;

namespace EInfrastructure.Core.Config.StorageExtensions.Param
{
    /// <summary>
    /// 上传文件（前端上传，后台生成策略信息）
    /// </summary>
    public class UploadPersistentOpsParam : UploadParam
    {
        /// <summary>
        /// 上传文件（前端上传，后台生成策略信息）
        /// </summary>
        /// <param name="key">文件地址</param>
        /// <param name="uploadPersistentOps">上传配置</param>
        public UploadPersistentOpsParam(string key, UploadPersistentOps uploadPersistentOps = null)
        {
            Key = key;
            UploadPersistentOps = uploadPersistentOps ?? new UploadPersistentOps();
        }

        /// <summary>
        /// 待上传文件名称
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 上传策略
        /// </summary>
        public UploadPersistentOps UploadPersistentOps { get; private set; }
    }
}
