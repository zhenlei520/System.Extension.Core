// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations
{
    /// <summary>
    /// 图片缩放
    /// </summary>
    public class ImageModeType : Enumeration
    {
        /// <summary>
        /// 等比缩放，限制在指定w与h的矩形内的最大图片
        /// </summary>
        public static ImageModeType LimitMax = new ImageModeType(1, "等比缩放，限制在指定w与h的矩形内的最大图片");

        /// <summary>
        /// 等比缩放，延伸出指定w与h的矩形框外的最小图片
        /// </summary>
        public static ImageModeType LimitMin = new ImageModeType(2, "等比缩放，延伸出指定w与h的矩形框外的最小图片");

        /// <summary>
        /// 固定宽高，将延伸出指定w与h的矩形框外的最小图片进行居中裁剪
        /// </summary>
        public static ImageModeType Fill = new ImageModeType(3, "固定宽高，将延伸出指定w与h的矩形框外的最小图片进行居中裁剪");

        /// <summary>
        /// 固定宽高，缩放填充
        /// </summary>
        public static ImageModeType Pad = new ImageModeType(4, "固定宽高，缩放填充");

        /// <summary>
        /// 固定宽高，强制缩放
        /// </summary>
        public static ImageModeType Fixed = new ImageModeType(5, "固定宽高，强制缩放");

        /// <summary>
        /// 按比例缩放
        /// </summary>
        public static ImageModeType Proportion = new ImageModeType(6, "按比例缩放");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        public ImageModeType(int id, string name) : base(id, name)
        {
        }
    }
}
