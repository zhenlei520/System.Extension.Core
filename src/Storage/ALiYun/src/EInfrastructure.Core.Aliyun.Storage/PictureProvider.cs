using System.Reflection;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage.Pictures;
using EInfrastructure.Core.Tools;

namespace EInfrastructure.Core.Aliyun.Storage
{
    public class PictureProvider : BaseStorageProvider, IPictureProvider
    {
        private readonly IStorageProvider _storageProvider;

        public PictureProvider(ALiYunStorageConfig aLiYunStorageConfig) : base(aLiYunStorageConfig)
        {
            _storageProvider = new StorageProvider(aLiYunStorageConfig);
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

        #region 抓取资源到空间

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFileParam">资源信息</param>
        /// <returns></returns>
        public bool FetchFile(FetchFileParam fetchFileParam)
        {
            var downloadStream =
                _storageProvider.DownloadStream(new FileDownloadStreamParam(fetchFileParam.SourceFileKey));
            if (downloadStream.State)
            {
                var ret = _storageProvider.UploadStream(new UploadByStreamParam(fetchFileParam.Key, downloadStream.FileStream,
                    new UploadPersistentOps()
                    {
                        Zone = fetchFileParam.PersistentOps.Zone,
                        Bucket = fetchFileParam.PersistentOps.Bucket,
                        Host = fetchFileParam.PersistentOps.Host,
                        IsUseHttps = fetchFileParam.PersistentOps.IsUseHttps,
                        UseCdnDomains = fetchFileParam.PersistentOps.UseCdnDomains,
                        ChunkUnit = fetchFileParam.PersistentOps.ChunkUnit,
                        MaxRetryTimes = fetchFileParam.PersistentOps.MaxRetryTimes,
                    }));
                if (ret.State)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
