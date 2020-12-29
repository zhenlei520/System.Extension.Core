// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 强转
    /// </summary>
    public partial class Extensions
    {
        #region obj转Char

        /// <summary>
        /// obj转Char
        /// </summary>
        /// <param name="obj">待转换参数</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static char ConvertToChar(this object obj, char defaultVal)
        {
            var result = obj.ConvertToChar(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

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
            var result = obj.ConvertToGuid(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

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
            var result = obj.ConvertToShort(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

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
            return obj.ConvertToShort(() => defaultVal);
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
            var result = obj.ConvertToInt(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

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
        public static long ConvertToLong(this object obj, long defaultVal)
        {
            var result = obj.ConvertToLong(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

            return defaultVal;
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
            var result = obj.ConvertToDecimal(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

            return defaultVal;
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
            var result = obj.ConvertToDouble(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

            return defaultVal;
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
            var result = obj.ConvertToFloat(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

            return default(float);
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
            var result = obj.ConvertToDateTime(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

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
            var result = obj.ConvertToByte(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

            return defaultVal;
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

        #region 转为字节数组

        /// <summary>
        /// 转为字节数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter se = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            se.Serialize(memStream, obj);
            byte[] bobj = memStream.ToArray();
            memStream.Close();
            return bobj;
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
            var result = obj.ConvertToSByte(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

            return defaultVal;
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
        ///
        /// </summary>
        private static Dictionary<string, bool> _boolMap = new Dictionary<string, bool>()
        {
            {"0", false},
            {"不", false},
            {"否", false},
            {"失败", false},
            {"no", false},
            {"fail", false},
            {"lose", false},
            {"false", false},
            {"1", true},
            {"是", true},
            {"ok", true},
            {"yes", true},
            {"success", true},
            {"pass", true},
            {"true", true},
            {"成功", true}
        };

        /// <summary>
        /// obj转bool
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static bool ConvertToBool(this object obj, bool defaultVal)
        {
            var result = obj.ConvertToBool(() => defaultVal);
            if (result != null)
            {
                return result.Value;
            }

            return defaultVal;
        }

        /// <summary>
        /// obj转bool
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static bool? ConvertToBool(this object obj, bool? defaultVal = null)
        {
            return obj.ConvertToBool(() => defaultVal);
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
            {
                string objStr = obj.ToString();
                var res = _boolMap.FirstOrDefault(x => x.Key.Equals(objStr, StringComparison.CurrentCultureIgnoreCase));
                if (res.Key.Equals(objStr,StringComparison.CurrentCultureIgnoreCase))
                {
                    return res.Value;
                }

                if (bool.TryParse(objStr, out var result))
                    return result;
            }

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

        #region 更改类型

        /// <summary>
        /// 更改源参数类型（适用于简单的类型转换，序列化反序列化不适用）
        /// </summary>
        /// <param name="obj">源数据</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static T ChangeType<T>(this object obj)
        {
            return (T) Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// 更改源参数类型集合（适用于简单的类型转换，序列化反序列化不适用）
        /// </summary>
        /// <param name="objList">源数据集合</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ChangeType<T>(this IEnumerable<object> objList)
        {
            return from s in objList select ChangeType<T>(s);
        }

        /// <summary>
        /// 更改源参数类型集合（适用于简单的类型转换，序列化反序列化不适用）
        /// </summary>
        /// <param name="objArray">源数据集合</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ChangeType<T>(params object[] objArray)
        {
            return objArray.ToList().ChangeType<T>();
        }

        #endregion
    }
}
