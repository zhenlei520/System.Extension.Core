// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Char类型扩展
    /// </summary>
    public partial class Extensions
    {
        #region ToDBC(转换为半角字符)

        /// <summary>
        /// 转换为半角字符
        /// </summary>
        /// <param name="value">值</param>
        public static char ConvertToDbc(this char value)
        {
            if (value == 12288)
                value = (char) 32;
            if (value > 65280 && value < 65375)
                value = (char) (value - 65248);
            return value;
        }

        #endregion

        #region ToSBC(转换为全角字符)

        /// <summary>
        /// 转换为全角字符
        /// </summary>
        /// <param name="value">值</param>
        public static char ConvertToSbc(this char value)
        {
            if (value == 32)
                value = (char) 12288;
            if (value < 127)
                value = (char) (value + 65248);
            return value;
        }

        #endregion

        #region char集合转为字符串

        /// <summary>
        /// char集合转为字符串
        /// </summary>
        /// <param name="param">待转换的char集合</param>
        /// <returns></returns>
        public static string ConvertToString(this IEnumerable<char> param)
        {
            var list = param?.ToList() ?? new List<char>();
            return list.Aggregate(new StringBuilder(list.Count), (sb, c) => sb.Append(c))
                .ToString();
        }

        /// <summary>
        /// char集合转为字符串
        /// </summary>
        /// <param name="param">待转换的char集合</param>
        /// <returns></returns>
        public static string ConvertToString(this List<char> param)
        {
            return param.Aggregate(new StringBuilder(param.Count), (sb, c) => sb.Append(c))
                .ToString();
        }

        #endregion

        #region 获取ASCII编码

        /// <summary>
        /// 获取ASCII编码
        /// </summary>
        /// <param name="value">值</param>
        public static int GetAsciiCode(this char value)
        {
            byte[] bytes = Encoding.GetEncoding(0).GetBytes(value.ToString());
            if (bytes.Length == 1)
                return bytes[0];
            return bytes[0] * 0x100 + bytes[1] - 0x10000;
        }

        #endregion

        #region 加密显示以*表示

        /// <summary>
        /// 加密显示以*表示
        /// </summary>
        /// <param name="number">显示N位*,-1默认显示6位</param>
        /// <param name="symbol">特殊符号，默认为*</param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public static string GetContentByEncryption(this char? symbol, int number = 6,
            int? errCode = null)
        {
            if (symbol == null)
            {
                symbol = '*';
            }

            string result = ""; //结果
            if (number < 0)
            {
                throw new BusinessException("number必须为正整数", HttpStatus.Err.Id);
            }

            for (int i = 0; i < number; i++)
            {
                result += symbol;
            }

            return result;
        }

        /// <summary>
        /// 加密显示以*表示
        /// </summary>
        /// <param name="symbol">特殊符号，默认为*</param>
        /// <param name="number">显示N次*,-1默认显示6位</param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public static string GetContentByEncryption(this string symbol, int number = 6,
            int? errCode = null)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                symbol = "*";
            }

            string result = ""; //结果
            if (number < 0)
            {
                throw new BusinessException("number必须为正整数", HttpStatus.Err.Id);
            }

            for (int i = 0; i < number; i++)
            {
                result += symbol;
            }

            return result;
        }

        #endregion

        #region 重复拼接字符

        /// <summary>
        /// 重复拼接字符
        /// </summary>
        /// <param name="value">字符</param>
        /// <param name="count">重复次数</param>
        public static string Repeat(this char value, int count) => new string(value, count);

        #endregion

        #region 是否在指定数组内/是否不在指定数组内

        /// <summary>
        /// 是否在指定数组内
        /// </summary>
        /// <param name="value">字符</param>
        /// <param name="values">数组</param>
        public static bool In(this char value, params char[] values) => Array.IndexOf(values, value) != -1;

        /// <summary>
        /// 是否不在指定数组内
        /// </summary>
        /// <param name="value">字符</param>
        /// <param name="values">数组</param>
        public static bool NotIn(this char value, params char[] values) => Array.IndexOf(values, value) == -1;

        #endregion

        #region 校验

        #region IsDoubleByte(是否双字节字符)

        /// <summary>
        /// 是否双字节字符
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDoubleByte(this char value) => Regex.IsMatch(value.ToString(), @"[^\x00-\xff]");

        #endregion

        #region IsLine(是否行标识)

        /// <summary>
        /// 是否行标识
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsLine(this char value)
        {
            if (value != '\r')
                return value == '\n';
            return true;
        }

        #endregion

        #region 是否中文

        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        public static bool IsChinese(this char value, bool isAll = false)
        {
            string str = value.ToString();
            var regex = GetRegexConfigurations().GetRegex(RegexDefault.Chinese, RegexOptions.IgnoreCase);
            if (!isAll)
            {
                return regex.IsMatch(str);
            }

            return regex.Match(str).Success && regex.Matches(str).Count == str.SafeString().Length;
        }

        #endregion

        #region 是否英文字母

        /// <summary>
        /// 是否英文字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLetters(this char value)=>char.IsLetter(value);

        #endregion

        #region 是否大写英文字母

        /// <summary>
        /// 是否大写英文字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUpperLetters(this char value)=>char.IsLetter(value) && char.IsUpper(value);

        #endregion

        #region 是否大写

        /// <summary>
        /// 是否大写
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUpper(this char value)=>char.IsUpper(value);

        #endregion

        #region 是否小写英文字母

        /// <summary>
        /// 是否小写英文字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLowerLetters(this char value)=> char.IsLetter(value) && char.IsLower(value);

        #endregion

        #region 是否小写

        /// <summary>
        /// 是否小写
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLower(this char value)=>char.IsLower(value);

        #endregion

        #region 是否十进制数字

        /// <summary>
        /// 是否十进制数字
        /// 十进制数字，就是 '0 '.. '9 '
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDigit(this char value)=>char.IsDigit(value);

        #endregion

        #region 是否数字字符

        /// <summary>
        /// 是否数字字符
        /// 判断的是数字类别，包括十进制数字 '0 '.. '9 '，还有用字母表示的数字，如表示罗马数字5的字母 'V '，还有表示其他数字的字符，如表示“1/2”的字符。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(this char value) => char.IsNumber(value);

        #endregion

        #region 是否标点符号

        /// <summary>
        /// 是否标点符号
        /// </summary>
        /// <param name="c">字符</param>
        public static bool IsPunctuation(this char c) => char.IsPunctuation(c);

        #endregion

        #region 是否分隔符号

        /// <summary>
        /// 是否分隔符号
        /// </summary>
        /// <param name="c">字符</param>
        public static bool IsSeparator(this char c) => char.IsSeparator(c);

        #endregion

        #region 是否符号字符

        /// <summary>
        /// 是否符号字符
        /// </summary>
        /// <param name="c">字符</param>
        public static bool IsSymbol(this char c) => char.IsSymbol(c);

        #endregion

        #endregion
    }
}
