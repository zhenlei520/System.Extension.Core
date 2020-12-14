// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools;

namespace EInfrastructure.Core.AliYun.Tbk
{
    /// <summary>
    ///
    /// </summary>
    internal class OpenApiUtil
    {
        public static Dictionary<string, string> BuildCommonParam(string method, string appKey)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"method", method},
                {"app_key", appKey},
                {"sign_method", "md5"},
                {"timestamp", DateTime.Now.FormatDate(FormatDateType.One)},
                {"v", "2.0"},
                {"format", "json"},
                {"simplify", "true"}
            };

            return dic;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        internal static string CreateSign(IDictionary<string, string> parameters, string secret)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams =
                new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder();

            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }

            // 第三步：使用MD5
            byte[] bytes;

            query.Append(secret);
            MD5 md5 = MD5.Create();
            bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(secret + query.ToString()));


            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
