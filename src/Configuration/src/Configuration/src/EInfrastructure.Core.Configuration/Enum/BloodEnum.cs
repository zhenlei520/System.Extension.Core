// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Configuration.Enum
{
    /// <summary>
    /// 血型
    /// </summary>
    public enum BloodEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")] Unknow = 0,

        /// <summary>
        /// A型
        /// </summary>
        [Description("A型")] A = 1,

        /// <summary>
        /// B型
        /// </summary>
        [Description("B型")] B = 2,

        /// <summary>
        /// Ab型
        /// </summary>
        [Description("AB型")] Ab = 3,

        /// <summary>
        /// O型
        /// </summary>
        [Description("O型")] O = 4
    }
}