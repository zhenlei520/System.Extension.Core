// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    /// 引擎
    /// 浏览器内核
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// 引擎名称
        /// </summary>
        [JsonProperty(PropertyName = "name",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; internal set; }

        /// <summary>
        /// 版本
        /// </summary>
        [JsonProperty(PropertyName = "version",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Versions Version { get; internal set; }
    }
}
