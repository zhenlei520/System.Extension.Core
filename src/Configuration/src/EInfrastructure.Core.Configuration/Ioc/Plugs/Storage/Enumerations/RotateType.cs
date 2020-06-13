// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations
{
    /// <summary>
    /// 旋转类型
    /// </summary>
    public class RotateType : Enumeration
    {
        /// <summary>
        /// 不旋转
        /// </summary>
        public static RotateType None = new RotateType(1, "不旋转");

        /// <summary>
        /// 自动旋转
        /// </summary>
        public static RotateType Auto = new RotateType(2, "自动旋转");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public RotateType(int id, string name) : base(id, name)
        {
        }
    }
}
