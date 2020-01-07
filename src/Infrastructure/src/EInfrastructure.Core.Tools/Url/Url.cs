// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using EInfrastructure.Core.Config.ExceptionExtensions;

namespace EInfrastructure.Core.Tools.Url
{
    /// <summary>
    /// url地址
    /// </summary>
    public class Url
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url">url地址</param>
        public Url(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new BusinessException("url is not empty");
            var uri = new Uri(url);
            Host = uri.Host;
            RequestUrl = url.Split('?')[0].Trim();
            UrlParameter = new UrlParameter(uri.Query);
        }

        /// <summary>
        /// 域
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; }

        /// <summary>
        /// Url请求
        /// </summary>
        public UrlParameter UrlParameter { get; }

        /// <summary>
        /// 得到请求参数
        /// 格式：参数1=参数值&参数2=参数值
        /// </summary>
        /// <param name="isSort">是否排序</param>
        /// <param name="isUrlEncode">是否url编码</param>
        /// <param name="encoding">编码格式，默认UTF8</param>
        /// <returns></returns>
        public string GetQueryResult(bool isSort = false, bool isUrlEncode = false, Encoding encoding = null)
        {
            return UrlParameter.GetQueryResult(isSort, isUrlEncode, encoding);
        }

        /// <summary>
        /// 得到请求地址不包含域
        /// 格式：/api/user?参数1=参数值&参数2=参数值
        /// </summary>
        /// <param name="isSort">是否排序</param>
        /// <param name="isUrlEncode">是否url编码</param>
        /// <param name="encoding">编码格式，默认UTF8</param>
        /// <returns></returns>
        public string GetQueryPath(bool isSort = false, bool isUrlEncode = false, Encoding encoding = null)
        {
            return $"{RequestUrl.Replace(Host, "")}{GetQueryResult(isSort, isUrlEncode, encoding)}";
        }

        /// <summary>
        /// 得到完整的请求地址
        /// 格式：{host}/api/user?参数1=参数值&参数2=参数值
        /// </summary>
        /// <param name="isSort">是否排序</param>
        /// <param name="isUrlEncode">是否url编码</param>
        /// <param name="encoding">编码格式，默认UTF8</param>
        /// <returns></returns>
        public string GetFullQueryPath(bool isSort = false, bool isUrlEncode = false, Encoding encoding = null)
        {
            return $"{Host}{GetQueryPath(isSort, isUrlEncode, encoding)}";
        }

        /// <summary>
        /// 获取Url
        /// </summary>
        private static string GetUrl(string url)
        {
            if (!url.Contains("?"))
                return $"{url}?";
            if (url.EndsWith("?"))
                return url;
            if (url.EndsWith("&"))
                return url;
            return $"{url}&";
        }
    }
}
