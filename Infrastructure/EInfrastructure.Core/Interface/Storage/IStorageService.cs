using EInfrastructure.Core.Interface.Storage.Param;

namespace EInfrastructure.Core.Interface.Storage
{
    /// <summary>
    /// 文件存储
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool UploadStream(UploadByStreamParam param);

        /// <summary>
        /// 根据文件上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool UploadFile(UploadByFormFileParam param);

    }
}