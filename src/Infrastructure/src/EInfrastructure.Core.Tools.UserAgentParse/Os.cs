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
            "Windows",
            "Android",
            "WoPhone",
            "Series40",
            "MeeGo",
            "Tizen",
            "Maemo",
            "Grid OS"
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
                        this.Version = new Regex("/Windows Phone ([0-9.]*)/").Matches(userAgent)[1].ToString()
                            .ConvertToDecimal(0);
                        this.Details = "2";
                    }

                    if (userAgent.Contains("Windows Phone OS"))
                    {
                        this.Name = "Windows Phone";
                        this.Version = new Regex("/Windows Phone OS ([0-9.]*)/").Matches(userAgent)[1]
                            .ConvertToDecimal(0);
                        this.Details = "2";

                        if (this.Version < 7)
                        {
                            this.Name = "Windows Mobile";
                        }
                    }
                }
                else if (Name == "Android")
                {
                    this.Name = "Android";
                    var match =
                        new Regex("/Android(?: )?(?:AllPhone_|CyanogenMod_)?(?:\\/)?v?([0-9.]+)/").Matches(
                            userAgent.Replace("-update", "."));
                    if (match.Count > 0)
                    {
                        this.Version = match[1].ConvertToDecimal(0);
                        this.Details = "3";
                    }

                    if (userAgent.Contains("Android Eclair"))
                    {
                        this.Version = 2.0m;
                        this.Details = "3";
                    }
                }
                else if (Name == "WoPhone")
                {
                    var match = new Regex("/WoPhone\\/([0-9\\.]*)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].ConvertToDecimal(0);
                    }
                }
                else if (Name == "Tizen")
                {
                    var match = new Regex("/Tizen[\\/ ]([0-9.]*)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].ConvertToDecimal(0);
                    }
                }
                else if (Name == "Grid OS")
                {
                    var match = new Regex("/Grid OS ([0-9.]*)/i").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = match[1].ConvertToDecimal(0);
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
                else if (userAgent.Contains("GoogleTV"))
                {
                    this.Name = "Google TV";
                    var match = new Regex("Chrome/5.").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = 1;
                    }

                    match = new Regex("Chrome/11.").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Version = 2;
                    }
                }
                else if (userAgent.Contains("BlackBerry"))
                {
                    this.Name = "BlackBerry OS";

                    if (!userAgent.Contains("Opera"))
                    {
                        var match = new Regex("/BlackBerry([0-9]*)\\/([0-9.]*)/").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[2].ConvertToDecimal(0);
                            this.Details = "2";
                        }

                        match = new Regex("/Version\\/([0-9.]*)/").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                            this.Details = "2";
                        }

                        if (this.Version >= 10)
                        {
                            this.Name = "BlackBerry";
                        }
                    }
                }
                else if (userAgent.Contains("RIM Tablet OS"))
                {
                    this.Name = "BlackBerry Tablet OS";
                    this.Version = new Regex("/RIM Tablet OS ([0-9.]*)/").Matches(userAgent)[1].ConvertToDecimal(0);
                    this.Details = "2";
                }
                else if (userAgent.Contains("PlayBook"))
                {
                    var match = new Regex("/Version\\/(10[0-9.]*)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "name";
                        this.Version = match[1].ConvertToDecimal(0);
                        this.Details = "2";
                    }
                }
                else
                {
                    #region WebOS

                    var match = new Regex("(?:web|hpw)OS").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "webOS";
                        match = new Regex("/(?:web|hpw)OS\\/([0-9.]*)/").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }

                    #endregion


                    match = new Regex("Series[ ]?60").Matches(userAgent);
                    if (userAgent.Contains("Symbian") || userAgent.Contains("S60") || match.Count > 0)
                    {
                        this.Name = "Series60";
                        if (userAgent.Contains("SymbianOS/9.1") && !userAgent.Contains("Series60"))
                        {
                            this.Version = 3.0m;
                        }

                        match = new Regex("/Series60\\/([0-9.]*)/").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }

                    match = new Regex("[b|B]ada").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "Bada";
                        match = new Regex("/[b|B]ada\\/([0-9.]*)/").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }

                    if (new Regex("/BREW/i").Matches(userAgent).Count > 0 ||
                        new Regex("BMP; U").Matches(userAgent).Count > 0)
                    {
                        this.Name = "Brew";

                        match = new Regex("/BREW; U; ([0-9.]*)/i").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                        else if ((match = new Regex("/;BREW\\/([0-9.]*)/i").Matches(userAgent)).Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }


                    match = new Regex("/\\(MTK;/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "MTK";
                    }

                    match = new Regex("CrOS").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "Chrome OS";
                    }

                    match = new Regex("Joli OS").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "Joli OS";
                        match = new Regex("/Joli OS\\/([0-9.]*)/i").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }

                    match = new Regex("Haiku").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "Haiku";
                    }

                    match = new Regex("QNX").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "QNX";
                    }

                    match = new Regex("OS\\/2; Warp").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "OS/2 Warp";

                        match = new Regex("/OS\\/2; Warp ([0-9.]*)/i").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }

                    match = new Regex("/AmigaOS/i").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "AmigaOS";
                    }

                    match = new Regex("/MorphOS/i").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "MorphOS";
                        match = new Regex("/MorphOS ([0-9.]*)/i").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }

                    match = new Regex("nook browser").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "Android";
                    }

                    match = new Regex("Nintendo 3DS").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";

                        match = new Regex(" /Version\\/([0-9.]*)/").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }

                    match = new Regex("PlayStation Vita").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";

                        match = new Regex("/PlayStation Vita ([0-9.]*)/").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match[1].ConvertToDecimal(0);
                        }
                    }

                    match = new Regex("/PlayStation 3/i").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";

                        match = new Regex("/PLAYSTATION 3;? ([0-9.]*)/").Matches(userAgent);
                        if (match.Count > 0)
                        {
                            this.Version = match.Count;
                        }
                    }

                    match = new Regex("Viera").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";
                    }

                    if (new Regex("AQUOSBrowser").Matches(userAgent).Count > 0 ||
                        new Regex("AQUOS-AS").Matches(userAgent).Count > 0)
                    {
                        this.Name = "";
                    }

                    match= new Regex("SMART-TV").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";
                    }


                    match= new Regex("SonyDTV|SonyBDP|SonyCEBrowser").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";
                    }

                    match= new Regex("NETTV\\/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";
                    }

                    match=new Regex("/LG NetCast\\.(?:TV|Media)-([0-9]*)/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";
                    }

                    match=new Regex("/LGSmartTV/").Matches(userAgent);
                    if (match.Count > 0)
                    {
                        this.Name = "";
                    }


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
