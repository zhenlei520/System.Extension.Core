// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    /// 浏览器信息
    /// </summary>
    public class Browser
    {
        public Browser()
        {
            this.Version = new Versions();
        }

        public Browser(string version)
        {
            this.Version = new Versions(version);
        }

        /// <summary>
        /// 浏览器名称
        /// </summary>
        [JsonProperty(PropertyName = "name",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; internal set; }

        /// <summary>
        /// 模式
        /// </summary>
        [JsonProperty(PropertyName = "mode",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Mode { get; internal set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [JsonProperty(PropertyName = "version",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Versions Version { get; internal set; }

        /// <summary>
        /// 版本类型
        /// </summary>
        [JsonProperty(PropertyName = "version_type",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string VersionType { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "stock",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Stock { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "hidden",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Hidden { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "channel",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Channel { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "detail",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Detail { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "build",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Builds { get; internal set; }
    }

    /// <summary>
    /// 浏览器规则
    /// </summary>
    internal class BrowserRules
    {
        public BrowserRules()
        {
            Name = "";
            Details = "";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regex"></param>
        /// <param name="details"></param>
        public BrowserRules(string name, Regex regex, string details) : this()
        {
            this.Name = name;
            this.Regex = regex;
            this.Details = details;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regex"></param>
        public BrowserRules(string name, Regex regex) : this(name, regex, "")
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regex"></param>
        /// <param name="options"></param>
        /// <param name="details"></param>
        public BrowserRules(string name, string regex, RegexOptions options, string details) : this(name,
            new Regex(regex, options), details)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regex"></param>
        /// <param name="options"></param>
        public BrowserRules(string name, string regex, RegexOptions options) : this(name, regex, options, "")
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regex"></param>
        public BrowserRules(string name, string regex) : this(name, regex, RegexOptions.None)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regex"></param>
        /// <param name="details"></param>
        public BrowserRules(string name, string regex, string details) : this(name, regex, RegexOptions.None, details)
        {
        }

        /// <summary>
        ///
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 正则
        /// </summary>
        internal Regex Regex { get; set; }

        /// <summary>
        /// 详细
        /// </summary>
        internal string Details { get; set; }
    }
}
