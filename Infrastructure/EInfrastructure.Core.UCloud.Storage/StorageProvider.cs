using System.IO;
using EInfrastructure.Core.AutoConfig.Extension;
using EInfrastructure.Core.Interface.IOC;
using EInfrastructure.Core.Interface.Storage;
using EInfrastructure.Core.Interface.Storage.Param;
using EInfrastructure.Core.UCloud.Storage.Config;

namespace EInfrastructure.Core.UCloud.Storage
{
    /// <summary>
    /// 文件存储
    /// </summary>
    public class StorageProvider : BaseStorageProvider, IStorageService, ISingleInstance
    {
        public StorageProvider(IWritableOptions<UCloudConfig> uCloudConfig) : base(uCloudConfig.Get<UCloudConfig>())
        {
            
        }

        #region 根据文件流上传

        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool UploadStream(UploadByStreamParam param)
        {
            return base.UploadFile(param.Stream, param.Key, Path.GetExtension(param.Key));
        }

        #endregion

        public bool UploadFile(UploadByFormFileParam param)
        {
            throw new System.NotImplementedException();
        }

        public string GetUploadCredentials(UploadPersistentOpsParam opsParam)
        {
            throw new System.NotImplementedException();
        }
    }
}