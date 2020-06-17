// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 短链接/短参数帮助类
    /// </summary>
    public class ShortenCommon
    {
        /// <summary>
        ///
        /// </summary>
        private static string[] _chars =
        {
            "a", "b", "c", "d", "e", "f", "g", "h",
            "i", "j", "k", "l", "m", "n", "o", "p",
            "q", "r", "s", "t", "u", "v", "w", "x",
            "y", "z", "0", "1", "2", "3", "4", "5",
            "6", "7", "8", "9", "A", "B", "C", "D",
            "E", "F", "G", "H", "I", "J", "K", "L",
            "M", "N", "O", "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z"
        };

        private const string ShortKey = "EInfrastructure.Core";

        #region 得到短参数信息

        /// <summary>
        /// 得到短参数信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="key">md5盐（默认）</param>
        /// <param name="number">生成短连接的长度</param>
        /// <returns>得到短参数的值（有四个，任选其一即可）</returns>
        public static string[] GetShortParam(string param, string key = null, int number = 6)
        {
            string hex = SecurityCommon.GetMd5Hash(key + param);
            string[] resUrl = new string[4];
            for (int i = 0; i < 4; i++)
            {
                int hexint =
                    0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16); //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
                string outChars = string.Empty;
                for (int j = 0; j < number; j++)
                {
                    //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                    int index = 0x0000003D & hexint;
                    //把取得的字符相加
                    outChars += _chars[index];
                    //每次循环按位右移5位
                    hexint = hexint >> 5;
                }

                resUrl[i] = outChars; //把字符串存入对应索引的输出数组
            }

            return resUrl;
        }

        #endregion
    }
}
