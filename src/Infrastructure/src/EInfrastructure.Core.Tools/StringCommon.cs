// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// <param name="number">倒数第n次出现（默认倒数第1次）</param>
        /// <param name="defaultIndexof">默认下标-1（未匹配到）</param>
        /// <returns></returns>
        public static int IndexOf(this string parameter, char character, int number = 1, int defaultIndexof = -1)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                return defaultIndexof;
            }

            string temp = "";
            int count = 1; //第1次匹配
            while (count < number)
            {
                var index = parameter.IndexOf(character);
                temp = temp.Substring(index, parameter.Length - index);
                count++;
            }

            return temp.IndexOf(character);
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
            if (string.IsNullOrEmpty(parameter))
            {
                return defaultIndexof;
            }

            string temp = "";
            int count = 1; //第1次匹配
            while (count < number)
            {
                temp = temp.Substring(0, parameter.LastIndexOf(character));
                count++;
            }

            return temp.LastIndexOf(character);
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
                    return mobile.Substring(0, 3) + "*****" + mobile.Substring(8);
                }

                if (mobile.IsPhone())
                {
                    return mobile.Substring(0, mobile.Length - 6) + "***" + mobile.Substring(mobile.Length - 3);
                }

                throw new System.Exception("请输入正确的手机号码");
            }

            throw new System.Exception("请输入正确的手机号码");
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

        #region 字符串转换为泛型集合

        /// <summary>
        /// 字符串转化为泛型集合
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="splitstr">要分割的字符,默认以,分割</param>
        /// <param name="isReplaceSpace">是否移除空格</param>
        /// <returns></returns>
        public static List<T> ConvertStrToList<T>(this string str, char splitstr = ',', bool isReplaceSpace = true)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new List<T>();
            }

            string[] strarray = str.Split(splitstr);
            if (isReplaceSpace)
            {
                return (from s in strarray where s != "" select (T) Convert.ChangeType(s, typeof(T))).ToList();
            }

            return (from s in strarray select (T) Convert.ChangeType(s, typeof(T))).ToList();
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

        #region 操作

        #region 清除字符串数组中的重复项

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            Hashtable h = new Hashtable();
            foreach (string s in strArray)
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
            return result;
        }

        #endregion

        #endregion
    }
}
