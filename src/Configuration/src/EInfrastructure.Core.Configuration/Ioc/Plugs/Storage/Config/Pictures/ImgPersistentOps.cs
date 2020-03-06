// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config.Pictures
{
    /// <summary>
    /// 上传策略
    /// </summary>
    public class ImgPersistentOps : UploadPersistentOps
    {
        /// <summary>
        /// 上传策略
        /// </summary>
        /// <param name="key">图片相对地址</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="model">缩放信息</param>
        /// <param name="waterMark">水印信息</param>
        public ImgPersistentOps(string key, int width = 0, int height = 0, int? model = null,
            StorageWaterMark waterMark = null)
        {
            Key = key;
            Width = width;
            Height = height;
            Mode = model == null
                ? ImageMode.Nothing
                : Enumeration.FromValue<ImageMode>(model.Value);
            WaterMark = waterMark;
        }

        /// <summary>
        /// 图片缩放信息
        /// </summary>
        public virtual ImageMode Mode { get; set; } = ImageMode.Nothing;

        /// <summary>
        /// 图片相对地址
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public virtual int Width { get; set; } = 0;

        /// <summary>
        /// 高
        /// </summary>
        public virtual int Height { get; set; } = 0;

        /// <summary>
        /// 是否覆盖上传
        /// </summary>
        public override bool IsAllowOverlap { get; set; } = true;

        /// <summary>
        /// 水印信息
        /// </summary>
        public virtual StorageWaterMark WaterMark { get; set; } = null;
    }
}
