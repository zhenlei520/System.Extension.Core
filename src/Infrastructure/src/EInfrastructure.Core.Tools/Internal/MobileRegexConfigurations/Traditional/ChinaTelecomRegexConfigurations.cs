﻿// Copyright (c) zhenlei520 All rights reserved.
// 中国电信天翼手机号码段有：133、149、153、173、177、180、181、189

using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.MobileRegexConfigurations.Traditional
{
    /// <summary>
    /// 中国电信传统手机号校验
    /// </summary>
    public class ChinaTelecomRegexConfigurations: BaseMobileRegexConfigurations, IMobileRegexConfigurations
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
            return CommunicationOperator.ChinaTelecom;
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
            return base.GetRegex(@"^1((33|49|53|7[37]|8[019]|9[139])[0-9])\d{7}$", options);
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
