// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    ///
    /// </summary>
    public class UserAgent
    {
        /// <summary>
        ///
        /// </summary>
        private UserAgent()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public UserAgent(string userAgent) : this()
        {
            this.Os=new Os(userAgent);
        }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public Browser Browser { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        public Devices Device { get; set; }

        /// <summary>
        /// 是否伪装
        /// </summary>
        public bool CamouFlage { get; set; }

        /// <summary>
        /// 引擎
        /// </summary>
        public Engine Engine { get; set; }

        /// <summary>
        /// 特性信息
        /// </summary>
        public List<string> Features { get; set; }

        /// <summary>
        /// 系统
        /// </summary>
        public Os Os { get; set; }
    }
}
