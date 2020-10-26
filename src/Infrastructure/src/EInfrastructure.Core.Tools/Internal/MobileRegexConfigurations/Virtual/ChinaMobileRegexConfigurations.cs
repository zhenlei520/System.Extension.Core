// Copyright (c) zhenlei520 All rights reserved.

using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.MobileRegexConfigurations.Virtual
{
    /// <summary>
    /// 中国移动虚拟手机号校验
    /// </summary>
    public class ChinaMobileRegexConfigurations: BaseMobileRegexConfigurations, IMobileRegexConfigurations
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
            return CommunicationOperatorType.Virtual;
        }

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Regex GetRegex(RegexOptions options)
        {
            return base.GetRegex(@"^165\d{8}$|(^17(03|05|06)\d{7}$", options);
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
