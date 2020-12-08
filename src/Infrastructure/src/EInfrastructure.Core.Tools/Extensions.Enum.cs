// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 枚举信息扩展
    /// </summary>
    public partial class Extensions
    {
        #region 得到自定义描述

        /// <summary>
        /// 得到自定义描述
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetCustomerObj<T>(this Enum value) where T : Attribute
        {
            return CustomAttributeCommon<T>.GetCustomAttribute(value.GetType(), value.ToString());
        }

        #endregion
    }
}
