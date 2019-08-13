// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enum;
using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public static class TypeConversionCommon
    {
        #region Object转换类型

        #region obj转Char

        /// <summary>
        /// obj转Char
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static char ConvertToChar(this object obj, char defaultVal)
        {
            char result;
            if (obj != null)
                if (char.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Char
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static char? ConvertToChar(this object obj, char? defaultVal = null)
        {
            char result;
            if (obj != null)
                if (char.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转Guid

        /// <summary>
        /// obj转Guid
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static Guid ConvertToGuid(this object obj, Guid defaultVal)
        {
            Guid result;
            if (obj != null)
                if (Guid.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Guid
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static Guid? ConvertToGuid(this object obj, Guid? defaultVal = null)
        {
            Guid result;
            if (obj != null)
                if (Guid.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转Int

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static int ConvertToInt(this object obj, int defaultVal)
        {
            int result;
            if (obj != null)
                if (int.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static int? ConvertToInt(this object obj, int? defaultVal = null)
        {
            int result;
            if (obj != null)
                if (int.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转Short

        /// <summary>
        /// obj转Short
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static short ConvertToShort(this object obj, short defaultVal)
        {
            short result;
            if (obj != null)
                if (short.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static short? ConvertToShort(this object obj, short? defaultVal = null)
        {
            short result;
            if (obj != null)
                if (short.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转decimal

        /// <summary>
        /// obj转decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(this object obj, decimal defaultVal)
        {
            decimal result;
            if (obj != null)
                if (decimal.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static decimal? ConvertToDecimal(this object obj, decimal? defaultVal = null)
        {
            decimal result;
            if (obj != null)
                if (decimal.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转double

        /// <summary>
        /// obj转double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static double ConvertToDouble(this object obj, double defaultVal)
        {
            double result;
            if (obj != null)
                if (double.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static double? ConvertToDouble(this object obj, double? defaultVal = null)
        {
            double result;
            if (obj != null)
                if (double.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转float

        /// <summary>
        /// obj转float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static float ConvertToFloat(this object obj, float defaultVal)
        {
            float result;
            if (obj != null)
                if (float.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static float? ConvertToFloat(this object obj, float? defaultVal = null)
        {
            float result;
            if (obj != null)
                if (float.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转datetime

        /// <summary>
        /// obj转datetime
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this object obj, DateTime defaultVal)
        {
            DateTime result;
            if (obj != null)
                if (DateTime.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTime(this object obj, DateTime? defaultVal = null)
        {
            DateTime result;
            if (obj != null)
                if (DateTime.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转byte

        /// <summary>
        /// obj转byte
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static byte ConvertToByte(this object obj, byte defaultVal)
        {
            byte result;
            if (obj != null)
                if (byte.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static byte? ConvertToByte(this object obj, byte? defaultVal = null)
        {
            byte result;
            if (obj != null)
                if (byte.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转sbyte

        /// <summary>
        /// obj转byte
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static sbyte ConvertToSByte(this object obj, sbyte defaultVal)
        {
            sbyte result;
            if (obj != null)
                if (sbyte.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static sbyte? ConvertToSByte(this object obj, sbyte? defaultVal = null)
        {
            sbyte result;
            if (obj != null)
                if (sbyte.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region obj转bool

        /// <summary>
        /// obj转bool
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static bool ConvertToBool(this object obj, bool defaultVal)
        {
            bool result;
            if (obj != null)
                if (bool.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static bool? ConvertToBool(this object obj, bool? defaultVal = null)
        {
            bool result;
            if (obj != null)
                if (bool.TryParse(obj.ToString(), out result))
                    return result;
                else
                    return defaultVal;
            return defaultVal;
        }

        #endregion

        #region 对可空类型进行判断转换

        /// <summary>
        /// 对可空类型进行判断转换
        /// </summary>
        /// <param name="value">DataReader字段的值</param>
        /// <param name="conversionType">该字段的类型</param>
        /// <returns></returns>
        public static object ConvertToSpecifyType(this object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter =
                    new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            return Convert.ChangeType(value, conversionType);
        }

        #endregion

        #endregion

        #region 文件类型转换

        #region 转换为Byte数组

        #region Stream转换为Byte数组

        /// <summary>
        /// Stream转换为Byte数组
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        #endregion

        #region String转换为Byte数组

        /// <summary>
        /// String转换为Byte数组
        /// </summary>
        /// <param name="para">待转换参数</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this string para)
        {
            UnicodeEncoding converter = new UnicodeEncoding();
            return converter.GetBytes(para);
        }

        #endregion

        #endregion

        #region 转换为Stream

        #region 将 byte[] 转成 Stream

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns></returns>
        public static Stream ConvertToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        #endregion

        #endregion

        #region 转换为String

        #region byte数组转换为string

        /// <summary>
        /// byte数组转换为string
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns></returns>
        public static string ConvertToString(this byte[] bytes)
        {
            System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();
            return converter.GetString(bytes);
        }

        #endregion

        #endregion

        #region base64字符串转byte数组

        /// <summary>
        /// 文件base64转byte数组
        /// </summary>
        /// <param name="base64">文件base64</param>
        /// <returns></returns>
        public static byte[] ConvertToByte(this string base64)
        {
            return Convert.FromBase64String(base64);
        }

        #endregion

        #region byte数组转换为base64

        /// <summary>
        /// byte数组转换为base64
        /// </summary>
        /// <param name="param">byte数组</param>
        /// <returns></returns>
        public static string ConvertToBase64(this byte[] param)
        {
            return Convert.ToBase64String(param);
        }

        #endregion

        #region 文件流转换为base64

        /// <summary>
        /// 文件流转换为base64
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static string ConvertToBase64(this Stream stream)
        {
            return ConvertToBase64(stream.ConvertToByteArray());
        }

        #endregion

        #endregion

        #region 清空小数点后0

        /// <summary>
        /// 清楚小数点后的0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearDecimal(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return "0";
            str = float.Parse(str).ToString("0.00");
            if (Int32.Parse(str.Substring(str.IndexOf(".", StringComparison.Ordinal) + 1)) == 0)
            {
                return str.Substring(0, str.IndexOf(".", StringComparison.Ordinal));
            }

            return str;
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
            int errCode = (int) HttpStatusEnum.Err)
        {
            if (symbol == null)
            {
                symbol = '*';
            }

            string result = ""; //结果
            if (number < 0)
            {
                throw new BusinessException("number必须为正整数", errCode);
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
            int errCode = (int) HttpStatusEnum.Err)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                symbol = "*";
            }

            string result = ""; //结果
            if (number < 0)
            {
                throw new BusinessException("number必须为正整数", errCode);
            }

            for (int i = 0; i < number; i++)
            {
                result += symbol;
            }

            return result;
        }

        #endregion

        #region 编码

        #region Unicode编码

        /// <summary>
        /// 汉字转换为Unicode编码
        /// </summary>
        /// <param name="str">要编码的汉字字符串</param>
        /// <returns>Unicode编码的的字符串</returns>
        public static string ConvertStringToUnicode(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2)
                r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }

        #endregion

        #region 将Unicode编码转换为汉字字符串

        /// <summary>
        /// 将Unicode编码转换为汉字字符串
        /// </summary>
        /// <param name="str">Unicode编码字符串</param>
        /// <returns>汉字字符串</returns>
        public static string ConvertUnicodeToString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte) int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte) int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }

            return r;
        }

        #endregion

        #endregion

        #region 值互换(左边最小值,右边最大值)

        /// <summary>
        /// 值互换(左边最小值,右边最大值)
        /// </summary>
        /// <param name="minParameter">最小值</param>
        /// <param name="maxParameter">最大值</param>
        public static void ChangeResult(ref int? minParameter, ref int? maxParameter)
        {
            if (minParameter > maxParameter)
            {
                var temp = maxParameter;
                maxParameter = minParameter;
                minParameter = temp;
            }
        }

        /// <summary>
        /// 值互换(左边最小值,右边最大值)
        /// </summary>
        /// <param name="minParameter">最小值</param>
        /// <param name="maxParameter">最大值</param>
        public static void ChangeResult(ref byte? minParameter, ref byte? maxParameter)
        {
            if (minParameter > maxParameter)
            {
                var temp = maxParameter;
                maxParameter = minParameter;
                minParameter = temp;
            }
        }

        /// <summary>
        /// 值互换(左边最小值,右边最大值)
        /// </summary>
        /// <param name="minParameter">最小值</param>
        /// <param name="maxParameter">最大值</param>
        public static void ChangeResult(ref float? minParameter, ref float? maxParameter)
        {
            if (minParameter > maxParameter)
            {
                var temp = maxParameter;
                maxParameter = minParameter;
                minParameter = temp;
            }
        }

        /// <summary>
        /// 值互换(左边最小值,右边最大值)
        /// </summary>
        /// <param name="minParameter">最小值</param>
        /// <param name="maxParameter">最大值</param>
        public static void ChangeResult(ref double? minParameter, ref double? maxParameter)
        {
            if (minParameter > maxParameter)
            {
                var temp = maxParameter;
                maxParameter = minParameter;
                minParameter = temp;
            }
        }

        /// <summary>
        /// 值互换(左边最小值,右边最大值)
        /// </summary>
        /// <param name="minParameter">最小值</param>
        /// <param name="maxParameter">最大值</param>
        public static void ChangeResult(ref decimal? minParameter, ref decimal? maxParameter)
        {
            if (minParameter > maxParameter)
            {
                var temp = maxParameter;
                maxParameter = minParameter;
                minParameter = temp;
            }
        }

        /// <summary>
        /// 值互换(左边最小值,右边最大值)
        /// </summary>
        /// <param name="minParameter">最小值</param>
        /// <param name="maxParameter">最大值</param>
        public static void ChangeResult(ref DateTime? minParameter, ref DateTime? maxParameter)
        {
            if (minParameter > maxParameter)
            {
                var temp = maxParameter;
                maxParameter = minParameter;
                minParameter = temp;
            }
        }

        #endregion
    }
}
