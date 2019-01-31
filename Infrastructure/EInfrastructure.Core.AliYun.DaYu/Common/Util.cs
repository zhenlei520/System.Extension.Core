using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Routing;

namespace EInfrastructure.Core.AliYun.DaYu.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    internal class Util
    {
        protected const string QuerySeparator = "&";
        protected const string HeaderSeparator = "\n";

        #region 创建sign

        /// <summary>
        /// 创建sign
        /// </summary>
        /// <param name="data"></param>
        /// <param name="strKey"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string CreateSign(Dictionary<string, string> data, string strKey, string method = "GET")
        {
            var dic = new RouteValueDictionary(data);
            string[] array = dic.OrderBy(a => a.Key, StringComparer.Ordinal).Select(a => PercentEncode(a.Key) + "=" + PercentEncode(a.Value.ToString())).ToArray();
            string dataStr = string.Join("&", array);
            string signStr = method + "&" + PercentEncode("/") + "&" + PercentEncode(dataStr);

            HMACSHA1 myhmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(strKey + "&"));
            byte[] byteArray = Encoding.UTF8.GetBytes(signStr);
            MemoryStream stream = new MemoryStream(byteArray);
            string signature = Convert.ToBase64String(myhmacsha1.ComputeHash(stream));

            return signature;

        } 
        #endregion

        #region 编码
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string PercentEncode(string value)
        {
            return UpperCaseUrlEncode(value)
                .Replace("+", "%20")
                .Replace("*", "%2A")
                .Replace("%7E", "~");
        } 
        #endregion

        #region 转为大写
        /// <summary>
        /// 转为大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string UpperCaseUrlEncode(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s)?.ToCharArray();
            if (temp != null)
                for (int i = 0; i < temp.Length - 2; i++)
                {
                    if (temp[i] == '%')
                    {
                        temp[i + 1] = char.ToUpper(temp[i + 1]);
                        temp[i + 2] = char.ToUpper(temp[i + 2]);
                    }
                }
            return new string(temp);
        } 
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static IDictionary<string, string> SortDictionary(Dictionary<string, string> dic)
        {
            IDictionary<string, string> sortedDictionary =
                new SortedDictionary<string, string>(dic, StringComparer.Ordinal);
            return sortedDictionary;
        } 
        #endregion

        #region 构建公共参数
        /// <summary>
        /// 构建公共参数
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> BuildCommonParam()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"AccessKeyId", "LTAIbrduImdIA16C"},
                {"Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")},
                {"SignatureMethod", "HMAC-SHA1"},
                {"SignatureVersion", "1.0"},
                {"SignatureNonce", Guid.NewGuid().ToString()}
            };
            return dic;
        } 
        #endregion

    }
}