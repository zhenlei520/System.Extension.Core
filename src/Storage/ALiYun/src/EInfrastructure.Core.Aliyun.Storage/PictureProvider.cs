using System.Reflection;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Logger;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage.Pictures;
using EInfrastructure.Core.Tools;

namespace EInfrastructure.Core.Aliyun.Storage
{
    /// <summary>
    /// 图片管理
    /// </summary>
    public class PictureProvider : BaseStorageProvider, IPictureProvider
    {
        private readonly IStorageProvider _storageProvider;
        private readonly ILoggerProvider _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="aLiYunStorageConfig"></param>
        public PictureProvider(ALiYunStorageConfig aLiYunStorageConfig) : this(aLiYunStorageConfig, null)
        {
        }

        public PictureProvider(ALiYunStorageConfig aLiYunStorageConfig, ILoggerProvider<PictureProvider> logger) : base(
            aLiYunStorageConfig)
        {
            this._logger = logger;
            _storageProvider = new StorageProvider(aLiYunStorageConfig, logger);
        }

        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 99;
        }

        #endregion

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region 根据图片base64流上传图片(图片处理还未做)

        /// <summary>
        /// 根据图片base64流上传图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Upload(UploadByBase64Param param)
        {
            var result = _storageProvider.UploadByteArray(new UploadByByteArrayParam(param.ImgPersistentOps.Key,
                param.Base64.ConvertToByte(), param.ImgPersistentOps));
            if (result.State)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
