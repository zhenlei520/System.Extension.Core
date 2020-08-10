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
                    CheckUserAgent(userAgent, @"/CentOS\/[0-9\.\-]+el([0-9_]+)/",
                        (mc3) => { this.Os.Version = mc3[1].SafeString().Replace("/_/", "").ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, "Fedora", (mc2) =>
                {
                    this.Os.Name = "Fedora";
                    CheckUserAgent(userAgent, @"/Fedora\/[0-9\.\-]+fc([0-9]+)/",
                        (mc3) => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, "Ubuntu", mc2 =>
                {
                    this.Os.Name = "Ubuntu";
                    CheckUserAgent(userAgent, @"/Ubuntu\/([0-9.]*)/",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, new[] { "Gentoo", "Kubuntu", "Debian", "Slackware", "SUSE", "Turbolinux" },
                    (mc2, regex) => { this.Os.Name = regex; });

                CheckUserAgent(userAgent, "Mandriva Linux", (mc2) =>
                {
                    this.Os.Name = "Mandriva";
                    CheckUserAgent(userAgent, @"/Mandriva Linux\/[0-9\.\-]+mdv([0-9]+)/",
                        (mc3) => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, "Red Hat",
                    (mc2) =>
                    {
                        this.Os.Name = "Red Hat";
                        CheckUserAgent(userAgent, @"/Red Hat[^\/]*\/[0-9\.\-]+el([0-9_]+)/",
                            (mc3) =>
                            {
                                this.Os.Version = mc3[1].SafeString().Replace("/_/g", ".").ConvertToDecimal(0);
                            });
                    });

                CheckUserAgent(userAgent, new[] { "iPhone( Simulator)?;", "iPad;", "iPod;", @"/iPhone\s*\d*s?[cp]?;/i" },
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
                                 CheckUserAgent(userAgent, @"/iPhone\s*\d*s?[cp]?;/i"))
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
                    CheckUserAgent(userAgent, @"/Mac OS X (10[0-9\._]*)/",
                        mc3 => { this.Os.Version = mc3[1].SafeString().Replace("/_/g", ".").ConvertToDecimal(0); });
                });

                CheckUserAgent(userAgent, "Windows", mc2 =>
                {
                    this.Os.Name = "Windows";
                    CheckUserAgent(userAgent, @"/Windows NT ([0-9]\.[0-9])/", mc3 =>
                    {
                        this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0);
                        this.Os.SetAlias();
                    });

                    CheckUserAgent(userAgent, new[] { "Windows 95", "Win95", "Win 9x 4.00" }, (mc3, regex) =>
                      {
                          this.Os.Version = 4.0m;
                          this.Os.Alias = "95";
                      });

                    CheckUserAgent(userAgent, new[] { "Windows 98", "Win98", "Win 9x 4.10" }, (mc3, regex) =>
                      {
                          this.Os.Version = 4.1m;
                          this.Os.Alias = "98";
                      });

                    CheckUserAgent(userAgent, new[] { "Windows ME", "WinME", "Win 9x 4.90" }, (mc3, regex) =>
                      {
                          this.Os.Version = 4.9m;
                          this.Os.Alias = "ME";
                      });

                    CheckUserAgent(userAgent, new[] { "Windows XP", "WinXP" }, (mc3, regex) =>
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
                        this.Browser.Mode = "desktop";
                    });

                    CheckUserAgent(userAgent, new[] { "Windows CE", "WinCE", "WindowsCE" }, (mc3, regex) =>
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
                              CheckUserAgent(userAgent, new[] { @"/WindowsCEOS\/([0-9.]*)/", "/Windows CE ([0-9.]*)/" },
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

                    CheckUserAgent(userAgent, @"/WindowsMobile\/([0-9.]*)/", mc3 =>
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

                        CheckUserAgent(userAgent, @"/IEMobile\/[^;]+; ([^;]+); ([^;]+)[;|\)]/", mc4 =>
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
                        @"/Android(?: )?(?:AllPhone_|CyanogenMod_)?(?:\/)?v?([0-9.]+)/", mc3 =>
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
                        @"/Eclair; (?:[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?) Build\/([^\/]*)\//", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent, @"/; ([^;]*[^;\s])\s+Build/", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent,
                        @"/[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?; ([^;]*[^;\s]);\s+Build/", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent, @"/\(([^;]+);U;Android\/[^;]+;[0-9]+\*[0-9]+;CTC\/2.0\)/",
                        out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent, @"/;\s?([^;]+);\s?[0-9]+\*[0-9]+;\s?CTC\/2.0/", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent, @"/zh-cn;\s*(.*?)(\/|build)/i", out mc4))
                    {
                        this.Device.Name = mc4[1].SafeString();
                    }
                    else if (CheckUserAgent(userAgent,
                        @"/Android [^;]+; (?:[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?; )?([^)]+)\)/", out mc4))
                    {
                        if (!CheckUserAgent(userAgent, "/[a-zA-Z][a-zA-Z](?:[-_][a-zA-Z][a-zA-Z])?/", out mc4))
                        {
                            this.Device.Name = mc4[1].SafeString();
                        }
                    }
                    else if (CheckUserAgent(userAgent, @"/^(.+?)\/\S+/i", out mc4))
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
                    CheckUserAgent(userAgent, @"Pre\/1.0", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pre";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, @"Pre\/1.1", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pre Plus";
                        this.Device.Identified = true;
                    });

                    CheckUserAgent(userAgent, @"Pre\/1.2", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pre 2";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, @"Pre\/3.0", mc3 =>
                    {
                        this.Device.Manufacturer = "HP";
                        this.Device.Name = "Pre 3";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, @"Pixi\/1.0", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pixi";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, @"Pixi\/1.1", mc3 =>
                    {
                        this.Device.Manufacturer = "Palm";
                        this.Device.Name = "Pixi Plus";
                        this.Device.Identified = true;
                    });
                    CheckUserAgent(userAgent, @"P160UN?A?\/1.0", mc3 =>
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
                    this.Device.DeviceType = DeviceType.Television;
                    CheckUserAgent(userAgent, "Chrome/5.", mc3 => { this.Os.Version = 2m; });
                    CheckUserAgent(userAgent, "Chrome/11.", mc3 => { this.Os.Version = 2m; });
                });

                /* WoPhone */
                CheckUserAgent(userAgent, "WoPhone", mc2 =>
                {
                    this.Os.Name = "WoPhone";
                    this.Device.DeviceType = DeviceType.Mobile;
                    CheckUserAgent(userAgent, @"/WoPhone\/([0-9\.]*)/",
                        mc3 => { this.Os.Version = mc3[1].ConvertToDecimal(0); });
                });

                /* BlackBerry */
                CheckUserAgent(userAgent, "BlackBerry", mc2 =>
                {
                    this.Os.Name = "BlackBerry OS";

                    if (!CheckUserAgent(userAgent, "Opera"))
                    {
                        MatchCollection mc3;
                        if (CheckUserAgent(userAgent, @"/BlackBerry([0-9]*)\/([0-9.]*)/", out mc3))
                        {
                            this.Device.Name = mc3[1].SafeString();
                            this.Os.Version = mc3[2].SafeString().ConvertToDecimal(0);
                            this.Os.Details = "2";
                        }

                        if (CheckUserAgent(userAgent, "/; BlackBerry ([0-9]*);/", out mc3))
                        {
                            this.Device.Name = mc3[1].SafeString();
                        }

                        if (CheckUserAgent(userAgent, @"/Version\/([0-9.]*)/", out mc3))
                        {
                            this.Os.Version = mc3[1].ConvertToDecimal(0);
                            this.Os.Details = "2";
                        }

                        if (this.Os.Version >= 10m)
                        {
                            this.Os.Name = "BlackBerry";
                        }

                        if (!string.IsNullOrEmpty(this.Device.Name))
                        {
                            var deviceInfo =
                                GloableConfigurations.BlackberryModels.FirstOrDefault(x => x.Key == this.Device.Name);
                            if (!string.IsNullOrEmpty(deviceInfo.Key))
                            {
                                this.Device.Name = "BlackBerry " + deviceInfo.Value + " " + this.Device.Name;
                            }
                            else
                            {
                                this.Device.Name = "BlackBerry " + this.Device.Name;
                            }
                        }
                        else
                        {
                            this.Device.Name = "BlackBerry";
                        }
                    }
                    else
                    {
                        this.Device.Name = "BlackBerry";
                    }

                    this.Device.Manufacturer = "RIM";
                    this.Device.DeviceType = DeviceType.Mobile;
                    this.Device.Identified = true;
                });

                /*  BlackBerry PlayBook  */
                if (CheckUserAgent(userAgent, "RIM Tablet OS"))
                {
                    this.Os.Name = "BlackBerry Tablet OS";
                    this.Os.Version = new Regex("/RIM Tablet OS ([0-9.]*)/").Matches(userAgent)[1].SafeString()
                        .ConvertToDecimal(0);
                    this.Os.Details = "2";
                    this.Device.Manufacturer = "RIM";
                    this.Device.Name = "BlackBerry PlayBook";
                    this.Device.DeviceType = DeviceType.Tablet;
                    this.Device.Identified = true;
                }
                else if (CheckUserAgent(userAgent, "PlayBook"))
                {
                    CheckUserAgent(userAgent, @"/Version\/(10[0-9.]*)/", mc2 =>
                    {
                        this.Os.Name = "BlackBerry";
                        this.Os.Version = mc2[1].ConvertToDecimal(0);
                        this.Os.Details = "2";

                        this.Device.Manufacturer = "RIM";
                        this.Device.Name = "BlackBerry PlayBook";
                        this.Device.DeviceType = DeviceType.Tablet;
                        this.Device.Identified = true;
                    });
                }

                /* WebOS */
                CheckUserAgent(userAgent, "(?:web|hpw)OS", mc2 =>
                {
                    this.Os.Name = "webOS";
                    this.Os.Version = new Regex(@"/(?:web|hpw)OS\/([0-9.]*)/").Matches(userAgent)[1].SafeString()
                        .ConvertToDecimal(0);

                    if (CheckUserAgent(userAgent, "tablet"))
                    {
                        this.Device.DeviceType = DeviceType.Tablet;
                    }

                    this.Device.Manufacturer = CheckUserAgent(userAgent, "hpwOS") ? "HP" : "Palm";
                    if (CheckUserAgent(userAgent, @"Pre\/1.0"))
                    {
                        this.Device.Name = "Pre";
                    }

                    if (CheckUserAgent(userAgent, @"Pre\/1.1"))
                    {
                        this.Device.Name = "Pre Plus";
                    }

                    if (CheckUserAgent(userAgent, @"Pre\/1.2"))
                    {
                        this.Device.Name = "Pre2";
                    }

                    if (CheckUserAgent(userAgent, @"Pre\/3.0"))
                    {
                        this.Device.Name = "Pre3";
                    }

                    if (CheckUserAgent(userAgent, @"Pixi\/1.0"))
                    {
                        this.Device.Name = "Pixi";
                    }

                    if (CheckUserAgent(userAgent, @"Pixi\/1.1"))
                    {
                        this.Device.Name = "Pixi Plus";
                    }

                    if (CheckUserAgent(userAgent, @"P160UN?A?\/1.0"))
                    {
                        this.Device.Name = "Veer";
                    }

                    if (CheckUserAgent(userAgent, @"TouchPad\/1.0"))
                    {
                        this.Device.Name = "TouchPad";
                    }

                    if (CheckUserAgent(userAgent, @"Emulator\/") || CheckUserAgent(userAgent, @"Desktop\/"))
                    {
                        this.Device.DeviceType = DeviceType.Emulator;
                        this.Device.Name = null;
                        this.Device.Manufacturer = null;
                    }

                    this.Device.Identified = true;
                });

                /* S60 */
                CheckUserAgent(userAgent, new[] { "Symbian", "Series[ ]?60", "S60" }, (mc2, regex) =>
                  {
                      this.Os.Name = "Series60";

                      if (CheckUserAgent(userAgent, "SymbianOS/9.1") && !CheckUserAgent(userAgent, "Series60"))
                      {
                          this.Os.Version = 3.0m;
                      }

                      CheckUserAgent(userAgent, @"/Series60\/([0-9.]*)/",
                          mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });

                      CheckUserAgent(userAgent, @"/Nokia([^\/;]+)[\/|;]/", mc3 =>
                      {
                          if (mc3[1].SafeString() != "Browser")
                          {
                              this.Device.Manufacturer = "Nokia";
                              this.Device.Name = mc3[1].SafeString();
                              this.Device.Identified = true;
                          }
                      });

                      CheckUserAgent(userAgent, @"/Vertu([^\/;]+)[\/|;]/", mc3 =>
                      {
                          this.Device.Manufacturer = "Vertu";
                          this.Device.Name = mc3[1].SafeString();
                          this.Device.Identified = true;
                      });


                      CheckUserAgent(userAgent, @"/Symbian; U; ([^;]+); [a-z][a-z]\-[a-z][a-z]/i", mc3 =>
                      {
                          this.Device.Manufacturer = "Nokia";
                          this.Device.Name = mc3[1].SafeString();
                          this.Device.Identified = true;
                      });

                      CheckUserAgent(userAgent, @"/Samsung\/([^;]*);/", mc3 =>
                      {
                          this.Device.Manufacturer = GloableConfigurations.StringsSamsung;
                          this.Device.Name = mc3[1].SafeString();
                          this.Device.Identified = true;
                      });

                      this.Device.DeviceType = DeviceType.Mobile;
                  });

                /* S40 */
                CheckUserAgent(userAgent, "Series40", mc2 =>
                {
                    this.Os.Name = "Series40";

                    CheckUserAgent(userAgent, @"/Nokia([^\/]+)\//", mc3 =>
                    {
                        this.Device.Manufacturer = "Nokia";
                        this.Device.Name = mc3[1].SafeString();
                        this.Device.Identified = true;
                    });

                    this.Device.DeviceType = DeviceType.Mobile;
                });

                /* MeeGo */
                CheckUserAgent(userAgent, "MeeGo", mc2 =>
                {
                    this.Os.Name = "MeeGo";
                    this.Device.DeviceType = DeviceType.Mobile;

                    CheckUserAgent(userAgent, @"/Nokia([^\)]+)\)/", mc3 =>
                    {
                        this.Device.Manufacturer = "Nokia";
                        this.Device.Name = mc3[1].SafeString();
                        this.Device.Identified = true;
                    });
                });

                /* Maemo */
                CheckUserAgent(userAgent, "Maemo", mc2 =>
                {
                    this.Os.Name = "Maemo";
                    this.Device.DeviceType = DeviceType.Mobile;

                    CheckUserAgent(userAgent, "/(N[0-9]+)/", mc3 =>
                    {
                        this.Device.Manufacturer = "Nokia";
                        this.Device.Name = mc3[1].SafeString();
                        this.Device.Identified = true;
                    });
                });

                /* Tizen */
                CheckUserAgent(userAgent, "Tizen", mc2 =>
                {
                    this.Os.Name = "Tizen";

                    CheckUserAgent(userAgent, @"/Tizen[\/ ]([0-9.]*)/",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });

                    this.Device.DeviceType = DeviceType.Mobile;

                    CheckUserAgent(userAgent, @"/\(([^;]+); ([^\/]+)\//", mc3 =>
                    {
                        if (mc3[1].SafeString() != "Linux")
                        {
                            this.Device.Manufacturer = mc3[1].SafeString();
                            this.Device.Name = mc3[2].SafeString();

                            List<KeyValuePair<string, string[]>> tizenModel =
                                GloableConfigurations.TizenModels.Where(x =>
                                    x.Key == this.Device.Manufacturer).Select(x => x.Value).FirstOrDefault() ??
                                new List<KeyValuePair<string, string[]>>();
                            if (tizenModel.Count > 0 && tizenModel
                                .Any(x => x.Key == this.Device.Name))
                            {
                                var manufacturer = this.Device.Manufacturer;
                                var model = CleanupModel(this.Device.Name);

                                var temp = GloableConfigurations.TizenModels
                                    .Where(x => x.Key == manufacturer).Select(x => x.Value).FirstOrDefault();
                                if (temp != null)
                                {
                                    this.Device.Manufacturer =
                                        temp.Where(x => x.Key == model).Select(x => x.Value).ToList()[0].SafeString();
                                    this.Device.Name = temp.Where(x => x.Key == model).Select(x => x.Value).ToList()[1]
                                        .SafeString();
                                    this.Device.Identified = true;
                                }
                            }
                        }
                    });
                });

                /* Bada */
                CheckUserAgent(userAgent, "[b|B]ada", mc2 =>
                {
                    this.Os.Name = "Bada";
                    CheckUserAgent(userAgent, @"/[b|B]ada\/([0-9.]*)/",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });

                    this.Device.DeviceType = DeviceType.Mobile;

                    CheckUserAgent(userAgent, @"/\(([^;]+); ([^\/]+)\//", mc3 =>
                    {
                        this.Device.Manufacturer = mc3[1].SafeString();
                        this.Device.Name = CleanupModel(mc3[2].SafeString());
                    });

                    var badaModel = GloableConfigurations.BadaModels.Where(x => x.Key == this.Device.Manufacturer)
                        .Select(x => x.Value).FirstOrDefault();
                    if (badaModel != null && badaModel.Count > 0 && badaModel.Any(x => x.Key == this.Device.Name))
                    {
                        var manufacturer = this.Device.Manufacturer;
                        var model = CleanupModel(this.Device.Name);


                        badaModel = GloableConfigurations.BadaModels.Where(x => x.Key == manufacturer)
                            .Select(x => x.Value).FirstOrDefault();

                        if (badaModel != null)
                        {
                            this.Device.Manufacturer =
                                badaModel.Where(x => x.Key == model).Select(x => x.Value).FirstOrDefault()?[0];
                            this.Device.Name =
                                badaModel.Where(x => x.Key == model).Select(x => x.Value).FirstOrDefault()?[1];
                        }

                        this.Device.Identified = true;
                    }
                });

                /* Brew */
                CheckUserAgent(userAgent, "/BREW/i", mc2 =>
                {
                    this.Os.Name = "Brew";
                    this.Device.DeviceType = DeviceType.Mobile;

                    MatchCollection mc3;

                    if (CheckUserAgent(userAgent, "/BREW; U; ([0-9.]*)/i", out mc3))
                    {
                        this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0);
                    }
                    else if (CheckUserAgent(userAgent, @"/;BREW\/([0-9.]*)/i", out mc3))
                    {
                        this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0);
                    }

                    CheckUserAgent(userAgent,
                        @"/\(([^;]+);U;REX\/[^;]+;BREW\/[^;]+;(?:.*;)?[0-9]+\*[0-9]+;CTC\/2.0\)/",
                        mc4 => { this.Device.Name = mc4[1].SafeString(); });

                    if (!string.IsNullOrEmpty(this.Device.Name))
                    {
                        var model = CleanupModel(this.Device.Name);

                        if (GloableConfigurations.BrewModels.Any(x => x.Key == model))
                        {
                            this.Device.Manufacturer = GloableConfigurations.BrewModels.Where(x => x.Key == model)
                                .Select(x => x.Value).FirstOrDefault()?[0];
                            this.Device.Name = GloableConfigurations.BrewModels.Where(x => x.Key == model)
                                .Select(x => x.Value).FirstOrDefault()?[1];
                            this.Device.Identified = true;
                        }
                    }
                });

                /* MTK */

                CheckUserAgent(userAgent, @"/\(MTK;/", mc2 =>
                {
                    this.Os.Name = "MTK";
                    this.Device.DeviceType = DeviceType.Mobile;
                });

                /* CrOS */
                CheckUserAgent(userAgent, "CrOS", mc2 =>
                {
                    this.Os.Name = "Chrome OS";
                    this.Device.DeviceType = DeviceType.Pc;
                });

                /* Joli OS */
                CheckUserAgent(userAgent, "Joli OS", mc2 =>
                {
                    this.Os.Name = "Joli OS";
                    this.Device.DeviceType = DeviceType.Pc;

                    CheckUserAgent(userAgent, @"/Joli OS\/([0-9.]*)/i",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                /* Haiku */
                CheckUserAgent(userAgent, "Haiku", mc2 =>
                {
                    this.Os.Name = "Haiku";
                    this.Device.DeviceType = DeviceType.Pc;
                });

                /* QNX */
                CheckUserAgent(userAgent, "QNX", mc2 =>
                {
                    this.Os.Name = "QNX";
                    this.Device.DeviceType = DeviceType.Mobile;
                });

                /* OS/2 Warp */
                CheckUserAgent(userAgent, @"OS\/2; Warp", mc2 =>
                {
                    this.Os.Name = "OS/2 Warp";
                    this.Device.DeviceType = DeviceType.Pc;

                    CheckUserAgent(userAgent, @"/OS\/2; Warp ([0-9.]*)/i",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });


                /* Grid OS */
                CheckUserAgent(userAgent, "Grid OS", mc2 =>
                {
                    this.Os.Name = "Grid OS";
                    this.Device.DeviceType = DeviceType.Tablet;

                    CheckUserAgent(userAgent, "/Grid OS ([0-9.]*)/i",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                /* AmigaOS */
                CheckUserAgent(userAgent, "/AmigaOS/i", mc2 =>
                {
                    this.Os.Name = "AmigaOS";
                    this.Device.DeviceType = DeviceType.Pc;

                    CheckUserAgent(userAgent, "/AmigaOS ([0-9.]*)/i",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                /* MorphOS */
                CheckUserAgent(userAgent, "/MorphOS/i", mc2 =>
                {
                    this.Os.Name = "MorphOS";
                    this.Device.DeviceType = DeviceType.Pc;
                    CheckUserAgent(userAgent, "/MorphOS ([0-9.]*)/i",
                        mc3 => { this.Os.Version = mc3[1].SafeString().ConvertToDecimal(0); });
                });

                /* Kindle */
                if (CheckUserAgent(userAgent, "Kindle") && !CheckUserAgent(userAgent, "Fire"))
                {
                    this.Os.Name = "";

                    this.Device.Manufacturer = "Amazon";
                    this.Device.Name = "Kindle";
                    this.Device.DeviceType = DeviceType.Reader;

                    if (CheckUserAgent(userAgent, @"Kindle\/2.0"))
                    {
                        this.Device.Name = "Kindle 2";
                    }

                    if (CheckUserAgent(userAgent, @"Kindle\/3.0"))
                    {
                        this.Device.Name = "Kindle 3 or later";
                    }

                    this.Device.Identified = true;
                }

                /* NOOK */
                CheckUserAgent(userAgent, "nook browser", mc2 =>
                {
                    this.Os.Name = "Android";
                    this.Device.Manufacturer = "Barnes & Noble";
                    this.Device.Name = "NOOK";
                    this.Device.DeviceType = DeviceType.Reader;
                    this.Device.Identified = true;
                });

                /* Bookeen */
                CheckUserAgent(userAgent, @"bookeen\/cybook", mc2 =>
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Bookeen";
                    this.Device.Name = "Cybook";
                    this.Device.DeviceType = DeviceType.Reader;

                    CheckUserAgent(userAgent, "Orizon", mc3 => { this.Device.Name = "Cybook Orizon"; });

                    this.Device.Identified = true;
                });

                /* Sony Reader */
                CheckUserAgent(userAgent, "EBRD1101", mc2 =>
                {
                    this.Os.Name = "";

                    this.Device.Manufacturer = "Sony";
                    this.Device.Name = "Reader";
                    this.Device.DeviceType = DeviceType.Reader;
                    this.Device.Identified = true;
                });

                /* iRiver */
                CheckUserAgent(userAgent, "Iriver ;", mc2 =>
                {
                    this.Os.Name = "";

                    this.Device.Manufacturer = "iRiver";
                    this.Device.Name = "Story";
                    this.Device.DeviceType = DeviceType.Reader;

                    if (CheckUserAgent(userAgent, "EB07"))
                    {
                        this.Device.Name = "Story HD EB07";
                    }

                    this.Device.Identified = true;
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
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Nintendo";
                    this.Device.Name = "Wii";
                    this.Device.DeviceType = DeviceType.Gaming;
                    this.Device.Identified = true;
                }

                if (CheckUserAgent(userAgent, "Nintendo DSi"))
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Nintendo";
                    this.Device.Name = "DSi";
                    this.Device.DeviceType = DeviceType.Gaming;
                    this.Device.Identified = true;
                }

                if (CheckUserAgent(userAgent, "Nintendo 3DS"))
                {
                    this.Os.Name = "";

                    this.Device.Manufacturer = "Nintendo";
                    this.Device.Name = "3DS";
                    this.Device.DeviceType = DeviceType.Gaming;

                    CheckUserAgent(userAgent, @"/Version\/([0-9.]*)/",
                        mc2 => { this.Os.Version = mc2[1].ConvertToDecimal(0); });

                    this.Device.Identified = true;
                }

                if (CheckUserAgent(userAgent, "PlayStation Portable"))
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Sony";
                    this.Device.Name = "Playstation Portable";
                    this.Device.DeviceType = DeviceType.Gaming;
                    this.Device.Identified = true;
                }

                if (CheckUserAgent(userAgent, "PlayStation Vita"))
                {
                    this.Os.Name = "";
                    CheckUserAgent(userAgent, "/PlayStation Vita ([0-9.]*)/",
                        mc2 => { this.Os.Version = mc2[1].SafeString().ConvertToDecimal(0); });
                    this.Device.Manufacturer = "Sony";
                    this.Device.Name = "PlayStation Vita";
                    this.Device.DeviceType = DeviceType.Gaming;
                    this.Device.Identified = true;
                }

                if (CheckUserAgent(userAgent, "/PlayStation 3/i"))
                {
                    this.Os.Name = "";
                    CheckUserAgent(userAgent, "/PLAYSTATION 3;? ([0-9.]*)/",
                        mc2 => { this.Os.Version = mc2[1].SafeString().ConvertToDecimal(0); });
                    this.Device.Manufacturer = "Sony";
                    this.Device.Name = "Playstation 3";
                    this.Device.DeviceType = DeviceType.Gaming;
                    this.Device.Identified = true;
                }

                /****************************************************
                 *      Panasonic Smart Viera
                 *
                 *      Mozilla/5.0 (FreeBSD; U; Viera; ja-JP) AppleWebKit/535.1 (KHTML, like Gecko) Viera/1.2.4 Chrome/14.0.835.202 Safari/535.1
                 */
                if (CheckUserAgent(userAgent, "Viera"))
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Panasonic";
                    this.Device.Name = "Smart Viera";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
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
                    this.Os.Name = "";
                    this.Device.Manufacturer = GloableConfigurations.StringsSharp;
                    this.Device.Name = "Aquos TV";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
                }

                /****************************************************
                 *      Samsung Smart TV
                 *
                 *      Mozilla/5.0 (SmartHub; SMART-TV; U; Linux/SmartTV; Maple2012) AppleWebKit/534.7 (KHTML, like Gecko) SmartTV Safari/534.7
                 *      Mozilla/5.0 (SmartHub; SMART-TV; U; Linux/SmartTV) AppleWebKit/531.2+ (KHTML, like Gecko) WebBrowser/1.0 SmartTV Safari/531.2+
                 */
                if (CheckUserAgent(userAgent, "SMART-TV"))
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = GloableConfigurations.StringsSamsung;
                    this.Device.Name = "Smart TV";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;

                    CheckUserAgent(userAgent, "/Maple([0-9]*)/", mc2 => { this.Device.Name += " " + mc2[1]; });
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
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Sony";
                    this.Device.Name = "Internet TV";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
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

                if (CheckUserAgent(userAgent, @"NETTV\/"))
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Philips";
                    this.Device.Name = "Net TV";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
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
                CheckUserAgent(userAgent, @"/LG NetCast\.(?:TV|Media)-([0-9]*)/", mc2 =>
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = GloableConfigurations.StringsLg;
                    this.Device.Name = "NetCast TV " + mc2[1].SafeString();
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
                });
                CheckUserAgent(userAgent, @"/LGSmartTV/", mc2 =>
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = GloableConfigurations.StringsLg;
                    this.Device.Name = "Smart TV";
                    this.Device.DeviceType = DeviceType.Television;
                    ;
                    this.Device.Identified = true;
                });

                /****************************************************
                *      Toshiba Smart TV
                *
                *      Mozilla/5.0 (Linux mipsel; U; HbbTV/1.1.1 (; TOSHIBA; DTV_RL953; 56.7.66.7; t12; ) ; ToshibaTP/1.3.0 (+VIDEO_MP4+VIDEO_X_MS_ASF+AUDIO_MPEG+AUDIO_MP4+DRM+NATIVELAUNCH) ; en) AppleWebKit/534.1 (KHTML, like Gecko)
                *      Mozilla/5.0 (DTV; TSBNetTV/T32013713.0203.7DD; TVwithVideoPlayer; like Gecko) NetFront/4.1 DTVNetBrowser/2.2 (000039;T32013713;0203;7DD) InettvBrowser/2.2 (000039;T32013713;0203;7DD)
                *      Mozilla/5.0 (Linux mipsel; U; HbbTV/1.1.1 (; TOSHIBA; 40PX200; 0.7.3.0.; t12; ) ; Toshiba_TP/1.3.0 (+VIDEO_MP4+AUDIO_MPEG+AUDIO_MP4+VIDEO_X_MS_ASF+OFFLINEAPP) ; en) AppleWebKit/534.1 (KHTML, like Gec
                */
                if (CheckUserAgent(userAgent, @"Toshiba_?TP\/") || CheckUserAgent(userAgent, @"TSBNetTV\/"))
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Toshiba";
                    this.Device.Name = "Smart TV";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
                }

                /* MachBlue XT */
                CheckUserAgent(userAgent, @"/mbxtWebKit\/([0-9.]*)/", mc2 =>
                {
                    this.Os.Name = "";
                    this.Device.Name = "MachBlue XT";
                    this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                    this.Browser.Detail = "2";
                    this.Device.DeviceType = DeviceType.Television;
                });

                /* ADB */
                CheckUserAgent(userAgent, @"/mbxtWebKit\/([0-9.]*)/", mc2 =>
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "ADB";
                    this.Device.Name =
                        (mc2[1].SafeString() != "Unknown" ? mc2[1].SafeString().Replace("ADB", "") + " " : "") +
                        "IPTV receiver";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
                });

                /* MStar */
                if (CheckUserAgent(userAgent, "/Mstar;OWB/"))
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "MStar";
                    this.Device.Name = "PVR";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;

                    this.Browser.Name = "Origyn Web Browser";
                }

                /* TechniSat */
                CheckUserAgent(userAgent, @"/\TechniSat ([^;]+);/", mc2 =>
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "TechniSat";
                    this.Device.Name = mc2[1].SafeString();
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
                });

                /* Technicolor */
                CheckUserAgent(userAgent, @"/\Technicolor_([^;]+);/", mc2 =>
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Technicolor";
                    this.Device.Name = mc2[1].SafeString();
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
                });

                /* Winbox Evo2 */
                CheckUserAgent(userAgent, @"/\Technicolor_([^;]+);/", mc2 =>
                {
                    this.Os.Name = "";
                    this.Device.Manufacturer = "Winbox";
                    this.Device.Name = "Evo2";
                    this.Device.DeviceType = DeviceType.Television;
                    this.Device.Identified = true;
                });

                /* Roku */
                CheckUserAgent(userAgent, @"/^Roku\/DVP-([0-9]+)/", mc2 =>
                {
                    this.Device.Manufacturer = "Roku";
                    this.Device.DeviceType = DeviceType.Television;
                    switch (mc2[1].SafeString())
                    {
                        case "2000":
                            this.Device.Name = "HD";
                            break;
                        case "2050":
                            this.Device.Name = "XD";
                            break;
                        case "2100":
                            this.Device.Name = "XDS";
                            break;
                        case "2400":
                            this.Device.Name = "LT";
                            break;
                        case "3000":
                            this.Device.Name = "2 HD";
                            break;
                        case "3050":
                            this.Device.Name = "2 XD";
                            break;
                        case "3100":
                            this.Device.Name = "2 XS";
                            break;
                    }
                });
                CheckUserAgent(userAgent, @"/HbbTV\/1.1.1 \([^;]*;\s*([^;]*)\s*;\s*([^;]*)\s*;/", mc2 =>
                {
                    var vendorName = mc2[1].SafeString().Trim();
                    var modelName = mc2[2].SafeString().Trim();

                    if (string.IsNullOrEmpty(this.Device.Manufacturer) && vendorName != "" &&
                        vendorName != "vendorName")
                    {
                        switch (vendorName)
                        {
                            case "LGE":
                                this.Device.Manufacturer = "LG";
                                break;
                            case "TOSHIBA":
                                this.Device.Manufacturer = "Toshiba";
                                break;
                            case "smart":
                                this.Device.Manufacturer = "Smart";
                                break;
                            case "tv2n":
                                this.Device.Manufacturer = "TV2N";
                                break;
                            default:
                                this.Device.Manufacturer = vendorName;
                                break;
                        }

                        if (string.IsNullOrEmpty(this.Device.Name) && modelName != "" && modelName != "modelName")
                        {
                            switch (modelName)
                            {
                                case "GLOBAL_PLAT3":
                                    this.Device.Name = "NetCast TV";
                                    break;
                                case "SmartTV2012":
                                    this.Device.Name = "Smart TV 2012";
                                    break;
                                case "videoweb":
                                    this.Device.Name = "Videoweb";
                                    break;
                                default:
                                    this.Device.Name = modelName;
                                    break;
                            }

                            if (vendorName == "Humax")
                            {
                                this.Device.Name = this.Device.Name.ToUppers();
                            }

                            this.Device.Identified = true;
                            this.Os.Name = "";
                        }
                    }

                    this.Device.DeviceType = DeviceType.Television;
                });

                /****************************************************
                 *      Detect type based on common identifiers
                 */
                if (CheckUserAgent(userAgent, @"InettvBrowser"))
                {
                    this.Device.DeviceType = DeviceType.Television;
                }

                if (CheckUserAgent(userAgent, @"MIDP"))
                {
                    this.Device.DeviceType = DeviceType.Mobile;
                }

                /****************************************************
                 *      Try to detect any devices based on common
                 *      locations of model ids
                 */
                if (string.IsNullOrEmpty(this.Device.Name) && string.IsNullOrEmpty(this.Device.Manufacturer))
                {
                    var candidates = new List<string>();
                    if (CheckUserAgent(userAgent, @"/^(Mozilla|Opera)/"))
                    {
                        CheckUserAgent(userAgent, @"/^(?:MQQBrowser\/[0-9\.]+\/)?([^\s]+)/", mc2 =>
                        {
                            var temp = mc2[1].SafeString();
                            temp = temp.Replace(@"/_TD$/", "");
                            temp = temp.SafeString().Replace("/_CMCC$/", "");
                            temp = temp.SafeString().Replace(@"/[_ ]Mozilla$/", "");
                            temp = temp.SafeString().Replace(@"/ Linux$/", "");
                            temp = temp.SafeString().Replace(@"/ Opera$/", "");
                            temp = temp.SafeString().Replace(@"/\/[0-9].*$/", "");

                            candidates.Add(temp);
                        });
                    }


                    CheckUserAgent(userAgent,
                        new[]
                        {
                            @"/[0-9]+x[0-9]+; ([^;]+)/", @"/[0-9]+X[0-9]+ ([^;\/\(\)]+)/",
                            @"/Windows NT 5.1; ([^;]+); Windows Phone/", @"/\) PPC; (?:[0-9]+x[0-9]+; )?([^;\/\(\)]+)/",
                            @"/\(([^;]+); U; Windows Mobile/", @"/Vodafone\/1.0\/([^\/]+)/", @"/\ ([^\s]+)$/"
                        },
                        (mc2, regex) => { candidates.Add(mc2[1].SafeString()); });

                    for (var i = 0; i < candidates.Count; i++)
                    {
                        var result = false;
                        if (string.IsNullOrEmpty(this.Device.Name) && string.IsNullOrEmpty(this.Device.Manufacturer))
                        {
                            var model = CleanupModel(candidates[i]);


                            if (this.Os.Name == "Android")
                            {
                                if (GloableConfigurations.AndroidModels.Any(x => x.Key == model))
                                {
                                    var deviceModel = GloableConfigurations.AndroidModels.Where(x => x.Key == model)
                                        .Select(x => x.Value).FirstOrDefault();

                                    if (deviceModel.Length > 0)
                                    {
                                        this.Device.Manufacturer = deviceModel[0];
                                        this.Device.Name = deviceModel[1];

                                        if (!string.IsNullOrEmpty(deviceModel[2]))
                                        {
                                            this.Device.DeviceType = DeviceType.GetAll<DeviceType>()
                                                .FirstOrDefault(x => x.Extend == deviceModel[2]);
                                        }
                                    }

                                    this.Device.Identified = true;

                                    result = true;
                                }
                            }

                            if (string.IsNullOrEmpty(this.Os.Name) || this.Os.Name == "Windows" ||
                                this.Os.Name == "Windows Mobile" || this.Os.Name == "Windows CE")
                            {
                                var deviceModel = GloableConfigurations.WindowsMobileModels
                                    .Where(x => x.Key == model).Select(x => x.Value).FirstOrDefault();

                                if (deviceModel.Length > 0)
                                {
                                    this.Device.Manufacturer = deviceModel[0];
                                    this.Device.Name = deviceModel[1];
                                    this.Device.DeviceType = DeviceType.Mobile;
                                    this.Device.Identified = true;

                                    if (this.Os.Name != "Windows Mobile")
                                    {
                                        this.Os.Name = "Windows Mobile";
                                        this.Os.Version = null;
                                    }

                                    result = true;
                                }
                            }
                        }

                        if (!result)
                        {
                            CheckUserAgent(userAgent, @"/^GIONEE-([^\s]+)/", mc2 =>
                            {
                                this.Device.Manufacturer = "Gionee";
                                this.Device.Name = CleanupModel(mc2[1].SafeString());
                                this.Device.DeviceType = DeviceType.Mobile;
                                this.Device.Identified = true;
                            });

                            CheckUserAgent(userAgent, @"/^HTC_?([^\/_]+)(?:\/|_|$)/", mc2 =>
                            {
                                this.Device.Manufacturer = GloableConfigurations.StringsHtc;
                                this.Device.Name = CleanupModel(mc2[1].SafeString());
                                this.Device.DeviceType = DeviceType.Mobile;
                                this.Device.Identified = true;
                            });

                            CheckUserAgent(userAgent, @"/^HUAWEI-([^\/]*)/", mc2 =>
                            {
                                this.Device.Manufacturer = GloableConfigurations.StringsHuawei;
                                this.Device.Name = CleanupModel(mc2[1].SafeString());
                                this.Device.DeviceType = DeviceType.Mobile;
                                this.Device.Identified = true;
                            });

                            CheckUserAgent(userAgent, @"/(?:^|\()LGE?(?:\/|-|_|\s)([^\s]*)/", mc2 =>
                            {
                                this.Device.Manufacturer = GloableConfigurations.StringsLg;
                                this.Device.Name = CleanupModel(mc2[1].SafeString());
                                this.Device.DeviceType = DeviceType.Mobile;
                                this.Device.Identified = true;
                            });

                            CheckUserAgent(userAgent, @"/^MOT-([^\/_]+)(?:\/|_|$)/", mc2 =>
                            {
                                this.Device.Manufacturer = GloableConfigurations.StringsMotorola;
                                this.Device.Name = CleanupModel(mc2[1].SafeString());
                                this.Device.DeviceType = DeviceType.Mobile;
                                this.Device.Identified = true;
                            });

                            CheckUserAgent(userAgent, @"/^Motorola_([^\/_]+)(?:\/|_|$)/", mc2 =>
                            {
                                this.Device.Manufacturer = GloableConfigurations.StringsMotorola;
                                this.Device.Name = CleanupModel(mc2[1].SafeString());
                                this.Device.DeviceType = DeviceType.Mobile;
                                this.Device.Identified = true;
                            });

                            CheckUserAgent(userAgent, @"/^Nokia([^\/]+)(?:\/|$)/", mc2 =>
                            {
                                this.Device.Manufacturer = "Nokia";
                                this.Device.Name = CleanupModel(mc2[1].SafeString());
                                this.Device.DeviceType = DeviceType.Mobile;
                                this.Device.Identified = true;

                                if (string.IsNullOrEmpty(this.Os.Name))
                                {
                                    this.Os.Name = "Series40";
                                }
                            });
                            CheckUserAgent(userAgent, @"/^SonyEricsson([^\/_]+)(?:\/|_|$)/", mc2 =>
                            {
                                this.Device.Manufacturer = GloableConfigurations.StringsSonyEricsson;
                                this.Device.Name = CleanupModel(mc2[1].SafeString());
                                this.Device.DeviceType = DeviceType.Mobile;
                                this.Device.Identified = true;
                            });
                        }
                    }
                }

                CheckUserAgent(userAgent, @"/\((?:LG[-|\/])(.*) (?:Browser\/)?AppleWebkit/", mc2 =>
                {
                    this.Device.Manufacturer = GloableConfigurations.StringsLg;
                    this.Device.Name = mc2[1].SafeString();
                    this.Device.DeviceType = DeviceType.Mobile;
                    this.Device.Identified = true;
                });

                CheckUserAgent(userAgent,
                    @"/^Mozilla\/5.0 \((?:Nokia|NOKIA)(?:\s?)([^\)]+)\)UC AppleWebkit\(like Gecko\) Safari\/530$/",
                    mc2 =>
                    {
                        this.Device.Manufacturer = "Nokia";
                        this.Device.Name = mc2[1].SafeString();
                        this.Device.DeviceType = DeviceType.Mobile;
                        this.Device.Identified = true;
                        this.Os.Name = "Series60";
                    });

                /****************************************************
                 *      Safari
                 */
                if (CheckUserAgent(userAgent, @"Safari"))
                {
                    if (this.Os.Name == "iOS")
                    {
                        this.Browser.Stock = true;
                        this.Browser.Hidden = true;
                        this.Browser.Name = "Safari";
                        this.Browser.Version = null;
                    }


                    if (this.Os.Name == "Mac OS X" || this.Os.Name == "Windows")
                    {
                        this.Browser.Name = "Safari";
                        this.Browser.Stock = this.Os.Name == "Mac OS X";

                        CheckUserAgent(userAgent, @"/Version\/([0-9\.]+)/",
                            mc2 => { this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0); });

                        if (CheckUserAgent(userAgent, @"/AppleWebKit\/[0-9\.]+\+/"))
                        {
                            this.Browser.Name = "WebKit Nightly Build";
                            this.Browser.Version = null;
                        }
                    }
                }


                /****************************************************
                 *      Internet Explorer
                 */
                if (CheckUserAgent(userAgent, @"MSIE"))
                {
                    this.Browser.Name = "Internet Explorer";

                    if (CheckUserAgent(userAgent, @"IEMobile") || CheckUserAgent(userAgent, @"Windows CE") ||
                        CheckUserAgent(userAgent, @"Windows Phone") || CheckUserAgent(userAgent, @"WP7"))
                    {
                        this.Browser.Name = "Mobile Internet Explorer";
                    }

                    CheckUserAgent(userAgent, @"/MSIE ([0-9.]*)/",
                        mc2 => { this.Browser.Version = mc2[1].ConvertToDecimal(0); });
                }

                /****************************************************
                 *      Opera
                 */

                if (CheckUserAgent(userAgent, @"/Opera/i"))
                {
                    this.Browser.Stock = false;
                    this.Browser.Name = "Opera";
                    CheckUserAgent(userAgent, @"/Opera[\/| ]([0-9.]*)/", mc2 =>
                    {
                        this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                    });
                    CheckUserAgent(userAgent, @"/Version\/([0-9.]*)/", mc2 =>
                    {
                        if (mc2[1].SafeString().ConvertToDecimal(0) > 10)
                        {
                            this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                        }
                        else
                        {
                            this.Browser.Version = null;
                        }
                    });

                    if (this.Browser.Version != null && CheckUserAgent(userAgent, "Edition Labs"))
                    {
                        this.Browser.VersionType = "alpha";
                        this.Browser.Channel = "Labs";
                    }

                    if (this.Browser.Version != null && CheckUserAgent(userAgent, "Edition Next"))
                    {
                        this.Browser.VersionType = "alpha";
                        this.Browser.Channel = "Next";
                    }

                    if (CheckUserAgent(userAgent, "Opera Tablet"))
                    {
                        this.Browser.VersionType = "Opera Mobile";
                        this.Device.DeviceType = DeviceType.Tablet;
                    }

                    if (CheckUserAgent(userAgent, "Opera Mobi"))
                    {
                        this.Browser.VersionType = "Opera Mobile";
                        this.Device.DeviceType = DeviceType.Mobile;
                    }

                    CheckUserAgent(userAgent, "/Opera Mini;/", mc2 =>
                     {
                         this.Browser.Name = "Opera Mini";
                         this.Browser.Version = null;
                         this.Browser.Mode = "proxy";
                         this.Device.DeviceType = DeviceType.Mobile;
                     });

                    CheckUserAgent(userAgent, @"/Opera Mini\/(?:att\/)?([0-9.]*)/", mc2 =>
                    {
                        this.Browser.Name = "Opera Mini";
                        this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                        this.Browser.Detail = "-1";

                        this.Browser.Mode = "proxy";
                        this.Device.DeviceType = DeviceType.Mobile;
                    });


                    if (this.Browser.Name == "Opera" && this.Device.DeviceType.Id == DeviceType.Mobile.Id)
                    {
                        this.Browser.Name = "Opera Mobile";
                        if (CheckUserAgent(userAgent, @"/BER/"))
                        {
                            this.Browser.Name = "Opera Mini";
                            this.Browser.Version = null;
                        }

                    }


                    if (CheckUserAgent(userAgent, "InettvBrowser"))
                    {
                        this.Device.DeviceType = DeviceType.Television;
                    }

                    if (CheckUserAgent(userAgent, "Opera TV") || CheckUserAgent(userAgent, "'Opera-TV"))
                    {
                        this.Browser.Name = "Opera";
                        this.Device.DeviceType = DeviceType.Television;
                    }

                    if (CheckUserAgent(userAgent, "Linux zbov"))
                    {
                        this.Browser.Name = "Opera Mobile";
                        this.Browser.Mode = "desktop";

                        this.Device.DeviceType = DeviceType.Mobile;

                        this.Os.Name = null;
                        this.Os.Version = null;
                    }

                    if (CheckUserAgent(userAgent, "Linux zvav"))
                    {
                        this.Browser.Name = "Mini";
                        this.Browser.Mode = "desktop";
                        this.Browser.Version = null;

                        this.Device.DeviceType = DeviceType.Mobile;

                        this.Os.Name = null;
                        this.Os.Version = null;
                    }
                }

                /****************************************************
                 *      Firefox
                 */
                if (CheckUserAgent(userAgent, "Firefox"))
                {
                    this.Browser.Stock = false;
                    this.Browser.Name = "Firefox";

                    CheckUserAgent(userAgent, @"/Firefox\/([0-9ab.]*)/", mc2 =>
                    {
                        this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                    });

                    if (this.Browser.VersionType == "alpha")
                    {
                        this.Browser.Channel = "Aurora";
                    }
                    if (this.Browser.VersionType == "beta")
                    {
                        this.Browser.Channel = "Beta";
                    }

                    if (CheckUserAgent(userAgent, @"Fennec"))
                    {

                        this.Device.DeviceType = DeviceType.Mobile;
                    }
                    if (CheckUserAgent(userAgent, @"Mobile; rv"))
                    {
                        this.Device.DeviceType = DeviceType.Mobile;
                    }

                    if (CheckUserAgent(userAgent, @"Tablet; rv"))
                    {
                        this.Device.DeviceType = DeviceType.Tablet;
                    }

                    if (this.Device.DeviceType == DeviceType.Mobile || this.Device.DeviceType == DeviceType.Tablet)
                    {
                        this.Browser.Name = "Firefox Mobile";
                    }
                }

                if (CheckUserAgent(userAgent, @"Namoroka"))
                {
                    this.Browser.Stock = false;
                    this.Browser.Name = "Firefox";

                    CheckUserAgent(userAgent, @"/Namoroka\/([0-9ab.]*)/", mc2 =>
                      {
                          this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                      });

                    this.Browser.Channel = "Namoroka";
                }

                if (CheckUserAgent(userAgent, @"Shiretoko"))
                {
                    this.Browser.Stock = false;
                    this.Browser.Name = "Firefox";
                    CheckUserAgent(userAgent, @"/Shiretoko\/([0-9ab.]*)/", mc2 =>
                     {
                         this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                     });
                    this.Browser.Channel = "Shiretoko";
                }

                if (CheckUserAgent(userAgent, @"Minefield"))
                {
                    this.Browser.Stock = false;
                    this.Browser.Name = "Firefox";

                    CheckUserAgent(userAgent, @"/Minefield\/([0-9ab.]*)/", mc2 =>
                    {
                        this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                    });

                    this.Browser.Channel = "Minefield";
                }

                if (CheckUserAgent(userAgent, @"Firebird"))
                {
                    this.Browser.Stock = false;
                    this.Browser.Name = "Firebird";

                    CheckUserAgent(userAgent, @"/Firebird\/([0-9ab.]*)/", mc2 =>
                    {
                        this.Browser.Version = mc2[1].SafeString().ConvertToDecimal(0);
                    });
                }

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
            str = str.Replace(@"/^\s+|\s+$/g", "");
            str = str.Replace(@"/\/[^/]+$/", "");
            str = str.Replace(@"/\/[^/]+ Android\/.*/", "");

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
            str = str.Replace(@"/^(LG)[ _\/]/", "$1-");
            str = str.Replace(@"/^(HTC.*)\s(?:v|V)?[0-9.]+$/", "$1");
            str = str.Replace(@"/^(HTC)[-\/]/", "$1 ");
            str = str.Replace("/^(HTC)([A-Z][0-9][0-9][0-9])/", "$1 $2");
            str = str.Replace(@"/^(Motorola[\s|-])/", "");
            str = str.Replace("/^(Moto|MOT-)/", "");

            str = str.Replace("/-?(orange(-ls)?|vodafone|bouygues)$/i", "");
            str = str.Replace(@"/http:\/\/.+$/i", "");

            str = str.Replace(@"/^\s+|\s+$/g", "");

            return str;
        }

        #endregion

        #endregion
    }
}
