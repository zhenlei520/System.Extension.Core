using EInfrastructure.Core.Interface.Storage.Param.Pictures;

namespace EInfrastructure.Core.Interface.Storage
{
    /// <summary>
    /// 图片
    /// </summary>
    public interface IPictureService
    {
        /// <summary>
        /// 根据图片base64流上传图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Upload(UploadByBase64Param param);
    }
}
