using System;
using System.IO;
using EInfrastructure.Core.Interface.Storage.Config;

namespace EInfrastructure.Core.Interface.Storage.FormModel
{
    public class UploadImgByStreamFormModel
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        public Guid FileNo { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 文件Stream
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// 图片上传策略
        /// </summary>
        public ImgPersistentOps PersistentOps { get; set; }
    }
}
