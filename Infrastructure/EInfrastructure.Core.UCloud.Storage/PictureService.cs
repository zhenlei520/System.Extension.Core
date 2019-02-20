using EInfrastructure.Core.Interface.IOC;
using EInfrastructure.Core.Interface.Storage;
using EInfrastructure.Core.Interface.Storage.Param.Pictures;

namespace EInfrastructure.Core.UCloud.Storage
{
    /// <summary>
    /// 图片服务
    /// </summary>
    public class PictureService : BaseStorageProvider, IPictureService, ISingleInstance
    {
        #region 根据图片base64上传

        /// <summary>
        /// 根据图片base64上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Upload(UploadByBase64Param param)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        #endregion
    }
}