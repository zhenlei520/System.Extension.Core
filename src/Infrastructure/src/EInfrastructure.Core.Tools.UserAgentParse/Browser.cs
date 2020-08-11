// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    /// 浏览器信息
    /// </summary>
    public class Browser
    {
        /// <summary>
        /// 浏览器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public Versions Version { get; set; }

        /// <summary>
        /// 版本类型
        /// </summary>
        public string VersionType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Stock { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public bool Hidden { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string Channel { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string Detail { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public bool Builds { get;internal set; }
    }
}
