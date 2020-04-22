// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params
{
    /// <summary>
    /// 根据文件流上传
    /// </summary>
    public class UploadByStreamParam
    {
        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="key">文件地址</param>
        /// <param name="stream">文件流</param>
        /// <param name="uploadPersistentOps">上传配置</param>
        public UploadByStreamParam(string key, Stream stream, UploadPersistentOps uploadPersistentOps = null)
        {
            Key = key;
            Stream = stream;
            UploadPersistentOps = uploadPersistentOps;
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 文件流
        /// </summary>
        public Stream Stream { get; private set; }

        /// <summary>
        /// 上传策略
        /// </summary>
        public UploadPersistentOps UploadPersistentOps { get; private set; }
    }
}
