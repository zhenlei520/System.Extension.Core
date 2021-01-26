// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Text.RegularExpressions;

namespace EInfrastructure.Core.UserAgentParse.Property
{
    /// <summary>
    ///
    /// </summary>
    internal class OsProperty
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="osState">状态</param>
        /// <param name="value">值</param>
        protected OsProperty(DeviceMatchType osState, string value)
        {
            this.Keys = null;
            this.ExceptRegexs = null;
            this.Regexs = null;
            this.ExceptRegexs = null;
            this.OsState = osState;
            this.Value = value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">Key名称，&&</param>
        /// <param name="value">值</param>
        public OsProperty(string[] keys, string value) : this(DeviceMatchType.Vague, value)
        {
            this.Keys = keys;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">Key名称，&&</param>
        /// <param name="exceptKeys">除哪些key什么之外</param>
        /// <param name="value">值</param>
        public OsProperty(string[] keys, string[] exceptKeys, string value) : this(
            DeviceMatchType.VagueAndExceptVague,
            value)
        {
            this.Keys = keys;
            this.ExceptKeys = exceptKeys;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="regexs">Key名称，&&</param>
        /// <param name="value">值</param>
        public OsProperty(Regex[] regexs, string value) : this(DeviceMatchType.Regex, value)
        {
            this.Regexs = regexs;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="regexs">Key名称，&&</param>
        /// <param name="exceptRegexs"></param>
        /// <param name="value">值</param>
        public OsProperty(Regex[] regexs, Regex[] exceptRegexs, string value) : this(
            DeviceMatchType.RegexAndExceptRegex,
            value)
        {
            this.Regexs = regexs;
            this.ExceptRegexs = exceptRegexs;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">Key名称，&&</param>
        /// <param name="regexs">Key名称，&&</param>
        /// <param name="value">值</param>
        public OsProperty(string[] keys, Regex[] regexs, string value) : this(DeviceMatchType.VagueAndRegex,
            value)
        {
            this.Regexs = regexs;
            this.Keys = keys;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">Key名称，&&</param>
        /// <param name="exceptKeys">除哪些key什么之外</param>
        /// <param name="regexs">Key名称，&&</param>
        /// <param name="exceptRegexs"></param>
        /// <param name="value">值</param>
        public OsProperty(string[] keys, string[] exceptKeys, Regex[] regexs, Regex[] exceptRegexs, string value) :
            this(DeviceMatchType.All,
                value)
        {
            this.Keys = keys;
            this.ExceptKeys = exceptKeys;
            this.Regexs = regexs;
            this.ExceptRegexs = exceptRegexs;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys">Key名称，&&</param>
        /// <param name="exceptKeys">除哪些key什么之外</param>
        /// <param name="regexs">Key名称，&&</param>
        /// <param name="exceptRegexs"></param>
        /// <param name="value">值</param>
        /// <param name="osState">状态</param>
        public OsProperty(string[] keys, string[] exceptKeys, Regex[] regexs, Regex[] exceptRegexs, string value,
            DeviceMatchType osState) : this(osState,
            value)
        {
            this.Keys = keys;
            this.ExceptKeys = exceptKeys;
            this.Regexs = regexs;
            this.ExceptRegexs = exceptRegexs;
        }

        #region 筛选os名称条件

        /// <summary>
        /// 包括 Key名称
        /// </summary>
        public string[] Keys { get; }

        /// <summary>
        /// 除哪些key什么之外
        /// </summary>
        public string[] ExceptKeys { get; }

        /// <summary>
        /// Regex
        /// </summary>
        public Regex[] Regexs { get; }

        /// <summary>
        /// 除哪些Regex
        /// </summary>
        public Regex[] ExceptRegexs { get; }

        #endregion

        /// <summary>
        /// 状态
        /// </summary>
        public DeviceMatchType OsState { get; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// os版本规则
        /// </summary>
        public OsVersionProperty OsVersionRules { get; set; }
    }
}
