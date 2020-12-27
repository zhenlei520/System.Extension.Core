// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    /// 正则表达式配置
    /// </summary>
    public interface IRegexConfigurationsProvider : ISingleInstance, IIdentify
    {
        /// <summary>
        /// 得到正则表达式规则
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <returns></returns>
        string GetRegexRule(RegexDefault regexDefault);

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <returns></returns>
        Regex GetRegex(RegexDefault regexDefault);

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Regex GetRegex(RegexDefault regexDefault, RegexOptions options);
    }

    /// <summary>
    /// 验证正则表达式默认配置
    /// </summary>
    public class RegexConfigurationsValidateDefaultProvider : IRegexConfigurationsProvider
    {
        /// <summary>
        /// 规则
        /// </summary>
        private List<KeyValuePair<RegexDefault, string>> _rules = new List<KeyValuePair<RegexDefault, string>>()
        {
            new KeyValuePair<RegexDefault, string>(RegexDefault.Email,
                @"^([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\.][a-z]{2,3}([\.][a-z]{2})?$"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.Phone, @"^(\d{3,4}-?)?\d{7,8}$"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.ZipCode, @"^\d{6}$"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.Chinese, @"^[\u4e00-\u9fa5]"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.Ip,
                @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.WebSite,
                @"((http|https)://)?(www.)?[a-z0-9\.]+(\.(com|net|cn|com\.cn|com\.net|net\.cn))(/[^\s\n]*)?"),
        };

        #region 权重信息

        /// <summary>
        /// 权重信息
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 99;
        }

        #endregion

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region 根据规则类型得到规则信息

        /// <summary>
        /// 根据规则类型得到规则信息
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <returns></returns>
        public string GetRegexRule(RegexDefault regexDefault)
        {
            if (_rules.Any(x => x.Key.Equals(regexDefault)))
            {
                return _rules.Where(x => x.Key.Equals(regexDefault)).Select(x => x.Value).FirstOrDefault();
            }

            throw new ArgumentNullException("rule regex");
        }

        #endregion

        #region 得到正则

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <returns></returns>
        public Regex GetRegex(RegexDefault regexDefault)
        {
            return GetRegex(regexDefault, RegexOptions.None);
        }

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Regex GetRegex(RegexDefault regexDefault, RegexOptions options)
        {
            return new Regex(GetRegexRule(regexDefault), options);
        }

        #endregion
    }

    /// <summary>
    /// 正则表达式默认配置
    /// </summary>
    public class RegexConfigurationsDefault : IRegexConfigurationsProvider
    {
        /// <summary>
        /// 规则
        /// </summary>
        private List<KeyValuePair<RegexDefault, string>> _rules = new List<KeyValuePair<RegexDefault, string>>()
        {
            new KeyValuePair<RegexDefault, string>(RegexDefault.Email,
                @"([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\.][a-z]{2,3}([\.][a-z]{2})?"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.Phone, @"(\d{3,4}-?)?\d{7,8}"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.ZipCode, @"\d{6}"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.Chinese, @"[\u4e00-\u9fa5]"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.Ip,
                @"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.WebSite,
                @"((http|https)://)?(www.)?[a-z0-9\.]+(\.(com|net|cn|com\.cn|com\.net|net\.cn))(/[^\s\n]*)?"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.PositiveInteger, @"[0-9]\d*"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.NegativeInteger, @"-[0-9]\d*"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.FloatingPointNumbers, @"[1-9]\d*\.\d*|0\.\d*[1-9]\d*"),
            new KeyValuePair<RegexDefault, string>(RegexDefault.NegativeFloatingPointNumbers,
                @"-[1-9]\d*\.\d*|0\.\d*[1-9]\d*"),
        };

        #region 权重信息

        /// <summary>
        /// 权重信息
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 100;
        }

        #endregion

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region 根据规则类型得到规则信息

        /// <summary>
        /// 根据规则类型得到规则信息
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <returns></returns>
        public string GetRegexRule(RegexDefault regexDefault)
        {
            if (_rules.Any(x => x.Key.Equals(regexDefault)))
            {
                return _rules.Where(x => x.Key.Equals(regexDefault)).Select(x => x.Value).FirstOrDefault();
            }

            throw new ArgumentNullException("rule regex");
        }

        #endregion

        #region 得到正则

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <returns></returns>
        public Regex GetRegex(RegexDefault regexDefault)
        {
            return GetRegex(regexDefault, RegexOptions.None);
        }

        /// <summary>
        /// 得到正则
        /// </summary>
        /// <param name="regexDefault"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Regex GetRegex(RegexDefault regexDefault, RegexOptions options)
        {
            return new Regex(GetRegexRule(regexDefault), options);
        }

        #endregion
    }
}
