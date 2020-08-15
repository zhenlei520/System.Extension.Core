// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Systems;
using Newtonsoft.Json;

namespace EInfrastructure.Core.UserAgentParse
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAgentBase
    {
        internal readonly IRegexConfigurations _regexConfigurations;

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

        /// <summary>
        /// 检测伪装
        /// </summary>
        private bool DetectCamouflage = true;

        #endregion

        /// <summary>
        ///
        /// </summary>
        private UserAgentBase()
        {
            _regexConfigurations = new RegexConfigurationsDefault();
            Os = new Os();
            Browser = new Browser()
            {
                Stock = true,
                Hidden = false,
                Channel = ""
            };
            Device = new Devices()
            {
                DeviceType = DeviceType.Pc,
                Identified = false
            };
            Engine = new Engine();
            Features = new List<string>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="useFeatures"></param>
        /// <param name="activeXObject"></param>
        private UserAgentBase(bool useFeatures, bool activeXObject) : this()
        {
            UseFeatures = useFeatures;
            IsSupportActiveXObject = activeXObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="useFeatures"></param>
        /// <param name="activeXObject"></param>
        public UserAgentBase(string userAgent, bool useFeatures = false, bool activeXObject = false) : this(useFeatures,
            activeXObject)
        {
            CheckUserAgent(userAgent, _osNameList.ToArray(), (mc, regex) => { Os.Name = regex; });
            CheckUserAgent(userAgent, "SunOS", (mc) => { Os.Name = "Solaris"; });
            CheckUserAgent(userAgent, "Linux", (mc) =>
            {
                Os.Name = "Linux";
                CheckUserAgent(userAgent, "CentOS", (mc2) =>
                {
                    Os.Name = "CentOS";
                    CheckUserAgent(userAgent, @"CentOS\/[0-9\.\-]+el([0-9_]+)",
                        (mc3) => { Os.Version = new Versions(GetVersionResult(mc3).ReplaceRegex("_", "")); });
                });

                CheckUserAgent(userAgent, "Fedora", (mc2) =>
                {
                    Os.Name = "Fedora";
                    CheckUserAgent(userAgent, @"Fedora\/[0-9\.\-]+fc([0-9]+)",
                        (mc3) => { Os.Version = GetVersion(mc3); });
                });

                CheckUserAgent(userAgent, "Ubuntu", mc2 =>
                {
                    Os.Name = "Ubuntu";
                    CheckUserAgent(userAgent, @"Ubuntu\/([0-9.]*)",
                        mc3 => { Os.Version = GetVersion(mc3); });
                });

                CheckUserAgent(userAgent, new[] {"Gentoo", "Kubuntu", "Debian", "Slackware", "SUSE", "Turbolinux"},
                    (mc2, regex) => { Os.Name = regex; });

                CheckUserAgent(userAgent, "Mandriva Linux", (mc2) =>
                {
                    Os.Name = "Mandriva";
                    CheckUserAgent(userAgent, @"Mandriva Linux\/[0-9\.\-]+mdv([0-9]+)",
                        (mc3) => { Os.Version = GetVersion(mc3); });
                });

                CheckUserAgent(userAgent, "Red Hat",
                    (mc2) =>
                    {
                        Os.Name = "Red Hat";
                        CheckUserAgent(userAgent, @"Red Hat[^\/]*\/[0-9\.\-]+el([0-9_]+)",
                            (mc3) => { Os.Version = new Versions(GetVersionResult(mc3).ReplaceRegex("_", ".")); });
                    });
            });

            if (userAgent.Test(@"iPhone( Simulator)?;") || userAgent.Test(@"iPad;") || userAgent.Test(@"iPod;") ||
                userAgent.Test(@"iPhone\s*\d*s?[cp]?;", RegexOptions.IgnoreCase))
            {
                Os.Name = "iOS";
                Os.Version = new Versions("1.0");
                CheckUserAgent(userAgent, "OS (.*) like Mac OS X",
                    mc3 =>
                    {
                        Os.Version =
                            new Versions(GetVersionResult(mc3).ReplaceRegex("_", RegexOptions.Compiled, "."));
                    });

                if (CheckUserAgent(userAgent, "iPhone Simulator;"))
                {
                    Device.DeviceType = DeviceType.Emulator;
                }
                else if (CheckUserAgent(userAgent, "iPod;"))
                {
                    Device.DeviceType = DeviceType.Media;
                    Device.Manufacturer = "Apple";
                    Device.Name = "iPod Touch";
                }
                else if (CheckUserAgent(userAgent, "iPhone;") ||
                         CheckUserAgent(userAgent, @"iPhone\s*\d*s?[cp]?;", RegexOptions.IgnoreCase))
                {
                    Device.DeviceType = DeviceType.Mobile;
                    Device.Manufacturer = "Apple";
                    Device.Name = "iPhone";
                }
                else
                {
                    Device.DeviceType = DeviceType.Tablet;
                    Device.Manufacturer = "Apple";
                    Device.Name = "iPad";
                }

                Device.Identified = true;
            }

            CheckUserAgent(userAgent, "Mac OS X", mc2 =>
            {
                Os.Name = "Mac OS X";
                CheckUserAgent(userAgent, @"Mac OS X (10[0-9\._]*)",
                    mc3 =>
                    {
                        Os.Version =
                            new Versions(GetVersionResult(mc3).ReplaceRegex("_", RegexOptions.Compiled, "."));
                    });
            });

            CheckUserAgent(userAgent, "Windows", mc2 =>
            {
                Os.Name = "Windows";
                CheckUserAgent(userAgent, @"Windows NT ([0-9]\.[0-9])", mc3 =>
                {
                    Os.Version = GetVersion(mc3);
                    Os.SetAlias();
                });

                CheckUserAgent(userAgent, new[] {"Windows 95", "Win95", "Win 9x 4.00"}, (mc3, regex) =>
                {
                    Os.Version = new Versions("4.0");
                    Os.Alias = "95";
                });

                CheckUserAgent(userAgent, new[] {"Windows 98", "Win98", "Win 9x 4.10"}, (mc3, regex) =>
                {
                    Os.Version = new Versions("4.1");
                    Os.Alias = "98";
                });

                CheckUserAgent(userAgent, new[] {"Windows ME", "WinME", "Win 9x 4.90"}, (mc3, regex) =>
                {
                    Os.Version = new Versions("4.9");
                    Os.Alias = "ME";
                });

                CheckUserAgent(userAgent, new[] {"Windows XP", "WinXP"}, (mc3, regex) =>
                {
                    Os.Version = new Versions("5.1");
                    Os.Alias = "XP";
                });

                CheckUserAgent(userAgent, "WP7", mc3 =>
                {
                    Os.Name = "Windows Phone";
                    Os.Version = new Versions("7.0");
                    Os.Details = "2";
                    Device.DeviceType = DeviceType.Mobile;
                    Browser.Mode = "desktop";
                });

                CheckUserAgent(userAgent, new[] {"Windows CE", "WinCE", "WindowsCE"}, (mc3, regex) =>
                {
                    if (CheckUserAgent(userAgent, " IEMobile"))
                    {
                        Os.Name = "Windows Mobile";
                        if (CheckUserAgent(userAgent, " IEMobile 8"))
                        {
                            Os.Version = new Versions("6.5");
                            Os.Details = "2";
                        }
                        else if (CheckUserAgent(userAgent, " IEMobile 7"))
                        {
                            Os.Version = new Versions("6.1");
                            Os.Details = "2";
                        }
                        else if (CheckUserAgent(userAgent, " IEMobile 6"))
                        {
                            Os.Version = new Versions("6.0");
                            Os.Details = "2";
                        }
                    }
                    else
                    {
                        Os.Name = "Windows CE";
                        CheckUserAgent(userAgent, new[] {@"WindowsCEOS\/([0-9.]*)", "Windows CE ([0-9.]*)"},
                            (mc4, regex2) =>
                            {
                                Os.Version = GetVersion(mc4);
                                Os.Details = "2";
                            });
                    }

                    Device.DeviceType = DeviceType.Mobile;
                });

                CheckUserAgent(userAgent, "Windows Mobile", mc3 =>
                {
                    Os.Name = "Windows Mobile";
                    Device.DeviceType = DeviceType.Mobile;
                });

                CheckUserAgent(userAgent, @"WindowsMobile\/([0-9.]*)", mc3 =>
                {
                    Os.Name = "Windows Mobile";
                    Os.Version = GetVersion(mc3);
                    Os.Details = "2";
                    Device.DeviceType = DeviceType.Mobile;
                });

                CheckUserAgent(userAgent, "Windows Phone [0-9]", mc3 =>
                {
                    Os.Name = "Windows Mobile";
                    Os.Version =
                        new Versions(new Regex("Windows Phone ([0-9.]*)").Matches(userAgent)[1].SafeString());
                    Os.Details = "2";
                    Device.DeviceType = DeviceType.Mobile;
                });

                CheckUserAgent(userAgent, "Windows Phone OS", mc3 =>
                {
                    Os.Name = "Windows Phone";
                    Os.Version = new Versions(new Regex("Windows Phone OS ([0-9.]*)").Matches(userAgent)[1]
                        .SafeString());
                    Os.Details = "2";

                    if (Os.Version < "7")
                    {
                        Os.Name = "Windows Mobile";
                    }

                    CheckUserAgent(userAgent, @"IEMobile\/[^;]+; ([^;]+); ([^;]+)[;|\)]", mc4 =>
                    {
                        Device.Name = mc4[2].SafeString();
                        Device.Manufacturer = mc4[1].SafeString();
                    });
                    Device.DeviceType = DeviceType.Mobile;

                    var manufacturer = Device.Manufacturer;
                    var model = CleanupModel(Device.Name);

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
                            Device.Manufacturer = list[0];
                            Device.Name = list[1];
                        }
                    }

                    if (manufacturer == "Microsoft" && model == "XDeviceEmulator")
                    {
                        Device.Manufacturer = "";
                        Device.Name = "";
                        Device.DeviceType = DeviceType.Emulator;
                        Device.Identified = true;
                    }
                });
            });

            CheckUserAgent(userAgent, "Android", mc2 =>
            {
                Os.Name = "Android";
                Os.Version = null;
                CheckUserAgent(userAgent.ReplaceRegex("-update", "."),
                    @"Android(?: )?(?:AllPhone_|CyanogenMod_)?(?:\/)?v?([0-9.]+)", mc3 =>
                    {
                        Os.Version = GetVersion(mc3);
                        Os.Details = "3";
                    });


                CheckUserAgent(userAgent, "Android Eclair", mc3 =>
                {
                    Os.Version = new Versions("2.0");
                    Os.Details = "3";
                });

                Device.DeviceType = DeviceType.Mobile;

                // if (Os.Version >= "3")
                // {
                //     Device.DeviceType = DeviceType.Tablet;
                // }

                if (Os.Version >= "4" && CheckUserAgent(userAgent, "Mobile"))
                {
                    Device.DeviceType = DeviceType.Mobile;
                }

                MatchCollection mc4;
                if (CheckUserAgent(userAgent,
                    @"Eclair; (?:[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?) Build\/([^\/]*)\\/", out mc4))
                {
                    Device.Name = GetMatchResult(mc4, 1);
                }
                else if (CheckUserAgent(userAgent, @"; ([^;]*[^;\s])\s+Build", out mc4))
                {
                    Device.Name = GetMatchResult(mc4, 1);
                }
                else if (CheckUserAgent(userAgent,
                    @"[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?; ([^;]*[^;\s]);\s+Build", out mc4))
                {
                    Device.Name = GetMatchResult(mc4, 1);
                }
                else if (CheckUserAgent(userAgent, @"\(([^;]+);U;Android\/[^;]+;[0-9]+\*[0-9]+;CTC\/2.0\)",
                    out mc4))
                {
                    Device.Name = GetMatchResult(mc4, 1);
                }
                else if (CheckUserAgent(userAgent, @";\s?([^;]+);\s?[0-9]+\*[0-9]+;\s?CTC\/2.0", out mc4))
                {
                    Device.Name = GetMatchResult(mc4, 1);
                }
                else if (CheckUserAgent(userAgent, @"zh-cn;\s*(.*?)(\/|build)", RegexOptions.IgnoreCase, out mc4))
                {
                    Device.Name = GetMatchResult(mc4, 1);
                }
                else if (CheckUserAgent(userAgent,
                    @"Android [^;]+; (?:[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?; )?([^)]+)\)", out mc4))
                {
                    if (!CheckUserAgent(userAgent, "[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?", out mc4))
                    {
                        Device.Name = GetMatchResult(mc4, 1);
                    }
                }
                else if (CheckUserAgent(userAgent, @"^(.+?)\/\S+", RegexOptions.IgnoreCase, out mc4))
                {
                    // Device.Name = mc4[0].SafeString().Split('/')[0];
                    Device.Name = GetMatchResult(mc4, 1);
                }

                /* Sometimes we get a model name that starts with Android, in that case it is a mismatch and we should ignore it */
                if (!string.IsNullOrEmpty(Device.Name) && Device.Name.Substring(0, 7) == "Android")
                {
                    Device.Name = "";
                }

                if (!string.IsNullOrEmpty(Device.Name))
                {
                    var model = CleanupModel(Device.Name);

                    var androidList = GloableConfigurations.AndroidModels.Where(x => x.Key == model)
                        .Select(x => x.Value).FirstOrDefault();
                    if (androidList != null && androidList.Length > 1)
                    {
                        Device.Manufacturer = androidList[0];
                        Device.Name = androidList[1];
                        if (androidList.Length > 2 && !string.IsNullOrEmpty(androidList[2]))
                        {
                            Device.DeviceType = DeviceType
                                .GetAll<DeviceType>().FirstOrDefault(x => x.Extend == androidList[2]);
                        }
                    }

                    if (model == "Emulator" || model ==
                        "x86 Emulator" || model == "x86 VirtualBox" || model == "vm")
                    {
                        Device.Manufacturer = null;
                        Device.Name = null;
                        Device.DeviceType = DeviceType.Emulator;
                        Device.Identified = true;
                    }
                }

                CheckUserAgent(userAgent, "HP eStation", mc3 =>
                {
                    Device.Manufacturer = "HP";
                    Device.Name = "eStation";
                    Device.DeviceType = DeviceType.Tablet;
                    Device.Identified = true;
                });
                CheckUserAgent(userAgent, @"Pre\/1.0", mc3 =>
                {
                    Device.Manufacturer = "Palm";
                    Device.Name = "Pre";
                    Device.Identified = true;
                });
                CheckUserAgent(userAgent, @"Pre\/1.1", mc3 =>
                {
                    Device.Manufacturer = "Palm";
                    Device.Name = "Pre Plus";
                    Device.Identified = true;
                });

                CheckUserAgent(userAgent, @"Pre\/1.2", mc3 =>
                {
                    Device.Manufacturer = "Palm";
                    Device.Name = "Pre 2";
                    Device.Identified = true;
                });
                CheckUserAgent(userAgent, @"Pre\/3.0", mc3 =>
                {
                    Device.Manufacturer = "HP";
                    Device.Name = "Pre 3";
                    Device.Identified = true;
                });
                CheckUserAgent(userAgent, @"Pixi\/1.0", mc3 =>
                {
                    Device.Manufacturer = "Palm";
                    Device.Name = "Pixi";
                    Device.Identified = true;
                });
                CheckUserAgent(userAgent, @"Pixi\/1.1", mc3 =>
                {
                    Device.Manufacturer = "Palm";
                    Device.Name = "Pixi Plus";
                    Device.Identified = true;
                });
                CheckUserAgent(userAgent, @"P160UN?A?\/1.0", mc3 =>
                {
                    Device.Manufacturer = "HP";
                    Device.Name = "Veer";
                    Device.Identified = true;
                });
            });

            /* Google TV */
            CheckUserAgent(userAgent, "GoogleTV", mc2 =>
            {
                Os.Name = "Google TV";
                Device.DeviceType = DeviceType.Television;
                CheckUserAgent(userAgent, "Chrome/5.", mc3 => { Os.Version = new Versions("2"); });
                CheckUserAgent(userAgent, "Chrome/11.", mc3 => { Os.Version = new Versions("2"); });
            });

            /* WoPhone */
            CheckUserAgent(userAgent, "WoPhone", mc2 =>
            {
                Os.Name = "WoPhone";
                Device.DeviceType = DeviceType.Mobile;
                CheckUserAgent(userAgent, @"WoPhone\/([0-9\.]*)",
                    mc3 => { Os.Version = GetVersion(mc3); });
            });

            /* BlackBerry */
            CheckUserAgent(userAgent, "BlackBerry", mc2 =>
            {
                Os.Name = "BlackBerry OS";

                if (!CheckUserAgent(userAgent, "Opera"))
                {
                    MatchCollection mc3;
                    if (CheckUserAgent(userAgent, @"BlackBerry([0-9]*)\/([0-9.]*)", out mc3))
                    {
                        Device.Name = mc3[1].SafeString();
                        Os.Version = GetVersion(mc3);
                        Os.Details = "2";
                    }

                    if (CheckUserAgent(userAgent, "; BlackBerry ([0-9]*);", out mc3))
                    {
                        Device.Name = mc3[1].SafeString();
                    }

                    if (CheckUserAgent(userAgent, @"Version\/([0-9.]*)", out mc3))
                    {
                        Os.Version = GetVersion(mc3);
                        Os.Details = "2";
                    }

                    if (Os.Version >= 10 + "")
                    {
                        Os.Name = "BlackBerry";
                    }

                    if (!string.IsNullOrEmpty(Device.Name))
                    {
                        var deviceInfo =
                            GloableConfigurations.BlackberryModels.FirstOrDefault(x => x.Key == Device.Name);
                        if (!string.IsNullOrEmpty(deviceInfo.Key))
                        {
                            Device.Name = "BlackBerry " + deviceInfo.Value + " " + Device.Name;
                        }
                        else
                        {
                            Device.Name = "BlackBerry " + Device.Name;
                        }
                    }
                    else
                    {
                        Device.Name = "BlackBerry";
                    }
                }
                else
                {
                    Device.Name = "BlackBerry";
                }

                Device.Manufacturer = "RIM";
                Device.DeviceType = DeviceType.Mobile;
                Device.Identified = true;
            });

            /*  BlackBerry PlayBook  */
            if (CheckUserAgent(userAgent, "RIM Tablet OS"))
            {
                Os.Name = "BlackBerry Tablet OS";
                Os.Version =
                    new Versions(new Regex("RIM Tablet OS ([0-9.]*)").Matches(userAgent)[1].SafeString());
                Os.Details = "2";
                Device.Manufacturer = "RIM";
                Device.Name = "BlackBerry PlayBook";
                Device.DeviceType = DeviceType.Tablet;
                Device.Identified = true;
            }
            else if (CheckUserAgent(userAgent, "PlayBook"))
            {
                CheckUserAgent(userAgent, @"Version\/(10[0-9.]*)", mc2 =>
                {
                    Os.Name = "BlackBerry";
                    Os.Version = GetVersion(mc2);
                    Os.Details = "2";

                    Device.Manufacturer = "RIM";
                    Device.Name = "BlackBerry PlayBook";
                    Device.DeviceType = DeviceType.Tablet;
                    Device.Identified = true;
                });
            }

            /* WebOS */
            CheckUserAgent(userAgent, "(?:web|hpw)OS", mc2 =>
            {
                Os.Name = "webOS";
                Os.Version =
                    new Versions(new Regex(@"(?:web|hpw)OS\/([0-9.]*)").Matches(userAgent)[1].SafeString());

                if (CheckUserAgent(userAgent, "tablet"))
                {
                    Device.DeviceType = DeviceType.Tablet;
                }

                Device.Manufacturer = CheckUserAgent(userAgent, "hpwOS") ? "HP" : "Palm";
                if (CheckUserAgent(userAgent, @"Pre\/1.0"))
                {
                    Device.Name = "Pre";
                }

                if (CheckUserAgent(userAgent, @"Pre\/1.1"))
                {
                    Device.Name = "Pre Plus";
                }

                if (CheckUserAgent(userAgent, @"Pre\/1.2"))
                {
                    Device.Name = "Pre2";
                }

                if (CheckUserAgent(userAgent, @"Pre\/3.0"))
                {
                    Device.Name = "Pre3";
                }

                if (CheckUserAgent(userAgent, @"Pixi\/1.0"))
                {
                    Device.Name = "Pixi";
                }

                if (CheckUserAgent(userAgent, @"Pixi\/1.1"))
                {
                    Device.Name = "Pixi Plus";
                }

                if (CheckUserAgent(userAgent, @"P160UN?A?\/1.0"))
                {
                    Device.Name = "Veer";
                }

                if (CheckUserAgent(userAgent, @"TouchPad\/1.0"))
                {
                    Device.Name = "TouchPad";
                }

                if (CheckUserAgent(userAgent, @"Emulator\\") || CheckUserAgent(userAgent, @"Desktop\\"))
                {
                    Device.DeviceType = DeviceType.Emulator;
                    Device.Name = null;
                    Device.Manufacturer = null;
                }

                Device.Identified = true;
            });

            /* S60 */
            CheckUserAgent(userAgent, new[] {"Symbian", "Series[ ]?60", "S60"}, (mc2, regex) =>
            {
                Os.Name = "Series60";

                if (CheckUserAgent(userAgent, "SymbianOS/9.1") && !CheckUserAgent(userAgent, "Series60"))
                {
                    Os.Version = new Versions(3.0 + "");
                }

                CheckUserAgent(userAgent, @"Series60\/([0-9.]*)",
                    mc3 => { Os.Version = GetVersion(mc3); });

                CheckUserAgent(userAgent, @"Nokia([^\/;]+)[\/|;]", mc3 =>
                {
                    if (mc3[1].SafeString() != "Browser")
                    {
                        Device.Manufacturer = "Nokia";
                        Device.Name = mc3[1].SafeString();
                        Device.Identified = true;
                    }
                });

                CheckUserAgent(userAgent, @"Vertu([^\/;]+)[\/|;]", mc3 =>
                {
                    Device.Manufacturer = "Vertu";
                    Device.Name = mc3[1].SafeString();
                    Device.Identified = true;
                });


                CheckUserAgent(userAgent, @"Symbian; U; ([^;]+); [a-z][a-z]\-[a-z][a-z]", RegexOptions.IgnoreCase,
                    mc3 =>
                    {
                        Device.Manufacturer = "Nokia";
                        Device.Name = mc3[1].SafeString();
                        Device.Identified = true;
                    });

                CheckUserAgent(userAgent, @"Samsung\/([^;]*);", mc3 =>
                {
                    Device.Manufacturer = GloableConfigurations.StringsSamsung;
                    Device.Name = mc3[1].SafeString();
                    Device.Identified = true;
                });

                Device.DeviceType = DeviceType.Mobile;
            });

            /* S40 */
            CheckUserAgent(userAgent, "Series40", mc2 =>
            {
                Os.Name = "Series40";

                CheckUserAgent(userAgent, @"Nokia([^\/]+)\\", mc3 =>
                {
                    Device.Manufacturer = "Nokia";
                    Device.Name = mc3[1].SafeString();
                    Device.Identified = true;
                });

                Device.DeviceType = DeviceType.Mobile;
            });

            /* MeeGo */
            CheckUserAgent(userAgent, "MeeGo", mc2 =>
            {
                Os.Name = "MeeGo";
                Device.DeviceType = DeviceType.Mobile;

                CheckUserAgent(userAgent, @"Nokia([^\)]+)\)", mc3 =>
                {
                    Device.Manufacturer = "Nokia";
                    Device.Name = mc3[1].SafeString();
                    Device.Identified = true;
                });
            });

            /* Maemo */
            CheckUserAgent(userAgent, "Maemo", mc2 =>
            {
                Os.Name = "Maemo";
                Device.DeviceType = DeviceType.Mobile;

                CheckUserAgent(userAgent, "(N[0-9]+)", mc3 =>
                {
                    Device.Manufacturer = "Nokia";
                    Device.Name = mc3[1].SafeString();
                    Device.Identified = true;
                });
            });

            /* Tizen */
            CheckUserAgent(userAgent, "Tizen", mc2 =>
            {
                Os.Name = "Tizen";

                CheckUserAgent(userAgent, @"Tizen[\/ ]([0-9.]*)",
                    mc3 => { Os.Version = GetVersion(mc3); });

                Device.DeviceType = DeviceType.Mobile;

                CheckUserAgent(userAgent, @"\(([^;]+); ([^\/]+)\\", mc3 =>
                {
                    if (mc3[1].SafeString() != "Linux")
                    {
                        Device.Manufacturer = mc3[1].SafeString();
                        Device.Name = mc3[2].SafeString();

                        List<KeyValuePair<string, string[]>> tizenModel =
                            GloableConfigurations.TizenModels.Where(x =>
                                x.Key == Device.Manufacturer).Select(x => x.Value).FirstOrDefault() ??
                            new List<KeyValuePair<string, string[]>>();
                        if (tizenModel.Count > 0 && tizenModel
                            .Any(x => x.Key == Device.Name))
                        {
                            var manufacturer = Device.Manufacturer;
                            var model = CleanupModel(Device.Name);

                            var temp = GloableConfigurations.TizenModels
                                .Where(x => x.Key == manufacturer).Select(x => x.Value).FirstOrDefault();
                            if (temp != null)
                            {
                                Device.Manufacturer =
                                    temp.Where(x => x.Key == model).Select(x => x.Value).ToList()[0].SafeString();
                                Device.Name = temp.Where(x => x.Key == model).Select(x => x.Value).ToList()[1]
                                    .SafeString();
                                Device.Identified = true;
                            }
                        }
                    }
                });
            });

            /* Bada */
            CheckUserAgent(userAgent, "[b|B]ada", mc2 =>
            {
                Os.Name = "Bada";
                CheckUserAgent(userAgent, @"[b|B]ada\/([0-9.]*)",
                    mc3 => { Os.Version = GetVersion(mc3); });

                Device.DeviceType = DeviceType.Mobile;

                CheckUserAgent(userAgent, @"\(([^;]+); ([^\/]+)\\", mc3 =>
                {
                    Device.Manufacturer = mc3[1].SafeString();
                    Device.Name = CleanupModel(mc3[2].SafeString());
                });

                var badaModel = GloableConfigurations.BadaModels.Where(x => x.Key == Device.Manufacturer)
                    .Select(x => x.Value).FirstOrDefault();
                if (badaModel != null && badaModel.Count > 0 && badaModel.Any(x => x.Key == Device.Name))
                {
                    var manufacturer = Device.Manufacturer;
                    var model = CleanupModel(Device.Name);


                    badaModel = GloableConfigurations.BadaModels.Where(x => x.Key == manufacturer)
                        .Select(x => x.Value).FirstOrDefault();

                    if (badaModel != null)
                    {
                        Device.Manufacturer =
                            badaModel.Where(x => x.Key == model).Select(x => x.Value).FirstOrDefault()?[0];
                        Device.Name =
                            badaModel.Where(x => x.Key == model).Select(x => x.Value).FirstOrDefault()?[1];
                    }

                    Device.Identified = true;
                }
            });

            /* Brew */
            CheckUserAgent(userAgent, "BREW", RegexOptions.IgnoreCase, mc2 =>
            {
                Os.Name = "Brew";
                Device.DeviceType = DeviceType.Mobile;

                MatchCollection mc3;

                if (CheckUserAgent(userAgent, "BREW; U; ([0-9.]*)", RegexOptions.IgnoreCase, out mc3))
                {
                    Os.Version = GetVersion(mc3);
                }
                else if (CheckUserAgent(userAgent, @";BREW\/([0-9.]*)", RegexOptions.IgnoreCase, out mc3))
                {
                    Os.Version = GetVersion(mc3);
                }

                CheckUserAgent(userAgent,
                    @"\(([^;]+);U;REX\/[^;]+;BREW\/[^;]+;(?:.*;)?[0-9]+\*[0-9]+;CTC\/2.0\)",
                    mc4 => { Device.Name = mc4[1].SafeString(); });

                if (!string.IsNullOrEmpty(Device.Name))
                {
                    var model = CleanupModel(Device.Name);

                    if (GloableConfigurations.BrewModels.Any(x => x.Key == model))
                    {
                        Device.Manufacturer = GloableConfigurations.BrewModels.Where(x => x.Key == model)
                            .Select(x => x.Value).FirstOrDefault()?[0];
                        Device.Name = GloableConfigurations.BrewModels.Where(x => x.Key == model)
                            .Select(x => x.Value).FirstOrDefault()?[1];
                        Device.Identified = true;
                    }
                }
            });

            /* MTK */

            CheckUserAgent(userAgent, @"\(MTK;", mc2 =>
            {
                Os.Name = "MTK";
                Device.DeviceType = DeviceType.Mobile;
            });

            /* CrOS */
            CheckUserAgent(userAgent, "CrOS", mc2 =>
            {
                Os.Name = "Chrome OS";
                Device.DeviceType = DeviceType.Pc;
            });

            /* Joli OS */
            CheckUserAgent(userAgent, "Joli OS", mc2 =>
            {
                Os.Name = "Joli OS";
                Device.DeviceType = DeviceType.Pc;

                CheckUserAgent(userAgent, @"Joli OS\/([0-9.]*)", RegexOptions.IgnoreCase,
                    mc3 => { Os.Version = GetVersion(mc3); });
            });

            /* Haiku */
            CheckUserAgent(userAgent, "Haiku", mc2 =>
            {
                Os.Name = "Haiku";
                Device.DeviceType = DeviceType.Pc;
            });

            /* QNX */
            CheckUserAgent(userAgent, "QNX", mc2 =>
            {
                Os.Name = "QNX";
                Device.DeviceType = DeviceType.Mobile;
            });

            /* OS/2 Warp */
            CheckUserAgent(userAgent, @"OS\/2; Warp", mc2 =>
            {
                Os.Name = "OS/2 Warp";
                Device.DeviceType = DeviceType.Pc;

                CheckUserAgent(userAgent, @"OS\/2; Warp ([0-9.]*)", RegexOptions.IgnoreCase,
                    mc3 => { Os.Version = GetVersion(mc3); });
            });


            /* Grid OS */
            CheckUserAgent(userAgent, "Grid OS", mc2 =>
            {
                Os.Name = "Grid OS";
                Device.DeviceType = DeviceType.Tablet;

                CheckUserAgent(userAgent, "Grid OS ([0-9.]*)", RegexOptions.IgnoreCase,
                    mc3 => { Os.Version = GetVersion(mc3); });
            });

            /* AmigaOS */
            CheckUserAgent(userAgent, "AmigaOS", RegexOptions.IgnoreCase, mc2 =>
            {
                Os.Name = "AmigaOS";
                Device.DeviceType = DeviceType.Pc;

                CheckUserAgent(userAgent, "AmigaOS ([0-9.]*)", RegexOptions.IgnoreCase,
                    mc3 => { Os.Version = GetVersion(mc3); });
            });

            /* MorphOS */
            CheckUserAgent(userAgent, "MorphOS", RegexOptions.IgnoreCase, mc2 =>
            {
                Os.Name = "MorphOS";
                Device.DeviceType = DeviceType.Pc;
                CheckUserAgent(userAgent, "MorphOS ([0-9.]*)", RegexOptions.IgnoreCase,
                    mc3 => { Os.Version = GetVersion(mc3); });
            });

            /* Kindle */
            if (CheckUserAgent(userAgent, "Kindle") && !CheckUserAgent(userAgent, "Fire"))
            {
                Os.Name = "";

                Device.Manufacturer = "Amazon";
                Device.Name = "Kindle";
                Device.DeviceType = DeviceType.Reader;

                if (CheckUserAgent(userAgent, @"Kindle\/2.0"))
                {
                    Device.Name = "Kindle 2";
                }

                if (CheckUserAgent(userAgent, @"Kindle\/3.0"))
                {
                    Device.Name = "Kindle 3 or later";
                }

                Device.Identified = true;
            }

            /* NOOK */
            CheckUserAgent(userAgent, "nook browser", mc2 =>
            {
                Os.Name = "Android";
                Device.Manufacturer = "Barnes & Noble";
                Device.Name = "NOOK";
                Device.DeviceType = DeviceType.Reader;
                Device.Identified = true;
            });

            /* Bookeen */
            CheckUserAgent(userAgent, @"bookeen\/cybook", mc2 =>
            {
                Os.Name = "";
                Device.Manufacturer = "Bookeen";
                Device.Name = "Cybook";
                Device.DeviceType = DeviceType.Reader;

                CheckUserAgent(userAgent, "Orizon", mc3 => { Device.Name = "Cybook Orizon"; });

                Device.Identified = true;
            });

            /* Sony Reader */
            CheckUserAgent(userAgent, "EBRD1101", mc2 =>
            {
                Os.Name = "";

                Device.Manufacturer = "Sony";
                Device.Name = "Reader";
                Device.DeviceType = DeviceType.Reader;
                Device.Identified = true;
            });

            /* iRiver */
            CheckUserAgent(userAgent, "Iriver ;", mc2 =>
            {
                Os.Name = "";

                Device.Manufacturer = "iRiver";
                Device.Name = "Story";
                Device.DeviceType = DeviceType.Reader;

                if (CheckUserAgent(userAgent, "EB07"))
                {
                    Device.Name = "Story HD EB07";
                }

                Device.Identified = true;
            });

            /****************************************************
             *      Nintendo
             *
             *      Opera/9.30 (Nintendo Wii; U; ; 3642; en)
             *      Opera/9.30 (Nintendo Wii; U; ; 2047-7; en)
             *      Opera/9.50 (Nintendo DSi; Opera/507; U; en-US)
             *      Mozilla/5.0 (Nintendo 3DS; U; ; en) Version/1.7455.US
             *      Mozilla/5.0 (Nintendo 3DS; U; ; en) Version/1.7455.EU
             */
            if (CheckUserAgent(userAgent, "Nintendo Wii"))
            {
                Os.Name = "";
                Device.Manufacturer = "Nintendo";
                Device.Name = "Wii";
                Device.DeviceType = DeviceType.Gaming;
                Device.Identified = true;
            }

            if (CheckUserAgent(userAgent, "Nintendo DSi"))
            {
                Os.Name = "";
                Device.Manufacturer = "Nintendo";
                Device.Name = "DSi";
                Device.DeviceType = DeviceType.Gaming;
                Device.Identified = true;
            }

            if (CheckUserAgent(userAgent, "Nintendo 3DS"))
            {
                Os.Name = "";

                Device.Manufacturer = "Nintendo";
                Device.Name = "3DS";
                Device.DeviceType = DeviceType.Gaming;

                CheckUserAgent(userAgent, @"Version\/([0-9.]*)",
                    mc2 => { Os.Version = GetVersion(mc2); });

                Device.Identified = true;
            }

            if (CheckUserAgent(userAgent, "PlayStation Portable"))
            {
                Os.Name = "";
                Device.Manufacturer = "Sony";
                Device.Name = "Playstation Portable";
                Device.DeviceType = DeviceType.Gaming;
                Device.Identified = true;
            }

            if (CheckUserAgent(userAgent, "PlayStation Vita"))
            {
                Os.Name = "";
                CheckUserAgent(userAgent, "PlayStation Vita ([0-9.]*)",
                    mc2 => { Os.Version = GetVersion(mc2); });
                Device.Manufacturer = "Sony";
                Device.Name = "PlayStation Vita";
                Device.DeviceType = DeviceType.Gaming;
                Device.Identified = true;
            }

            if (CheckUserAgent(userAgent, "PlayStation 3", RegexOptions.IgnoreCase))
            {
                Os.Name = "";
                CheckUserAgent(userAgent, "PLAYSTATION 3;? ([0-9.]*)",
                    mc2 => { Os.Version = GetVersion(mc2); });
                Device.Manufacturer = "Sony";
                Device.Name = "Playstation 3";
                Device.DeviceType = DeviceType.Gaming;
                Device.Identified = true;
            }

            /****************************************************
             *      Panasonic Smart Viera
             *
             *      Mozilla/5.0 (FreeBSD; U; Viera; ja-JP) AppleWebKit/535.1 (KHTML, like Gecko) Viera/1.2.4 Chrome/14.0.835.202 Safari/535.1
             */
            if (CheckUserAgent(userAgent, "Viera"))
            {
                Os.Name = "";
                Device.Manufacturer = "Panasonic";
                Device.Name = "Smart Viera";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            }

            /****************************************************
             *      Sharp AQUOS TV
             *
             *      Mozilla/5.0 (DTV) AppleWebKit/531.2  (KHTML, like Gecko) AQUOSBrowser/1.0 (US00DTV;V;0001;0001)
             *      Mozilla/5.0 (DTV) AppleWebKit/531.2+ (KHTML, like Gecko) Espial/6.0.4 AQUOSBrowser/1.0 (CH00DTV;V;0001;0001)
             *      Opera/9.80 (Linux armv6l; U; en) Presto/2.8.115 Version/11.10 AQUOS-AS/1.0 LC-40LE835X
             */
            if (CheckUserAgent(userAgent, "AQUOSBrowser") || CheckUserAgent(userAgent, "AQUOS-AS"))
            {
                Os.Name = "";
                Device.Manufacturer = GloableConfigurations.StringsSharp;
                Device.Name = "Aquos TV";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            }

            /****************************************************
             *      Samsung Smart TV
             *
             *      Mozilla/5.0 (SmartHub; SMART-TV; U; Linux/SmartTV; Maple2012) AppleWebKit/534.7 (KHTML, like Gecko) SmartTV Safari/534.7
             *      Mozilla/5.0 (SmartHub; SMART-TV; U; Linux/SmartTV) AppleWebKit/531.2+ (KHTML, like Gecko) WebBrowser/1.0 SmartTV Safari/531.2+
             */
            if (CheckUserAgent(userAgent, "SMART-TV"))
            {
                Os.Name = "";
                Device.Manufacturer = GloableConfigurations.StringsSamsung;
                Device.Name = "Smart TV";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;

                CheckUserAgent(userAgent, "Maple([0-9]*)", mc2 => { Device.Name += " " + mc2[1]; });
            }

            /****************************************************
            *      Sony Internet TV
            *
            *      Opera/9.80 (Linux armv7l; U; InettvBrowser/2.2(00014A;SonyDTV115;0002;0100) KDL-46EX640; CC/USA; en) Presto/2.8.115 Version/11.10
            *      Opera/9.80 (Linux armv7l; U; InettvBrowser/2.2(00014A;SonyDTV115;0002;0100) KDL-40EX640; CC/USA; en) Presto/2.10.250 Version/11.60
            *      Opera/9.80 (Linux armv7l; U; InettvBrowser/2.2(00014A;SonyDTV115;0002;0100) N/A; CC/USA; en) Presto/2.8.115 Version/11.10
            *      Opera/9.80 (Linux mips; U; InettvBrowser/2.2 (00014A;SonyDTV115;0002;0100) ; CC/JPN; en) Presto/2.9.167 Version/11.50
            *      Opera/9.80 (Linux mips; U; InettvBrowser/2.2 (00014A;SonyDTV115;0002;0100) AZ2CVT2; CC/CAN; en) Presto/2.7.61 Version/11.00
            *      Opera/9.80 (Linux armv6l; Opera TV Store/4207; U; (SonyBDP/BDV11); en) Presto/2.9.167 Version/11.50
            *      Opera/9.80 (Linux armv6l ; U; (SonyBDP/BDV11); en) Presto/2.6.33 Version/10.60
            *      Opera/9.80 (Linux armv6l; U; (SonyBDP/BDV11); en) Presto/2.8.115 Version/11.10
            */
            if (CheckUserAgent(userAgent, "SonyDTV|SonyBDP|SonyCEBrowser"))
            {
                Os.Name = "";
                Device.Manufacturer = "Sony";
                Device.Name = "Internet TV";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            }

            /****************************************************
             *      Philips Net TV
             *
             *      Opera/9.70 (Linux armv6l ; U; CE-HTML/1.0 NETTV/2.0.2; en) Presto/2.2.1
             *      Opera/9.80 (Linux armv6l ; U; CE-HTML/1.0 NETTV/3.0.1;; en) Presto/2.6.33 Version/10.60
             *      Opera/9.80 (Linux mips; U; CE-HTML/1.0 NETTV/3.0.1; PHILIPS-AVM-2012; en) Presto/2.9.167 Version/11.50
             *      Opera/9.80 (Linux mips ; U; HbbTV/1.1.1 (; Philips; ; ; ; ) CE-HTML/1.0 NETTV/3.1.0; en) Presto/2.6.33 Version/10.70
             *      Opera/9.80 (Linux i686; U; HbbTV/1.1.1 (; Philips; ; ; ; ) CE-HTML/1.0 NETTV/3.1.0; en) Presto/2.9.167 Version/11.50
             */

            if (CheckUserAgent(userAgent, @"NETTV\\"))
            {
                Os.Name = "";
                Device.Manufacturer = "Philips";
                Device.Name = "Net TV";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            }

            /****************************************************
             *      LG NetCast TV
             *
             *      Mozilla/5.0 (DirectFB; Linux armv7l) AppleWebKit/534.26+ (KHTML, like Gecko) Version/5.0 Safari/534.26+ LG Browser/5.00.00(+mouse+3D+SCREEN+TUNER; LGE; GLOBAL-PLAT4; 03.09.22; 0x00000001;); LG NetCast.TV-2012
             *      Mozilla/5.0 (DirectFB; Linux armv7l) AppleWebKit/534.26+ (KHTML, like Gecko) Version/5.0 Safari/534.26+ LG Browser/5.00.00(+SCREEN+TUNER; LGE; GLOBAL-PLAT4; 01.00.00; 0x00000001;); LG NetCast.TV-2012
             *      Mozilla/5.0 (DirectFB; U; Linux armv6l; en) AppleWebKit/531.2  (KHTML, like Gecko) Safari/531.2  LG Browser/4.1.4( BDP; LGE; Media/BD660; 6970; abc;); LG NetCast.Media-2011
             *      Mozilla/5.0 (DirectFB; U; Linux 7631; en) AppleWebKit/531.2  (KHTML, like Gecko) Safari/531.2  LG Browser/4.1.4( NO_NUM; LGE; Media/SP520; ST.3.97.409.F; 0x00000001;); LG NetCast.Media-2011
             *      Mozilla/5.0 (DirectFB; U; Linux 7630; en) AppleWebKit/531.2  (KHTML, like Gecko) Safari/531.2  LG Browser/4.1.4( 3D BDP NO_NUM; LGE; Media/ST600; LG NetCast.Media-2011
             *      (LGSmartTV/1.0) AppleWebKit/534.23 OBIGO-T10/2.0
             */
            CheckUserAgent(userAgent, @"LG NetCast\.(?:TV|Media)-([0-9]*)", mc2 =>
            {
                Os.Name = "";
                Device.Manufacturer = GloableConfigurations.StringsLg;
                Device.Name = "NetCast TV " + mc2[1].SafeString();
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            });
            CheckUserAgent(userAgent, @"LGSmartTV", mc2 =>
            {
                Os.Name = "";
                Device.Manufacturer = GloableConfigurations.StringsLg;
                Device.Name = "Smart TV";
                Device.DeviceType = DeviceType.Television;
                ;
                Device.Identified = true;
            });

            /****************************************************
            *      Toshiba Smart TV
            *
            *      Mozilla/5.0 (Linux mipsel; U; HbbTV/1.1.1 (; TOSHIBA; DTV_RL953; 56.7.66.7; t12; ) ; ToshibaTP/1.3.0 (+VIDEO_MP4+VIDEO_X_MS_ASF+AUDIO_MPEG+AUDIO_MP4+DRM+NATIVELAUNCH) ; en) AppleWebKit/534.1 (KHTML, like Gecko)
            *      Mozilla/5.0 (DTV; TSBNetTV/T32013713.0203.7DD; TVwithVideoPlayer; like Gecko) NetFront/4.1 DTVNetBrowser/2.2 (000039;T32013713;0203;7DD) InettvBrowser/2.2 (000039;T32013713;0203;7DD)
            *      Mozilla/5.0 (Linux mipsel; U; HbbTV/1.1.1 (; TOSHIBA; 40PX200; 0.7.3.0.; t12; ) ; Toshiba_TP/1.3.0 (+VIDEO_MP4+AUDIO_MPEG+AUDIO_MP4+VIDEO_X_MS_ASF+OFFLINEAPP) ; en) AppleWebKit/534.1 (KHTML, like Gec
            */
            if (CheckUserAgent(userAgent, @"Toshiba_?TP\\") || CheckUserAgent(userAgent, @"TSBNetTV\\"))
            {
                Os.Name = "";
                Device.Manufacturer = "Toshiba";
                Device.Name = "Smart TV";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            }

            /* MachBlue XT */
            CheckUserAgent(userAgent, @"mbxtWebKit\/([0-9.]*)", mc2 =>
            {
                Os.Name = "";
                Device.Name = "MachBlue XT";
                Browser.Version = GetVersion(mc2);
                Browser.Detail = "2";
                Device.DeviceType = DeviceType.Television;
            });

            /* ADB */
            CheckUserAgent(userAgent, @"mbxtWebKit\/([0-9.]*)", mc2 =>
            {
                Os.Name = "";
                Device.Manufacturer = "ADB";
                Device.Name =
                    (mc2[1].SafeString() != "Unknown" ? mc2[1].SafeString().ReplaceRegex("ADB", "") + " " : "") +
                    "IPTV receiver";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            });

            /* MStar */
            if (CheckUserAgent(userAgent, "Mstar;OWB"))
            {
                Os.Name = "";
                Device.Manufacturer = "MStar";
                Device.Name = "PVR";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;

                Browser.Name = "Origyn Web Browser";
            }

            /* TechniSat */
            CheckUserAgent(userAgent, @"\\TechniSat ([^;]+);", mc2 =>
            {
                Os.Name = "";
                Device.Manufacturer = "TechniSat";
                Device.Name = mc2[1].SafeString();
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            });

            /* Technicolor */
            CheckUserAgent(userAgent, @"\\Technicolor_([^;]+);", mc2 =>
            {
                Os.Name = "";
                Device.Manufacturer = "Technicolor";
                Device.Name = mc2[1].SafeString();
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            });

            /* Winbox Evo2 */
            CheckUserAgent(userAgent, @"\\Technicolor_([^;]+);", mc2 =>
            {
                Os.Name = "";
                Device.Manufacturer = "Winbox";
                Device.Name = "Evo2";
                Device.DeviceType = DeviceType.Television;
                Device.Identified = true;
            });

            /* Roku */
            CheckUserAgent(userAgent, @"^Roku\/DVP-([0-9]+)", mc2 =>
            {
                Device.Manufacturer = "Roku";
                Device.DeviceType = DeviceType.Television;
                switch (mc2[1].SafeString())
                {
                    case "2000":
                        Device.Name = "HD";
                        break;
                    case "2050":
                        Device.Name = "XD";
                        break;
                    case "2100":
                        Device.Name = "XDS";
                        break;
                    case "2400":
                        Device.Name = "LT";
                        break;
                    case "3000":
                        Device.Name = "2 HD";
                        break;
                    case "3050":
                        Device.Name = "2 XD";
                        break;
                    case "3100":
                        Device.Name = "2 XS";
                        break;
                }
            });
            CheckUserAgent(userAgent, @"HbbTV\/1.1.1 \([^;]*;\s*([^;]*)\s*;\s*([^;]*)\s*;", mc2 =>
            {
                var vendorName = mc2[1].SafeString().Trim();
                var modelName = mc2[2].SafeString().Trim();

                if (string.IsNullOrEmpty(Device.Manufacturer) && vendorName != "" &&
                    vendorName != "vendorName")
                {
                    switch (vendorName)
                    {
                        case "LGE":
                            Device.Manufacturer = "LG";
                            break;
                        case "TOSHIBA":
                            Device.Manufacturer = "Toshiba";
                            break;
                        case "smart":
                            Device.Manufacturer = "Smart";
                            break;
                        case "tv2n":
                            Device.Manufacturer = "TV2N";
                            break;
                        default:
                            Device.Manufacturer = vendorName;
                            break;
                    }

                    if (string.IsNullOrEmpty(Device.Name) && modelName != "" && modelName != "modelName")
                    {
                        switch (modelName)
                        {
                            case "GLOBAL_PLAT3":
                                Device.Name = "NetCast TV";
                                break;
                            case "SmartTV2012":
                                Device.Name = "Smart TV 2012";
                                break;
                            case "videoweb":
                                Device.Name = "Videoweb";
                                break;
                            default:
                                Device.Name = modelName;
                                break;
                        }

                        if (vendorName == "Humax")
                        {
                            Device.Name = Device.Name.ToUppers();
                        }

                        Device.Identified = true;
                        Os.Name = "";
                    }
                }

                Device.DeviceType = DeviceType.Television;
            });

            /****************************************************
             *      Detect type based on common identifiers
             */
            if (CheckUserAgent(userAgent, @"InettvBrowser"))
            {
                Device.DeviceType = DeviceType.Television;
            }

            if (CheckUserAgent(userAgent, @"MIDP"))
            {
                Device.DeviceType = DeviceType.Mobile;
            }

            /****************************************************
             *      Try to detect any devices based on common
             *      locations of model ids
             */
            if (string.IsNullOrEmpty(Device.Name) && string.IsNullOrEmpty(Device.Manufacturer))
            {
                var candidates = new List<string>();
                if (CheckUserAgent(userAgent, @"^(Mozilla|Opera)"))
                {
                    CheckUserAgent(userAgent, @"^(?:MQQBrowser\/[0-9\.]+\/)?([^\s]+)", mc2 =>
                    {
                        var temp = mc2[0].SafeString();
                        temp = temp.ReplaceRegex(@"_TD$", "");
                        temp = temp.SafeString().ReplaceRegex("_CMCC$", "");
                        temp = temp.SafeString().ReplaceRegex(@"[_ ]Mozilla$", "");
                        temp = temp.SafeString().ReplaceRegex(@" Linux$", "");
                        temp = temp.SafeString().ReplaceRegex(@" Opera$", "");
                        temp = temp.SafeString().ReplaceRegex(@"\/[0-9].*$", "");

                        candidates.Add(temp);
                    });
                }


                CheckUserAgent(userAgent,
                    new[]
                    {
                        @"[0-9]+x[0-9]+; ([^;]+)", @"[0-9]+X[0-9]+ ([^;\/\(\)]+)",
                        @"Windows NT 5.1; ([^;]+); Windows Phone", @"\) PPC; (?:[0-9]+x[0-9]+; )?([^;\/\(\)]+)",
                        @"\(([^;]+); U; Windows Mobile", @"Vodafone\/1.0\/([^\/]+)", @"\ ([^\s]+)$"
                    },
                    (mc2, regex) => { candidates.Add(mc2[0].SafeString()); });

                for (var i = 0; i < candidates.Count; i++)
                {
                    var result = false;
                    if (string.IsNullOrEmpty(Device.Name) && string.IsNullOrEmpty(Device.Manufacturer))
                    {
                        var model = CleanupModel(candidates[i]);


                        if (Os.Name == "Android")
                        {
                            if (GloableConfigurations.AndroidModels.Any(x => x.Key == model))
                            {
                                var deviceModel = GloableConfigurations.AndroidModels.Where(x => x.Key == model)
                                    .Select(x => x.Value).FirstOrDefault();

                                if (deviceModel.Length > 0)
                                {
                                    Device.Manufacturer = deviceModel[0];
                                    Device.Name = deviceModel[1];

                                    if (!string.IsNullOrEmpty(deviceModel[2]))
                                    {
                                        Device.DeviceType = DeviceType.GetAll<DeviceType>()
                                            .FirstOrDefault(x => x.Extend == deviceModel[2]);
                                    }
                                }

                                Device.Identified = true;

                                result = true;
                            }
                        }

                        if (string.IsNullOrEmpty(Os.Name) || Os.Name == "Windows" ||
                            Os.Name == "Windows Mobile" || Os.Name == "Windows CE")
                        {
                            var deviceModel = GloableConfigurations.WindowsMobileModels
                                .Where(x => x.Key == model).Select(x => x.Value).FirstOrDefault();

                            if (deviceModel != null && deviceModel.Length > 0)
                            {
                                Device.Manufacturer = deviceModel[0];
                                Device.Name = deviceModel[1];
                                Device.DeviceType = DeviceType.Mobile;
                                Device.Identified = true;

                                if (Os.Name != "Windows Mobile")
                                {
                                    Os.Name = "Windows Mobile";
                                    Os.Version = null;
                                }

                                result = true;
                            }
                        }
                    }

                    if (!result)
                    {
                        CheckUserAgent(userAgent, @"^GIONEE-([^\s]+)", mc2 =>
                        {
                            Device.Manufacturer = "Gionee";
                            Device.Name = CleanupModel(mc2[1].SafeString());
                            Device.DeviceType = DeviceType.Mobile;
                            Device.Identified = true;
                        });

                        CheckUserAgent(userAgent, @"^HTC_?([^\/_]+)(?:\/|_|$)", mc2 =>
                        {
                            Device.Manufacturer = GloableConfigurations.StringsHtc;
                            Device.Name = CleanupModel(mc2[1].SafeString());
                            Device.DeviceType = DeviceType.Mobile;
                            Device.Identified = true;
                        });

                        CheckUserAgent(userAgent, @"^HUAWEI-([^\/]*)", mc2 =>
                        {
                            Device.Manufacturer = GloableConfigurations.StringsHuawei;
                            Device.Name = CleanupModel(mc2[1].SafeString());
                            Device.DeviceType = DeviceType.Mobile;
                            Device.Identified = true;
                        });

                        CheckUserAgent(userAgent, @"(?:^|\()LGE?(?:\/|-|_|\s)([^\s]*)", mc2 =>
                        {
                            Device.Manufacturer = GloableConfigurations.StringsLg;
                            Device.Name = CleanupModel(mc2[1].SafeString());
                            Device.DeviceType = DeviceType.Mobile;
                            Device.Identified = true;
                        });

                        CheckUserAgent(userAgent, @"^MOT-([^\/_]+)(?:\/|_|$)", mc2 =>
                        {
                            Device.Manufacturer = GloableConfigurations.StringsMotorola;
                            Device.Name = CleanupModel(mc2[1].SafeString());
                            Device.DeviceType = DeviceType.Mobile;
                            Device.Identified = true;
                        });

                        CheckUserAgent(userAgent, @"^Motorola_([^\/_]+)(?:\/|_|$)", mc2 =>
                        {
                            Device.Manufacturer = GloableConfigurations.StringsMotorola;
                            Device.Name = CleanupModel(mc2[1].SafeString());
                            Device.DeviceType = DeviceType.Mobile;
                            Device.Identified = true;
                        });

                        CheckUserAgent(userAgent, @"^Nokia([^\/]+)(?:\/|$)", mc2 =>
                        {
                            Device.Manufacturer = "Nokia";
                            Device.Name = CleanupModel(mc2[1].SafeString());
                            Device.DeviceType = DeviceType.Mobile;
                            Device.Identified = true;

                            if (string.IsNullOrEmpty(Os.Name))
                            {
                                Os.Name = "Series40";
                            }
                        });
                        CheckUserAgent(userAgent, @"^SonyEricsson([^\/_]+)(?:\/|_|$)", mc2 =>
                        {
                            Device.Manufacturer = GloableConfigurations.StringsSonyEricsson;
                            Device.Name = CleanupModel(mc2[1].SafeString());
                            Device.DeviceType = DeviceType.Mobile;
                            Device.Identified = true;
                        });
                    }
                }
            }

            CheckUserAgent(userAgent, @"\((?:LG[-|\/])(.*) (?:Browser\/)?AppleWebkit", mc2 =>
            {
                Device.Manufacturer = GloableConfigurations.StringsLg;
                Device.Name = mc2[1].SafeString();
                Device.DeviceType = DeviceType.Mobile;
                Device.Identified = true;
            });

            CheckUserAgent(userAgent,
                @"^Mozilla\/5.0 \((?:Nokia|NOKIA)(?:\s?)([^\)]+)\)UC AppleWebkit\(like Gecko\) Safari\/530$",
                mc2 =>
                {
                    Device.Manufacturer = "Nokia";
                    Device.Name = mc2[1].SafeString();
                    Device.DeviceType = DeviceType.Mobile;
                    Device.Identified = true;
                    Os.Name = "Series60";
                });

            /****************************************************
             *      Safari
             */
            if (CheckUserAgent(userAgent, @"Safari"))
            {
                if (Os.Name == "iOS")
                {
                    Browser.Stock = true;
                    Browser.Hidden = true;
                    Browser.Name = "Safari";
                    Browser.Version = null;
                }


                if (Os.Name == "Mac OS X" || Os.Name == "Windows")
                {
                    Browser.Name = "Safari";
                    Browser.Stock = Os.Name == "Mac OS X";

                    CheckUserAgent(userAgent, @"Version\/([0-9\.]+)",
                        mc2 => { Browser.Version = GetVersion(mc2); });

                    if (CheckUserAgent(userAgent, @"AppleWebKit\/[0-9\.]+\+"))
                    {
                        Browser.Name = "WebKit Nightly Build";
                        Browser.Version = null;
                    }
                }
            }


            /****************************************************
             *      Internet Explorer
             */
            if (CheckUserAgent(userAgent, @"MSIE"))
            {
                Browser.Name = "Internet Explorer";

                if (CheckUserAgent(userAgent, @"IEMobile") || CheckUserAgent(userAgent, @"Windows CE") ||
                    CheckUserAgent(userAgent, @"Windows Phone") || CheckUserAgent(userAgent, @"WP7"))
                {
                    Browser.Name = "Mobile Internet Explorer";
                }

                CheckUserAgent(userAgent, @"MSIE ([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /****************************************************
             *      Opera
             */

            if (CheckUserAgent(userAgent, @"Opera", RegexOptions.IgnoreCase))
            {
                Browser.Stock = false;
                Browser.Name = "Opera";
                CheckUserAgent(userAgent, @"Opera[\/| ]([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
                CheckUserAgent(userAgent, @"Version\/([0-9.]*)", mc2 =>
                {
                    if (GetVersion(mc2) >= "10")
                    {
                        Browser.Version = GetVersion(mc2);
                    }
                    else
                    {
                        Browser.Version = null;
                    }
                });

                if (Browser.Version != null && CheckUserAgent(userAgent, "Edition Labs"))
                {
                    Browser.VersionType = "alpha";
                    Browser.Channel = "Labs";
                }

                if (Browser.Version != null && CheckUserAgent(userAgent, "Edition Next"))
                {
                    Browser.VersionType = "alpha";
                    Browser.Channel = "Next";
                }

                if (CheckUserAgent(userAgent, "Opera Tablet"))
                {
                    Browser.VersionType = "Opera Mobile";
                    Device.DeviceType = DeviceType.Tablet;
                }

                if (CheckUserAgent(userAgent, "Opera Mobi"))
                {
                    Browser.VersionType = "Opera Mobile";
                    Device.DeviceType = DeviceType.Mobile;
                }

                CheckUserAgent(userAgent, "Opera Mini;", mc2 =>
                {
                    Browser.Name = "Opera Mini";
                    Browser.Version = null;
                    Browser.Mode = "proxy";
                    Device.DeviceType = DeviceType.Mobile;
                });

                CheckUserAgent(userAgent, @"Opera Mini\/(?:att\/)?([0-9.]*)", mc2 =>
                {
                    Browser.Name = "Opera Mini";
                    Browser.Version = GetVersion(mc2);
                    Browser.Detail = "-1";

                    Browser.Mode = "proxy";
                    Device.DeviceType = DeviceType.Mobile;
                });


                if (Browser.Name == "Opera" && Device.DeviceType.Id == DeviceType.Mobile.Id)
                {
                    Browser.Name = "Opera Mobile";
                    if (CheckUserAgent(userAgent, @"BER"))
                    {
                        Browser.Name = "Opera Mini";
                        Browser.Version = null;
                    }
                }


                if (CheckUserAgent(userAgent, "InettvBrowser"))
                {
                    Device.DeviceType = DeviceType.Television;
                }

                if (CheckUserAgent(userAgent, "Opera TV") || CheckUserAgent(userAgent, "'Opera-TV"))
                {
                    Browser.Name = "Opera";
                    Device.DeviceType = DeviceType.Television;
                }

                if (CheckUserAgent(userAgent, "Linux zbov"))
                {
                    Browser.Name = "Opera Mobile";
                    Browser.Mode = "desktop";

                    Device.DeviceType = DeviceType.Mobile;

                    Os.Name = null;
                    Os.Version = null;
                }

                if (CheckUserAgent(userAgent, "Linux zvav"))
                {
                    Browser.Name = "Mini";
                    Browser.Mode = "desktop";
                    Browser.Version = null;

                    Device.DeviceType = DeviceType.Mobile;

                    Os.Name = null;
                    Os.Version = null;
                }
            }

            /****************************************************
             *      Firefox
             */
            if (CheckUserAgent(userAgent, "Firefox"))
            {
                Browser.Stock = false;
                Browser.Name = "Firefox";

                CheckUserAgent(userAgent, @"Firefox\/([0-9ab.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });

                if (Browser.VersionType == "alpha")
                {
                    Browser.Channel = "Aurora";
                }

                if (Browser.VersionType == "beta")
                {
                    Browser.Channel = "Beta";
                }

                if (CheckUserAgent(userAgent, @"Fennec"))
                {
                    Device.DeviceType = DeviceType.Mobile;
                }

                if (CheckUserAgent(userAgent, @"Mobile; rv"))
                {
                    Device.DeviceType = DeviceType.Mobile;
                }

                if (CheckUserAgent(userAgent, @"Tablet; rv"))
                {
                    Device.DeviceType = DeviceType.Tablet;
                }

                if (Device.DeviceType == DeviceType.Mobile || Device.DeviceType == DeviceType.Tablet)
                {
                    Browser.Name = "Firefox Mobile";
                }
            }

            if (CheckUserAgent(userAgent, @"Namoroka"))
            {
                Browser.Stock = false;
                Browser.Name = "Firefox";

                CheckUserAgent(userAgent, @"Namoroka\/([0-9ab.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });

                Browser.Channel = "Namoroka";
            }

            if (CheckUserAgent(userAgent, @"Shiretoko"))
            {
                Browser.Stock = false;
                Browser.Name = "Firefox";
                CheckUserAgent(userAgent, @"Shiretoko\/([0-9ab.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
                Browser.Channel = "Shiretoko";
            }

            if (CheckUserAgent(userAgent, @"Minefield"))
            {
                Browser.Stock = false;
                Browser.Name = "Firefox";

                CheckUserAgent(userAgent, @"Minefield\/([0-9ab.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });

                Browser.Channel = "Minefield";
            }

            if (CheckUserAgent(userAgent, @"Firebird"))
            {
                Browser.Stock = false;
                Browser.Name = "Firebird";

                CheckUserAgent(userAgent, @"Firebird\/([0-9ab.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /* SeaMonkey */
            if (CheckUserAgent(userAgent, "SeaMonkey"))
            {
                Browser.Stock = false;
                Browser.Name = "SeaMonkey";
                CheckUserAgent(userAgent, @"SeaMonkey\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /* Netscape */
            if (CheckUserAgent(userAgent, "Netscape"))
            {
                Browser.Stock = false;
                Browser.Name = "Netscape";
                CheckUserAgent(userAgent, @"Netscape[0-9]?\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /* Konqueror */
            if (CheckUserAgent(userAgent, "[k|K]onqueror"))
            {
                Browser.Name = "Konqueror";
                CheckUserAgent(userAgent, @"[k|K]onqueror\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /* Chrome */
            CheckUserAgent(userAgent, @"(?:Chrome|CrMo|CriOS)\/([0-9.]*)", mc2 =>
            {
                Browser.Stock = false;
                Browser.Name = "Chrome";
                Browser.Version = GetVersion(mc2);

                if (Os.Name == "Android")
                {
                    switch (Browser.Version.ToString().ConvertStrToList<string>().Take(3).ConvertListToString('.'))
                    {
                        case "16.0.912":
                            Browser.Channel = "Beta";
                            break;
                        case "18.0.1025":
                            Browser.Detail = "1";
                            break;
                        default:
                            Browser.Channel = "Nightly";
                            break;
                    }
                }
                else
                {
                    switch (Browser.Version.ToString().ConvertStrToList<string>().Take(3).ConvertListToString("."))
                    {
                        case "0.2.149":
                        case "0.3.154":
                        case "0.4.154":
                        case "1.0.154":
                        case "2.0.172":
                        case "3.0.195":
                        case "4.0.249":
                        case "4.1.249":
                        case "5.0.375":
                        case "6.0.472":
                        case "7.0.517":
                        case "8.0.552":
                        case "9.0.597":
                        case "10.0.648":
                        case "11.0.696":
                        case "12.0.742":
                        case "13.0.782":
                        case "14.0.835":
                        case "15.0.874":
                        case "16.0.912":
                        case "17.0.963":
                        case "18.0.1025":
                        case "19.0.1084":
                        case "20.0.1132":
                        case "21.0.1180":
                            if (Browser.Version.Minor == 0)
                            {
                                Browser.Detail = "1";
                            }
                            else
                            {
                                Browser.Detail = "2";
                            }

                            break;
                        default:
                            Browser.Channel = "Nightly";
                            break;
                    }
                }
            });

            /****************************************************
             *      Chrome Frame
             */
            if (CheckUserAgent(userAgent, "chromeframe"))
            {
                Browser.Stock = false;
                Browser.Name = "Chrome Frame";

                CheckUserAgent(userAgent, @"Chromium\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /****************************************************
             *      BrowserNG
             */
            if (CheckUserAgent(userAgent, "BrowserNG"))
            {
                Browser.Name = "Nokia Browser";
                CheckUserAgent(userAgent, @"NokiaBrowser\/([0-9.]*)", mc2 =>
                {
                    Browser.Version = GetVersion(mc2);
                    Browser.Detail = "3";
                    Browser.Builds = false;
                });
            }

            /****************************************************
             *      Nokia Browser
             */
            if (CheckUserAgent(userAgent, "NokiaBrowser"))
            {
                Browser.Name = "Nokia Browser";

                CheckUserAgent(userAgent, @"NokiaBrowser\/([0-9.]*)", mc2 =>
                {
                    Browser.Version = GetVersion(mc2);
                    Browser.Detail = "3";
                });
            }

            /****************************************************
              *      MicroB
              */
            if (CheckUserAgent(userAgent, "Maemo[ |_]Browser"))
            {
                Browser.Name = "MicroB";

                CheckUserAgent(userAgent, @"Maemo[ |_]Browser[ |_]([0-9.]*)", mc2 =>
                {
                    Browser.Version = GetVersion(mc2);
                    Browser.Detail = "3";
                });
            }

            /****************************************************
             *      NetFront
             */
            if (CheckUserAgent(userAgent, "NetFront"))
            {
                Browser.Name = "NetFront";
                Device.DeviceType = DeviceType.Mobile;

                CheckUserAgent(userAgent, @"NetFront\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
                if (CheckUserAgent(userAgent, "InettvBrowser"))
                {
                    Device.DeviceType = DeviceType.Television;
                }
            }

            /****************************************************
             *      Silk
             */
            if (CheckUserAgent(userAgent, "Silk"))
            {
                if (CheckUserAgent(userAgent, "Silk-Accelerated"))
                {
                    Browser.Name = "Silk";
                    CheckUserAgent(userAgent, @"Silk\/([0-9.]*)",
                        mc2 =>
                        {
                            Browser.Version = GetVersion(mc2);
                            Browser.Detail = "2";
                        });

                    Device.Manufacturer = "Amazon";
                    Device.Name = "Kindle Fire";
                    Device.DeviceType = DeviceType.Tablet;
                    Device.Identified = true;

                    if (Os.Name != "Android")
                    {
                        Os.Name = "Android";
                        Os.Version = null;
                    }
                }
            }

            /****************************************************
             *      Dolfin
             */
            if (CheckUserAgent(userAgent, "Dolfin"))
            {
                Browser.Name = "Dolfin";

                CheckUserAgent(userAgent, @"Dolfin\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /****************************************************
             *      Iris
             */
            if (CheckUserAgent(userAgent, "Iris"))
            {
                Browser.Name = "Iris";
                Device.DeviceType = DeviceType.Mobile;
                Device.Name = null;
                Device.Manufacturer = null;

                Os.Name = "Windows Mobile";
                Os.Version = null;
                CheckUserAgent(userAgent, @"Iris\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });

                if (CheckUserAgent(userAgent, @" WM([0-9]) ", out MatchCollection mc3))
                {
                    Os.Version = new Versions(GetVersionResult(mc3) + ".0");
                }
                else
                {
                    Browser.Name = "desktop";
                }
            }

            /****************************************************
             *      Jasmine
             */
            if (CheckUserAgent(userAgent, "Jasmine"))
            {
                Browser.Name = "Jasmine";
                CheckUserAgent(userAgent, @"Jasmine\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /****************************************************
             *      Boxee
             */
            if (CheckUserAgent(userAgent, "Boxee"))
            {
                Browser.Name = "Boxee";
                Device.DeviceType = DeviceType.Television;
                CheckUserAgent(userAgent, @"Boxee\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /****************************************************
             *      Espial
             */
            if (CheckUserAgent(userAgent, "Espial"))
            {
                Browser.Name = "Espial";

                Os.Name = "";
                Os.Version = null;

                if (Device.DeviceType.Id != DeviceType.Television.Id)
                {
                    Device.DeviceType = DeviceType.Television;
                    Device.Name = null;
                    Device.Manufacturer = null;
                }

                CheckUserAgent(userAgent, @"Jasmine\/([0-9.]*)",
                    mc2 => { Browser.Version = GetVersion(mc2); });
            }

            /****************************************************
             *      ANT Galio
             */
            CheckUserAgent(userAgent, @"ANTGalio\/([0-9.]*)",
                mc2 =>
                {
                    Browser.Name = "ANT Galio";
                    Browser.Version = GetVersion(mc2);
                    Browser.Detail = "3";
                    Device.DeviceType = DeviceType.Television;
                });

            /****************************************************
             *      NetFront NX
             */
            CheckUserAgent(userAgent, @"NX\/([0-9.]*)",
                mc2 =>
                {
                    Browser.Name = "NetFront NX";
                    Browser.Version = GetVersion(mc2);
                    Browser.Detail = "2";
                    if (CheckUserAgent(userAgent, @"NX\/([0-9.]*)"))
                    {
                        Device.DeviceType = DeviceType.Television;
                    }
                    else if (CheckUserAgent(userAgent, @"mobile", RegexOptions.IgnoreCase))
                    {
                        Device.DeviceType = DeviceType.Mobile;
                    }
                    else
                    {
                        Device.DeviceType = DeviceType.Pc;
                    }

                    Os.Name = null;
                    Os.Version = null;
                });

            /****************************************************
             *      Obigo
             */
            if (CheckUserAgent(userAgent, "Obigo", RegexOptions.IgnoreCase))
            {
                Browser.Name = "Obigo";
                CheckUserAgent(userAgent, @"Obigo\/([0-9.]*)", RegexOptions.IgnoreCase,
                    mc2 => { Browser.Version = GetVersion(mc2); });

                CheckUserAgent(userAgent, @"Obigo\/([A-Z])([0-9.]*)", RegexOptions.IgnoreCase,
                    mc2 =>
                    {
                        Browser.Name = "Obigo" + mc2[1].SafeString();
                        Browser.Version = GetVersion(mc2);
                    });

                CheckUserAgent(userAgent, @"Obigo-([A-Z])([0-9.]*)\\", RegexOptions.IgnoreCase,
                    mc2 =>
                    {
                        Browser.Name = "Obigo" + mc2[1].SafeString();
                        Browser.Version = GetVersion(mc2);
                    });
            }

            /****************************************************
             *      UC Web
             */
            if (CheckUserAgent(userAgent, "UCWEB"))
            {
                Browser.Stock = false;
                Browser.Name = "UC Browser";

                CheckUserAgent(userAgent, @"UCWEB([0-9]*[.][0-9]*)",
                    mc2 =>
                    {
                        Browser.Version = GetVersion(mc2);
                        Browser.Detail = "3";
                    });

                if (Os.Name == "Linux")
                {
                    Os.Name = "";
                }

                Device.DeviceType = DeviceType.Mobile;
                CheckUserAgent(userAgent, @"^IUC \(U;\s?iOS ([0-9\.]+);",
                    mc2 =>
                    {
                        Os.Name = "iOS";
                        Browser.Version = GetVersion(mc2);
                    });
                CheckUserAgent(userAgent,
                    @"^JUC \(Linux; U; ([0-9\.]+)[^;]*; [^;]+; ([^;]*[^\s])\s*; [0-9]+\*[0-9]+\)",
                    mc2 =>
                    {
                        var model = CleanupModel(mc2[2].SafeString());

                        Os.Name = "Android";
                        Os.Version = GetVersion(mc2);

                        var androidInfo = GloableConfigurations.AndroidModels.Where(x => x.Key == model)
                            .Select(x => x.Value).FirstOrDefault();
                        if (androidInfo != null && androidInfo.Length > 0)
                        {
                            Device.Manufacturer = androidInfo[0];
                            Device.Name = androidInfo[1];

                            if (androidInfo.Length >= 3)
                            {
                                Device.DeviceType = DeviceType.GetAll<DeviceType>()
                                    .FirstOrDefault(x => x.Extend == androidInfo[2]);
                            }

                            Device.Identified = true;
                        }
                    });
            }

            if (CheckUserAgent(userAgent, @"\) UC "))
            {
                Browser.Stock = false;
                Browser.Name = "UC Browser";
            }

            CheckUserAgent(userAgent, @"UCBrowser\/([0-9.]*)", mc2 =>
            {
                Browser.Stock = false;
                Browser.Name = "UC Browser";
                Browser.Version = GetVersion(mc2);
                Browser.Detail = "2";
            });

            /****************************************************
             *      NineSky
             */
            CheckUserAgent(userAgent, @"Ninesky(?:-android-mobile(?:-cn)?)?\/([0-9.]*)", mc2 =>
            {
                Browser.Name = "NineSky";
                Browser.Version = GetVersion(mc2);

                if (Os.Name != "Android")
                {
                    Os.Name = "Android";
                    Os.Version = null;

                    Device.Manufacturer = null;
                    Device.Name = null;
                }
            });

            /****************************************************
             *      Skyfire
             */
            CheckUserAgent(userAgent, @"Skyfire\/([0-9.]*)", mc2 =>
            {
                Browser.Name = "Skyfire";
                Browser.Version = GetVersion(mc2);

                Device.DeviceType = DeviceType.Mobile;
                Os.Name = "Android";
                Os.Version = null;
            });


            /****************************************************
             *      Dolphin HD
             */
            CheckUserAgent(userAgent, @"DolphinHDCN\/([0-9.]*)", mc2 =>
            {
                Browser.Name = "Dolphin";
                Browser.Version = GetVersion(mc2);

                Device.DeviceType = DeviceType.Mobile;

                if (Os.Name != "Android")
                {
                    Os.Name = "Android";
                    Os.Version = null;
                }
            });

            CheckUserAgent(userAgent, @"Dolphin\/INT", mc2 =>
            {
                Browser.Name = "Dolphin";
                Device.DeviceType = DeviceType.Mobile;
            });

            /****************************************************
              *      QQ Browser
              */
            CheckUserAgent(userAgent, @"(M?QQBrowser)\/([0-9.]*)", mc2 =>
            {
                Browser.Name = "QQ Browser";

                var version = mc2[2].SafeString();

                if (CheckUserAgent(version, @"^[0-9][0-9]$"))
                {
                    version = version[0] + "." + version[1];
                }

                Browser.Version = new Versions(version);
                Browser.Detail = "2";

                Browser.Channel = "";

                if (string.IsNullOrEmpty(Os.Name) && mc2[1].SafeString() == "QQBrowser")
                {
                    Os.Name = "Windows";
                }
            });

            /* iBrowser */
            CheckUserAgent(userAgent, @"(iBrowser)\/([0-9.]*)", mc2 =>
            {
                Browser.Name = "iBrowser";
                var version = mc2[2].SafeString();

                if (CheckUserAgent(version, @"[0-9][0-9]"))
                {
                    version = version[0] + "." + version[1];
                }

                Browser.Version = new Versions(version);
                Browser.Detail = "2";
                Browser.Channel = "";
            });

            /****************************************************
             *      Puffin
             */
            CheckUserAgent(userAgent, @"Puffin\/([0-9.]*)", mc2 =>
            {
                Browser.Name = "Puffin";
                Browser.Version = GetVersion(mc2);
                Browser.Detail = "2";

                Device.DeviceType = DeviceType.Mobile;
                if (Os.Name == "Linux")
                {
                    Os.Name = null;
                    Os.Version = null;
                }
            });

            /****************************************************
             *      360 Extreme Explorer
             */
            if (CheckUserAgent(userAgent, "360EE"))
            {
                Browser.Stock = false;
                Browser.Name = "360 Extreme Explorer";
                Browser.Version = null;
            }

            /****************************************************
             *      Midori
             */
            CheckUserAgent(userAgent, @"Midori\/([0-9.]*)", mc2 =>
            {
                Browser.Name = "Midori";
                Browser.Version = GetVersion(mc2);

                if (Os.Name != "Linux")
                {
                    Os.Name = "Linux";
                    Os.Version = null;
                }

                Device.Manufacturer = null;
                Device.Name = null;
                Device.DeviceType = DeviceType.Pc;
            });

            /****************************************************
             *      Others
             */

            for (var b = 0; b < this.BrowserRuleses.Count; b++)
            {
                CheckUserAgent(userAgent, this.BrowserRuleses[b].Regex, mc2 =>
                {
                    Browser.Name = this.BrowserRuleses[b].Name;
                    Browser.Channel = "";
                    Browser.Stock = false;

                    if (!string.IsNullOrEmpty(mc2[1].SafeString()))
                    {
                        Browser.Version = GetVersion(mc2);
                        Browser.Detail = this.BrowserRuleses[b].Details;
                    }
                    else
                    {
                        Browser.Version = null;
                    }
                });
            }

            /****************************************************
             *      WebKit
             */
            CheckUserAgent(userAgent, @"WebKit\/([0-9.]*)", RegexOptions.IgnoreCase, mc2 =>
            {
                Engine.Name = "Webkit";
                Engine.Version = GetVersion(mc2);
            });

            CheckUserAgent(userAgent, @"Browser\/AppleWebKit([0-9.]*)", RegexOptions.IgnoreCase, mc2 =>
            {
                Engine.Name = "Webkit";

                Engine.Version = GetVersion(mc2);
            });

            /* KHTML */
            CheckUserAgent(userAgent, @"KHTML\/([0-9.]*)", mc2 =>
            {
                Engine.Name = "KHTML";
                Engine.Version = GetVersion(mc2);
            });

            /****************************************************
             *      Gecko
             */
            if (CheckUserAgent(userAgent, @"Gecko") &&
                !CheckUserAgent(userAgent, @"like Gecko", RegexOptions.IgnoreCase))
            {
                Engine.Name = "Gecko";
                CheckUserAgent(userAgent, @"; rv:([^\)]+)\)",
                    mc2 => { Engine.Version = GetVersion(mc2); });
            }

            /****************************************************
             *      Presto
             */
            CheckUserAgent(userAgent, @"Presto\/([0-9.]*)", mc2 =>
            {
                Engine.Name = "Presto";
                Engine.Version = GetVersion(mc2);
            });

            /****************************************************
             *      Trident
             */
            CheckUserAgent(userAgent, @"Trident\/([0-9.]*)", mc2 =>
            {
                Engine.Name = "Trident";
                Engine.Version = GetVersion(mc2);

                if (Browser.Name == "Internet Explorer")
                {
                    if (ParseVersion(Engine.Version.ToString()) == 6 &&
                        (Browser.Version.ToString()).ConvertToDecimal(0) < 10)
                    {
                        Browser.Version = new Versions("10.0");
                        Browser.Mode = "compat";
                    }

                    if (ParseVersion(Engine.Version.ToString()) == 5 &&
                        (Browser.Version.ToString()).ConvertToDecimal(0) < 9)
                    {
                        Browser.Version = new Versions("9.0");
                        Browser.Mode = "compat";
                    }

                    if (ParseVersion(Engine.Version.ToString()) == 4 &&
                        (Browser.Version.ToString()).ConvertToDecimal(0) < 8)
                    {
                        Browser.Version = new Versions("8.0");
                        Browser.Mode = "compat";
                    }
                }

                if (Os.Name == "Windows Phone")
                {
                    if (ParseVersion(Engine.Version.ToString()) == 5 &&
                        Os.Version.ToString().ConvertToDecimal(0) < 7.5m)
                    {
                        Os.Version = new Versions("7.5");
                    }
                }
            });

            /****************************************************
             *      Corrections
             */
            if (Os.Name == "Android" && Browser.Stock)
            {
                Browser.Hidden = true;
            }

            if (Os.Name == "iOS" && Browser.Name == "Opera Mini")
            {
                Os.Version = null;
            }

            if (Browser.Name == "" && Engine.Name != "Webkit")
            {
                Engine.Name = "Webkit";
                Engine.Version = null;
            }

            if (Device.DeviceType.Id == DeviceType.Television.Id && Browser.Name == "Opera")
            {
                Browser.Name = "Opera Devices";

                List<KeyValuePair<string, string>> versionMaps = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("2.10", "3.2"),
                    new KeyValuePair<string, string>("2.9", "3.1"),
                    new KeyValuePair<string, string>("2.8", "3.0"),
                    new KeyValuePair<string, string>("2.7", "2.9"),
                    new KeyValuePair<string, string>("2.6", "2.8"),
                    new KeyValuePair<string, string>("2.4", "10.3"),
                    new KeyValuePair<string, string>("2.3", "10"),
                    new KeyValuePair<string, string>("2.2", "9.7"),
                    new KeyValuePair<string, string>("2.1", "9.6")
                };

                if (versionMaps.Any(x => x.Key == Engine.Version.ToString()))
                {
                    Browser.Version = new Versions(versionMaps
                        .Where(x => x.Key == Engine.Version.ToString()).Select(x => x.Value).FirstOrDefault());
                }
                else
                {
                    Browser.Version = null;
                }

                Os.Name = null;
                Os.Version = null;
            }

            /****************************************************
             *      Camouflage
             */
            if (DetectCamouflage)
            {
                CheckUserAgent(userAgent, @"Mac OS X 10_6_3; ([^;]+); [a-z]{2}-(?:[a-z]{2})?\)", mc2 =>
                {
                    Browser.Name = "";
                    Browser.Version = null;
                    Browser.Mode = "desktop";
                    Os.Name = "Android";
                    Os.Version = null;
                    Engine.Name = "Webkit";
                    Engine.Version = null;
                    Device.Name = mc2[1].SafeString();
                    Device.DeviceType = DeviceType.Mobile;

                    var model = CleanupModel(Device.Name);
                    var androidInfo = GloableConfigurations.AndroidModels.Where(x => x.Key == model)
                        .Select(x => x.Value).FirstOrDefault();

                    if (androidInfo != null && androidInfo.Length > 0)
                    {
                        Device.Manufacturer = androidInfo[0];
                        Device.Name = androidInfo[1];

                        if (androidInfo.Length > 2)
                        {
                            Device.DeviceType = DeviceType.GetAll<DeviceType>()
                                .FirstOrDefault(x => x.Extend == androidInfo[2]);
                            Device.Identified = true;
                        }
                    }
                });

                CheckUserAgent(userAgent, @"Linux Ventana; [a-z]{2}-[a-z]{2}; (.+) Build", mc2 =>
                {
                    Browser.Name = "";
                    Browser.Version = null;
                    Browser.Mode = "desktop";

                    Os.Name = "Android";
                    Os.Version = null;
                    Engine.Name = "Webkit";
                    Engine.Version = null;

                    Device.Name = mc2[1].SafeString();
                    Device.DeviceType = DeviceType.Mobile;

                    var model = CleanupModel(Device.Name);
                    var androidInfo = GloableConfigurations.AndroidModels.Where(x => x.Key == model)
                        .Select(x => x.Value).FirstOrDefault();

                    if (androidInfo != null && androidInfo.Length > 0)
                    {
                        Device.Manufacturer = androidInfo[0];
                        Device.Name = androidInfo[1];

                        if (androidInfo.Length > 2)
                        {
                            Device.DeviceType = DeviceType.GetAll<DeviceType>()
                                .FirstOrDefault(x => x.Extend == androidInfo[2]);
                            Device.Identified = true;
                        }
                    }
                });

                if (Browser.Name == "Safari")
                {
                    if (Os.Name != "iOS" &&
                        new Regex(@"AppleWebKit\/([0-9]+.[0-9]+)", RegexOptions.IgnoreCase).Matches(userAgent)[1] !=
                        new Regex(@"Safari\/([0-9]+.[0-9]+)", RegexOptions.IgnoreCase).Matches(userAgent)[1])
                    {
                        Features.Add("safariMismatch");
                        CamouFlage = true;
                    }

                    if (Os.Name == "iOS" && CheckUserAgent(userAgent, @"^Mozilla"))
                    {
                        Features.Add("noMozillaPrefix");
                        CamouFlage = true;
                    }

                    if (CheckUserAgent(userAgent, @"!/Version\/[0-9\.]+"))
                    {
                        Features.Add("noVersion");
                        CamouFlage = true;
                    }
                }

                if (Browser.Name == "Chrome")
                {
                    if (CheckUserAgent(userAgent,
                        @"!/(?:Chrome|CrMo|CriOS)\/([0-9]{1,2}\.[0-9]\.[0-9]{3,4}\.[0-9]+)"))
                    {
                        Features.Add("wrongVersion");
                        CamouFlage = true;
                    }
                }

                if (UseFeatures)
                {
                    /* If it claims not to be Trident, but it is probably Trident running camouflage mode */
                    if (IsSupportActiveXObject)
                    {
                        Features.Add("trident");

                        if (!string.IsNullOrEmpty(Engine.Name) && Engine.Name != "Trident")
                        {
                            CamouFlage = string.IsNullOrEmpty(Browser.Name) ||
                                         Browser.Name != "Maxthon";
                        }
                    }

                    if (IsOpera)
                    {
                        Features.Add("presto");

                        if (!string.IsNullOrEmpty(Engine.Name) && Engine.Name != "Presto")
                        {
                            CamouFlage = true;
                        }

                        if (Browser.Name == "Internet Explorer")
                        {
                            CamouFlage = true;
                        }
                    }

                    if (IsMozilla)
                    {
                        Features.Add("gecko");

                        if (!string.IsNullOrEmpty(Engine.Name) && Engine.Name != "Gecko")
                        {
                            CamouFlage = true;
                        }

                        if (Browser.Name == "Internet Explorer")
                        {
                            CamouFlage = true;
                        }
                    }

                    if (IsWebKit)
                    {
                        Features.Add("webkit");

                        if (!string.IsNullOrEmpty(Engine.Name) && Engine.Name != "Webkit")
                        {
                            CamouFlage = true;
                        }

                        if (Browser.Name == "Internet Explorer")
                        {
                            CamouFlage = true;
                        }
                    }

                    /* If it claims to be Safari and uses V8, it is probably an Android device running camouflage mode */
                    if (Engine.Name == "Webkit")
                    {
                        Features.Add("v8");

                        if (Browser != null && Browser.Name == "Safari")
                        {
                            CamouFlage = true;
                        }
                    }

                    /* If we have an iPad that is not 768 x 1024, we have an imposter */
                    if (Device.Name == "iPad")
                    {
                        if ((DeviceWidth != 0 && DeviceHeight != 0) &&
                            (DeviceWidth != 768 && DeviceHeight != 1024) &&
                            (DeviceWidth != 1024 && DeviceHeight != 768))
                        {
                            Features.Add("sizeMismatch");
                            CamouFlage = true;
                        }
                    }

                    /* If we have an iPhone or iPod that is not 320 x 480, we have an imposter */
                    if (Device.Name == "iPhone" || Device.Name == "iPod")
                    {
                        if ((DeviceWidth != 0 && DeviceHeight != 0) &&
                            (DeviceWidth != 320 && DeviceHeight != 480) &&
                            (DeviceWidth != 480 && DeviceHeight != 320))
                        {
                            Features.Add("sizeMismatch");
                            CamouFlage = true;
                        }
                    }

                    if (Os.Name == "iOS" && Os.Version != null)
                    {
                        if (Os.Version.ToString().ConvertToDecimal(0) < 4.0m)
                        {
                            Features.Add("foundSandbox");
                            CamouFlage = true;
                        }

                        if (Os.Version.ToString().ConvertToDecimal(0) < 4.2m)
                        {
                            Features.Add("foundSockets");
                            CamouFlage = true;
                        }

                        if (Os.Version.ToString().ConvertToDecimal(0) < 5.0m)
                        {
                            Features.Add("foundWorker");
                            CamouFlage = true;
                        }

                        if (Os.Version.ToString().ConvertToDecimal(0) > 2.1m)
                        {
                            Features.Add("noAppCache");
                            CamouFlage = true;
                        }
                    }

                    if (Os.Name != "iOS" && Os.Name == "Safari" && Browser.Version != null)
                    {
                        if (Os.Version.ToString().ConvertToDecimal(0) < 4.0m)
                        {
                            Features.Add("foundAppCache");
                            CamouFlage = true;
                        }

                        if (Os.Version.ToString().ConvertToDecimal(0) < 4.1m)
                        {
                            Features.Add("foundHistory");
                            CamouFlage = true;
                        }

                        if (Os.Version.ToString().ConvertToDecimal(0) < 5.1m)
                        {
                            Features.Add("foundHistory");
                            CamouFlage = true;
                        }

                        if (Browser.Version.ToString().ConvertToDecimal(0) < 5.2m)
                        {
                            Features.Add("foundHistory");
                            CamouFlage = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [JsonProperty(PropertyName = "browser",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Browser Browser { get; set; }

        /// <summary>
        /// 系统
        /// </summary>
        [JsonProperty(PropertyName = "os",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Os Os { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        [JsonProperty(PropertyName = "device",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Devices Device { get; set; }

        /// <summary>
        /// 是否伪装
        /// </summary>
        [JsonProperty(PropertyName = "camouflage",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool CamouFlage { get; set; }

        /// <summary>
        /// 引擎
        /// </summary>
        [JsonProperty(PropertyName = "engine",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Engine Engine { get; set; }

        /// <summary>
        /// 特性信息
        /// </summary>
        [JsonProperty(PropertyName = "features",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> Features { get; set; }

        /// <summary>
        /// 是否使用特性
        /// </summary>
        [JsonProperty(PropertyName = "use_features",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool UseFeatures { get; private set; }

        /// <summary>
        /// 是否支持ActiveX
        /// </summary>
        [JsonProperty(PropertyName = "issupport_activexobject",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsSupportActiveXObject { get; private set; }

        /// <summary>
        /// 是否Opera浏览器
        /// </summary>
        [JsonProperty(PropertyName = "isopera",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsOpera { get; internal set; }

        /// <summary>
        /// Gecko浏览器
        /// Mozilla
        /// </summary>
        /// <returns></returns>
        [JsonProperty(PropertyName = "ismozilla",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsMozilla { get; internal set; }

        /// <summary>
        /// If it claims not to be Webkit, but it is probably Webkit running camouflage mode
        /// </summary>
        /// <returns></returns>
        [JsonProperty(PropertyName = "is_webKit",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsWebKit { get; internal set; }

        /// <summary>
        /// 设备宽度
        /// </summary>
        [JsonProperty(PropertyName = "device_width",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int DeviceWidth { get; internal set; }

        /// <summary>
        /// 设备高度
        /// </summary>
        [JsonProperty(PropertyName = "device_height",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int DeviceHeight { get; internal set; }
    }
}
