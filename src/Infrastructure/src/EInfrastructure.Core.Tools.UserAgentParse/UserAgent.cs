// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools.Systems;

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    ///
    /// </summary>
    public class UserAgent
    {
        #region property

        /// <summary>
        ///
        /// </summary>
        private readonly List<string> _osNameList = new List<string>()
        {
            "Unix",
            "FreeBSD",
            "OpenBSD",
            "NetBSD",
            "SunOS",
        };

        #endregion

        /// <summary>
        ///
        /// </summary>
        private UserAgent()
        {
            this.Os = new Os();
            this.Browser = new Browser();
            this.Device = new Devices();
            this.Engine = new Engine();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public UserAgent(string userAgent) : this()
        {
            CheckUserAgent(userAgent, _osNameList.ToArray(), (mc, regex) => { this.Os.Name = regex; });
            CheckUserAgent(userAgent, "SunOS", (mc) => { this.Os.Name = "Solaris"; });
            CheckUserAgent(userAgent, "Linux", (mc) =>
            {
                this.Os.Name = "Linux";
                CheckUserAgent(userAgent, "CentOS", (mc2) =>
                {
                    this.Os.Name = "CentOS";
                    CheckUserAgent(userAgent, "/CentOS\\/[0-9\\.\\-]+el([0-9_]+)/",
                        (mc3) => { this.Os.Version = mc3[1].SafeString().Replace("/_/", "").ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, "Fedora", (mc2) =>
                {
                    this.Os.Name = "Fedora";
                    CheckUserAgent(userAgent, "/Fedora\\/[0-9\\.\\-]+fc([0-9]+)/",
                        (mc3) => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, "Ubuntu", mc2 =>
                {
                    this.Os.Name = "Ubuntu";
                    CheckUserAgent(userAgent, "/Ubuntu\\/([0-9.]*)/",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, new[] {"Gentoo", "Kubuntu", "Debian", "Slackware", "SUSE", "Turbolinux"},
                    (mc2, regex) => { this.Os.Name = regex; });

                CheckUserAgent(userAgent, "Mandriva Linux", (mc2) =>
                {
                    this.Os.Name = "Mandriva";
                    CheckUserAgent(userAgent, "/Mandriva Linux\\/[0-9\\.\\-]+mdv([0-9]+)/",
                        (mc3) => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, "Red Hat",
                    (mc2) =>
                    {
                        this.Os.Name = "Red Hat";
                        CheckUserAgent(userAgent, "/Red Hat[^\\/]*\\/[0-9\\.\\-]+el([0-9_]+)/",
                            (mc3) =>
                            {
                                this.Os.Version = mc3[1].SafeString().Replace("/_/g", ".").ConvertToDecimal(0);
                            });
                    });

                CheckUserAgent(userAgent, new[] {"iPhone( Simulator)?;", "iPad;", "iPod;", "/iPhone\\s*\\d*s?[cp]?;/i"},
                    (mc2, regex) =>
                    {
                        this.Os.Name = "iOS";
                        this.Os.Version = 1.0m;
                        CheckUserAgent(userAgent, "/OS (.*) like Mac OS X/",
                            mc3 => { this.Os.Version = mc3[1].SafeString().Replace("/_/g", ".").ConvertToDecimal(0); });

                        if (CheckUserAgent(userAgent, "iPhone Simulator;"))
                        {
                            this.Device.DeviceType = DeviceType.Emulator;
                        }
                        else if (CheckUserAgent(userAgent, "iPod;"))
                        {
                            this.Device.DeviceType = DeviceType.Media;
                            this.Device.Manufacturer = "Apple";
                            this.Device.Name = "iPod Touch";
                        }
                        else if (CheckUserAgent(userAgent, "iPhone;") ||
                                 CheckUserAgent(userAgent, "/iPhone\\s*\\d*s?[cp]?;/i"))
                        {
                            this.Device.DeviceType = DeviceType.Mobile;
                            this.Device.Manufacturer = "Apple";
                            this.Device.Name = "iPhone";
                        }
                        else
                        {
                            this.Device.DeviceType = DeviceType.Tablet;
                            this.Device.Manufacturer = "Apple";
                            this.Device.Name = "iPad";
                        }

                        this.Device.Identified = true;
                    });

                CheckUserAgent(userAgent, "Mac OS X", mc2 =>
                {
                    this.Os.Name = "Mac OS X";
                    CheckUserAgent(userAgent, "/Mac OS X (10[0-9\\._]*)/",
                        mc3 => { this.Os.Version = mc3[1].SafeString().Replace("/_/g", ".").ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, "Windows", mc2 =>
                {
                    this.Os.Name = "Windows";
                    CheckUserAgent(userAgent, "/Windows NT ([0-9]\\.[0-9])/", mc3 =>
                    {
                        this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0);
                        this.Os.SetAlias();
                    });

                    CheckUserAgent(userAgent, new[] {"Windows 95", "Win95", "Win 9x 4.00"}, (mc3, regex) =>
                    {
                        this.Os.Version = 4.0m;
                        this.Os.Alias = "95";
                    });

                    CheckUserAgent(userAgent, new[] {"Windows 98", "Win98", "Win 9x 4.10"}, (mc3, regex) =>
                    {
                        this.Os.Version = 4.1m;
                        this.Os.Alias = "98";
                    });

                    CheckUserAgent(userAgent, new[] {"Windows ME", "WinME", "Win 9x 4.90"}, (mc3, regex) =>
                    {
                        this.Os.Version = 4.9m;
                        this.Os.Alias = "ME";
                    });

                    CheckUserAgent(userAgent, new[] {"Windows XP", "WinXP"}, (mc3, regex) =>
                    {
                        this.Os.Version = 5.1m;
                        this.Os.Alias = "XP";
                    });

                    CheckUserAgent(userAgent, "WP7", mc3 =>
                    {
                        this.Os.Name = "Windows Phone";
                        this.Os.Version = 7.0m;
                        this.Os.Details = "2";
                        this.Device.DeviceType = DeviceType.Mobile;
                        this.Browser.Name = "desktop";
                    });

                    CheckUserAgent(userAgent, new[] {"Windows CE", "WinCE", "WindowsCE"}, (mc3, regex) =>
                    {
                        if (CheckUserAgent(userAgent, " IEMobile"))
                        {
                            this.Os.Name = "Windows Mobile";
                            if (CheckUserAgent(userAgent, " IEMobile 8"))
                            {
                                this.Os.Version = 6.5m;
                                this.Os.Details = "2";
                            }
                            else if (CheckUserAgent(userAgent, " IEMobile 7"))
                            {
                                this.Os.Version = 6.1m;
                                this.Os.Details = "2";
                            }
                            else if (CheckUserAgent(userAgent, " IEMobile 6"))
                            {
                                this.Os.Version = 6.0m;
                                this.Os.Details = "2";
                            }
                        }
                        else
                        {
                            this.Os.Name = "Windows CE";
                            CheckUserAgent(userAgent, new[] {"/WindowsCEOS\\/([0-9.]*)/", "/Windows CE ([0-9.]*)/"},
                                (mc4, regex2) =>
                                {
                                    this.Os.Version = mc4[1].SafeString().ConvertToDecimal(0);
                                    this.Os.Details = "2";
                                });
                        }

                        this.Device.DeviceType = DeviceType.Mobile;
                    });

                    CheckUserAgent(userAgent, "Windows Mobile", mc3 =>
                    {
                        this.Os.Name = "Windows Mobile";
                        this.Device.DeviceType = DeviceType.Mobile;
                    });

                    CheckUserAgent(userAgent, "/WindowsMobile\\/([0-9.]*)/", mc3 =>
                    {
                        this.Os.Name = "Windows Mobile";
                        this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0);
                        this.Os.Details = "2";
                        this.Device.DeviceType = DeviceType.Mobile;
                    });

                    CheckUserAgent(userAgent, "Windows Phone [0-9]", mc3 =>
                    {
                        this.Os.Name = "Windows Mobile";
                        this.Os.Version = new Regex("/Windows Phone ([0-9.]*)/").Matches(userAgent)[1].SafeString()
                            .ConvertToDecimal(0);
                        this.Os.Details = "2";
                        this.Device.DeviceType = DeviceType.Mobile;
                    });

                    CheckUserAgent(userAgent, "Windows Phone OS", mc3 =>
                    {
                        this.Os.Name = "Windows Phone";
                        this.Os.Version = new Regex("/Windows Phone OS ([0-9.]*)/").Matches(userAgent)[1].SafeString()
                            .ConvertToDecimal(0);
                        this.Os.Details = "2";

                        if (this.Os.Version < 7)
                        {
                            this.Os.Name = "Windows Mobile";
                        }

                        CheckUserAgent(userAgent, "/IEMobile\\/[^;]+; ([^;]+); ([^;]+)[;|\\)]/", mc4 =>
                        {
                            this.Device.Name = mc4[2].SafeString();
                            this.Device.Manufacturer = mc4[1].SafeString();
                        });
                        this.Device.DeviceType = DeviceType.Mobile;

                        var manufacturer = this.Device.Manufacturer;
                        var model = CleanupModel(this.Device.Name);

                        if (GloableConfigurations.WindowsPhoneModels.Any(x => x.Key == manufacturer) &&
                            GloableConfigurations.WindowsPhoneModels.Any(x =>
                                x.Key == manufacturer && x.Value.Any(y => y.Key == model)))
                        {
                            var list =
                                GloableConfigurations.WindowsPhoneModels.Where(x => x.Key == manufacturer)
                                    .SelectMany(x => x.Value).Where(x => x.Key == model).Select(x => x.Value)
                                    .FirstOrDefault();
                            if (list != null && list.Length > 1)
                            {
                                this.Device.Manufacturer = list[0];
                                this.Device.Name = list[1];
                            }
                        }

                        if (manufacturer == "Microsoft" && model == "XDeviceEmulator")
                        {
                            this.Device.Manufacturer = "";
                            this.Device.Name = "";
                            this.Device.DeviceType = DeviceType.Emulator;
                            this.Device.Identified = true;
                        }
                    });
                });

                CheckUserAgent(userAgent, "Android", mc2 =>
                {
                    this.Os.Name = "Android";
                    this.Os.Version = null;
                    CheckUserAgent(userAgent.Replace("-update", "."),
                        "/Android(?: )?(?:AllPhone_|CyanogenMod_)?(?:\\/)?v?([0-9.]+)/", mc3 =>
                        {
                            this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0);
                            this.Os.Details = "3";
                        });


                    CheckUserAgent(userAgent, "Android Eclair", mc3 =>
                    {
                        this.Os.Version = 2.0m;
                        this.Os.Details = "3";
                    });

                    this.Device.DeviceType = DeviceType.Mobile;

                    if (this.Os.Version >= 3)
                    {
                        this.Device.DeviceType = DeviceType.Tablet;
                    }

                    if (this.Os.Version >= 4 && CheckUserAgent(userAgent, "Mobile"))
                    {
                        this.Device.DeviceType = DeviceType.Mobile;
                    }

                    MatchCollection mc4;
                    if (CheckUserAgent(userAgent,
                        "/Eclair; (?:[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?) Build\\/([^\\/]*)\\//", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent, "/; ([^;]*[^;\\s])\\s+Build/", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent,
                        "/[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?; ([^;]*[^;\\s]);\\s+Build/", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent, "/\\(([^;]+);U;Android\\/[^;]+;[0-9]+\\*[0-9]+;CTC\\/2.0\\)/",
                        out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent, "/;\\s?([^;]+);\\s?[0-9]+\\*[0-9]+;\\s?CTC\\/2.0/", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent, "/zh-cn;\\s*(.*?)(\\/|build)/i", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent,
                        "/Android [^;]+; (?:[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?; )?([^)]+)\\)/", out mc4))
                    {
                        if (!CheckUserAgent(userAgent, "/[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?/", out mc4))
                        {
                            this.Device.Name = mc4[1].SafeString();
                        }
                    }
                    else if (CheckUserAgent(userAgent, "/^(.+?)\\/\\S+/i", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }

                    /* Sometimes we get a model name that starts with Android, in that case it is a mismatch and we should ignore it */
                    if (!string.IsNullOrEmpty(this.Device.Name) && this.Device.Name.Substring(0, 7) == "Android")
                    {
                        this.Device.Name = "";
                    }

                    if (!string.IsNullOrEmpty(this.Device.Name))
                    {
                        var model = CleanupModel(this.Device.Name);

                        var androidList = GloableConfigurations.AndroidModels.Where(x => x.Key == model)
                            .Select(x => x.Value).FirstOrDefault();
                        if (androidList != null && androidList.Length > 1)
                        {
                            this.Device.Manufacturer = androidList[0];
                            this.Device.Name = androidList[1];
                            if (androidList.Length > 2 && !string.IsNullOrEmpty(androidList[2]))
                            {
                                this.Device.DeviceType = DeviceType
                                    .GetAll<DeviceType>().FirstOrDefault(x => x.Extend == androidList[2]);
                            }
                        }

                        if (model == "Emulator" || model ==
                            "x86 Emulator" || model == "x86 VirtualBox" || model == "vm")
                        {
                            this.Device.Manufacturer = null;
                            this.Device.Name = null;
                            this.Device.DeviceType = DeviceType.Emulator;
                            this.Device.Identified = true;
                        }
                    }

                    CheckUserAgent(userAgent, "HP eStation", mc3 =>
                    {
                        this.Device.Manufacturer = "HP";
                        this.Device.Name = "eStation";
                        this.Device.DeviceType = DeviceType.Tablet;
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, "Pre\\/1.0", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pre";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, "Pre\\/1.1", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pre Plus";
                        this.Device.Identified = true;
                    });

                    CheckUserAgent(userAgent, "Pre\\/1.2", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pre 2";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, "Pre\\/3.0", mc3 =>
                    {
                        this.Device.Manufacturer = "HP";
                        this.Device.Name = "Pre 3";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, "Pixi\\/1.0", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pixi";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, "Pixi\\/1.1", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pixi Plus";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, "P160UN?A?\\/1.0", mc3 =>
                    {
                        this.Device.Manufacturer = "HP";
                        this.Device.Name = "Veer";
                        this.Device.Identified = true;
                    });
                });

                /* Google TV */
                CheckUserAgent(userAgent, "GoogleTV", mc2 =>
                {
                    this.Os.Name = "Google TV";
                    this.Device.DeviceType=DeviceType.Television;
                    CheckUserAgent(userAgent, "Chrome/5.", mc3 =>
                    {
                        this.Os.Version = 2m;
                    });
                    CheckUserAgent(userAgent, "Chrome/11.", mc3 =>
                    {
                        this.Os.Version = 2m;
                    });
                });

                /* WoPhone */

            });
        }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public Browser Browser { get; set; }

        /// <summary>
        /// 系统
        /// </summary>
        public Os Os { get; set; }

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

        #region private methods

        #region 检查UserAgent

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regexs">正则表达式</param>
        /// <param name="action">验证通过后执行</param>
        private void CheckUserAgent(string userAgent, string[] regexs, Action<MatchCollection, string> action)
        {
            foreach (var regex in regexs)
            {
                CheckUserAgent(userAgent, regex, action);
            }
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        private bool CheckUserAgent(string userAgent, string regex)
        {
            return CheckUserAgent(userAgent, regex, out MatchCollection match);
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="match"></param>
        private bool CheckUserAgent(string userAgent, string regex, out MatchCollection match)
        {
            match = new Regex(regex).Matches(userAgent);
            return match.Count > 0;
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="action">验证通过后执行</param>
        private void CheckUserAgent(string userAgent, string regex, Action<MatchCollection> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MatchCollection match = new Regex(regex).Matches(userAgent);
            if (match.Count > 0)
            {
                action.Invoke(match);
            }
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="action">验证通过后执行</param>
        private void CheckUserAgent(string userAgent, string regex, Action<MatchCollection, string> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MatchCollection match = new Regex(regex).Matches(userAgent);
            if (match.Count > 0)
            {
                action.Invoke(match, regex);
            }
        }

        #endregion

        #region cleanupModel

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string CleanupModel(string str)
        {
            str = str.SafeString();

            str = str.Replace("/_TD$/", "");
            str = str.Replace("/_CMCC$/", "");

            str = str.Replace("/_/g", " ");
            str = str.Replace("/^\\s+|\\s+$/g", "");
            str = str.Replace("/\\/[^/]+$/", "");
            str = str.Replace("/\\/[^/]+ Android\\/.*/", "");

            str = str.Replace("/^tita on /", "");
            str = str.Replace("/^Android on /", "");
            str = str.Replace("/^Android for /", "");
            str = str.Replace("/^ICS AOSP on /", "");
            str = str.Replace("/^Full AOSP on /", "");
            str = str.Replace("/^Full Android on /", "");
            str = str.Replace("/^Full Cappuccino on /", "");
            str = str.Replace("/^Full MIPS Android on /", "");
            str = str.Replace("/^Full Android/", "");

            str = str.Replace("/^Acer ?/i", "");
            str = str.Replace("/^Iconia /", "");
            str = str.Replace("/^Ainol /", "");
            str = str.Replace("/^Coolpad ?/i", "Coolpad ");
            str = str.Replace("/^ALCATEL /", "");
            str = str.Replace("/^Alcatel OT-(.*)/", "one touch $1");
            str = str.Replace("/^YL-/", "");
            str = str.Replace("/^Novo7 ?/i", "Novo7 ");
            str = str.Replace("/^GIONEE /", "");
            str = str.Replace("/^HW-/", "");
            str = str.Replace("/^Huawei[ -]/i", "Huawei ");
            str = str.Replace("/^SAMSUNG[ -]/i", "");
            str = str.Replace("/^SonyEricsson/", "");
            str = str.Replace("/^Lenovo Lenovo/", "Lenovo");
            str = str.Replace("/^LNV-Lenovo/", "Lenovo");
            str = str.Replace("/^Lenovo-/", "Lenovo ");
            str = str.Replace("/^(LG)[ _\\/]/", "$1-");
            str = str.Replace("/^(HTC.*)\\s(?:v|V)?[0-9.]+$/", "$1");
            str = str.Replace("/^(HTC)[-\\/]/", "$1 ");
            str = str.Replace("/^(HTC)([A-Z][0-9][0-9][0-9])/", "$1 $2");
            str = str.Replace("/^(Motorola[\\s|-])/", "");
            str = str.Replace("/^(Moto|MOT-)/", "");

            str = str.Replace("/-?(orange(-ls)?|vodafone|bouygues)$/i", "");
            str = str.Replace("/http:\\/\\/.+$/i", "");

            str = str.Replace("/^\\s+|\\s+$/g", "");

            return str;
        }

        #endregion

        #endregion
    }
}
