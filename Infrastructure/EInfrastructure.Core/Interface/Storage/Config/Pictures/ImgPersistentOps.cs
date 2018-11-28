using System.Collections.Generic;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Storage.Enum;

namespace EInfrastructure.Core.Interface.Storage.Config.Pictures
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
        public ImgPersistentOps(string key, int width = 0, int height = 0, ImageModeEnum model = ImageModeEnum.Nothing,
            StorageWaterMark waterMark = null)
        {
            Key = key;
            Width = width;
            Height = height;
            Mode = model;
            WaterMark = waterMark;
        }

        /// <summary>
        /// 图片缩放信息
        /// </summary>
        public ImageModeEnum Mode { get; set; } = ImageModeEnum.Nothing;

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