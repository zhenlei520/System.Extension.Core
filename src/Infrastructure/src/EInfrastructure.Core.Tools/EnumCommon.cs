// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumCommon
    {
        private static Hashtable _enumDesciption;

        static EnumCommon()
        {
            _enumDesciption = GetDescriptionContainer();
        }

        private static Hashtable GetDescriptionContainer()
        {
            _enumDesciption = new Hashtable();
            return _enumDesciption;
        }

        #region 得到枚举字典（key对应枚举的值，value对应枚举的注释）

        /// <summary>
        /// 得到枚举字典（key对应枚举的值，value对应枚举的注释）
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, string> ToDescriptionDictionary<TEnum>()
        {
            Array arrays = Enum.GetValues(typeof(TEnum));
            Dictionary<int, string> dics = new Dictionary<int, string>();
            foreach (Enum value in arrays)
            {
                dics.Add(Convert.ToInt32(value), GetDescription(value));
            }

            return dics;
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
                dics.Add(item, GetCustomerObj<T>(item));
            }

            return dics;
        }

        #endregion

        #region 得到枚举字典key的集合

        /// <summary>
        /// 得到枚举字典key的集合（例如：enum Gender{
        ///    Boy=0,
        ///    Girl=1
        /// }）//其中Girl与Boy为Key
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static List<string> GetKeys<TEnum>()
        {
            Array arrays = Enum.GetValues(typeof(TEnum));
            List<string> keys = new List<string>();
            foreach (Enum key in arrays)
            {
                keys.Add(key.ToString());
            }

            return keys;
        }

        #endregion

        #region 得到枚举字典value的集合

        /// <summary>
        /// 得到枚举字典value的集合
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static List<int> GetValues<TEnum>()
        {
            Array arrays = Enum.GetValues(typeof(TEnum));
            List<int> values = new List<int>();
            foreach (Enum value in arrays)
            {
                values.Add(Convert.ToInt32(value));
            }

            return values;
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
                list.Add(CustomAttributeCommon<T>.GetCustomAttribute(type, nameof(array)));
            }

            return list;
        }

        /// <summary>
        /// 得到枚举字典自定义属性的集合
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Obsolete("此方法已过时，建议使用typeof(Enum).GetCustomerObjects()")]
        public static List<T> GetCustomerObjects<T>(this Enum value) where T : Attribute
        {
            return GetCustomerObjects<T>(value.GetType());
        }

        #endregion

        #region 返回枚举项的描述信息

        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string GetDescription(this Enum value)
        {
            return CustomAttributeCommon<DescriptionAttribute>.GetCustomAttribute(value.GetType(), value.ToString())
                ?.Description;
        }

        #endregion

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

        #region 判断值是否在枚举类型中存在

        /// <summary>
        /// 判断值是否在枚举中存在
        /// </summary>
        /// <param name="enumValue">需要判断的参数</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static bool IsExist(this int enumValue, Type enumType)
        {
            return Enum.IsDefined(enumType, enumValue);
        }

        /// <summary>
        /// 判断值是否在枚举中存在
        /// </summary>
        /// <param name="enumValue">需要判断的参数</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static bool IsExist(this int? enumValue, Type enumType)
        {
            if (enumValue == null)
                return false;
            return ((int) enumValue).IsExist(enumType);
        }

        #endregion
    }
}
