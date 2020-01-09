// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EInfrastructure.Core.Config.ExceptionExtensions;

namespace EInfrastructure.Core.Tools
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
            return obj.ConvertToChar(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转Char
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static char? ConvertToChar(this object obj, char? defaultVal = null)
        {
            return obj.ConvertToChar(() => defaultVal);
        }

        /// <summary>
        /// obj转Char
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static char? ConvertToChar(this object obj, Func<char?> func)
        {
            if (obj != null)
                if (char.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToGuid(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转Guid
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static Guid? ConvertToGuid(this object obj, Guid? defaultVal = null)
        {
            return obj.ConvertToGuid(() => defaultVal);
        }

        /// <summary>
        /// obj转Guid
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static Guid? ConvertToGuid(this object obj, Func<Guid?> func)
        {
            if (obj != null)
                if (Guid.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToShort(()=>defaultVal).Value;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static short? ConvertToShort(this object obj, short? defaultVal = null)
        {
            return obj.ConvertToShort(()=>defaultVal);
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static short? ConvertToShort(this object obj, Func<short?> func)
        {
            if (obj != null)
                if (short.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToInt(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static int? ConvertToInt(this object obj, int? defaultVal = null)
        {
            return obj.ConvertToInt(() => defaultVal);
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static int? ConvertToInt(this object obj, Func<int?> func)
        {
            if (obj != null)
                if (int.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
        }

        #endregion

        #region obj转long

        /// <summary>
        /// obj转long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static long ConvertToLong(this object obj, long defaultVal = default(long))
        {
            return obj.ConvertToLong(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static long? ConvertToLong(this object obj, long? defaultVal = null)
        {
            return obj.ConvertToLong(() => defaultVal);
        }

        /// <summary>
        /// obj转long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static long? ConvertToLong(this object obj, Func<long?> func)
        {
            if (obj != null)
                if (long.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToDecimal(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static decimal? ConvertToDecimal(this object obj, decimal? defaultVal = null)
        {
            return obj.ConvertToDecimal(() => defaultVal);
        }

        /// <summary>
        /// obj转decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static decimal? ConvertToDecimal(this object obj, Func<decimal?> func)
        {
            if (obj != null)
                if (decimal.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToDouble(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static double? ConvertToDouble(this object obj, double? defaultVal = null)
        {
            return obj.ConvertToDouble(() => defaultVal);
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static double? ConvertToDouble(this object obj, Func<double?> func)
        {
            if (obj != null)
                if (double.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToFloat(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static float? ConvertToFloat(this object obj, float? defaultVal = null)
        {
            return obj.ConvertToFloat(() => defaultVal);
        }

        /// <summary>
        /// obj转float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static float? ConvertToFloat(this object obj, Func<float?> func)
        {
            if (obj != null)
                if (float.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToDateTime(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTime(this object obj, DateTime? defaultVal = null)
        {
            return obj.ConvertToDateTime(() => defaultVal);
        }

        /// <summary>
        /// obj转dateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static DateTime? ConvertToDateTime(this object obj, Func<DateTime?> func)
        {
            if (obj != null)
                if (DateTime.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToByte(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转byte
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static byte? ConvertToByte(this object obj, byte? defaultVal = null)
        {
            return obj.ConvertToByte(() => defaultVal);
        }

        /// <summary>
        /// obj转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static byte? ConvertToByte(this object obj, Func<byte?> func)
        {
            if (obj != null)
                if (byte.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
        }

        #endregion

        #region obj转sbyte

        /// <summary>
        /// obj转sbyte
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static sbyte ConvertToSByte(this object obj, sbyte defaultVal)
        {
            return obj.ConvertToSByte(() => defaultVal).Value;
        }

        /// <summary>
        /// obj转sbyte
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static sbyte? ConvertToSByte(this object obj, sbyte? defaultVal = null)
        {
            return obj.ConvertToSByte(() => defaultVal);
        }

        /// <summary>
        /// obj转sbyte
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static sbyte? ConvertToSByte(this object obj, Func<sbyte?> func)
        {
            if (obj != null)
                if (sbyte.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return obj.ConvertToBool(()=>defaultVal).Value;
        }

        /// <summary>
        /// obj转bool
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static bool? ConvertToBool(this object obj, bool? defaultVal = null)
        {
            return obj.ConvertToBool(()=>defaultVal);
        }

        /// <summary>
        /// obj转bool
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func">默认值</param>
        /// <returns></returns>
        private static bool? ConvertToBool(this object obj, Func<bool?> func)
        {
            if (obj != null)
                if (bool.TryParse(obj.ToString(), out var result))
                    return result;
            return func.Invoke();
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
            return stream.ConvertToByteArrayAsync(true).Result;
        }

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream">流</param>
        public static async Task<byte[]> ConvertToByteArrayAsync(Stream stream)
        {
            return await stream.ConvertToByteArrayAsync(false);
        }

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="isSync"></param>
        /// <returns></returns>
        private static async Task<byte[]> ConvertToByteArrayAsync(this Stream stream, bool isSync)
        {
            if (stream == null || stream.CanRead)
            {
                return new byte[] { };
            }

            if (!stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            var bytes = new byte[stream.Length];
            if (isSync)
            {
                stream.Read(bytes, 0, bytes.Length);
            }
            else
            {
                await stream.ReadAsync(bytes, 0, bytes.Length);
            }

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
            return para.ConvertToByteArray(Encoding.UTF8);
        }

        /// <summary>
        /// String转换为Byte数组
        /// </summary>
        /// <param name="para">待转换参数</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this string para, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(para))
                return new byte[] { };
            return encoding.GetBytes(para);
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

        #region 文件流转字符串

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static string ConvertToString(this Stream stream)
        {
            return stream.ConvertToString(Encoding.UTF8);
        }

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">是否自动释放文件</param>
        /// <returns></returns>
        public static string ConvertToString(this Stream stream, Encoding encoding, int bufferSize = 1024 * 2,
            bool isCloseStream = true)
        {
            return stream.ConvertToStringAsync(encoding, true, bufferSize, isCloseStream).Result;
        }

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static async Task<string> ConvertToStringAsync(this Stream stream)
        {
            return await stream.ConvertToStringAsync(Encoding.UTF8, false);
        }

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">是否自动释放文件</param>
        /// <returns></returns>
        public static async Task<string> ConvertToStringAsync(this Stream stream, Encoding encoding,
            int bufferSize = 1024 * 2,
            bool isCloseStream = true)
        {
            return await stream.ConvertToStringAsync(encoding, false, bufferSize, isCloseStream);
        }

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="isSync">是否同步</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">是否自动释放文件</param>
        /// <returns></returns>
        private static async Task<string> ConvertToStringAsync(this Stream stream, Encoding encoding, bool isSync,
            int bufferSize = 1024 * 2,
            bool isCloseStream = true)
        {
            if (stream == null || encoding == null || stream.CanRead == false)
                return string.Empty;
            using (var reader = new StreamReader(stream, encoding, true, bufferSize, !isCloseStream))
            {
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                var result = isSync ? reader.ReadToEnd() : await reader.ReadToEndAsync();
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                return result;
            }
        }

        #endregion

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

        /// <summary>
        /// 文件流转换为base64
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static async Task<string> ConvertToBase64Async(this Stream stream)
        {
            return  ConvertToBase64(await stream.ConvertToByteArrayAsync(false));
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
            int? errCode = null)
        {
            if (symbol == null)
            {
                symbol = '*';
            }

            string result = ""; //结果
            if (number < 0)
            {
                throw new BusinessException("number必须为正整数");
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
                throw new BusinessException("number必须为正整数");
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
