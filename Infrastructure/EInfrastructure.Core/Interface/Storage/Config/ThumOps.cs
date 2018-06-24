using EInfrastructure.Core.Interface.Storage.Enum;

namespace EInfrastructure.Core.Interface.Storage.Config
{
    /// <summary>
    /// 缩略图信息
    /// </summary>
    public class ThumOps
    {
        /// <summary>
        /// 缩略图信息
        /// </summary>
        /// <param name="key">图片相对地址</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="model">缩放信息</param>
        public ThumOps(string key, int width, int height, ImageModeEnum model = ImageModeEnum.Nothing)
        {
            Key = key;
            Width = width;
            Height = height;
            Mode = model;
        }

        /// <summary>
        /// 图片缩放信息
        /// </summary>
        public ImageModeEnum Mode { get; set; }

        /// <summary>
        /// 图片相对地址
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
    }
}
