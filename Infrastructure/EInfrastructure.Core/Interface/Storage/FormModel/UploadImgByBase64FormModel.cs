using System;
using EInfrastructure.Core.Interface.Storage.Config;

namespace EInfrastructure.Core.Interface.Storage.FormModel
{
    /// <summary>
    /// 根据base64上传图片
    /// </summary>
    public class UploadImgByBase64FormModel
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        public Guid FileNo { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 文件base64
        /// </summary>
        public string Base64 { get; set; }

        /// <summary>
        /// 图片上传策略
        /// </summary>
        public ImgPersistentOps PersistentOps { get; set; }
    }
}
