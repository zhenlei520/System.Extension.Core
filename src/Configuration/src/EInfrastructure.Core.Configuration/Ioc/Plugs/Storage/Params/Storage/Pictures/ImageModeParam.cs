// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage.Pictures
{
    /// <summary>
    /// 图片缩放
    /// </summary>
    public class ImageModeParam
    {
        /// <summary>
        ///
        /// </summary>
        private ImageModeParam()
        {
            this.Width = null;
            this.Height = null;
            this.MinLength = null;
            this.MaxLenght = null;
            this.Limit = null;
            this.Proportion = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mode">图片缩放信息</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="minLenght">指定目标缩略图的最短边</param>
        /// <param name="maxLength">指定目标缩略图的最长边</param>
        /// <param name="isLimit">指定当目标缩略图大于原图时是否处理,值是true表示不处理；值是false表示处理(默认为true)</param>
        public ImageModeParam(ImageModeType mode, int? width, int? height, int? minLenght, int? maxLength,
            bool? isLimit)
        {
            this.Mode = mode;
            this.Width = width;
            this.Height = height;
            this.Limit = isLimit;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="minLenght">指定目标缩略图的最短边</param>
        /// <param name="maxLength">指定目标缩略图的最长边</param>
        /// <param name="color">填充的颜色，默认是白色。参数的填写方式：采用16进制颜色码表示，例如00FF00（绿色）</param>
        /// <param name="isLimit">指定当目标缩略图大于原图时是否处理,值是true表示不处理；值是false表示处理(默认为true)</param>
        public ImageModeParam(int? width, int? height, int? minLenght, int? maxLength, string color,
            bool? isLimit) : this(
            ImageModeType.Pad, width,
            height, minLenght, maxLength, isLimit)
        {
            this.Color = color;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="proportion">缩放比例。倍数百分比。小于100为缩小，大于100为放大</param>
        public ImageModeParam(int proportion) : this()
        {
            this.Mode = ImageModeType.Proportion;
            this.Proportion = proportion;
        }

        /// <summary>
        /// 图片缩放信息
        /// </summary>
        public ImageModeType Mode { get; }

        /// <summary>
        /// 宽
        /// 阿里云：1~4096
        /// </summary>
        public int? Width { get; }

        /// <summary>
        /// 高
        /// 阿里云：1~4096
        /// </summary>
        public int? Height { get; }

        /// <summary>
        /// 指定目标缩略图的最长边
        /// 阿里云：1~4096
        /// </summary>
        public int? MaxLenght { get; }

        /// <summary>
        /// 指定目标缩略图的最短边
        /// 阿里云：1~4096
        /// </summary>
        public int? MinLength { get; }

        /// <summary>
        /// 指定当目标缩略图大于原图时是否处理。
        /// 值是true表示不处理；值是false表示处理
        /// 默认是true（不处理）
        /// </summary>
        public bool? Limit { get; }

        /// <summary>
        /// 阿里云
        /// 当缩放模式选择为pad（缩放填充）时，可以选择填充的颜色，
        /// 默认是白色。参数的填写方式：采用16进制颜色码表示，例如00FF00（绿色）。
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// 缩放比例
        /// 阿里云：倍数百分比。小于100为缩小，大于100为放大
        /// </summary>
        public int? Proportion { get; }
    }
}
