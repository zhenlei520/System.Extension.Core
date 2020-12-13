// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Tools.Common.Systems;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static class StringCommon
    {
        #region 在字符串前后增加特殊字符

        /// <summary>
        /// 在字符串前后增加特殊字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="c">增加字符串</param>
        /// <param name="isReturnNull">是否返回空，如果为true，则返回空字符串，否则返回c</param>
        /// <param name="isAppendStart">字符串开头时否增加特殊字符</param>
        /// <param name="isAppendEnd">字符串结尾时否增加特殊字符</param>
        /// <param name="isClearNull">是否清除C之间的空数据</param>
        /// <returns></returns>
        public static string AppendChar(string str, char c = ',', bool isReturnNull = false, bool isAppendStart = true,
            bool isAppendEnd = true, bool isClearNull = true)
        {
            if (string.IsNullOrEmpty(str))
            {
                return isReturnNull ? "" : (c + "");
            }

            StringBuilder sb = new StringBuilder();
            string[] strArray = str.Split(c);
            int count = 0;
            for (var i = 0; i < strArray.Length; i++)
            {
                if (string.IsNullOrEmpty(strArray[i]) && isClearNull)
                {
                    continue;
                }

                count++;
                if (isAppendStart && count == 1)
                {
                    sb.AppendFormat(c + "{0}" + c, strArray[i]);
                    continue;
                }

                if (!isAppendEnd && i == strArray.Length)
                {
                    sb.AppendFormat("{0}", strArray[i]);
                    continue;
                }

                sb.AppendFormat("{0}" + c, strArray[i]);
            }

            return sb.ToString();
        }

        #endregion

        #region 隐藏手机

        /// <summary>
        /// 隐藏手机
        /// </summary>
        public static string HideMobile(string mobile)
        {
            if (!string.IsNullOrWhiteSpace(mobile))
            {
                if (mobile.IsMobile())
                {
                    return mobile.EncryptSpecialStr("*", 3, 4);
                }

                if (mobile.IsPhone())
                {
                    var strArray = mobile.Split('-');
                    if (strArray.Length == 2)
                    {
                        return strArray[0] + "-" + strArray[1].EncryptSpecialStr("*", 2, -1);
                    }

                    if (mobile.Length <= 7)
                    {
                        return mobile.EncryptSpecialStr("*", 2, 3);
                    }

                    return mobile.EncryptSpecialStr("*", mobile.Length - 6, 3);
                }

                throw new Exception("请输入正确的手机号码");
            }

            throw new Exception("请输入正确的手机号码");
        }

        #endregion

        #region 返回数组原来的第一个元素的值,数组中移除第一个值

        /// <summary>
        /// 返回数组原来的第一个元素的值,数组中移除第一个值
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回数组原来的第一个元素的值</returns>
        public static T Shift<T>(this T[] list)
        {
            return list.ToList().Shift();
        }

        #endregion

        #region 正则表达式

        /// <summary>
        /// 正则表达式
        /// </summary>
        /// <param name="str">待校验的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string[]>> Match(this string str, string regex)
        {
            return str.Match(regex, RegexOptions.None);
        }

        /// <summary>
        /// 正则匹配，得到匹配到的字符串集合
        /// </summary>
        /// <param name="str">待匹配的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options">正则表达式设置</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string[]>> Match(this string str, string regex, RegexOptions options)
        {
            int startat = (uint) (options & RegexOptions.RightToLeft) > 0U ? str.Length : 0;
            return str.Match(regex, options, startat);
        }

        /// <summary>
        /// 正则匹配，得到匹配到的字符串集合
        /// </summary>
        /// <param name="str">待匹配的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options">正则表达式设置</param>
        /// <param name="startat"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string[]>> Match(this string str, string regex, RegexOptions options,
            int startat)
        {
            Regex reg = new Regex(regex, options);
            var matchCollection = reg.Matches(str, startat);
            List<KeyValuePair<string, string[]>> retList = new List<KeyValuePair<string, string[]>>();

            foreach (Match match in matchCollection)
            {
                string[] arrays = new string[match.Length];
                for (int i = 0; i < match.Length; i++)
                {
                    arrays[i] = match.Groups[i].SafeString(false);
                }

                retList.Add(new KeyValuePair<string, string[]>(match.SafeString(false), arrays));
            }

            return retList;
        }

        #endregion

        #region 操作

        #region 清除字符串数组中的重复项以及对字符串进行剪切

        /// <summary>
        /// 清除字符串数组中的重复项以及对字符串进行剪切
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度 当其值大于0时，对其进行剪切</param>
        /// <returns></returns>
        public static IEnumerable<string> DistinctStringArray(IEnumerable<string> strArray, int maxElementLength)
        {
            Hashtable h = new Hashtable();
            foreach (var s in strArray.ToList())
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }

                h[k.Trim()] = s;
            }

            string[] result = new string[h.Count];
            h.Keys.CopyTo(result, 0);
            return result.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        #endregion

        #endregion
    }
}
