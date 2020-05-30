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
        private List<OsProperty> _osList = new List<OsProperty>()
        {
            new OsProperty(new[] {"Unix"}, "Unix"),
            new OsProperty(new[] {"FreeBSD"}, "FreeBSD"),
            new OsProperty(new[] {"OpenBSD"}, "OpenBSD"),
            new OsProperty(new[] {"NetBSD"}, "NetBSD"),
            new OsProperty(new[] {"SunOS"}, "Solaris"),
            new OsProperty(new[] {"Linux", "CentOS"}, "CentOS")
            {
                OsVersionRules = new OsVersionProperty("CentOS/[0-9/./-]+el([0-9_]+)", 1,
                    new KeyValuePair<string, string>("/_/g", "."))
            },
            new OsProperty(new[] {"Linux", "Debian"}, "Debian"),
            new OsProperty(new[] {"Linux", "Fedora"}, "Fedora"),
            new OsProperty(new[] {"Linux", "Gentoo"}, "Gentoo"),
            new OsProperty(new[] {"Linux", "Kubuntu"}, "Kubuntu"),
            new OsProperty(new[] {"Linux", "Mandriva Linux"}, "Mandriva"),
            new OsProperty(new[] {"Linux", "Mageia"}, "Mageia"),
            new OsProperty(new[] {"Linux", "Red Hat"}, "Red Hat"),
            new OsProperty(new[] {"Linux", "Slackware"}, "Slackware"),
            new OsProperty(new[] {"Linux", "SUSE"}, "SUSE"),
            new OsProperty(new[] {"Linux", "Turbolinux"}, "Turbolinux"),
            new OsProperty(new[] {"Linux", "Ubuntu"}, "Ubuntu"),
            new OsProperty(new[] {"Linux"}, "Linux"),
            new OsProperty(new[] {"iPhone( Simulator)?;"}, "iOS"),
            new OsProperty(new[] {"iPad;"}, "iOS"),
            new OsProperty(new[] {"iPod;"}, "iOS"),
            new OsProperty(new[] {new Regex(@"/iPhone\s*\d*s?[cp]?;/i"),}, "iOS"),
            new OsProperty(new[] {"Mac OS X"}, "Mac OS X"),
            new OsProperty(new[] {"Windows"}, "Windows"),
            new OsProperty(new[] {"Android"}, "Android"),
            new OsProperty(new[] {"GoogleTV"}, "GoogleTV"),
            new OsProperty(new[] {"WoPhone"}, "WoPhone"),
            new OsProperty(new[] {"BlackBerry"}, "BlackBerry OS"),
            new OsProperty(new[] {"RIM Tablet OS"}, "BlackBerry Tablet OS"),
            new OsProperty(new[] {"PlayBook"}, new[] {new Regex(@"/Version\/(10[0-9.]*)/"),}, "BlackBerry"),
            new OsProperty(new[] {"(?:web|hpw)OS"}, "webOS"),
            new OsProperty(new[] {"Symbian", "Series[ ]?60", "S60"}, "Series60"),
            new OsProperty(new[] {"Series40"}, "Series40"),
            new OsProperty(new[] {"MeeGo"}, "MeeGo"),
            new OsProperty(new[] {"Maemo"}, "Maemo"),
            new OsProperty(new[] {"Tizen"}, "Tizen"),
            new OsProperty(new[] {"[b|B]ada"}, "Bada"),
            new OsProperty(new[] {"BMP; U"}, "Brew"),
            new OsProperty(new[] {new Regex(@"/BREW/i"),}, "Brew"),
            new OsProperty(new[] {new Regex(@"/\(MTK;/"),}, "MTK"),
            new OsProperty(new[] {"CrOS"}, "Chrome OS"),
            new OsProperty(new[] {"Joli OS"}, "Joli OS"),
            new OsProperty(new[] {"Haiku"}, "Haiku"),
            new OsProperty(new[] {"QNX"}, "QNX"),
            new OsProperty(new[] {@"OS\/2; Warp"}, "OS/2 Warp"),
            new OsProperty(new[] {"Grid OS"}, "Grid OS"),
            new OsProperty(new[] {new Regex(@"/AmigaOS/i"),}, "AmigaOS"),
            new OsProperty(new[] {new Regex(@"/MorphOS/i"),}, "MorphOS"),
            new OsProperty(new[] {"Kindle"}, null, null, new[] {new Regex("Fire"),}, "", OsState.VagueAndExceptRegex),
            new OsProperty(new[] {"nook browser"}, "Android"),
            new OsProperty(new[] {@"bookeen\/cybook"}, ""),
            new OsProperty(new[] {"EBRD1101"}, ""),
            new OsProperty(new[] {"Iriver"}, ""),
            new OsProperty(new[] {"Nintendo Wii"}, ""),
            new OsProperty(new[] {"Nintendo DSi"}, ""),
            new OsProperty(new[] {"Nintendo 3DS"}, ""),
            new OsProperty(new[] {"PlayStation Portable"}, ""),
            new OsProperty(new[] {"PlayStation Vita"}, ""),
            new OsProperty(new[] {new Regex(@"/PlayStation 3/i"),}, ""),
            new OsProperty(new[] {"Viera"}, ""),
            new OsProperty(new[] {"AQUOSBrowser", "AQUOS-AS"}, ""),
            new OsProperty(new[] {"SMART-TV"}, ""),
            new OsProperty(new[] {"SonyDTV|SonyBDP|SonyCEBrowser"}, ""),
            new OsProperty(new[] {@"NETTV\/"}, ""),
            new OsProperty(new[] {new Regex(@"/LG NetCast\.(?:TV|Media)-([0-9]*)/"),}, ""),
            new OsProperty(new[] {new Regex(@"/LGSmartTV/"),}, ""),
            new OsProperty(new[] {@"Toshiba_?TP\/", @"TSBNetTV\/"}, ""),
            new OsProperty(new[] {new Regex(@"/mbxtWebKit\/([0-9.]*)/"),}, ""),
            new OsProperty(new[] {new Regex(@"/\(ADB; ([^\)]+)\)/"),}, ""),
            new OsProperty(new[] {new Regex(@"/Mstar;OWB/"),}, ""),
            new OsProperty(new[] {new Regex(@"/\\TechniSat ([^;]+);/"),}, ""),
            new OsProperty(new[] {new Regex(@"/\\Technicolor_([^;]+);/"),}, ""),
            new OsProperty(new[] {new Regex(@"/Winbox Evo2/"),}, ""),
            new OsProperty(new[] {new Regex(@"/^Roku\/DVP-([0-9]+)/"),}, ""),
            new OsProperty(new[] {new Regex(@"/HbbTV\/1.1.1 \([^;]*;\s*([^;]*)\s*;\s*([^;]*)\s*;/"),}, ""),
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public Os(string userAgent)
        {
            var osInfo = this._osList
                .FirstOrDefault(x =>
                    (x.OsState.Id == OsState.Vague.Id && x.Keys.All(y => userAgent.Contains(y))) ||
                    (x.OsState.Id == OsState.Regex.Id && x.Regexs.All(y => y.IsMatch(userAgent))));

            this.Name = osInfo?.Value;

            if (osInfo?.OsVersionRules != null)
            {
                var regex = new Regex(osInfo.OsVersionRules.VersionRegex).Match(userAgent);
                if (regex.Success)
                {
                    var versionStr = regex.Groups[osInfo.OsVersionRules.VersionMatchIndex]?.Value ?? "";
                    this.Version = Regex.Replace(versionStr, osInfo.OsVersionRules.VersionReplaceRegex.Key,
                        osInfo.OsVersionRules.VersionReplaceRegex.Value);
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
    }
}
