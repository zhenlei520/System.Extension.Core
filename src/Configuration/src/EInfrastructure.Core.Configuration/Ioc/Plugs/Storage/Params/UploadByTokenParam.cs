using System;
using System.IO;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Param
{
    /// <summary>
    /// 根据token上传文件
    /// </summary>
    public class UploadByTokenParam
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="stream">文件流</param>
        /// <param name="token"></param>
        /// <param name="uploadPersistentOps"></param>
        public UploadByTokenParam(string key, Stream stream, string token,
            UploadPersistentOps uploadPersistentOps = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream cannot be empty");
            }

            Key = key;
            Stream = stream;
            Token = token;
            UploadPersistentOps = uploadPersistentOps;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="byteArray">文件字符数组</param>
        /// <param name="token">文件Token</param>
        /// <param name="uploadPersistentOps">上传策略</param>
        public UploadByTokenParam(string key, byte[] byteArray, string token,
            UploadPersistentOps uploadPersistentOps = null)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                throw new ArgumentNullException("byteArray cannot be empty");
            }

            Key = key;
            ByteArray = byteArray;
            Token = token;
            UploadPersistentOps = uploadPersistentOps;
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 文件流
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// 文件字符数组
        /// </summary>
        public byte[] ByteArray { get; }

        /// <summary>
        /// 文件Token
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// 上传策略
        /// </summary>
        public UploadPersistentOps UploadPersistentOps { get; }
    }
}
