using System.Collections.Generic;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Storage.Enum;

namespace EInfrastructure.Core.Interface.Storage.Config
{
    /// <summary>
    /// 上传策略
    /// </summary>
    public class ImgPersistentOps
    {
        public ImgPersistentOps()
        {
        }

        /// <summary>
        /// 上传策略
        /// </summary>
        /// <param name="key">图片相对地址</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="model">缩放信息</param>
        /// <param name="waterMark">水印信息</param>
        /// <param name="thumOpsList">缩略图信息</param>
        public ImgPersistentOps(string key, int width = 0, int height = 0, ImageModeEnum model = ImageModeEnum.Nothing,
            StorageWaterMark waterMark = null, List<ThumOps> thumOpsList = null)
        {
            Key = key;
            Width = width;
            Height = height;
            Mode = model;
            WaterMark = waterMark;
            if (thumOpsList != null)
            {
                ThumOpsList = thumOpsList;
            }
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
        /// 扩展
        /// </summary>
        public string Ext { get; protected set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileTypeEnum FileType { get; protected set; } = FileTypeEnum.Other;

        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; } = 0;

        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; } = 0;

        /// <summary>
        /// 水印信息
        /// </summary>
        public StorageWaterMark WaterMark { get; set; } = null;

        /// <summary>
        /// 图片缩略图信息
        /// </summary>
        public List<ThumOps> ThumOpsList { get; set; } = new List<ThumOps>();

        /// <summary>
        /// 存储配置信息
        /// 对应七牛配置信息
        /// 对应本地配置信息
        /// 对应阿里云配置信息
        /// 对应腾讯云配置信息
        /// </summary>
        public object Json { get; protected set; }

        public virtual void CheckData()
        {
            if (!string.IsNullOrEmpty(Key))
                Key = Key.UrlDecode();
        }
    }
}