// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Param
{
    /// <summary>
    /// 根据文件字节数组上传
    /// </summary>
    public class UploadByByteArrayParam
    {
        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="key">文件地址</param>
        /// <param name="byteArray">字节数组</param>
        /// <param name="uploadPersistentOps">上传配置</param>
        public UploadByByteArrayParam(string key, byte[] byteArray, UploadPersistentOps uploadPersistentOps = null)
        {
            Key = key;
            ByteArray = byteArray;
            UploadPersistentOps = uploadPersistentOps;
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 文件流
        /// </summary>
        public byte[] ByteArray { get; private set; }

        /// <summary>
        /// 上传策略
        /// </summary>
        public UploadPersistentOps UploadPersistentOps { get; private set; }
    }
}
