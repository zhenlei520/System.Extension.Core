// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Config.Entities.Extensions;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static class StringCommon
    {
        #region 判断位置

        #region 获取字符第N次出现的下标位置

        #region 得到第number次出现character的位置下标

        /// <summary>
        /// 得到第number次出现character的位置下标
        /// </summary>
        /// <param name="parameter">待匹配字符串</param>
        /// <param name="character">匹配的字符串</param>
        /// <param name="number">得到第number次（默认第1次）</param>
        /// <param name="defaultIndexof">默认下标-1（未匹配到）</param>
        /// <returns></returns>
        public static int IndexOf(this string parameter, char character, int number = 1, int defaultIndexof = -1)
        {
            if (string.IsNullOrEmpty(parameter) || number <= 0)
            {
                return defaultIndexof;
            }

            int index = 0;
            int count = 1; //第1次匹配
            while (count < number)
            {
                var tempIndex = (parameter.IndexOf(character));
                index += tempIndex;
                parameter = parameter.Substring(tempIndex + 1);
                count++;
            }

            return index + parameter.IndexOf(character) + number - 1;
        }

        #endregion

        #region 得到倒数第number次出现character的位置下标

        /// <summary>
        /// 得到倒数第number次出现character的位置下标
        /// </summary>
        /// <param name="parameter">待匹配字符串</param>
        /// <param name="character">匹配的字符串</param>
        /// <param name="number">倒数第n次出现（默认倒数第1次）</param>
        /// <param name="defaultIndexof">默认下标-1（未匹配到）</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static int LastIndexOf(this string parameter, char character, int number = 1, int defaultIndexof = -1)
        {
            return IndexOf(parameter, character, parameter.Split(character).Length - number, defaultIndexof);
        }

        #endregion

        #endregion

        #endregion

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
                    return EncryptStr(mobile, "*", 3, 4);
                }

                if (mobile.IsPhone())
                {
                    var strArray = mobile.Split('-');
                    if (strArray.Length == 2)
                    {
                        return strArray[0] + "-" + EncryptStr(strArray[1], "*", 2, -1);
                    }

                    if (mobile.Length <= 7)
                    {
                        return EncryptStr(mobile, "*", 2, 3);
                    }

                    return EncryptStr(mobile, "*", mobile.Length - 6, 3);
                }

                throw new Exception("请输入正确的手机号码");
            }

            throw new Exception("请输入正确的手机号码");
        }

        #endregion

        #region 加密隐藏信息（将原信息其中一部分数据替换为特殊字符）

        /// <summary>
        /// 加密隐藏信息（将原信息其中一部分数据替换为特殊字符）
        /// </summary>
        /// <param name="param">原参数信息</param>
        /// <param name="key">更换后的特殊字符</param>
        /// <param name="index">下标</param>
        /// <param name="length">位数,-1代表到队尾</param>
        /// <returns></returns>
        public static string EncryptStr(string param, string key, int index, int length = -1)
        {
            if (string.IsNullOrEmpty(param))
            {
                return "";
            }

            string str = "";
            if (index > param.Length - 1)
            {
                return param;
            }

            str = param.Substring(0, index);
            if (length == -1)
            {
                length = param.Length - index;
            }

            for (int i = 0; i < length; i++)
            {
                str += key;
            }

            if (index + length < param.Length)
            {
                str += param.Substring(index + length);
            }

            return str;
        }

        #endregion

        #region 判断字符串是否全部相等

        /// <summary>
        /// 判断字符串是否全部相等
        /// </summary>
        /// <param name="number">待验证的字符串</param>
        /// <returns></returns>
        public static bool IsEqualNumber(string number)
        {
            for (int i = 0; i < number.Length - 1; i++)
            {
                if (number[i] != number[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 字符串转换为泛型集合

        /// <summary>
        /// 字符串转化为泛型集合
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="splitStr">要分割的字符,默认以,分割</param>
        /// <param name="isReplaceSpace">是否移除空格</param>
        /// <returns></returns>
        public static List<T> ConvertStrToList<T>(this string str, char splitStr = ',', bool isReplaceSpace = true)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new List<T>();
            }

            string[] strArray = str.Split(splitStr);
            Expression<Func<string, bool>> condition = x => true;
            if (isReplaceSpace)
            {
                condition = condition.And(x => !string.IsNullOrEmpty(x));
            }

            return strArray.Where(condition.Compile()).ChangeType<T>().ToList();
        }

        #endregion

        #region 首字母小写

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }

        #endregion

        #region 首字母大写

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
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

        #region Replace 结合正则表达式移除

        /// <summary>
        ///
        /// </summary>
        /// <param name="str">原参数</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="newStr">替换后的值</param>
        /// <returns></returns>
        public static string ReplaceRegex(this string str, string regex, string newStr)
        {
            return ReplaceRegex(str, regex, RegexOptions.None, newStr);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str">原参数</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options"></param>
        /// <param name="newStr">替换后的值</param>
        /// <returns></returns>
        public static string ReplaceRegex(this string str, string regex, RegexOptions options, string newStr)
        {
            Regex reg = new Regex(regex, options);
            return reg.Replace(str, newStr);
        }

        #endregion

        #region 正则表达式

        /// <summary>
        /// 正则表达式
        /// </summary>
        /// <param name="str">待校验的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns></returns>
        public static string[] Match(this string str, string regex)
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
        public static string[] Match(this string str, string regex, RegexOptions options)
        {
            int startat = (uint) (options & RegexOptions.RightToLeft) > 0U ? str.Length : 0;
            return str.Match(regex, RegexOptions.None, startat);
        }

        /// <summary>
        /// 正则匹配，得到匹配到的字符串集合
        /// </summary>
        /// <param name="str">待匹配的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options">正则表达式设置</param>
        /// <param name="startat"></param>
        /// <returns></returns>
        public static string[] Match(this string str, string regex, RegexOptions options, int startat)
        {
            Regex reg = new Regex(regex, options);
            var res = reg.Matches(str, startat);
            List<string> list = new List<string>();
            for (int i = 0; i < res.Count; i++)
            {
                list.Add(res[i].Value);
            }

            return list.ToArray();
        }

        #endregion

        #region 判断正则表达式是否匹配到

        /// <summary>
        /// 判断正则表达式是否匹配到
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool Test(this string str, string regex)
        {
            return str.Test(regex, RegexOptions.None);
        }

        /// <summary>
        /// 判断正则表达式是否匹配到
        /// </summary>
        /// <param name="str">待匹配的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options">正则表达式设置</param>
        /// <returns></returns>
        public static bool Test(this string str, string regex, RegexOptions options)
        {
            return new Regex(regex, options).IsMatch(str);
        }

        #endregion

        #region Indicates whether the specified string is null or an

        /// <summary>
        /// Indicates whether the specified string is null or an
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Indicates whether the specified string is null or an
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
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
