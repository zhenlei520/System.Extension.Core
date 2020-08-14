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
    public partial class UserAgentBase
    {
        /// <summary>
        ///
        /// </summary>
        internal List<BrowserRules> BrowserRuleses = new List<BrowserRules>()
        {
            new BrowserRules("AdobeAIR", @"AdobeAIR\/([0-9.]*)"),
            new BrowserRules("Awesomium", @"Awesomium\/([0-9.]*)"),
            new BrowserRules("Canvace", @"Canvace Standalone\/([0-9.]*)"),
            new BrowserRules("Ekioh", @"Ekioh\/([0-9.]*)"),
            new BrowserRules("JavaFX", @"JavaFX\/([0-9.]*)"),
            new BrowserRules("GFXe", @"GFXe\/([0-9.]*)"),
            new BrowserRules("LuaKit", @"luakit"),
            new BrowserRules("Titanium", @"Titanium\/([0-9.]*)"),
            new BrowserRules("OpenWebKitSharp", @"OpenWebKitSharp"),
            new BrowserRules("Prism", @"Prism\/([0-9.]*)"),
            new BrowserRules("Qt", @"Qt\/([0-9.]*)"),
            new BrowserRules("QtEmbedded", "QtEmbedded"),
            new BrowserRules("QtEmbedded", @"QtEmbedded.*Qt\/([0-9.]*)"),
            new BrowserRules("RhoSimulator", "RhoSimulator"),
            new BrowserRules("UWebKit", @"UWebKit\/([0-9.]*)"),
            new BrowserRules("PhantomJS", @"PhantomJS\/([0-9.]*)"),
            new BrowserRules("Google Web Preview", @"Google Web Preview"),
            new BrowserRules("Google Earth", @"Google Earth\/([0-9.]*)"),
            new BrowserRules("EA Origin", @"Origin\/([0-9.]*)"),
            new BrowserRules("SecondLife", @"SecondLife\/([0-9.]*)"),
            new BrowserRules("Valve Steam", @"Valve Steam"),
            new BrowserRules("Songbird", @"Songbird\/([0-9.]*)"),
            new BrowserRules("Thunderbird", @"Thunderbird\/([0-9.]*)"),
            new BrowserRules("Abrowser", @"Abrowser\/([0-9.]*)"),
            new BrowserRules("arora", @"[Aa]rora\/([0-9.]*)"),
            new BrowserRules("Baidu Browser", @"M?BaiduBrowser\/([0-9.]*)", RegexOptions.IgnoreCase),
            new BrowserRules("Camino", @"Camino\/([0-9.]*)"),
            new BrowserRules("Canure", @"Canure\/([0-9.]*)", "3"),
            new BrowserRules("CometBird", @"CometBird\/([0-9.]*)"),
            new BrowserRules("Comodo Dragon", @"Comodo_Dragon\/([0-9.]*)", "2"),
            new BrowserRules("Conkeror", @"[Cc]onkeror\/([0-9.]*)"),
            new BrowserRules("CoolNovo", @"(?:CoolNovo|CoolNovoChromePlus)\/([0-9.]*)", "3"),
            new BrowserRules("ChromePlus", @"ChromePlus(?:\/([0-9.]*))?$", "3"),
            new BrowserRules("Daedalus", @"Daedalus ([0-9.]*)", "2"),
            new BrowserRules("Demobrowser", @"demobrowser\/([0-9.]*)"),
            new BrowserRules("Dooble", @"Dooble(?:\/([0-9.]*))?"),
            new BrowserRules("DWB", @"dwb(?:-hg)?(?:\/([0-9.]*))?"),
            new BrowserRules("Epiphany", @"Epiphany\/([0-9.]*)"),
            new BrowserRules("FireWeb", @"FireWeb\/([0-9.]*)"),
            new BrowserRules("Flock", @"Flock\/([0-9.]*)", "3"),
            new BrowserRules("Galeon", @"Galeon\/([0-9.]*)", "3"),
            new BrowserRules("Helium", @"HeliumMobileBrowser\/([0-9.]*)"),
            new BrowserRules("iCab", @"iCab\/([0-9.]*)"),
            new BrowserRules("Iceape", @"Iceape\/([0-9.]*)"),
            new BrowserRules("IceCat", @"IceCat ([0-9.]*)"),
            new BrowserRules("Iceweasel", @"Iceweasel\/([0-9.]*)"),
            new BrowserRules("InternetSurfboard", @"InternetSurfboard\/([0-9.]*)"),
            new BrowserRules("Iron", @"Iron\/([0-9.]*)", "2"),
            new BrowserRules("Isis", @"BrowserServer"),
            new BrowserRules("Jumanji", @"jumanji"),
            new BrowserRules("Kazehakase", @"Kazehakase\/([0-9.]*)"),
            new BrowserRules("KChrome", @"KChrome\/([0-9.]*)", "3"),
            new BrowserRules("K-Meleon", @"K-Meleon\/([0-9.]*)"),
            new BrowserRules("Leechcraft", @"Leechcraft(?:\/([0-9.]*))?", "2"),
            new BrowserRules("Lightning", @"Lightning\/([0-9.]*)"),
            new BrowserRules("Lunascape", @"Lunascape[\/| ]([0-9.]*)", "3"),
            new BrowserRules("iLunascape", @"iLunascape\/([0-9.]*)", "3"),
            new BrowserRules("Maxthon", @"Maxthon[\/ ]([0-9.]*)", "3"),
            new BrowserRules("MiniBrowser", @"MiniBr?owserM\/([0-9.]*)"),
            new BrowserRules("MiniBrowser", @"MiniBrowserMobile\/([0-9.]*)"),
            new BrowserRules("MixShark", @"MixShark\/([0-9.]*)"),
            new BrowserRules("Motorola WebKit", @"MotorolaWebKit\/([0-9.]*)", "3"),
            new BrowserRules("NetFront LifeBrowser", @"NetFrontLifeBrowser\/([0-9.]*)"),
            new BrowserRules("Netscape Navigator", @"Navigator\/([0-9.]*)", "3"),
            new BrowserRules("Odyssey", @"OWB\/([0-9.]*)"),
            new BrowserRules("OmniWeb", @"OmniWeb"),
            new BrowserRules("Orca", @"Orca\/([0-9.]*)"),
            new BrowserRules("Origyn", @"Origyn Web Browser"),
            new BrowserRules("Palemoon", @"Pale[mM]oon\/([0-9.]*)"),
            new BrowserRules("Phantom", @"Phantom\/V([0-9.]*)"),
            new BrowserRules("Polaris", @"Polaris\/v?([0-9.]*)", RegexOptions.IgnoreCase, "2"),
            new BrowserRules("QtCreator", @"QtCreator\/([0-9.]*)"),
            new BrowserRules("QtQmlViewer", @"QtQmlViewer"),
            new BrowserRules("QtTestBrowser", @"QtTestBrowser\/([0-9.]*)"),
            new BrowserRules("QtWeb", @"QtWeb Internet Browser\/([0-9.]*)"),
            new BrowserRules("QupZilla", @"QupZilla\/([0-9.]*)"),
            new BrowserRules("Roccat", @"Roccat\/([0-9]\.[0-9.]*)"),
            new BrowserRules("Raven for Mac", @"Raven for Mac\/([0-9.]*)"),
            new BrowserRules("rekonq", @"rekonq"),
            new BrowserRules("RockMelt", @"RockMelt\/([0-9.]*)", "2"),
            new BrowserRules("Sleipnir", @"Sleipnir\/([0-9.]*)", "3"),
            new BrowserRules("SMBrowser", @"SMBrowser"),
            new BrowserRules("Sogou Explorer", @"SE 2.X MetaSr"),
            new BrowserRules("Snowshoe", @"Snowshoe\/([0-9.]*)", "2"),
            new BrowserRules("Sputnik", @"Sputnik\/([0-9.]*)", RegexOptions.IgnoreCase, "3"),
            new BrowserRules("Stainless", @"Stainless\/([0-9.]*)"),
            new BrowserRules("SunChrome", @"SunChrome\/([0-9.]*)"),
            new BrowserRules("Surf", @"Surf\/([0-9.]*)"),
            new BrowserRules("TaoBrowser", @"TaoBrowser\/([0-9.]*)", "2"),
            new BrowserRules("TaomeeBrowser", @"TaomeeBrowser\/([0-9.]*)", "2"),
            new BrowserRules("TazWeb", @"TazWeb"),
            new BrowserRules("Viera", @"Viera\/([0-9.]*)"),
            new BrowserRules("Villanova", @"Villanova\/([0-9.]*)", "3"),
            new BrowserRules("Wavelink Velocity", @"Wavelink Velocity Browser\/([0-9.]*)", "2"),
            new BrowserRules("WebPositive", @"WebPositive"),
            new BrowserRules("WebRender", @"WebRender"),
            new BrowserRules("Wyzo", @"Wyzo\/([0-9.]*)", "3"),
            new BrowserRules("Zetakey", @"Zetakey Webkit\/([0-9.]*)"),
            new BrowserRules("Zetakey", @"Zetakey\/([0-9.]*)")
        };


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
        private bool CheckUserAgent(string userAgent, string regex, RegexOptions options)
        {
            return CheckUserAgent(userAgent, regex, options, out MatchCollection match);
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="match"></param>
        private bool CheckUserAgent(string userAgent, string regex, out MatchCollection match)
        {
            return CheckUserAgent(userAgent, regex, RegexOptions.None, out match);
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options"></param>
        /// <param name="match"></param>
        private bool CheckUserAgent(string userAgent, string regex, RegexOptions options, out MatchCollection match)
        {
            match = new Regex(regex, options).Matches(userAgent);
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
            CheckUserAgent(userAgent, regex, RegexOptions.None, action);
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options"></param>
        /// <param name="action">验证通过后执行</param>
        private void CheckUserAgent(string userAgent, string regex, RegexOptions options,
            Action<MatchCollection> action)
        {
            CheckUserAgent(userAgent, regex, options, (mc, reg) => { action?.Invoke(mc); });
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="action">验证通过后执行</param>
        private void CheckUserAgent(string userAgent, Regex regex, Action<MatchCollection> action)
        {
            CheckUserAgent(userAgent, regex, (mc, reg) => { action?.Invoke(mc); });
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="action">验证通过后执行</param>
        private void CheckUserAgent(string userAgent, string regex, Action<MatchCollection, string> action)
        {
            CheckUserAgent(userAgent, regex, RegexOptions.None, action);
        }


        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="action">验证通过后执行</param>
        private void CheckUserAgent(string userAgent, Regex regex, Action<MatchCollection, Regex> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MatchCollection match = regex.Matches(userAgent);
            if (match.Count > 0)
            {
                action.Invoke(match, regex);
            }
        }

        /// <summary>
        /// 检查UserAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="options"></param>
        /// <param name="action">验证通过后执行</param>
        private void CheckUserAgent(string userAgent, string regex, RegexOptions options,
            Action<MatchCollection, string> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MatchCollection match = new Regex(regex, options).Matches(userAgent);
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

            str = str.ReplaceRegex("_TD$/", "");
            str = str.ReplaceRegex("_CMCC$/", "");

            str = str.ReplaceRegex("_/g", " ");
            str = str.ReplaceRegex(@"^\s+|\s+$/g", "");
            str = str.ReplaceRegex(@"\/[^/]+$/", "");
            str = str.ReplaceRegex(@"\/[^/]+ Android\/.*/", "");

            str = str.ReplaceRegex("^tita on /", "");
            str = str.ReplaceRegex("^Android on /", "");
            str = str.ReplaceRegex("^Android for /", "");
            str = str.ReplaceRegex("^ICS AOSP on /", "");
            str = str.ReplaceRegex("^Full AOSP on /", "");
            str = str.ReplaceRegex("^Full Android on /", "");
            str = str.ReplaceRegex("^Full Cappuccino on /", "");
            str = str.ReplaceRegex("^Full MIPS Android on /", "");
            str = str.ReplaceRegex("^Full Android/", "");

            str = str.ReplaceRegex("^Acer ?", RegexOptions.IgnoreCase, "");
            str = str.ReplaceRegex("^Iconia /", "");
            str = str.ReplaceRegex("^Ainol /", "");
            str = str.ReplaceRegex("^Coolpad ?", RegexOptions.IgnoreCase, "Coolpad ");
            str = str.ReplaceRegex("^ALCATEL /", "");
            str = str.ReplaceRegex("^Alcatel OT-(.*)/", "one touch $1");
            str = str.ReplaceRegex("^YL-/", "");
            str = str.ReplaceRegex("^Novo7 ?", RegexOptions.IgnoreCase, "Novo7 ");
            str = str.ReplaceRegex("^GIONEE /", "");
            str = str.ReplaceRegex("^HW-/", "");
            str = str.ReplaceRegex("^Huawei[ -]", RegexOptions.IgnoreCase, "Huawei ");
            str = str.ReplaceRegex("^SAMSUNG[ -]", RegexOptions.IgnoreCase, "");
            str = str.ReplaceRegex("^SonyEricsson/", "");
            str = str.ReplaceRegex("^Lenovo Lenovo/", "Lenovo");
            str = str.ReplaceRegex("^LNV-Lenovo/", "Lenovo");
            str = str.ReplaceRegex("^Lenovo-/", "Lenovo ");
            str = str.ReplaceRegex(@"^(LG)[ _\/]/", "$1-");
            str = str.ReplaceRegex(@"^(HTC.*)\s(?:v|V)?[0-9.]+$/", "$1");
            str = str.ReplaceRegex(@"^(HTC)[-\/]/", "$1 ");
            str = str.ReplaceRegex("^(HTC)([A-Z][0-9][0-9][0-9])/", "$1 $2");
            str = str.ReplaceRegex(@"^(Motorola[\s|-])/", "");
            str = str.ReplaceRegex("^(Moto|MOT-)/", "");

            str = str.ReplaceRegex("-?(orange(-ls)?|vodafone|bouygues)$", RegexOptions.IgnoreCase, "");
            str = str.ReplaceRegex(@"http:\/\/.+$", RegexOptions.IgnoreCase, "");

            str = str.ReplaceRegex(@"^\s+|\s+$/g", "");

            return str;
        }

        #endregion

        #region 转换版本号

        /// <summary>
        /// 转换版本号
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private decimal ParseVersion(string version)
        {
            var components = version.Split('.');
            string major = components.Shift();
            return (major + '.' + components.ToList().ConvertListToString("")).ConvertToDecimal(0);
        }

        #endregion

        #region 得到版本信息

        /// <summary>
        /// 得到版本信息
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="regexDefault"></param>
        /// <returns></returns>
        internal string GetVersionResult(MatchCollection mc,RegexDefault regexDefault=null)
        {
            string str = "";
            if (mc.Count > 1)
            {
                str= mc[1].SafeString();
            }
            else
            {
                str = mc[0].SafeString();
            }

            if (regexDefault == null)
            {
                return str;
            }

            return str.Match(_regexConfigurations.GetRegexRule(regexDefault))[0];
        }

        /// <summary>
        /// 得到版本信息
        /// </summary>
        /// <param name="mc"></param>
        /// <returns></returns>
        internal Versions GetVersion(MatchCollection mc)
        {
            return new Versions(GetVersionResult(mc,RegexDefault.FloatingPointNumbers));
        }

        #endregion

        #endregion
    }
}
