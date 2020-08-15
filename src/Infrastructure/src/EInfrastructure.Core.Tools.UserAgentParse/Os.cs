// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    /// 系统
    /// </summary>
    public class Os
    {
        public Os()
        {
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        [JsonProperty(PropertyName = "name",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; internal set; }

        /// <summary>
        /// 别名
        /// </summary>
        [JsonProperty(PropertyName = "alias",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Alias { get; internal set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        [JsonProperty(PropertyName = "version",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Versions Version { get; internal set; }

        /// <summary>
        /// 详情
        /// </summary>
        [JsonProperty(PropertyName = "detail",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Details { get; internal set; }

        /// <summary>
        /// 版本与系统别名关系
        /// </summary>
        private List<KeyValuePair<string, string>> VersionAndAliasRelarionList =
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(6.2 + "", "8"),
                new KeyValuePair<string, string>(6.1 + "", "7"),
                new KeyValuePair<string, string>(6.0 + "", "Vista"),
                new KeyValuePair<string, string>(5.2 + "", "Server 2003"),
                new KeyValuePair<string, string>(5.1 + "", "XP"),
                new KeyValuePair<string, string>(5.0 + "", "2000"),
            };

        #region 设置别名

        /// <summary>
        /// 设置别名
        /// </summary>
        internal void SetAlias()
        {
            Alias = VersionAndAliasRelarionList.Where(x => x.Key == Version.ToString()).Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(Alias))
            {
                Alias = "NT" + Version;
            }
        }

        #endregion
    }
}
