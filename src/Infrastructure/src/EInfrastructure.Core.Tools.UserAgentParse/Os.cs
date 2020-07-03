// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Tools.UserAgentParse.Property;

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    /// 系统
    /// </summary>
    public class Os
    {
        /// <summary>
        ///
        /// </summary>
        private List<string> OsNameList = new List<string>()
        {
            "Unix",
            "FreeBSD",
            "OpenBSD",
            "NetBSD",
            "SunOS",
            "CentOS",
            "Debian",
            "Fedora",
            "Gentoo",
            "Kubuntu",
            "Mandriva Linux",
            "Linux",
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public Os(string userAgent)
        {
            Name = OsNameList.Where(userAgent.Contains).FirstOrDefault();
            if (Name == "CentOS")
            {
                var match = new Regex("/CentOS\\/[0-9\\.\\-]+el([0-9_]+)/").Matches(userAgent);
                if (match.Count > 0)
                {
                    this.Version = match[1].Value.Replace("/_/g", "");
                }
            }
            else if (Name == "Fedora")
            {
                var match = new Regex("/Fedora\\/[0-9\\.\\-]+fc([0-9]+)/").Matches(userAgent);
                if (match.Count > 0)
                {
                    this.Version = match[1].Value;
                }
            }
            else if (Name == "Mandriva Linux")
            {
                this.Name = "Mandriva";
                var match = new Regex("/Mandriva Linux\\/[0-9\\.\\-]+mdv([0-9]+)/").Matches(userAgent);
                if (match.Count > 0)
                {
                    this.Version = match[1].Value;
                }
            }
            else if (Name == "Mageia")
            {
                var match = new Regex("/Mageia\\/[0-9\\.\\-]+mga([0-9]+)/").Matches(userAgent);
                if (match.Count > 0)
                {
                    this.Version = match[1].Value;
                }
            }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; private set; }
    }
}
