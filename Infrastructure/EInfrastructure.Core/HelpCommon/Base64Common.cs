using System;
using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// base64编码方式
    /// </summary>
    public class Base64Common
    {
        private static readonly IDictionary<string, string> Mappings =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"data:image/jpeg;base64", "image/jpeg"},
                {"data:image/png;base64", "image/png"},
                {"data:image/gif;base64", "image/gif"},
                {"data:image/x-icon;base64", "image/x-icon"},
                {"data:text/css;base64", "text/css"},
                {"data:text/javascript;base64", "application/x-javascript"},
                {"data:text/html;base64", "text/html"},
            };

        #region GetBaseEncoding

        /// <summary>
        /// 得到文件base编码信息
        /// </summary>
        /// <param name="contentType">文件类型</param>
        /// <returns></returns>
        public static string GetBaseEncoding(string contentType)
        {
            return Mappings.Where(x => x.Value == contentType).Select(x => x.Key).FirstOrDefault();
        }

        #endregion
    }
}