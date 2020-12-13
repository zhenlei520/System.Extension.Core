// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// url地址方法
    /// </summary>
    public static class UrlCommon
    {
        #region 得到url地址

        /// <summary>
        /// 得到url地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUrl(this string str)
        {
            string regexStr = "[a-zA-z]+://[^\\s]*";
            Regex reg = new Regex(regexStr, RegexOptions.Multiline);
            MatchCollection matchs = reg.Matches(str);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    return item.Value;
                }
            }

            throw new BusinessException("无效的链接", HttpStatus.Err.Id);
        }

        #endregion

        #region Url编码

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="target">待加密字符串</param>
        /// <returns></returns>
        public static string UrlEncode(this string target)
        {
            return HttpUtility.UrlEncode(target);
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="target">待加密字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string UrlEncode(this string target, Encoding encoding)
        {
            return HttpUtility.UrlEncode(target, encoding);
        }

        #endregion

        #region Url解码

        /// <summary>
        ///
        /// </summary>
        /// <param name="target">待解密字符串</param>
        /// <returns></returns>
        public static string UrlDecode(this string target)
        {
            return HttpUtility.UrlDecode(target);
        }

        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="target">待解密字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string UrlDecode(this string target, Encoding encoding)
        {
            return HttpUtility.UrlDecode(target, encoding);
        }

        #endregion

        #region Html属性编码

        /// <summary>
        /// Html属性编码
        /// </summary>
        /// <param name="target">待加密字符串</param>
        /// <returns></returns>
        public static string AttributeEncode(this string target)
        {
            return HttpUtility.HtmlAttributeEncode(target);
        }

        #endregion

        #region Html编码

        /// <summary>
        /// Html编码
        /// </summary>
        /// <param name="target">待加密字符串</param>
        /// <returns></returns>
        public static string HtmlEncode(this string target)
        {
            return HttpUtility.HtmlEncode(target);
        }

        #endregion

        #region Html解码

        /// <summary>
        /// Html解码
        /// </summary>
        /// <param name="target">待解密字符串</param>
        /// <returns></returns>
        public static string HtmlDecode(this string target)
        {
            return HttpUtility.HtmlDecode(target);
        }

        #endregion
    }
}
