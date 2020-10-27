// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools.Component;
using EInfrastructure.Core.Tools.Enumerations;
using EInfrastructure.Core.Tools.Expressions;
using EInfrastructure.Core.Tools.Internal;
using EInfrastructure.Core.Tools.Systems;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 验证方法
    /// </summary>
    public static class ValidateCommon
    {
        private static IRegexConfigurations _regexConfigurations;
        private static ICollection<IMobileRegexConfigurations> _mobileRegexConfigurations;

        /// <summary>
        ///
        /// </summary>
        static ValidateCommon()
        {
            _regexConfigurations = new RegexConfigurationsValidateDefault();
            _mobileRegexConfigurations = new ServiceProvider().GetServices<IMobileRegexConfigurations>().ToList();
        }

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
            return _regexConfigurations.GetRegex(RegexDefault.ZipCode, RegexOptions.IgnoreCase).IsMatch(s);
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
            return _regexConfigurations.GetRegex(RegexDefault.Email, RegexOptions.IgnoreCase).IsMatch(s);
        }

        #endregion

        #region 是否为手机号

        /// <summary>
        /// 是否为手机号(验证中国)
        /// </summary>
        /// <param name="str">待验证的手机号</param>
        /// <returns></returns>
        public static bool IsMobile(this string str)
        {
            return str.IsMobile(Nationality.China, (CommunicationOperator)null!, CommunicationOperatorType.Traditional);
        }

        /// <summary>
        /// 是否为手机号
        /// </summary>
        /// <param name="str">待验证的手机号</param>
        /// <param name="nationality">国家</param>
        /// <param name="communicationOperator">运营商类型（默认查询所有运营商）</param>
        /// <param name="operatorType">运营商类型</param>
        /// <returns></returns>
        public static bool IsMobile<T1, T2, T3>(this string str, T1 nationality,
            T2 communicationOperator = null, T3 operatorType = null)
            where T1 : Nationality?
            where T2 : CommunicationOperator?
            where T3 : CommunicationOperatorType?
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Expression<Func<IMobileRegexConfigurations, bool>> condition = x => true;

            if (nationality != null)
            {
                condition = condition.And(x => x.GetNationality().Equals(nationality));
            }

            if (communicationOperator != null)
            {
                condition = condition.And(x => x.GetCommunicationOperator().Equals(communicationOperator));
            }

            if (operatorType != null)
            {
                condition = condition.And(x => x.GetCommunicationOperatorType().Equals(operatorType));
            }

            var regexList = _mobileRegexConfigurations.Where(condition.Compile()).ToList();

            return regexList.Any(x => x.IsVerify(str, RegexOptions.Compiled));
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
            return GetRegexConfigurations().GetRegex(RegexDefault.Phone, RegexOptions.IgnoreCase).IsMatch(s);
        }

        #endregion

        #region 是否是纯数字（支持正负数）

        /// <summary>
        /// 是否是纯数字（支持正负数）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(this string str)
        {
            return IsNumber(str, null);
        }

        /// <summary>
        /// 是否是纯数字（支持正负数，默认不验证正负数）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="numericType">类型，当为null时指的是不限制大小写</param>
        /// <returns></returns>
        public static bool IsNumber(this string str, NumericType? numericType)
        {
            if (numericType == null)
            {
                numericType = NumericType.Nolimit;
            }

            if (string.IsNullOrEmpty(str)) //验证这个参数是否为空
                return false; //是，就返回False
            ASCIIEncoding ascii = new ASCIIEncoding(); //new ASCIIEncoding 的实例
            byte[] bytestr = ascii.GetBytes(str); //把string类型的参数保存到数组里
            List<int> asciiList = new List<int>();
            foreach (byte c in bytestr) //遍历这个数组里的内容
            {
                if (numericType.Equals(NumericType.Nolimit))
                {
                    if (((c < 48 || c > 57) && c != 45 && c != 43) ||
                        ((c == 45 || c == 43) && asciiList.Any(x => x == 45 || x == 43))) //判断是否为数字
                    {
                        return false; //不是，就返回False
                    }
                }
                else if (numericType.Equals(NumericType.Plus))
                {
                    if (((c < 48 || c > 57) && c != 43) ||
                        c == 43 && asciiList.Any(x => x == 43))
                    {
                        return false; //不是，就返回False
                    }
                }
                else if (numericType.Equals(NumericType.Minus))
                {
                    if (((c < 48 || c > 57) && c != 45) ||
                        c == 45 && asciiList.Any(x => x == 45))
                    {
                        return false; //不是，就返回False
                    }
                }
                else
                {
                    return false;
                }

                asciiList.Add(c);
            }

            if (numericType.Equals(NumericType.Minus) && asciiList.All(x => x != 45))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 是否为Double类型

        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(this object expression)
        {
            return expression.ConvertToDouble() != null;
        }

        #endregion

        #region 是否为Decimal类型

        /// <summary>
        /// 是否为Decimal类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDecimal(this object expression)
        {
            return expression.ConvertToDecimal() != null;
        }

        #endregion

        #region 是否为Long类型

        /// <summary>
        /// 是否为Long类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsLong(this object expression)
        {
            return expression.ConvertToLong() != null;
        }

        #endregion

        #region 是否为Int类型

        /// <summary>
        /// 是否为Int类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsInt(this object expression)
        {
            return expression.ConvertToInt() != null;
        }

        #endregion

        #region 是否为Short类型

        /// <summary>
        /// 是否为Short类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsShort(this object expression)
        {
            return expression.ConvertToShort() != null;
        }

        #endregion

        #region 是否为Guid类型

        /// <summary>
        /// 是否为Guid类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsGuid(this object expression)
        {
            return expression.ConvertToGuid() != null;
        }

        #endregion

        #region 是否为Char类型

        /// <summary>
        /// 是否为Char类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsChar(this object expression)
        {
            return expression.ConvertToChar() != null;
        }

        #endregion

        #region 是否为Float类型

        /// <summary>
        /// 是否为Float类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsFloat(this object expression)
        {
            return expression.ConvertToFloat() != null;
        }

        #endregion

        #region 是否为DateTime类型

        /// <summary>
        /// 是否为DateTime类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDateTime(this object expression)
        {
            return expression.ConvertToDateTime() != null;
        }

        #endregion

        #region 是否为Byte类型

        /// <summary>
        /// 是否为Byte类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsByte(this object expression)
        {
            return expression.ConvertToByte() != null;
        }

        #endregion

        #region 是否为SByte类型

        /// <summary>
        /// 是否为SByte类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsSByte(this object expression)
        {
            return expression.ConvertToSByte() != null;
        }

        #endregion

        #region 是否为Bool类型

        /// <summary>
        /// 是否为Bool类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsBool(this object expression)
        {
            return expression.ConvertToBool() != null;
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
                if (numberArray.Length == 1)
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

        /// <summary>
        /// 判断精度是否正确
        /// </summary>
        /// <param name="str">带匹配的字符串</param>
        /// <param name="maxScale">最大保留小数位数</param>
        /// <returns></returns>
        public static bool IsMaxScale(this double str, int maxScale)
        {
            return str.ToString(CultureInfo.InvariantCulture).IsMaxScale(maxScale);
        }

        /// <summary>
        /// 判断精度是否正确
        /// </summary>
        /// <param name="str">带匹配的字符串</param>
        /// <param name="maxScale">最大保留小数位数</param>
        /// <returns></returns>
        public static bool IsMaxScale(this int str, int maxScale)
        {
            return str.ToString(CultureInfo.InvariantCulture).IsMaxScale(maxScale);
        }

        /// <summary>
        /// 判断精度是否正确
        /// </summary>
        /// <param name="str">带匹配的字符串</param>
        /// <param name="maxScale">最大保留小数位数</param>
        /// <returns></returns>
        public static bool IsMaxScale(this decimal str, int maxScale)
        {
            return str.ToString(CultureInfo.InvariantCulture).IsMaxScale(maxScale);
        }


        /// <summary>
        /// 判断精度是否正确
        /// </summary>
        /// <param name="str">带匹配的字符串</param>
        /// <param name="maxScale">最大保留小数位数</param>
        /// <returns></returns>
        public static bool IsMaxScale(this float str, int maxScale)
        {
            return str.ToString(CultureInfo.InvariantCulture).IsMaxScale(maxScale);
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
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
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

        #region 是否为IP

        /// <summary>
        /// 是否为IP
        /// </summary>
        public static bool IsIp(this string s)
        {
            return GetRegexConfigurations().GetRegex(RegexDefault.Ip).IsMatch(s);
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
            return GetRegexConfigurations().GetRegex(RegexDefault.WebSite, RegexOptions.IgnoreCase).IsMatch(webUrl);
        }

        #endregion

        #region 判断是否中文

        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isAll">是否全部中文，默认有中文就可以，true：全部都是中文</param>
        /// <returns></returns>
        public static bool IsChinese(this string str, bool isAll = false)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            var regex = GetRegexConfigurations().GetRegex(RegexDefault.Chinese, RegexOptions.IgnoreCase);
            if (!isAll)
            {
                return regex.IsMatch(str);
            }

            return regex.Match(str).Success && regex.Matches(str).Count == str.SafeString().Length;
        }

        #endregion

        #region Indicates whether the specified string is null or an

        /// <summary>
        /// Indicates whether the specified string is null or an
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Indicates whether the specified string is null or an
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        #endregion

        #region 设置正则表达式配置驱动（不建议更换默认配置）

        /// <summary>
        /// 设置正则表达式配置驱动（不建议更换默认配置）
        /// </summary>
        /// <param name="regexConfigurations"></param>
        public static void SetRegexConfigurations(IRegexConfigurations regexConfigurations)
        {
            _regexConfigurations = regexConfigurations ?? throw new ArgumentNullException(nameof(regexConfigurations));
        }

        #endregion

        #region 得到正则表达式配置驱动

        /// <summary>
        /// 得到正则表达式配置驱动
        /// </summary>
        /// <returns></returns>
        public static IRegexConfigurations GetRegexConfigurations()
        {
            return _regexConfigurations;
        }

        #endregion

        #region 刷新手机号验证

        /// <summary>
        /// 刷新手机号验证
        /// </summary>
        /// <param name="regexConfigurationses"></param>
        public static void RefreshMobileRegexConfigurations(
            ICollection<IMobileRegexConfigurations> regexConfigurationses)
        {
            ValidateCommon._mobileRegexConfigurations = regexConfigurationses ?? new List<IMobileRegexConfigurations>();
        }

        #endregion
    }
}
