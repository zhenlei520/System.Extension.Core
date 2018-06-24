using System;
using EInfrastructure.Core.Interface.Storage.Config;

namespace EInfrastructure.Core.Interface.Storage.FormModel
{
    /// <summary>
    /// 通过源图上传
    /// </summary>
    public class UploadImgByOriginFormModel : FetchFileFormModel
    {
        /// <summary>
        /// 图片上传策略
        /// </summary>
        public ImgPersistentOps PersistentOps { get; set; }
    }
}
