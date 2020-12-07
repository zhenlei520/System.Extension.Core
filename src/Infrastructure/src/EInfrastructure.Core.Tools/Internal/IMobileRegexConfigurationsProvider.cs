// Copyright (c) zhenlei520 All rights reserved.

using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    /// 手机号校验配置
    /// </summary>
    public interface IMobileRegexConfigurationsProvider : ISingleInstance, IIdentify
    {
        /// <summary>
        /// 得到国家
        /// </summary>
        /// <returns></returns>
        Nationality GetNationality();

        /// <summary>
        /// 得到运营商
        /// </summary>
        /// <returns></returns>
        CommunicationOperator GetCommunicationOperator();

        /// <summary>
        /// 得到运营商类型
        /// </summary>
        /// <returns></returns>
        CommunicationOperatorType GetCommunicationOperatorType();

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Regex GetRegex(RegexOptions options);

        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <param name="str">待验证的字符串</param>
        /// <param name="options"></param>
        /// <returns></returns>
        bool IsVerify(string str, RegexOptions options);
    }

    /// <summary>
    ///
    /// </summary>
    public class BaseMobileRegexConfigurations : IdentifyDefault
    {
        /// <summary>
        /// 得到正则匹配内容
        /// </summary>
        /// <param name="regex">正则匹配规则</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual Regex GetRegex(string regex, RegexOptions options)
        {
            return new Regex(regex, options);
        }
    }
}
