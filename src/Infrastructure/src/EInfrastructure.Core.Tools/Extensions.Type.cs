// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 类型扩展信息
    /// </summary>
    public partial class Extensions
    {
        #region 得到自定义描述

        /// <summary>
        /// 得到自定义描述
        /// </summary>
        /// <param name="sourceType">类类型</param>
        /// <param name="name">属性名称</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this Type sourceType, string name) where T : Attribute
        {
            if (!string.IsNullOrEmpty(name))
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = sourceType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    if (Attribute.GetCustomAttribute(fieldInfo,
                        typeof(T), false) is T attr)
                    {
                        return attr;
                    }
                }
            }

            return null;
        }

        #endregion

        #region 判断是否枚举

        /// <summary>
        /// 判断是否枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        #endregion
    }
}
