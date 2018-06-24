using System;
using EInfrastructure.Core.Interface.Storage.Config;

namespace EInfrastructure.Core.Interface.Storage.FormModel
{
    /// <summary>
    /// 根据字节数组上传图片
    /// </summary>
    public class UploadImgByByteArrayFormModel
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        public Guid FileNo { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 文件字节数组
        /// </summary>
        public byte[] Buffer { get; set; }

        /// <summary>
        /// 图片上传策略
        /// </summary>
        public ImgPersistentOps PersistentOps { get; set; }
    }
}
