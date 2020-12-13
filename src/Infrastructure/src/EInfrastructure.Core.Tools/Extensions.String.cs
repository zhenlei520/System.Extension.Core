// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Common;
using EInfrastructure.Core.Tools.Common.Systems;
using EInfrastructure.Core.Tools.Enumerations;
using EInfrastructure.Core.Tools.Expressions;
using EInfrastructure.Core.Tools.Internal;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public partial class Extensions
    {
        #region 根据身份证号码获取出生日期

        /// <summary>
        /// 根据身份证号码获取出生日期
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static DateTime? GetBirthday(this string cardNo, Func<DateTime?> func = null)
        {
            if (!cardNo.IsIdCard())
            {
                throw new BusinessException("请输入合法的身份证号码", HttpStatus.Err.Id);
            }

            string timeStr = cardNo.Length == 15
                ? ("19" + cardNo.Substring(6, 2)) + "-" + cardNo.Substring(8, 2) + "-" +
                  cardNo.Substring(10, 2)
                : cardNo.Substring(6, 4) + "-" + cardNo.Substring(10, 2) + "-" + cardNo.Substring(12, 2);
            return timeStr.ConvertToDateTime(func?.Invoke());
        }

        #endregion

        #region 得到生肖信息

        /// <summary>
        /// 根据身份证号得到生肖信息 如果身份证号码错误，则返回Null
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <returns></returns>
        public static Animal GetAnimal(this string cardNo)
        {
            if (!cardNo.IsIdCard())
            {
                return null;
            }

            var birthday = cardNo.GetBirthday();
            return birthday != null ? GetAnimal(birthday.Value.Year) : null;
        }

        #endregion

        #region 根据身份证号码获取性别

        /// <summary>
        /// 根据身份证号码获取性别
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <param name="action">查询性别失败转换，默认为未知</param>
        /// <returns></returns>
        public static Gender GetGender(this string cardNo, Func<Gender> action = null)
        {
            if (!cardNo.IsIdCard())
            {
                return action?.Invoke() ?? Gender.Unknow;
            }

            int? gender = (cardNo.Length == 15 ? cardNo.Substring(14, 1) : cardNo.Substring(16, 1)).ConvertToInt(null);
            if (gender == null)
            {
                return action?.Invoke() ?? Gender.Unknow;
            }

            return gender % 2 == 0 ? Gender.Girl : Gender.Boy;
        }

        #endregion

        #region 大小写转换

        #region 全部转换为大小写

        #region 转为大写

        /// <summary>
        /// 转为大写
        /// </summary>
        /// <param name="parameter">需要转换的参数</param>
        /// <returns></returns>
        public static string ToUppers(this string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                return "";
            }

            return parameter.ToUpper();
        }

        #endregion

        #region 转为小写

        /// <summary>
        /// 转为小写
        /// </summary>
        /// <param name="parameter">需要转换的参数</param>
        /// <returns></returns>
        public static string ToLowers(this string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                return "";
            }

            return parameter.ToLower();
        }

        #endregion

        #endregion

        #region 首字母转换大小写

        #region 首字母小写

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }

        #endregion

        #region 首字母大写

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
        }

        #endregion

        #endregion

        #endregion

        #region Replace 结合正则表达式移除

        /// <summary>
        ///
        /// </summary>
        /// <param name="str">原参数</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="newStr">替换后的值</param>
        /// <returns></returns>
        public static string ReplaceRegex(this string str, string regex, string newStr)
        {
            return ReplaceRegex(str, regex, RegexOptions.None, newStr);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str">原参数</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options"></param>
        /// <param name="newStr">替换后的值</param>
        /// <returns></returns>
        public static string ReplaceRegex(this string str, string regex, RegexOptions options, string newStr)
        {
            Regex reg = new Regex(regex, options);
            return reg.Replace(str, newStr);
        }

        #endregion

        #region 字符串转换为泛型集合

        /// <summary>
        /// 字符串转化为泛型集合
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="splitStr">要分割的字符,默认以,分割</param>
        /// <param name="isReplaceSpace">是否移除空格</param>
        /// <returns></returns>
        public static List<T> ConvertStrToList<T>(this string str, char splitStr = ',', bool isReplaceSpace = true)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new List<T>();
            }

            string[] strArray = str.Split(splitStr);
            Expression<Func<string, bool>> condition = x => true;
            if (isReplaceSpace)
            {
                condition = condition.And(x => !string.IsNullOrEmpty(x));
            }

            return strArray.Where(condition.Compile()).ChangeType<T>().ToList();
        }

        #endregion

        #region 获取字符第出现的下标位置

        #region 得到第number次出现character的位置下标

        /// <summary>
        /// 得到第number次出现character的位置下标
        /// </summary>
        /// <param name="parameter">待匹配字符串</param>
        /// <param name="character">匹配的字符串</param>
        /// <param name="number">得到第number次（默认第1次）</param>
        /// <param name="defaultIndexof">默认下标-1（未匹配到）</param>
        /// <returns></returns>
        public static int IndexOf(this string parameter, char character, int number = 1, int defaultIndexof = -1)
        {
            if (string.IsNullOrEmpty(parameter) || number <= 0)
            {
                return defaultIndexof;
            }

            int index = 0;
            int count = 1; //第1次匹配
            while (count < number)
            {
                var tempIndex = (parameter.IndexOf(character));
                index += tempIndex;
                parameter = parameter.Substring(tempIndex + 1);
                count++;
            }

            return index + parameter.IndexOf(character) + number - 1;
        }

        #endregion

        #region 得到倒数第number次出现character的位置下标

        /// <summary>
        /// 得到倒数第number次出现character的位置下标
        /// </summary>
        /// <param name="parameter">待匹配字符串</param>
        /// <param name="character">匹配的字符串</param>
        /// <param name="number">倒数第n次出现（默认倒数第1次）</param>
        /// <param name="defaultIndexof">默认下标-1（未匹配到）</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static int LastIndexOf(this string parameter, char character, int number = 1, int defaultIndexof = -1)
        {
            return IndexOf(parameter, character, parameter.Split(character).Length - number, defaultIndexof);
        }

        #endregion

        #endregion

        #region 加密隐藏信息（将原信息其中一部分数据替换为特殊字符）

        /// <summary>
        /// 加密隐藏信息（将原信息其中一部分数据替换为特殊字符）
        /// </summary>
        /// <param name="param">原参数信息</param>
        /// <param name="key">更换后的特殊字符</param>
        /// <param name="index">下标</param>
        /// <param name="length">位数,-1代表到队尾</param>
        /// <returns></returns>
        public static string EncryptSpecialStr(this string param, string key, int index, int length = -1)
        {
            if (param.IsNullOrWhiteSpace())
            {
                return "";
            }

            string str = "";
            if (index > param.Length - 1)
            {
                return param;
            }

            str = param.Substring(0, index);
            if (length == -1)
            {
                length = param.Length - index;
            }

            for (int i = 0; i < length; i++)
            {
                str += key;
            }

            if (index + length < param.Length)
            {
                str += param.Substring(index + length);
            }

            return str;
        }

        #endregion

        #region 判断字符串是否全部相等

        /// <summary>
        /// 判断字符串是否全部相等
        /// </summary>
        /// <param name="str">待验证的字符串</param>
        /// <returns></returns>
        public static bool IsEqualNumber(this string str)
        {
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] != str[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 得到字符串长度（一个汉字占用两个字符）

        /// <summary>
        /// 得到字符串的字符长度（一个汉字占用两个字符）
        /// </summary>
        /// <param name="param">待校验的字符串</param>
        /// <returns></returns>
        public static int GetLength(this string param)
        {
            if (param.IsNullOrEmpty())
            {
                return 0;
            }

            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(param);
            foreach (var t in s)
            {
                if (t == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }

            return tempLen;
        }

        #endregion

        #region 判断正则表达式是否匹配

        /// <summary>
        /// 判断正则表达式是否匹配
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool Test(this string str, string regex)
        {
            return str.Test(regex, RegexOptions.None);
        }

        /// <summary>
        /// 判断正则表达式是否匹配到
        /// </summary>
        /// <param name="str">待匹配的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options">正则表达式设置</param>
        /// <returns></returns>
        public static bool Test(this string str, string regex, RegexOptions options)
        {
            if (str.IsNullOrEmpty())
            {
                return false;
            }

            return new Regex(regex, options).IsMatch(str);
        }

        #endregion

        #region 根据身份证号码得到星座信息

        /// <summary>
        /// 根据身份证号码得到星座信息
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <returns></returns>
        public static Constellation GetConstellation(this string cardNo)
        {
            if (cardNo.IsIdCard())
            {
                return ObjectCommon.SafeObject<Constellation>(!cardNo.IsNullOrWhiteSpace(),
                    () => (cardNo.GetBirthday().GetConstellationFromBirthday(), Constellation.Unknow));
            }
            return Constellation.Unknow;
        }

        #endregion

        #region 是否邮政编码

        /// <summary>
        /// 是否邮政编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsZipCode(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return _regexConfigurations.GetRegex(RegexDefault.ZipCode, RegexOptions.IgnoreCase).IsMatch(str);
        }

        #endregion

        #region 是否为邮箱

        /// <summary>
        /// 是否为邮箱
        /// </summary>
        public static bool IsEmail(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return _regexConfigurations.GetRegex(RegexDefault.Email, RegexOptions.IgnoreCase).IsMatch(str);
        }

        #endregion

        #region 是否为手机号

        /// <summary>
        /// 是否为手机号(验证中国,三大运营商的传统手机号号段)
        /// </summary>
        /// <param name="str">待验证的手机号</param>
        /// <param name="regexOptions">匹配方式，默认完全匹配(null)</param>
        /// <returns></returns>
        public static bool IsMobile(this string str, RegexOptions? regexOptions = null)
        {
            return str.IsMobile(Nationality.China, (CommunicationOperator) null!,
                CommunicationOperatorType.Traditional, regexOptions);
        }

        /// <summary>
        /// 是否为手机号
        /// </summary>
        /// <param name="str">待验证的手机号</param>
        /// <param name="nationality">国家</param>
        /// <param name="communicationOperator">运营商类型（默认查询所有运营商）</param>
        /// <param name="operatorType">运营商类型</param>
        /// <param name="regexOptions">匹配方式，默认完全匹配(null)</param>
        /// <returns></returns>
        public static bool IsMobile<T1, T2, T3>(this string str, T1 nationality,
            T2 communicationOperator = null, T3 operatorType = null, RegexOptions? regexOptions = null)
            where T1 : Nationality
            where T2 : CommunicationOperator
            where T3 : CommunicationOperatorType
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Expression<Func<IMobileRegexConfigurationsProvider, bool>> condition = x => true;

            if (nationality != null)
            {
                condition = condition.And(x => x.Nationality.Equals(nationality));
            }

            if (communicationOperator != null)
            {
                condition = condition.And(x => x.CommunicationOperator.Equals(communicationOperator));
            }

            if (operatorType != null)
            {
                condition = condition.And(x => x.CommunicationOperatorType.Equals(operatorType));
            }

            var regexList = _mobileRegexConfigurations.Where(condition.Compile()).ToList();

            if (regexOptions == null)
            {
                regexOptions = RegexOptions.Compiled;
            }

            return regexList.Any(x => x.IsVerify(str, regexOptions.Value));
        }

        #endregion

        #region 是否为固话号

        /// <summary>
        /// 是否为固话号
        /// </summary>
        public static bool IsPhone(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return GetRegexConfigurations().GetRegex(RegexDefault.Phone, RegexOptions.IgnoreCase).IsMatch(str);
        }

        #endregion

        #region 是否是纯数字（支持正负数）

        /// <summary>
        /// 是否是纯数字（支持正负数，默认不验证正负数）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="numericType">类型，当为null时指的是不限制大小写</param>
        /// <returns></returns>
        public static bool IsNumber(this string str, NumericType numericType = null)
        {
            if (numericType == null)
            {
                numericType = NumericType.Nolimit;
            }

            if (string.IsNullOrEmpty(str)) //验证这个参数是否为空
                return false; //是，就返回False
            ASCIIEncoding ascii = new ASCIIEncoding(); //new ASCIIEncoding 的实例
            byte[] byteStr = ascii.GetBytes(str); //把string类型的参数保存到数组里
            List<int> asciiList = new List<int>();
            foreach (byte c in byteStr) //遍历这个数组里的内容
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
            if (id.Length == 15)
                return CheckIdCard15(id);
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
        public static bool IsIp(this string str)
        {
            return GetRegexConfigurations().GetRegex(RegexDefault.Ip).IsMatch(str);
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

        /// <summary>
        /// Indicates whether the specified string is null or an
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list.GetListCount() == 0;
        }

        #endregion
    }
}
