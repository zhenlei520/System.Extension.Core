// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// Url帮助类
    /// </summary>
    public class UrlCommon
    {
        #region 补全Url地址

        /// <summary>
        /// 补全Url地址
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="isHttps">是否https，默认不是https</param>
        /// <returns></returns>
        public static string CompletionUrl(string url, bool isHttps = false)
        {
            if (url.IsNullOrWhiteSpace())
            {
                return "";
            }

            if (url.StartsWith("//"))
            {
                return (isHttps ? "https" : "http") + ":" + url;
            }

            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }

            return (isHttps ? "https" : "http") + "://" + url;
        }

        #endregion
    }
}
