// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Tools.UserAgentParse.Property
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
        protected OsProperty(OsState osState, string value)
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
        public OsProperty(string[] keys, string value) : this(OsState.Vague, value)
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
            OsState.VagueAndExceptVague,
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
        /// <param name="regex">正则表达式</param>
        public OsProperty(Regex[] regexs, string value) : this(OsState.Regex, value)
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
            OsState.RegexAndExceptRegex,
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
        public OsProperty(string[] keys, Regex[] regexs, string value) : this(OsState.VagueAndRegex,
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
            this(OsState.All,
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
            OsState osState) : this(osState,
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
        public OsState OsState { get; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// os版本规则
        /// </summary>
        public OsVersionProperty OsVersionRules { get; set; }
    }

    /// <summary>
    /// 状态
    /// </summary>
    internal class OsState : Enumeration
    {
        /// <summary>
        /// 模糊匹配
        /// </summary>
        public static OsState Vague = new OsState(1, "模糊匹配");

        /// <summary>
        /// 正则表达式
        /// </summary>
        public static OsState Regex = new OsState(2, "正则表达式");

        /// <summary>
        /// 模糊匹配和除什么之外的模糊匹配
        /// </summary>
        public static OsState VagueAndExceptVague = new OsState(3, "VagueAndExceptVague");

        /// <summary>
        /// 正则表达式和除什么之外正则表达式匹配
        /// </summary>
        public static OsState RegexAndExceptRegex = new OsState(4, "RegexAndExceptRegex");

        /// <summary>
        /// 模糊匹配和除什么之外的正则表达式匹配
        /// </summary>
        public static OsState VagueAndExceptRegex = new OsState(5, "VagueAndExceptRegex");

        /// <summary>
        /// 正则表达式和除什么之外模糊匹配
        /// </summary>
        public static OsState RegexAndExceptVague = new OsState(5, "RegexAndExceptVague");

        /// <summary>
        /// 模糊匹配和正则表达式
        /// </summary>
        public static OsState VagueAndRegex = new OsState(5, "模糊匹配和正则表达式");

        /// <summary>
        /// 模糊匹配和正则表达式以及排除模糊匹配和排除正则表达式
        /// </summary>
        public static OsState All = new OsState(6, "All");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public OsState(int id, string name) : base(id, name)
        {
        }
    }
}
