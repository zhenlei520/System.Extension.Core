using System.IO;
using EInfrastructure.Core.Interface.Storage.Config;

namespace EInfrastructure.Core.Interface.Storage.Param
{
    /// <summary>
    /// 根据文件流上传
    /// </summary>
    public class UploadByStreamParam
    {
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