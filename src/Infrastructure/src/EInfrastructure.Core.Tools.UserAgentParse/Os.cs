// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
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
        private List<OsProperty> _osList = new List<OsProperty>()
        {
            new OsProperty(new[] {"Unix"}, "Unix"),
            new OsProperty(new[] {"FreeBSD"}, "FreeBSD"),
            new OsProperty(new[] {"OpenBSD"}, "OpenBSD"),
            new OsProperty(new[] {"NetBSD"}, "NetBSD"),
            new OsProperty(new[] {"SunOS"}, "SunOS"),
            new OsProperty(new[] {"Linux"}, "Linux"),
            new OsProperty(new[] {"CentOS"}, "CentOS"),
            new OsProperty(new[] {"Debian"}, "Debian"),
            new OsProperty(new[] {"Fedora"}, "Fedora"),
            new OsProperty(new[] {"Gentoo"}, "Gentoo"),
            new OsProperty(new[] {"Kubuntu"}, "Kubuntu"),
            new OsProperty(new[] {"Mandriva Linux"}, "Mandriva"),
            new OsProperty(new[] {"Mageia"}, "Mageia"),
            new OsProperty(new[] {"Red Hat"}, "Red Hat"),
            new OsProperty(new[] {"Slackware"}, "Slackware"),
            new OsProperty(new[] {"SUSE"}, "SUSE"),
            new OsProperty(new[] {"Turbolinux"}, "Turbolinux"),
            new OsProperty(new[] {"iPhone( Simulator)?;"}, "iOS"),
            new OsProperty(new[] {"iPad;"}, "iOS"),
            new OsProperty(new[] {"iPod;"}, "iOS"),
            new OsProperty(new[]{new Regex(@"/iPhone\s*\d*s?[cp]?;/i"), }, "iOS"),
            new OsProperty(new[] {"Mac OS X"}, "Mac OS X"),
            new OsProperty(new[] {"Windows"}, "Windows"),
            new OsProperty(new[] {"Android"}, "Android"),
            new OsProperty(new[] {"GoogleTV"}, "GoogleTV"),
            new OsProperty(new[] {"WoPhone"}, "WoPhone"),
            new OsProperty(new[] {"BlackBerry"}, "BlackBerry OS"),
            new OsProperty(new[] {"RIM Tablet OS"}, "BlackBerry Tablet OS"),
            new OsProperty(new[] {"PlayBook"},new []{new Regex(@"/Version\/(10[0-9.]*)/"), }, "BlackBerry"),
            new OsProperty(new[] {"(?:web|hpw)OS"}, "webOS"),
            new OsProperty(new[] {"Symbian","Series[ ]?60","S60"}, "Series60"),
            new OsProperty(new[] {"Series40"}, "Series40"),
            new OsProperty(new[] {"MeeGo"}, "MeeGo"),
            new OsProperty(new[] {"Maemo"}, "Maemo"),
            new OsProperty(new[] {"Tizen"}, "Tizen"),
            new OsProperty(new[] {"[b|B]ada"}, "Bada"),
            new OsProperty(new[] {"BMP; U"}, "Brew"),
            new OsProperty(new[] {new Regex(@"/BREW/i"), }, "Brew"),
            new OsProperty(new[] {new Regex(@"/\(MTK;/"), }, "MTK"),
            new OsProperty(new[] {"CrOS"}, "Chrome OS"),
            new OsProperty(new[] {"Joli OS"}, "Joli OS"),
            new OsProperty(new[] {"Haiku"}, "Haiku"),
            new OsProperty(new[] {"QNX"}, "QNX"),
            new OsProperty(new[] {@"OS\/2; Warp"}, "OS/2 Warp"),
            new OsProperty(new[] {"Grid OS"}, "Grid OS"),
            new OsProperty(new[] {new Regex(@"/AmigaOS/i"), }, "AmigaOS"),
            new OsProperty(new[] {new Regex(@"/MorphOS/i"), }, "MorphOS"),
            new OsProperty(new[] {""}, ""),
            new OsProperty(new[] {""}, ""),
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public Os(string userAgent)
        {
            if (userAgent.Contains("Unix"))
            {
                this.Name = "Unix";
            }
            else if (userAgent.Contains("FreeBSD"))
            {
                this.Name = "FreeBSD";
            }
            else if (userAgent.Contains(""))
            {
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
    }
}
