// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.StorageExtensions.Config;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.Config.StorageExtensions.Param
{
    /// <summary>
    /// 根据文件上传
    /// </summary>
    public class UploadByFormFileParam: UploadParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="file">文件信息</param>
        /// <param name="uploadPersistentOps">上传策略</param>
        public UploadByFormFileParam(string key, IFormFile file, UploadPersistentOps uploadPersistentOps = null)
        {
            Key = key;
            File = file;
            UploadPersistentOps = uploadPersistentOps;
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 文件流
        /// </summary>
        public IFormFile File { get; private set; }

        /// <summary>
        /// 上传策略
        /// </summary>
        public UploadPersistentOps UploadPersistentOps { get; private set; }
    }
}
