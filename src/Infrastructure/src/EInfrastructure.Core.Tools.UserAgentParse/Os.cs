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
            "Mageia",
            "Red Hat",
            "Slackware",
            "SUSE",
            "Turbolinux",
            "Ubuntu",
            "Linux",
            "Mac OS X",
            "Windows"
        };

        /// <summary>
        /// Ios
        /// </summary>
        private List<Regex> IosRegexList = new List<Regex>()
        {
            new Regex("iPhone( Simulator)?;"),
            new Regex("iPad;"),
            new Regex("iPod;"),
            new Regex("/iPhone\\s*\\d*s?[cp]?;/i")
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public Os(string userAgent)
        {
            Name = OsNameList.Where(userAgent.Contains).FirstOrDefault();
            if (!string.IsNullOrEmpty(Name))
            {
                if (Name == "CentOS")
                {
                    var match = new Regex("/CentOS\\/[0-9\\.\\-]+el([0-9_]+)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].Value.Replace("/_/g", "").ConvertToDecimal(0);
                    }
                }
                else if (Name == "Fedora")
                {
                    var match = new Regex("/Fedora\\/[0-9\\.\\-]+fc([0-9]+)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].Value.ConvertToDecimal(0);
                    }
                }
                else if (Name == "Mandriva Linux")
                {
                    this.Name = "Mandriva";
                    var match = new Regex("/Mandriva Linux\\/[0-9\\.\\-]+mdv([0-9]+)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].Value.ConvertToDecimal(0);
                    }
                }
                else if (Name == "Mageia")
                {
                    var match = new Regex("/Mageia\\/[0-9\\.\\-]+mga([0-9]+)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].Value.ConvertToDecimal(0);
                    }
                }
                else if (Name == "Red Hat")
                {
                    var match = new Regex("/Red Hat[^\\/]*\\/[0-9\\.\\-]+el([0-9_]+)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].Value.Replace("/_/g", "").ConvertToDecimal(0);
                    }
                }
                else if (Name == "Ubuntu")
                {
                    var match = new Regex("/Ubuntu\\/([0-9.]*)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].Value.ConvertToDecimal(0);
                    }
                }
                else if (Name == "Mac OS X")
                {
                    var match = new Regex("/Mac OS X (10[0-9\\._]*)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].Value.Replace("/_/g", "").ConvertToDecimal(0);
                    }
                }
                else if (Name == "Windows")
                {
                    var match = new Regex("/Windows NT ([0-9]\\.[0-9])/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].Value.ConvertToDecimal(0);
                        switch (this.Version)
                        {
                            case 6.2m:
                                this.Alias = "8";
                                break;
                            case 6.1m:
                                this.Alias = "7";
                                break;
                            case 6.0m:
                                this.Alias = "Vista";
                                break;
                            case 5.2m:
                                this.Alias = "Server 2003";
                                break;
                            case 5.1m:
                                this.Alias = "XP";
                                break;
                            case 5.0m:
                                this.Alias = "2000";
                                break;
                            default:
                                this.Alias = "NT " + this.Version;
                                break;
                        }
                    }


                    if (userAgent.Contains("Windows 95") || userAgent.Contains("Win95") ||
                        userAgent.Contains("Win 9x 4.00"))
                    {
                        this.Version = 4.0m;
                        this.Alias = "95";
                    }

                    if (userAgent.Contains("Windows 98") || userAgent.Contains("Win98") ||
                        userAgent.Contains("Win 9x 4.10"))
                    {
                        this.Version = 4.1m;
                        this.Alias = "98";
                    }

                    if (userAgent.Contains("Windows ME") || userAgent.Contains("WinME") ||
                        userAgent.Contains("Win 9x 4.90"))
                    {
                        this.Version = 4.9m;
                        this.Alias = "ME";
                    }

                    if (userAgent.Contains("Windows XP") || userAgent.Contains("WinXP"))
                    {
                        this.Version = 5.1m;
                        this.Alias = "Xp";
                    }

                    if (userAgent.Contains("WP7"))
                    {
                        this.Name = "Windows Phone";
                        this.Version = 7.0m;
                        this.Details = "2";
                    }

                    if (userAgent.Contains("Windows CE") || userAgent.Contains("WinCE") ||
                        userAgent.Contains("WindowsCE"))
                    {
                        if (userAgent.Contains("IEMobile"))
                        {
                            this.Name = "Windows Mobile";
                            if (userAgent.Contains("IEMobile 8"))
                            {
                                this.Version = 6.5m;
                                this.Details = "2";
                            }
                            else if (userAgent.Contains("IEMobile 7"))
                            {
                                this.Version = 6.1m;
                                this.Details = "2";
                            }
                            else if (userAgent.Contains("IEMobile 6"))
                            {
                                this.Version = 6.0m;
                                this.Details = "2";
                            }
                        }
                        else
                        {
                            this.Name = "Windows CE";

                            match = new Regex("/WindowsCEOS\\/([0-9.]*)/").Matches(userAgent);
                            if (match.Count > 0)
                            {
                                this.Version = match[1].Value.ConvertToDecimal(0);
                                this.Details = "2";
                            }

                            match = new Regex("/Windows CE ([0-9.]*)/").Matches(userAgent);
                            if (match.Count > 0)
                            {
                                this.Version = match[1].Value.ConvertToDecimal(0);
                                this.Details = "2";
                            }
                        }
                    }

                    if (userAgent.Contains("Windows Mobile"))
                    {
                        this.Name = "Windows Mobile";
                    }

                    match = new Regex("/WindowsMobile\\/([0-9.]*)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "Windows Mobile";
                        this.Version = match[1].ConvertToDecimal(0);
                        this.Details = "2";
                    }

                    match = new Regex("Windows Phone [0-9]").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "Windows Mobile";
                        this.Version = new Regex("/Windows Phone ([0-9.]*)/").Matches(userAgent)[1].ToString().ConvertToDecimal(0);
                        this.Details = "2";
                    }

                    if (userAgent.Contains("Windows Phone OS"))
                    {
                        this.Name = "Windows Phone";
                        this.Version = new Regex("/Windows Phone OS ([0-9.]*)/").Matches(userAgent)[1].ConvertToDecimal(0);
                        this.Details = "2";


                    }
                }
            }
            else
            {
                if (IosRegexList.Any(x => x.IsMatch(userAgent)))
                {
                    Name = "Ios";
                    Version = 1.0m;

                    var match = new Regex("/OS (.*) like Mac OS X/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].ToString().Replace("/_/g", "").ConvertToDecimal(0);
                    }
                }
                else if (userAgent.Contains(""))
                {
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
        public decimal Version { get; internal set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; private set; }

        #region private methods

        #endregion
    }
}
