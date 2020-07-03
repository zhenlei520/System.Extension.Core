// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Text.RegularExpressions;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 验证方法
    /// </summary>
    public static class ValidateCommon
    {
        /// <summary>
        /// 邮件正则表达式
        /// </summary>
        private static readonly Regex Emailregex =
            new Regex(@"^([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\.][a-z]{2,3}([\.][a-z]{2})?$",
                RegexOptions.IgnoreCase);

        /// <summary>
        /// 手机号正则表达式
        /// </summary>
        private static readonly Regex Mobileregex = new Regex("^(13|14|15|16|17|18|19)[0-9]{9}$");

        /// <summary>
        /// 固话号正则表达式
        /// </summary>
        private static readonly Regex Phoneregex = new Regex(@"^(\d{3,4}-?)?\d{7,8}$");

        /// <summary>
        /// 邮政编码正则表达式
        /// </summary>
        private static readonly Regex ZipCodeRegex = new Regex(@"^\d{6}$");

        /// <summary>
        /// 数字正则表达式
        /// </summary>
        private static readonly Regex NumberRegex = new Regex(@"\d");

        /// <summary>
        /// 纯数字
        /// </summary>
        private static readonly Regex OnlyNumberRegex = new Regex(@"^-?\d+$");

        /// <summary>
        /// 中文正则表达式
        /// </summary>
        private static readonly Regex Chineseregex = new Regex("^[\u4e00-\u9fa5]");

        /// <summary>
        /// IP正则表达式
        /// </summary>
        private static readonly Regex Ipregex =
            new Regex(
                @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");

        /// <summary>
        /// 网址正则表达式
        /// </summary>
        private static readonly Regex WebSiteRegex =
            new Regex(@"((http|https)://)?(www.)?[a-z0-9\.]+(\.(com|net|cn|com\.cn|com\.net|net\.cn))(/[^\s\n]*)?");

        #region 是否邮政编码

        /// <summary>
        /// 是否邮政编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsZipCode(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return ZipCodeRegex.IsMatch(s);
        }

        #endregion

        #region 是否为邮箱

        /// <summary>
        /// 是否为邮箱
        /// </summary>
        public static bool IsEmail(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Emailregex.IsMatch(s);
        }

        #endregion

        #region 是否为手机号

        /// <summary>
        /// 是否为手机号
        /// </summary>
        public static bool IsMobile(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Mobileregex.IsMatch(s);
        }

        #endregion

        #region 是否为固话号

        /// <summary>
        /// 是否为固话号
        /// </summary>
        public static bool IsPhone(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Phoneregex.IsMatch(s);
        }

        #endregion

        #region 是否包含数字

        /// <summary>
        /// 是否包含数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return NumberRegex.IsMatch(s);
        }

        #endregion

        #region 是否为纯数字

        /// <summary>
        /// 是否为纯数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsOnlyNumber(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return OnlyNumberRegex.IsMatch(s);
        }

        #endregion

        #region 判断精度是否正确

        /// <summary>
        /// 判断精度是否正确
        /// </summary>
        /// <param name="str">带匹配的字符串</param>
        /// <param name="maxScale">最大保留小数位数</param>
        /// <returns></returns>
        public static bool IsMaxScale(this string str, int maxScale)
        {
            if (double.TryParse(str, out double temp))
            {
                var numberArray = str.Split('.');
                if (numberArray.Length==1)
                {
                    return maxScale >= 0;
                }

                var numberStr = numberArray[1];
                int index;
                do
                {
                    if (numberStr.Length == 1)
                    {
                        return 1 <= maxScale;
                    }

                    index = numberStr.Length - 1;
                    if (numberStr[index] == '0')
                    {
                        numberStr = numberStr.Substring(0, index);
                    }
                    else
                    {
                        return numberStr.Length <= maxScale;
                    }
                } while (index > 0);
            }

            return false;
        }

        #endregion

        #region 是否是身份证号

        /// <summary>
        /// 是否是身份证号
        /// </summary>
        public static bool IsIdCard(this string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            if (id.Length == 18)
                return CheckIdCard18(id);
            else if (id.Length == 15)
                return CheckIdCard15(id);
            else
                return false;
        }

        /// <summary>
        /// 是否为18位身份证号
        /// </summary>
        private static bool CheckIdCard18(this string id)
        {
            long n;
            if (long.TryParse(id.Remove(17), out n) == false || n < Math.Pow(10, 16) ||
                long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n) == false)
                return false; //数字验证

            string address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
                return false; //省份验证

            string birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            if (DateTime.TryParse(birth, out _) == false)
                return false; //生日验证

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());

            int y;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != id.Substring(17, 1).ToLower())
                return false; //校验码验证

            return true; //符合GB11643-1999标准
        }

        /// <summary>
        /// 是否为15位身份证号
        /// </summary>
        private static bool CheckIdCard15(this string id)
        {
            long n;
            if (long.TryParse(id, out n) == false || n < Math.Pow(10, 14))
                return false; //数字验证

            string address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
                return false; //省份验证

            string birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            if (DateTime.TryParse(birth, out _) == false)
                return false; //生日验证

            return true; //符合15位身份证标准
        }

        #endregion

        #region 判断是否为Guid

        /// <summary>
        /// 判断是否为Guid
        /// </summary>
        /// <param name="strSrc"></param>
        /// <returns></returns>
        public static bool IsGuid(this string strSrc)
        {
            return Guid.TryParse(strSrc, out _);
        }

        #endregion

        #region 是否为IP

        /// <summary>
        /// 是否为IP
        /// </summary>
        public static bool IsIp(this string s)
        {
            return Ipregex.IsMatch(s);
        }

        #endregion

        #region 判断是否网址

        /// <summary>
        ///  判断是否网址
        /// </summary>
        /// <param name="webUrl">网址</param>
        /// <returns></returns>
        public static bool IsUrl(this string webUrl)
        {
            if (string.IsNullOrEmpty(webUrl))
                return false;
            return WebSiteRegex.IsMatch(webUrl);
        }

        #endregion

        #region 判断是否中文

        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsChinese(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Chineseregex.IsMatch(s);
        }

        #endregion
    }
}
