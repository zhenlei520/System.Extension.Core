// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage.Pictures
{
    /// <summary>
    /// 旋转
    /// 旋转
    /// </summary>
    public class RotateParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="rotateType"></param>
        /// <param name="rotate"></param>
        public RotateParam(RotateType rotateType, int rotate)
        {
            this.RotateType = rotateType;
            this.Rotate = rotate;
        }
        /// <summary>
        /// 旋转类型
        /// </summary>
        public RotateType RotateType { get; }

        /// <summary>
        /// 旋转角度
        /// 图片按顺时针旋转的角度。[0, 360]
        /// 默认为0，不旋转
        /// </summary>
        public int Rotate { get; }
    }
}
