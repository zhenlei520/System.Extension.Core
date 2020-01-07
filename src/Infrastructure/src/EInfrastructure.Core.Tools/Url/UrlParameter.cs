// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EInfrastructure.Core.Config.EnumerationExtensions;
using EInfrastructure.Core.Config.ExceptionExtensions;
using EInfrastructure.Core.Tools.Systems;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Tools.Url
{
    /// <summary>
    /// Url参数
    /// </summary>
    public class UrlParameter
    {
        public IDictionary<string, object> _params;

        /// <summary>
        ///
        /// </summary>
        public UrlParameter()
        {
            _params = new Dictionary<string, object>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query">例：a=1&b=2&c=3</param>
        /// <exception cref="BusinessException"></exception>
        public UrlParameter(string query)
        {
            _params = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(query))
                return;
            query.Split('&').ToList().ForEach(item =>
            {
                if (item.Split('=').Length != 2)
                {
                    throw new BusinessException("url参数异常");
                }

                Add(item.Split('=')[0].ToString(), item.Split('=')[1].ToString());
            });
        }

        /// <summary>
        /// 初始化参数生成器
        /// </summary>
        /// <param name="dictionary">字典</param>
        public UrlParameter(IDictionary<string, object> dictionary)
        {
            _params = dictionary == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(dictionary);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isOverride">存在是否覆盖，默认覆盖</param>
        /// <returns></returns>
        public UrlParameter Add(string key, object value, bool isOverride = true)
        {
            if (string.IsNullOrWhiteSpace(key))
                return this;
            value = GetValue(value);
            if (_params.ContainsKey(key))
                _params[key] = value;
            else
                _params.Add(key, value);
            return this;
        }

        /// <summary>
        /// 获取键值对集合
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> Get()
        {
            return _params;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _params.Clear();
        }

        /// <summary>
        /// 移除参数
        /// </summary>
        /// <param name="key">键</param>
        public bool Remove(string key)
        {
            return _params.Remove(key);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">参数名</param>
        public object GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            if (_params.ContainsKey(key))
                return _params[key];
            return string.Empty;
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="isSort">是否按参数名排序</param>
        /// <param name="isUrlEncode">是否Url编码</param>
        /// <param name="encoding">字符编码，默认值：UTF-8</param>
        public IDictionary<string, object> GetDictionary(bool isSort = true, bool isUrlEncode = false,
            Encoding encoding = null)
        {
            var result = _params.ToDictionary(t => t.Key,
                t => GetEncodeValue(t.Value, isUrlEncode, encoding ?? Encoding.UTF8));
            if (isSort == false)
                return result;
            return new SortedDictionary<string, object>(result);
        }

        /// <summary>
        /// 得到请求地址不包含域
        /// 格式：/api/user?参数1=参数值&参数2=参数值
        /// </summary>
        /// <param name="isSort">是否排序</param>
        /// <param name="isUrlEncode">是否url编码</param>
        /// <param name="encoding">编码格式，默认UTF8</param>
        /// <returns></returns>
        public string GetQueryResult(bool isSort = false, bool isUrlEncode = false, Encoding encoding = null)
        {
            string result = "";
            foreach (var param in GetDictionary(isSort, isUrlEncode, encoding))
                result += (param.Key + "=" + param.Value + "&");
            result = result.Trim();
            return result.Length > 1 ? result.Substring(0, result.Length - 1) : result;
        }

        /// <summary>
        /// 获取编码的值
        /// </summary>
        private object GetEncodeValue(object value, bool isUrlEncode, Encoding encoding)
        {
            if (isUrlEncode)
                return value.SafeString().UrlEncode(encoding);
            return value;
        }


        /// <summary>
        /// 获取值
        /// </summary>
        private object GetValue(object value)
        {
            if (value == null)
                return null;
            if (value is DateTime dateTime)
                return dateTime.FormatDate(FormatDateType.One);
            if (value is bool boolValue)
                return boolValue.ToString().ToLower();
            if (value is string)
                return value.SafeString().Trim();
            if (value is decimal || value is short || value is ushort || value is long || value is double)
                return value;
            if (value.GetType() == typeof(object))
                return JsonConvert.SerializeObject(value);
            return value;
        }
    }
}
