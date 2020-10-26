// Copyright (c) zhenlei520 All rights reserved.

using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.MobileRegexConfigurations.Traditional
{
    /// <summary>
    /// 中国移动传统手机号校验
    /// </summary>
    public class ChinaMobileRegexConfigurations : BaseMobileRegexConfigurations, IMobileRegexConfigurations
    {
        /// <summary>
        /// 得到国家
        /// </summary>
        /// <returns></returns>
        public Nationality GetNationality()
        {
            return Nationality.China;
        }

        /// <summary>
        /// 得到运营商
        /// </summary>
        /// <returns></returns>
        public CommunicationOperator GetCommunicationOperator()
        {
            return CommunicationOperator.ChinaMobile;
        }

        /// <summary>
        /// 得到运营商类型
        /// </summary>
        /// <returns></returns>
        public CommunicationOperatorType GetCommunicationOperatorType()
        {
            return CommunicationOperatorType.Traditional;
        }

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Regex GetRegex(RegexOptions options)
        {
            return base.GetRegex(@"(^1(3[4-9]|4[7]|5[0-27-9]|7[28]|8[2-478]|9[58])\d{8}$", options);
        }

        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <param name="str">待验证的字符串</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool IsVerify(string str, RegexOptions options)
        {
            return this.GetRegex(options).IsMatch(str);
        }
    }
}
