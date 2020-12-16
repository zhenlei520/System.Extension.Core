// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;

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
            var list = param?.ToList()??new List<char>();
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
        /// <param name="value"></param>
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

        #endregion
    }
}
