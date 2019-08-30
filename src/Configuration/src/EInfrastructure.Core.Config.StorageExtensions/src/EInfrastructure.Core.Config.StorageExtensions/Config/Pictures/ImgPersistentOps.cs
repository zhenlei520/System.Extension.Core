// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.StorageExtensions.Enumeration;

namespace EInfrastructure.Core.Config.StorageExtensions.Config.Pictures
{
    /// <summary>
    /// 上传策略
    /// </summary>
    public class ImgPersistentOps
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
                : Configuration.SeedWork.Enumeration.FromValue<ImageMode>(model.Value);
            WaterMark = waterMark;
        }

        /// <summary>
        /// 图片缩放信息
        /// </summary>
        public ImageMode Mode { get; set; } = ImageMode.Nothing;

        /// <summary>
        /// 图片相对地址
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; } = 0;

        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; } = 0;

        /// <summary>
        /// 是否覆盖上传
        /// </summary>
        public bool IsAllowOverlap { get; set; } = true;

        /// <summary>
        /// 水印信息
        /// </summary>
        public StorageWaterMark WaterMark { get; set; } = null;
    }
}
