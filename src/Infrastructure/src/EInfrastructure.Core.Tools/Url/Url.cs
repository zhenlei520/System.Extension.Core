// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

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
        /// <param name="url">完整的url地址</param>
        public Url(string url) : this(url, null)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="host">域</param>
        /// <param name="url">完整的url地址</param>
        /// <param name="isUseHttps"></param>
        public Url(string host, string url, bool? isUseHttps = null) : this(GetFullPath(host, url), isUseHttps)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url">完整的url地址</param>
        /// <param name="isUseHttps">是否是https，默认自动识别</param>
        public Url(string url, bool? isUseHttps)
        {
            if (string.IsNullOrEmpty(url))
                throw new BusinessException("url is not empty", HttpStatus.Err.Id);
            var uri = new Uri(url);
            Host = uri.Host;
            Scheme = uri.Scheme.ToLowers();
            IsHttps = isUseHttps ?? Scheme.Equals("https", StringComparison.CurrentCultureIgnoreCase);
            PathAndQuery = uri.PathAndQuery;
            Path = PathAndQuery.Split('?')[0].Trim();
            RequestUrl = url.Split('?')[0].Trim();
            RequestUrl = FormatUrl(RequestUrl, IsHttps);
            if (url.Split('?').Length > 1)
            {
                UrlParameter = new UrlParameter(url.Split('?')[1].Trim());
            }
            else
            {
                UrlParameter = new UrlParameter();
            }
        }

        /// <summary>
        /// 域
        /// </summary>
        public string Host { get; }

        /// <summary>
        ///
        /// </summary>
        public string Scheme { get; }

        /// <summary>
        /// 去除scheme以及host，携带参数
        /// </summary>
        public string PathAndQuery { get; }

        /// <summary>
        /// 去除scheme以及host，不携带参数
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 请求地址(带scheme)
        /// </summary>
        public string RequestUrl { get; }

        /// <summary>
        /// Url请求
        /// </summary>
        public UrlParameter UrlParameter { get; }

        /// <summary>
        /// 是否https
        /// </summary>
        public bool IsHttps { get; }

        #region 得到请求参数

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

        #endregion

        #region 得到请求地址不包含域

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
            var queryParam = GetQueryResult(isSort, isUrlEncode, encoding);
            if (!string.IsNullOrEmpty(queryParam))
            {
                return $"{RequestUrl.Replace(Scheme + "://" + Host, "")}?{queryParam}";
            }

            return $"{RequestUrl.Replace(Scheme + "://" + Host, "")}";
        }

        #endregion

        #region 得到完整的请求地址

        /// <summary>
        /// 得到完整的请求地址
        /// 格式：{host}/api/user?参数1=参数值&参数2=参数值
        /// </summary>
        /// <param name="isSort">是否排序</param>
        /// <param name="isUrlEncode">是否url编码</param>
        /// <param name="encoding">编码格式，默认UTF8</param>
        /// <param name="isContainerScheme">是否包含Scheme</param>
        /// <returns></returns>
        public string GetFullQueryPath(bool isSort = false, bool isUrlEncode = false, Encoding encoding = null,
            bool isContainerScheme = false)
        {
            var res = $"{Host}{GetQueryPath(isSort, isUrlEncode, encoding)}";
            if (isContainerScheme)
            {
                return Scheme + "://" + res;
            }

            return res;
        }

        #endregion

        #region private methods

        #region 获取Url

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

        #endregion

        #region 标准化Url地址

        /// <summary>
        /// 标准化Url地址
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <param name="isHttps">是否https</param>
        /// <returns></returns>
        private string FormatUrl(string url, bool isHttps)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return "";
            }

            if (url.StartsWith("//"))
            {
                if (isHttps)
                {
                    return $"https:" + url;
                }

                return $"http:{url}";
            }

            if (url.StartsWith("http:") && isHttps)
            {
                return $"https:{url.Substring(5)}";
            }

            if (url.StartsWith("https:") && !isHttps)
            {
                return $"http:{url.Substring(6)}";
            }

            return url;
        }

        #endregion

        #region 得到完整的url

        /// <summary>
        /// 得到完整的url
        /// </summary>
        /// <param name="host">host地址，带scheme</param>
        /// <param name="url">url地址，不考虑直接就是参数的情况</param>
        /// <returns></returns>
        private static string GetFullPath(string host, string url)
        {
            if (url.IsNullOrEmpty())
            {
                return host;
            }
            else
            {
                if (url.StartsWith("?") || url.StartsWith("/"))
                {
                    if (host.EndsWith("/"))
                    {
                        return host.Substring(0, host.Length - 1) + url;
                    }

                    return host + url;
                }
                else
                {
                    if (host.EndsWith("/"))
                    {
                        return host + url;
                    }
                    else
                    {
                        return host + "/" + url;
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
