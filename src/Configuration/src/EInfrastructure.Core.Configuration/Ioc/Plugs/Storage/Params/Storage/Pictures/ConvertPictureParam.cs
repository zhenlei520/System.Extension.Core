// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage.Pictures
{
    /// <summary>
    /// 转换图片参数
    /// </summary>
    public class ConvertPictureParam
    {
        /// <summary>
        /// 缩放参数
        /// </summary>
        public ImageModeParam ImageMode { get; }

        /// <summary>
        /// 旋转参数
        /// </summary>
        public RotateParam Rotate { get; }
    }
}
