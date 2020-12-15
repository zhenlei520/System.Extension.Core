// Copyright (c) zhenlei520 All rights reserved.
// 中国联通号段：130,131,132,155,156,166,176,185,186

using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.MobileRegexConfigurations.Traditional
{
    /// <summary>
    /// 中国联通传统手机号校验
    /// </summary>
    public class ChinaUnicomRegexConfigurationsProvider : BaseMobileRegexConfigurations,
        IMobileRegexConfigurationsProvider
    {
        /// <summary>
        /// 得到国家
        /// </summary>
        /// <returns></returns>
        public Nationality Nationality => Nationality.China;

        /// <summary>
        /// 得到运营商
        /// </summary>
        /// <returns></returns>
        public CommunicationOperator CommunicationOperator => CommunicationOperator.ChinaUnicom;

        /// <summary>
        /// 得到运营商类型
        /// </summary>
        /// <returns></returns>
        public CommunicationOperatorType CommunicationOperatorType => CommunicationOperatorType.Traditional;

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Regex GetRegex(RegexOptions options)
        {
            return base.GetRegex(@"^1(3[0-2]|4[5]|5[56]|7[6]|8[56])\d{8}$", options);
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
