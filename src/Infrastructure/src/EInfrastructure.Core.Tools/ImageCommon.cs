// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Text.RegularExpressions;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageCommon
    {
        /// <summary>
        /// 默认支持的图片格式集合
        /// </summary>
        private static string[] defaultImgs = new[] {"jpg", "jpeg", "gif", "bmp", "png"};

        #region 获取图片地址集合

        /// <summary>
        /// 获取图片地址集合
        /// 默认支持BMP、JPG、JPEG、PNG、GIF，希望获取更多格式可调用其重载方法
        /// </summary>
        /// <param name="htmlStr">html字符串或包含图片标签的字符串</param>
        /// <returns></returns>
        public static string[] GetImageUrls(string htmlStr)
        {
            return GetImageUrls(htmlStr, defaultImgs);
        }

        /// <summary>
        /// 获取图片地址集合
        /// </summary>
        /// <param name="htmlStr">html字符串或包含图片标签的字符串</param>
        /// <param name="exts">支持扩展信息，例如：仅想获取jpg，就写jpg</param>
        /// <returns></returns>
        public static string[] GetImageUrls(string htmlStr, params string[] exts)
        {
            if (exts.Length == 0)
            {
                return new string[0];
            }

            Regex regObj = new Regex("<img.+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] strAry = new string[regObj.Matches(htmlStr).Count];
            int i = 0;
            foreach (Match matchItem in regObj.Matches(htmlStr))
            {
                strAry[i] = GetImageUrl(matchItem.Value, exts);
                i++;
            }

            return strAry;
        }

        #endregion

        #region 获取图片地址

        /// <summary>
        /// 获取图片地址
        /// </summary>
        /// <param name="imgStr">含图片网址的字符串</param>
        /// <param name="exts">支持扩展信息，例如：仅想获取jpg，就写jpg</param>
        /// <returns></returns>
        public static string GetImageUrl(string imgStr, params string[] exts)
        {
            if (exts.Length == 0)
            {
                return string.Empty;
            }

            string regex = $"http://.+.(?:{exts.ConvertToString('|')})";
            string str = "";
            Regex regObj = new Regex(regex,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match matchItem in regObj.Matches(imgStr))
            {
                str = matchItem.Value;
            }

            return str;
        }

        /// <summary>
        /// 获取图片地址
        /// 默认支持BMP、JPG、JPEG、PNG、GIF，希望获取更多格式可调用其重载方法
        /// </summary>
        /// <param name="imgStr">含图片网址的字符串</param>
        /// <returns></returns>
        public static string GetImageUrl(string imgStr)
        {
            return GetImageUrl(imgStr, defaultImgs);
        }

        #endregion
    }
}
