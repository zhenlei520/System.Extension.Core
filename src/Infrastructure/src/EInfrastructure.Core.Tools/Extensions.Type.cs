// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using EInfrastructure.Core.Configuration.Exception;

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

        #region 得到枚举对应的值与自定义属性集合

        /// <summary>
        /// 得到枚举与对应的自定义属性信息
        /// </summary>
        /// <typeparam name="T">自定义属性</typeparam>
        /// <returns></returns>
        public static Dictionary<Enum, T> ToEnumAndAttributes<T>(this Type type) where T : Attribute
        {
            Array arrays = Enum.GetValues(type);
            Dictionary<Enum, T> dics = new Dictionary<Enum, T>();
            foreach (Enum item in arrays)
            {
                dics.Add(item, item.GetCustomerObj<T>());
            }

            return dics;
        }

        #endregion

        #region 得到枚举字典自定义属性的集合

        /// <summary>
        /// 得到枚举字典自定义属性的集合
        /// </summary>
        /// <param name="type">type必须是枚举</param>
        /// <returns></returns>
        public static List<T> GetCustomerObjects<T>(this Type type) where T : Attribute
        {
            if (!type.IsEnum)
            {
                throw new BusinessException(nameof(type) + "不是枚举");
            }

            Array arrays = Enum.GetValues(type);
            List<T> list = new List<T>();
            foreach (Enum array in arrays)
            {
                list.Add(type.GetCustomAttribute<T>(nameof(array)));
            }

            return list;
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
