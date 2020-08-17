// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.UserAgentParse.Property
{
    /// <summary>
    /// os版本
    /// </summary>
    internal class OsVersionProperty
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="versionRegex">版本 正则表达式</param>
        /// <param name="versionMatchIndex">版本匹配第几条</param>
        /// <param name="versionReplaceRegex">版本 替换正则表达式</param>
        public OsVersionProperty(string versionRegex, int versionMatchIndex,
            KeyValuePair<string, string> versionReplaceRegex)
        {
            this.VersionRegex = versionRegex;
            this.VersionMatchIndex = versionMatchIndex;
            this.VersionReplaceRegex = versionReplaceRegex;
        }

        /// <summary>
        /// 版本 正则表达式
        /// </summary>
        public string VersionRegex { get; internal set; }

        /// <summary>
        /// 版本匹配第几条
        /// </summary>
        public int VersionMatchIndex { get; internal set; }

        /// <summary>
        /// 版本 替换正则表达式
        /// </summary>
        public KeyValuePair<string, string> VersionReplaceRegex { get; internal set; }
    }
}
