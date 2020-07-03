// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 自定义属性
    /// CustomAttributeCommon<ENameAttribute, string>.GetCustomAttributeValue(typeof(Test), x => x.Name)
    /// </summary>
    /// 其中Test为用户自定义类
    public static class CustomAttributeCommon<T, TSource>
        where T : Attribute
        where TSource : IComparable
    {
        /// <summary>
        /// Cache Data
        /// </summary>
        private static readonly Dictionary<string, TSource> Cache = new Dictionary<string, TSource>();

        #region 获取自定义属性的值

        /// <summary>
        /// 获取自定义属性的值
        /// </summary>
        /// <param name="sourceType">头部标有CustomAttribute类的类型</param>
        /// <param name="attributeValueAction">取Attribute具体哪个属性值的匿名函数</param>
        /// <param name="name">field name或property name</param>
        /// <returns>返回Attribute的值，没有则返回null</returns>
        public static TSource GetCustomAttributeValue(Type sourceType,
            Func<T, TSource> attributeValueAction,
            string name = null)
        {
            return GetAttributeValue(sourceType, attributeValueAction, name);
        }

        #endregion

        #region private methods

        #region 结合缓存得到自定义属性的值

        /// <summary>
        /// 结合缓存得到自定义属性的值
        /// </summary>
        /// <param name="sourceType">头部标有CustomAttribute类的类型</param>
        /// <param name="attributeValueAction">取Attribute具体哪个属性值的匿名函数</param>
        /// <param name="name"></param>
        /// <typeparam name="T">Attribute的子类型</typeparam>
        /// <typeparam name="TSource">属性的类型</typeparam>
        /// <returns></returns>
        private static TSource GetAttributeValue(Type sourceType, Func<T, TSource> attributeValueAction,
            string name)
        {
            var key = BuildKey(sourceType, name);
            if (!Cache.ContainsKey(key))
            {
                CacheAttributeValue(sourceType, attributeValueAction, name);
            }

            return Cache[key];
        }

        #endregion

        #region 缓存自定义属性value

        /// <summary>
        /// 缓存自定义属性value
        /// </summary>
        private static void CacheAttributeValue(Type type,
            Func<T, TSource> attributeValueAction, string name)
        {
            var key = BuildKey(type, name);

            var value = GetValue(type, attributeValueAction, name);

            lock (key + "_attributeValueLockKey")
            {
                if (!Cache.ContainsKey(key))
                {
                    Cache[key] = value;
                }
            }
        }

        #endregion

        #region 得到自定义属性或者类指定信息

        /// <summary>
        /// 得到自定义属性或者类指定信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attributeValueAction"></param>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSource">属性类型</typeparam>
        /// <returns></returns>
        private static TSource GetValue(Type type,
            Func<T, TSource> attributeValueAction, string name)
        {
            object attribute = null;
            if (string.IsNullOrEmpty(name))
            {
                attribute =
                    type.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            }
            else
            {
                var propertyInfo = type.GetProperty(name);
                if (propertyInfo != null)
                {
                    attribute =
                        propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                }

                var fieldInfo = type.GetField(name);
                if (fieldInfo != null)
                {
                    attribute = fieldInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                }
            }

            return attribute == null ? default(TSource) : attributeValueAction((T) attribute);
        }

        #endregion

        #region 构建缓存key

        /// <summary>
        /// 构建缓存key
        /// </summary>
        private static string BuildKey(Type type, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return type.FullName;
            }

            return type.FullName + "." + name;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 自定义属性帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class CustomAttributeCommon<T> where T : Attribute
    {
        #region 得到自定义描述

        /// <summary>
        /// 得到自定义描述
        /// </summary>
        /// <param name="sourceType">类类型</param>
        /// <param name="name">属性名称</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetCustomAttribute(Type sourceType, string name)
        {
            // 获取枚举常数名称。
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
    }
}
