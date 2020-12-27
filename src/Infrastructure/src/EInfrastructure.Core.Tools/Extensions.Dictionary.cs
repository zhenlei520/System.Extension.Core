// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 字典类型扩展
    /// </summary>
    public partial class Extensions
    {
        #region 字典转对象

        /// <summary>
        /// 字典类型转化为对象
        /// </summary>
        /// <param name="dic">字典</param>
        /// <returns></returns>
        public static object ConvertToObject(this IDictionary<string, object> dic)
        {
            return dic.ConvertToKeyValuePairs().ConvertToObject();
        }

        /// <summary>
        /// 字典类型转化为对象
        /// </summary>
        /// <param name="dic">字典</param>
        /// <returns></returns>
        public static T ConvertToObject<T>(this IDictionary<string, object> dic) where T : new()
        {
            var md = new T();
            foreach (var d in dic)
            {
                try
                {
                    var value = d.Value;
                    var property=md.GetType().GetProperty(d.Key);
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(md, value);
                    }

                }
                catch (Exception e)
                {
                    // ignored
                }
            }

            return md;
        }

        #endregion

        #region 字典类型转换为KeyValuePair集合

        /// <summary>
        /// 字典类型转换为KeyValuePair集合
        /// </summary>
        /// <param name="dic">字典</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> ConvertToKeyValuePairs(this IDictionary<string, object> dic)
        {
            if (dic.IsNullOrDbNull())
            {
                return new List<KeyValuePair<string, object>>();
            }

            return dic.Select(x => new KeyValuePair<string, object>(x.Key, x.Value)).ToList();
        }

        #endregion

        #region 得到安全的Dictionary类型

        /// <summary>
        /// 得到安全的Dictionary类型
        /// </summary>
        /// <param name="dictionary">字典类型</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> SafeDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                return new Dictionary<TKey, TValue>();
            }

            return dictionary;
        }

        #endregion

        #region 如果不存在则添加，否则跳过

        /// <summary>
        /// 如果不存在则添加，否则跳过
        /// </summary>
        /// <param name="dictionary">字典类型</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key, TValue value)
        {
            dictionary = dictionary.SafeDictionary();
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        /// <summary>
        /// 如果不存在则添加，否则跳过
        /// </summary>
        /// <param name="dictionary">字典类型</param>
        /// <param name="key">键</param>
        /// <param name="func">委托</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key, Func<TValue> func)
        {
            dictionary = dictionary.SafeDictionary();
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, func.Invoke());
            }

            return dictionary;
        }

        /// <summary>
        /// 如果不存在则添加，否则跳过
        /// </summary>
        /// <param name="dictionary">字典类型</param>
        /// <param name="dictionary2">待添加的字典</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue> dictionary2)
        {
            dictionary = dictionary.SafeDictionary();
            foreach (var item in dictionary2.SafeDictionary())
            {
                if (!dictionary.ContainsKey(item.Key))
                {
                    dictionary.Add(item.Key, item.Value);
                }
            }

            return dictionary;
        }

        #endregion

        #region 如果不存在则添加，否则则更新

        /// <summary>
        /// 如果不存在则添加，否则则更新
        /// </summary>
        /// <param name="dictionary">字典类型</param>
        /// <param name="dictionary2">待添加的字典</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> TryAddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue> dictionary2)
        {
            dictionary = dictionary.SafeDictionary();
            foreach (var item in dictionary2.SafeDictionary())
            {
                if (!dictionary.ContainsKey(item.Key))
                {
                    dictionary.Add(item.Key, item.Value);
                }
                else
                {
                    dictionary[item.Key] = item.Value;
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 如果不存在则添加，否则则更新
        /// </summary>
        /// <param name="dictionary">字典类型</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> TryAddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key, TValue value)
        {
            dictionary = dictionary.SafeDictionary();
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                dictionary[key] = value;
            }

            return dictionary;
        }

        #endregion
    }
}
