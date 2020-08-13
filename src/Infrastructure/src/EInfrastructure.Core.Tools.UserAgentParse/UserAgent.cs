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
        /// <summary>
        ///
        /// </summary>
        public string UA { get; private set; }


        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public UserAgent(string userAgent)
        {
            UA = userAgent;
        }

        #region 得到结果

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <returns></returns>
        public UserAgentBase Execute()
        {
            var uaData = new UserAgentBase(UA);
            string[] mc, tempMc;
            if (uaData.Device.DeviceType.Equals(DeviceType.Mobile) ||
                uaData.Device.DeviceType.Equals(DeviceType.Tablet))
            {
                if ((mc = UA.Match(
                        @"/(ZTE|Samsung|Motorola|HTC|Coolpad|Huawei|Lenovo|LG|Sony Ericsson|Oppo|TCL|Vivo|Sony|Meizu|Nokia)/i")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = mc[1].SafeString();
                    if (!string.IsNullOrEmpty(uaData.Device.Name) &&
                        uaData.Device.Name.IndexOf(mc[1].SafeString(), StringComparison.Ordinal) >= 0)
                    {
                        uaData.Device.Name = uaData.Device.Name.Replace(mc[1].SafeString(), "");
                    }
                }

                // handle Apple
                // 苹果就这3种iPod iPad iPhone
                if ((mc = UA.Match(@"/(iPod|iPad|iPhone)/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Apple";
                    uaData.Device.Name = mc[1].SafeString();
                }
                // handle Samsung
                // 特殊型号可能以xxx-开头 或者直接空格接型号 兼容build结尾或直接)结尾
                // Galaxy nexus才是三星 nexus是google手机
                // 三星手机类型：galaxy xxx|SM-xxx|GT-xxx|SCH-xxx|SGH-xxx|SPH-xxx|SHW-xxx  若这些均未匹配到，则启用在中关村在线爬取到的机型白名单进行判断
                else if ((mc = UA.Match(
                        @"/[-\s](Galaxy[\s-_]nexus|Galaxy[\s-_]\w*[\s-_]\w*|Galaxy[\s-_]\w*|SM-\w*|GT-\w*|s[cgp]h-\w*|shw-\w*|ATIV|i9070|omnia|s7568|A3000|A3009|A5000|A5009|A7000|A7009|A8000|C101|C1116|C1158|E400|E500F|E7000|E7009|G3139D|G3502|G3502i|G3508|G3508J|G3508i|G3509|G3509i|G3558|G3559|G3568V|G3586V|G3589W|G3606|G3608|G3609|G3812|G388F|G5108|G5108Q|G5109|G5306W|G5308W|G5309W|G550|G600|G7106|G7108|G7108V|G7109|G7200|G720NO|G7508Q|G7509|G8508S|G8509V|G9006V|G9006W|G9008V|G9008W|G9009D|G9009W|G9198|G9200|G9208|G9209|G9250|G9280|I535|I679|I739|I8190N|I8262|I879|I879E|I889|I9000|I9060|I9082|I9082C|I9082i|I9100|I9100G|I9108|I9128|I9128E|I9128i|I9152|I9152P|I9158|I9158P|I9158V|I9168|I9168i|I9190|I9192|I9195|I9195I|I9200|I9208|I9220|I9228|I9260|I9268|I9300|I9300i|I9305|I9308|I9308i|I939|I939D|I939i|I9500|I9502|I9505|I9507V|I9508|I9508V|I959|J100|J110|J5008|J7008|N7100|N7102|N7105|N7108|N7108D|N719|N750|N7505|N7506V|N7508V|N7509V|N900|N9002|N9005|N9006|N9008|N9008S|N9008V|N9009|N9100|N9106W|N9108V|N9109W|N9150|N916|N9200|P709|P709E|P729|S6358|S7278|S7278U|S7562C|S7562i|S7898i|b9388)[\s\)]/i")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = "Samsung";
                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：[A-Z]+[0-9]+[A-Z]*, 例如 G9006 G9006V 其实应该是G9006 另外三星只保留3位
                    uaData.Device.Name = mc[1].SafeString()
                        .ReplaceRegex(@"/Galaxy S VI/i", "Galaxy S6")
                        .ReplaceRegex(@"/Galaxy S V/i", "Galaxy S5")
                        .ReplaceRegex(@"/Galaxy S IV/i", "Galaxy S4")
                        .ReplaceRegex(@"/Galaxy s III/i", "Galaxy S3")
                        .ReplaceRegex(@"/Galaxy S II/i", "Galaxy S2")
                        .ReplaceRegex(@"/Galaxy S I/i", "Galaxy S1")
                        .ReplaceRegex(@"/([a-z]+[0-9]{3})[0-9]?[a-z]*/i", "$1");
                }
                // 针对三星已经匹配出的数据做处理
                else if (uaData.Device.Manufacturer.SafeString().ToLower() ==
                    "samsung" && !string.IsNullOrEmpty(uaData.Device.Name))
                {
                    uaData.Device.Name = uaData.Device.Name.SafeString()
                        .ReplaceRegex(@"/Galaxy S VI/i", "Galaxy S6")
                        .ReplaceRegex(@"/Galaxy S V/i", "Galaxy S5")
                        .ReplaceRegex(@"/Galaxy S IV/i", "Galaxy S4")
                        .ReplaceRegex(@"/Galaxy s III/i", "Galaxy S3")
                        .ReplaceRegex(@"/Galaxy S II/i", "Galaxy S2")
                        .ReplaceRegex(@"/Galaxy S I/i", "Galaxy S1")
                        .ReplaceRegex(@"/([a-z]+[0-9]{3})[0-9]?[a-z]*/i", "$1");
                }
                // handle Huawei
                // 兼容build结尾或直接)结尾
                // 华为机型特征：Huawei[\s-_](\w*[-_]?\w*)  或者以 7D-  ALE-  CHE-等开头
                else if ((mc = UA.Match(
                        @"/(Huawei[\s-_](\w*[-_]?\w*)|\s(7D-\w*|ALE-\w*|ATH-\w*|CHE-\w*|CHM-\w*|Che1-\w*|Che2-\w*|D2-\w*|G616-\w*|G620S-\w*|G621-\w*|G660-\w*|G750-\w*|GRA-\w*|Hol-\w*|MT2-\w*|MT7-\w*|PE-\w*|PLK-\w*|SC-\w*|SCL-\w*|H60-\w*|H30-\w*)[\s\)])/i")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = "Huawei";
                    if (!string.IsNullOrEmpty(mc[2].SafeString()))
                    {
                        uaData.Device.Name = mc[2].SafeString();
                    }
                    else if (!string.IsNullOrEmpty(mc[3].SafeString()))
                    {
                        uaData.Device.Name = mc[3].SafeString();
                    }

                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：xxx-[A-Z][0-9]+ 例如  H30-L01  H30-L00  H30-L20  都应该是 H30-L
                    // h30-l  h30-h  h30-t 都是H30
                    if ((mc = UA.Match(@"/(\w*)[\s-_]+[a-z0-9]+/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1].SafeString();
                    }
                }
                // handle Xiaomi
                // 兼容build结尾或直接)结尾 以及特殊的HM处理方案(build/hm2013011)
                // xiaomi手机类型: mi m1 m2 m3 hm 开头
                // hongmi有特殊判断build/hm2015011
                else if ((mc = UA.Match(@"/;\s(mi|m1|m2|m3|m4|hm)(\s*\w*)[\s\)]/i")).Length > 0)
                {
                    if ((tempMc = UA.Match(@"/(meitu|MediaPad)/i")).Length > 0)
                    {
                        // 美图手机名字冒充小米 比如 meitu m4 mizhi
                        uaData.Device.Manufacturer = tempMc[1].SafeString();
                        uaData.Device.Name = "";
                    }
                    // 若匹配出的 match[2]没空格 会出现很多例如 mizi mizhi miha 但也会出现mi3 minote之类 特殊处理下
                    else if (mc[2].SafeString().Length > 0 && !new Regex(@"(/\s/)").IsMatch(mc[2].SafeString()))
                    {
                        if ((tempMc = mc[2].Match(@"/(\d)/i")).Length > 0)
                        {
                            uaData.Device.Name = mc[1].SafeString() + "-" + tempMc[1].SafeString();
                        }
                    }
                    else
                    {
                        uaData.Device.Manufacturer = "Xiaomi";
                        if (!string.IsNullOrEmpty(mc[2].SafeString()) && mc[2].SafeString().Length > 0)
                        {
                            mc[2] = mc[2].ReplaceRegex(@"/\s/", "");
                            uaData.Device.Name =
                                (mc[1].Substring(mc[1].Length - 2) + '-' + mc[2]).ReplaceRegex(@"/m(\d)-/i", "MI-$1");
                        }
                        else
                        {
                            uaData.Device.Name = (mc[1].Substring(mc[1].Length - 2)).ReplaceRegex(@"/m(\d)/i", "MI-$1");
                        }

                        // 解决移动联通等不同发行版导致的机型不同问题
                        // 特征：mi-3c,例如mi-4LTE mi-4 其实应该是 mi-4
                        if (uaData.Device.Name.Test(@"/(mi|hm)(-\d)/i"))
                        {
                            // 看看是不是 MI-3S  MI-4S....
                            if ((mc = uaData.Device.Name.Match(@"/(mi|hm)(-\ds)/i")).Length > 0)
                            {
                                uaData.Device.Name = mc[1] + mc[2];
                            }
                            // 防止 MI-20150XX等滥竽充数成为MI-2
                            else if ((mc = uaData.Device.Name.Match(@"/(mi|hm)(-\d{2})/i")).Length > 0)
                            {
                                uaData.Device.Name = mc[1];
                            }
                            // 将mi-3c mi-3a mi-3w等合为mi-3
                            else if ((mc = uaData.Device.Name.Match(@"/(mi|hm)(-\d)[A-Z]/i")).Length > 0)
                            {
                                uaData.Device.Name = mc[1] + mc[2];
                            }
                        }

                        // 去除 mi-4g这样的东西
                        if ((mc = uaData.Device.Name.Match(@"/(mi|hm)(-\dg)/i")).Length > 0)
                        {
                            uaData.Device.Name = mc[1];
                        }
                    }
                }
                else if (UA.Test(@"/build\/HM\d{0,7}\)/i"))
                {
                    uaData.Device.Manufacturer = "Xiaomi";
                    uaData.Device.Name = "HM";
                }
                else if ((mc = UA.Match(@"/redmi\s?(\d+)?/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Xiaomi";
                    uaData.Device.Name = "HM-" + mc[1];
                }
                else if (!uaData.Device.Manufacturer.IsNullOrEmpty() && uaData.Device.Manufacturer.ToLower() ==
                    "xiaomi" && !uaData.Device.Name.IsNullOrEmpty())
                {
                    // 针对通过base库判断出数据时命名风格不同。特殊处理适配如下
                    if ((mc = uaData.Device.Name.Match(@"/mi-one/i")).Length > 0)
                    {
                        uaData.Device.Name = "MI-1";
                    }
                    // mi 2
                    else if ((mc = uaData.Device.Name.Match(@"/mi-two/i")).Length > 0)
                    {
                        uaData.Device.Name = "MI-2";
                    }
                    // 20150xxx2014501
                    else if ((mc = uaData.Device.Name.Match(@"/\d{6}/i")).Length > 0)
                    {
                        uaData.Device.Name = "";
                    }
                    else if ((mc = uaData.Device.Name.Match(@"/redmi/i")).Length > 0)
                    {
                        uaData.Device.Name = uaData.Device.Name.ToUpper().ReplaceRegex(@"/redmi/i", "HM");
                    }
                    // m1 m2 m3 写法不标准 另外判断是否是 m1-s
                    else if ((mc = uaData.Device.Name.Match(@"/(m\d)[\s-_](s?)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1].ReplaceRegex(@"/m/", "MI-") + mc[2];
                    }
                    // mi-2w  mi-3w 等格式化为mi-2  mi-3
                    else if ((mc = uaData.Device.Name.Match(@"/(hm|mi)[\s-_](\d?)[a-rt-z]/i")).Length > 0)
                    {
                        if ((tempMc = uaData.Device.Name.Match(@"/(hm|mi)[\s-_](\d?)[a-rt-z]/i")).Length > 0)
                        {
                            uaData.Device.Name = tempMc[1] + '-' + tempMc[2] + tempMc[3];
                        }
                        else
                        {
                            uaData.Device.Name = !mc[2].IsNullOrEmpty() ? mc[1] + '-' + mc[2] : mc[1];
                        }
                    }
                    // 处理hm
                    else if ((mc = uaData.Device.Name.Match(@"/hm/i")).Length > 0)
                    {
                        // 判断是不是 hm-201xxx充数
                        if ((mc = uaData.Device.Name.Match(@"/(hm)[\s-_](\d{2})/i")).Length > 0)
                        {
                            uaData.Device.Name = "HM";
                        }
                        // 判断是不是 hm-2s hm-1s
                        else if ((mc = uaData.Device.Name.Match(@"/(hm)[\s-_](\ds)/i")).Length > 0)
                        {
                            uaData.Device.Name = "HM-" + mc[2];
                        }
                        else if ((mc = uaData.Device.Name.Match(@"/(hm)[\s-_](\d)[a-z]/i")).Length > 0)
                        {
                            uaData.Device.Name = "HM-" + mc[2];
                        }
                        else
                        {
                            uaData.Device.Name = "HM";
                        }

                        // 过滤类似 2g 3g等数据
                        if (uaData.Device.Name.Test(@"/hm-\dg/"))
                        {
                            uaData.Device.Name = "HM";
                        }
                    }
                }
                else if ((mc = UA.Match(
                        @"/(vivo[\s-_](\w*)|\s(E1\w?|E3\w?|E5\w?|V1\w?|V2\w?|S11\w?|S12\w?|S1\w?|S3\w?|S6\w?|S7\w?|S9\w?|X1\w?|X3\w?|X520\w?|X5\w?|X5Max|X5Max+|X5Pro|X5SL|X710F|X710L|Xplay|Xshot|Xpaly3S|Y11\w?|Y11i\w?|Y11i\w?|Y13\w?|Y15\w?|Y17\w?|Y18\w?|Y19\w?|Y1\w?|Y20\w?|Y22\w?|Y22i\w?|Y23\w?|Y27\w?|Y28\w?|Y29\w?|Y33\w?|Y37\w?|Y3\w?|Y613\w?|Y622\w?|Y627\w?|Y913\w?|Y923\w?|Y927\w?|Y928\w?|Y929\w?|Y937\w?)[\s\)])/i")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = "Vivo";
                    uaData.Device.Name = mc[1];
                    // 首先剔除 viv-  vivo-  bbg- 等打头的内容
                    uaData.Device.Name = uaData.Device.Name.ReplaceRegex(@"/(viv[\s-_]|vivo[\s-_]|bbg[\s-_])/i", "");
                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：[A-Z][0-9]+[A-Z] 例如  X5F X5L X5M X5iL 都应该是 X5
                    if ((mc = uaData.Device.Name.Match(@"/([a-z]+[0-9]+)i?[a-z]?[\s-_]?/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1];
                    }
                }
                // handle Oppo
                else if ((mc = UA.Match(
                        @"/(Oppo[\s-_](\w*)|\s(1100|1105|1107|3000|3005|3007|6607|A100|A103|A105|A105K|A109|A109K|A11|A113|A115|A115K|A121|A125|A127|A129|A201|A203|A209|A31|A31c|A31t|A31u|A51kc|A520|A613|A615|A617|E21W|Find|Mirror|N5110|N5117|N5207|N5209|R2010|R2017|R6007|R7005|R7007|R7c|R7t|R8000|R8007|R801|R805|R807|R809T|R8107|R8109|R811|R811W|R813T|R815T|R815W|R817|R819T|R8200|R8205|R8207|R821T|R823T|R827T|R830|R830S|R831S|R831T|R833T|R850|Real|T703|U2S|U521|U525|U529|U539|U701|U701T|U705T|U705W|X9000|X9007|X903|X905|X9070|X9077|X909|Z101|R829T)[\s\)])/i")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = "Oppo";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[2];
                    }
                    else if (!mc[3].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[3];
                    }

                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：[A-Z][0-9]+[A-Z] 例如  A31c A31s 都应该是 A31
                    // 对 Plus 做特殊处理
                    if ((mc = uaData.Device.Name.Match(@"/([a-z]+[0-9]+)-?(plus)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1] + '-' + mc[2];
                    }
                    else if ((mc = uaData.Device.Name.Match(@"/(\w*-?[a-z]+[0-9]+)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1];
                    }
                }
                else if (!uaData.Device.Manufacturer.IsNullOrEmpty() &&
                         uaData.Device.Manufacturer.ToLower() == "oppo" && !uaData.Device.Name.IsNullOrEmpty())
                {
                    // 针对base库的数据做数据格式化处理
                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：[A-Z][0-9]+[A-Z] 例如  A31c A31s 都应该是 A31
                    // 对 Plus 做特殊处理
                    if ((mc = uaData.Device.Name.Match(@"/([a-z]+[0-9]+)-?(plus)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1] + '-' + mc[2];
                    }
                    else if ((mc = uaData.Device.Name.Match(@"/(\w*-?[a-z]+[0-9]+)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1];
                    }
                }
                // handle Lenovo
                // 兼容build结尾或直接)结尾 兼容Lenovo-xxx/xxx以及Leveno xxx build等
                else if ((mc = UA.Match(
                        @"/(Lenovo[\s-_](\w*[-_]?\w*)|\s(A3580|A3860|A5500|A5600|A5860|A7600|A806|A800|A808T|A808T-I|A936|A938t|A788t|K30-E|K30-T|K30-W|K50-T3s|K50-T5|K80M|K910|K910e|K920|S90-e|S90-t|S90-u|S968T|X2-CU|X2-TO|Z90-3|Z90-7)[\s\)])/i")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = "Lenovo";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[2];
                    }
                    else if (!mc[3].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[3];
                    }

                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：[A-Z][0-9]+[A-Z] 例如  A360t A360 都应该是 A360
                    if ((mc = uaData.Device.Name.Match(@"/([a-z]+[0-9]+)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1];
                    }
                }
                // handle coolpad
                else if ((mc = UA.Match(
                        @"/(Coolpad[\s-_](\w*)|\s(7295C|7298A|7620L|8908|8085|8970L|9190L|Y80D)[\s\)])/i"))
                    .Length > 0)
                {
                    uaData.Device.Manufacturer = "Coolpad";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[2];
                    }
                    else if (!mc[3].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[3];
                    }

                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：[A-Z][0-9]+[A-Z] 例如  8297-t01 8297-c01 8297w 都应该是 8297
                    if ((mc = uaData.Device.Name.Match(@"/([a-z]?[0-9]+)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1];
                    }
                }
                else if (!uaData.Device.Manufacturer.IsNullOrEmpty() &&
                         uaData.Device.Manufacturer.ToLower() == "coolpad" && !uaData.Device.Name.IsNullOrEmpty())
                {
                    // base 库适配
                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：[A-Z][0-9]+[A-Z] 例如  8297-t01 8297-c01 8297w 都应该是 8297
                    if ((mc = uaData.Device.Name.Match(@"([a-z]?[0-9]+)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1];
                    }
                }
                // handle meizu
                else if ((mc = UA.Match(@"/\s(mx\d*\w*|mz-(\w*))\s(\w*)\s*\w*\s*build/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Meizu";
                    var tmpModel = !mc[2].IsNullOrEmpty() ? mc[2] : mc[1];
                    if (!mc[3].IsNullOrEmpty())
                    {
                        uaData.Device.Name = tmpModel + '-' + mc[3];
                    }
                    else
                    {
                        uaData.Device.Name = tmpModel + "";
                    }
                }
                else if ((mc = UA.Match(@"/M463C|M35\d/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Meizu";
                    uaData.Device.Name = mc[1];
                }
                // handle htc
                else if ((mc = UA.Match(
                        @"/(Htc[-_\s](\w*)|\s(601e|606w|608t|609d|610t|6160|619d|620G|626d|626s|626t|626w|709d|801e|802d|802t|802w|809D|816d|816e|816t|816v|816w|826d|826s|826t|826w|828w|901e|919d|A310e|A50AML|A510e|A620d|A620e|A620t|A810e|A9191|Aero|C620d|C620e|C620t|D316d|D516d|D516t|D516w|D820mt|D820mu|D820t|D820ts|D820u|D820us|E9pt|E9pw|E9sw|E9t|HD7S|M8Et|M8Sd|M8St|M8Sw|M8d|M8e|M8s|M8si|M8t|M8w|M9W|M9ew|Phablet|S510b|S510e|S610d|S710d|S710e|S720e|S720t|T327t|T328d|T328t|T328w|T329d|T329t|T329w|T528d|T528t|T528w|T8698|WF5w|X315e|X710e|X715e|X720d|X920e|Z560e|Z710e|Z710t|Z715e)[\s\)])/")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = "Htc";
                    uaData.Device.Name = mc[1];
                }
                // handle Gionee
                else if ((mc = UA.Match(@"/(Gionee[\s-_](\w*)|\s(GN\d+\w*)[\s\)])/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Gionee";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[2];
                    }
                    else if (!mc[3].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[3];
                    }
                }
                // handle LG
                else if ((mc = UA.Match(
                        @"/(LG[-_](\w*)|\s(D728|D729|D802|D855|D856|D857|D858|D859|E985T|F100L|F460|H778|H818|H819|P895|VW820)[\s\)])/i")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = "Lg";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[2];
                    }
                    else if (!mc[3].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[3];
                    }
                }
                // handle tcl
                else if ((mc = UA.Match(@"/(Tcl[\s-_](\w*)|\s(H916T|P588L|P618L|P620M|P728M)[\s\)])/")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Tcl";
                    uaData.Device.Name = mc[1];
                }
                // ZTE
                else if ((mc = UA.Match(@"/(V9180|N918)/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Zte";
                    uaData.Device.Name = mc[1];
                }
                else if (!uaData.Device.Manufacturer.IsNullOrEmpty() && uaData.Device.Manufacturer.ToLower() == "zte" &&
                         !uaData.Device.Name.IsNullOrEmpty())
                {
                    // base 库适配
                    // 解决移动联通等不同发行版导致的机型不同问题
                    // 特征：[A-Z][0-9]+[A-Z] 例如  Q505T Q505u 都应该是 Q505
                    if ((mc = uaData.Device.Name.Match(@"/([a-z]?[0-9]+)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1];
                    }
                }
                // UIMI
                else if ((mc = UA.Match(@"/(UIMI\w*|umi\w*)[\s-_](\w*)\s*\w*\s*build/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Uimi";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[1] + '-' + mc[2];
                    }
                    else
                    {
                        uaData.Device.Name = mc[1] + "";
                    }
                }
                // eton
                else if ((mc = UA.Match(@"/eton[\s-_](\w*)/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Eton";
                    uaData.Device.Name = mc[1];
                }
                // Smartisan
                else if ((mc = UA.Match(@"/(SM705|SM701|YQ601|YQ603)/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Smartisan";
                    var temp = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("SM705", "T1"),
                        new KeyValuePair<string, string>("SM701", "T1"),
                        new KeyValuePair<string, string>("YQ601", "U1"),
                        new KeyValuePair<string, string>("YQ603", "U1")
                    };
                    if (temp.Any(x => x.Key == mc[1]))
                    {
                        uaData.Device.Name = temp.Where(x => x.Key == mc[1]).Select(x => x.Value).FirstOrDefault();
                    }
                    else
                    {
                        uaData.Device.Name = mc[1];
                    }
                }
                // handle Asus
                else if ((mc = UA.Match(
                        @"/(Asus[\s-_](\w*)|\s(A500CG|A500KL|A501CG|A600CG|PF400CG|PF500KL|T001|X002|X003|ZC500TG|ZE550ML|ZE551ML)[\s\)])/i")
                    ).Length > 0)
                {
                    uaData.Device.Manufacturer = "Asus";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[2];
                    }
                    else if (!mc[3].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[3];
                    }
                }
// handle nubia
                else if ((mc = UA.Match(@"/(Nubia[-_\s](\w*)|(NX501|NX505J|NX506J|NX507J|NX503A|nx\d+\w*)[\s\)])/i"))
                    .Length > 0)
                {
                    uaData.Device.Manufacturer = "Nubia";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[2];
                    }
                    else if (!mc[3].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[3];
                    }
                }
                // handle haier
                else if ((mc = UA.Match(@"/(HT-\w*)|Haier[\s-_](\w*-?\w*)/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Haier";
                    if (!mc[1].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[1];
                    }
                    else if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = mc[2];
                    }
                }
                // tianyu
                else if ((mc = UA.Match(@"/K-Touch[\s-_](tou\s?ch\s?(\d)|\w*)/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "K-Touch";
                    if (!mc[2].IsNullOrEmpty())
                    {
                        uaData.Device.Name = "Ktouch" + mc[2];
                    }
                    else
                    {
                        uaData.Device.Name = mc[1];
                    }
                }

                // DOOV
                else if ((mc = UA.Match(@"/Doov[\s-_](\w*)/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Doov";
                    uaData.Device.Name = mc[1];
                }
                // coobee
                else if (UA.Test(@"/koobee/i"))
                {
                    uaData.Device.Manufacturer = "koobee";
                }

                // sony
                else if (UA.Test(@"/C69/i"))
                {
                    uaData.Device.Manufacturer = "Sony";
                }

                // haojixing
                else if (UA.Test(@"/N787|N818S/i"))
                {
                    uaData.Device.Manufacturer = "Haojixing";
                }

                // haisense
                else if ((mc = UA.Match(@"/(hs-|Hisense[\s-_])(\w*)/i")).Length > 0)
                {
                    uaData.Device.Manufacturer = "Hisense";
                    uaData.Device.Name = mc[2];
                }

                // format the style of manufacturer
                if (!uaData.Device.Manufacturer.IsNullOrEmpty())
                {
                    uaData.Device.Manufacturer = uaData.Device.Manufacturer.Substring(0, 1).ToUpper() +
                                                 uaData.Device.Manufacturer.Substring(1).ToLower();
                }

                // format the style of model
                if (!uaData.Device.Name.IsNullOrEmpty())
                {
                    uaData.Device.Name = uaData.Device.Name.SafeString().ToUpper().ReplaceRegex(@"/-+|_+|\s+/g", " ");
                    uaData.Device.Name = uaData.Device.Name.Match(@"/\s*(\w*\s*\w*)/")[1].ReplaceRegex(@"/\s+/", "-");

                    // 针对三星、华为做去重的特殊处理
                    if (uaData.Device.Manufacturer == "Samsung")
                    {
                        var temp = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("SCH-I95", "GT-I950"),
                            new KeyValuePair<string, string>("SCH-I93", "GT-I930"),
                            new KeyValuePair<string, string>("SCH-I86", "GT-I855"),
                            new KeyValuePair<string, string>("SCH-N71", "GT-N710"),
                            new KeyValuePair<string, string>("SCH-I73", "GT-S789"),
                            new KeyValuePair<string, string>("SCH-P70", "GT-I915"),
                        };
                        if (temp.Any(x => x.Key == uaData.Device.Name))
                        {
                            uaData.Device.Name = temp.Where(x => x.Key == uaData.Device.Name)
                                .Select(x => x.Value).FirstOrDefault();
                        }
                    }
                    else if (uaData.Device.Manufacturer == "Huawei")
                    {
                        var temp = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("CHE1", "CHE"),
                            new KeyValuePair<string, string>("CHE2", "CHE"),
                            new KeyValuePair<string, string>("G620S", "G621"),
                            new KeyValuePair<string, string>("C8817D", "G621")
                        };
                        if (temp.Any(x => x.Key == uaData.Device.Name))
                        {
                            uaData.Device.Name = temp.Where(x => x.Key == uaData.Device.Name)
                                .Select(x => x.Value).FirstOrDefault();
                        }
                    }
                }

                // 针对xiaomi 的部分数据没有格式化成功，格式化1次
                if (!uaData.Device.Manufacturer.IsNullOrEmpty() && uaData.Device.Manufacturer == "Xiaomi")
                {
                    if ((mc = uaData.Device.Name.Match(@"/(hm|mi)-(note)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1] + '-' + mc[2];
                    }
                    else if ((mc = uaData.Device.Name.Match(@"/(hm|mi)-(\ds?)/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1] + '-' + mc[2];
                    }
                    else if ((mc = uaData.Device.Name.Match(@"/(hm|mi)-(\d)[a-rt-z]/i")).Length > 0)
                    {
                        uaData.Device.Name = mc[1] + '-' + mc[2];
                    }
                }
            }

            // handle browser
            // if (!uaData.browser.name) {
            // ua = ua.toLowerCase();
            if (uaData.Device.DeviceType.Equals(DeviceType.Pc))
            {
                /*
                 * 360 security Explorer
                 */
                if ((mc = UA.Match(@"/360se(?:[ \/]([\w.]+))?/i")).Length > 0)
                {
                    uaData.Browser.Name = "360 security Explorer";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * the world
                 */
                else if ((mc = UA.Match(@"/the world(?:[ \/]([\w.]+))?/i")).Length > 0)
                {
                    uaData.Browser.Name = "the world";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * tencenttraveler
                 */
                else if ((mc = UA.Match(@"/tencenttraveler ([\w.]+)/i")).Length > 0)
                {
                    uaData.Browser.Name = "tencenttraveler";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * LBBROWSER
                 */
                else if ((mc = UA.Match(@"/LBBROWSER/i")).Length > 0)
                {
                    uaData.Browser.Name = "LBBROWSER";
                }
            }
            else if (uaData.Device.DeviceType.Equals(DeviceType.Mobile) ||
                     uaData.Device.DeviceType.Equals(DeviceType.Tablet))
            {
                /**
                 * BaiduHD
                 */
                if ((mc = UA.Match(@"/BaiduHD\s+([\w.]+)/i")).Length > 0)
                {
                    uaData.Browser.Name = "BaiduHD";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * 360 Browser
                 */
                else if ((mc = UA.Match(@"/360.s*aphone\s*browser\s*\(version\s*([\w.]+)\)/i")).Length > 0)
                {
                    uaData.Browser.Name = "360 Browser";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * Baidu Browser
                 */
                else if ((mc = UA.Match(@"/flyflow\/([\w.]+)/i")).Length > 0)
                {
                    uaData.Browser.Name = "Baidu Browser";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * Baidu HD
                 */
                else if ((mc = UA.Match(@"/baiduhd ([\w.]+)/i")).Length > 0)
                {
                    uaData.Browser.Name = "Baidu HD";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * baidubrowser
                 */
                else if ((mc = UA.Match(@"/baidubrowser\/([\d\.]+)\s/i")).Length > 0)
                {
                    uaData.Browser.Name = "baidubrowser";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * LieBaoFast
                 */
                else if ((mc = UA.Match(@"/LieBaoFast\/([\w.]+)/i")).Length > 0)
                {
                    uaData.Browser.Name = "LieBao Fast";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * LieBao
                 */
                else if ((mc = UA.Match(@"/LieBao\/([\w.]+)/i")).Length > 0)
                {
                    uaData.Browser.Name = "LieBao";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * SOUGOU
                 */
                else if ((mc = UA.Match(@"/Sogou\w+\/([0-9\.]+)/i")).Length > 0)
                {
                    uaData.Browser.Name = "SogouMobileBrowser";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * 百度国际
                 */
                else if ((mc = UA.Match(@"/bdbrowser\w+\/([0-9\.]+)/i")).Length > 0)
                {
                    uaData.Browser.Name = "百度国际";
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
                /**
                 * Android Chrome Browser
                 */
                else if (uaData.Os.Name == "Android" && UA.Test(@"/safari/i") &&
                         ((mc = UA.Match(@"/chrome\/([0-9\.]+)/i")).Length > 0))
                {
                    if ((tempMc = UA.Match(@"/\s+(\w+Browser)\/?([\d\.]*)/")).Length > 0)
                    {
                        uaData.Browser.Name = tempMc[1];
                        if (!tempMc[2].IsNullOrEmpty())
                        {
                            uaData.Browser.Version = new Versions(tempMc[2]);
                        }
                        else
                        {
                            uaData.Browser.Version = new Versions()
                            {
                                Original = mc[1]
                            };
                        }
                    }
                    else
                    {
                        uaData.Browser.Name = "Android Chrome";
                        uaData.Browser.Version = new Versions()
                        {
                            Original = mc[1]
                        };
                    }
                }
                /**
                 * Android Google Browser
                 */
                else if (uaData.Os.Name == "Android" && UA.Test(@"/safari/i") &&
                         ((mc = UA.Match(@"/version\/([0-9\.]+)/i")).Length > 0))
                {
                    if ((tempMc = UA.Match(@"/\)s+(\w+Browser)\/?([\d\.]*)/")).Length > 0)
                    {
                        uaData.Browser.Name = tempMc[1];
                        if (!tempMc[2].IsNullOrEmpty())
                        {
                            uaData.Browser.Version = new Versions(tempMc[2]);
                        }
                        else
                        {
                            uaData.Browser.Version = new Versions()
                            {
                                Original = mc[1]
                            };
                        }
                    }
                    else
                    {
                        uaData.Browser.Name = "Android Browser";
                        uaData.Browser.Version = new Versions()
                        {
                            Original = mc[1]
                        };
                    }
                }
                /**
                 * 'Mozilla/5.0 (iPad; CPU OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Mobile/9B206' belongs to Safari
                 */
                else if (UA.Test(@"/(ipad|iphone).* applewebkit\/.* mobile/i"))
                {
                    uaData.Browser.Name = "Safari";
                }
            }

            if ((mc = UA.Match(@"/baiduboxapp\/?([\d\.]*)/i")).Length > 0)
            {
                uaData.Browser.Name = "百度框";
                if (!mc[1].IsNullOrEmpty())
                {
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }

                // uaData.Browser.Name = 'baidu box';
            }
            else if (UA.Test(@"/BaiduLightAppRuntime/i"))
            {
                uaData.Browser.Name = "轻应用runtime";
                // uaData.Browser.Name = 'qing runtime';
            }
            else if (UA.Test(@"/Weibo/i"))
            {
                uaData.Browser.Name = "微博";
                // uaData.Browser.Name = 'weibo';
            }
            else if (UA.Test(@"/MQQ/i"))
            {
                uaData.Browser.Name = "手机QQ";
                // uaData.Browser.Name = 'mobile qq';
            }
            else if (UA.Test(@"/hao123/i"))
            {
                uaData.Browser.Name = "hao123";
            }

            // }
            if ((mc = UA.Match(@"/MicroMessenger\/([\w.]+)/i")).Length > 0)
            {
                uaData.Browser.Name = "微信";
                var tmpVersion = (mc[1]).ReplaceRegex(@"/_/g", ".");
                tempMc = tmpVersion.Match(@"/(\d+\.\d+\.\d+\.\d+)/");
                if (tempMc.Length > 0)
                {
                    tmpVersion = tempMc[1];
                }

                uaData.Browser.Version = new Versions(tmpVersion);
            }

            if ((mc = UA.Match(@"/UCBrowser\/([\w.]+)/i")).Length > 0)
            {
                uaData.Browser.Name = "UC Browser";
                uaData.Browser.Version = new Versions()
                {
                    Original = mc[1]
                };
            }

            if ((mc = UA.Match(@"/OPR\/([\w.]+)/i")).Length > 0)
            {
                uaData.Browser.Name = "Opera";
                uaData.Browser.Version = new Versions()
                {
                    Original = mc[1]
                };
            }
            else if ((mc = UA.Match(@"/OPiOS\/([\w.]+)/i")).Length > 0)
            {
                uaData.Browser.Name = "Opera";
                uaData.Browser.Version = new Versions()
                {
                    Original = mc[1]
                };
            }
            // IE 11
            else if (UA.Test(@"/Trident\/7/i") && UA.Test(@"/rv:11/i"))
            {
                uaData.Browser.Name = "Internet Explorer";
                uaData.Browser.Version = new Versions()
                {
                    Major = 11,
                    Original = "11"
                };
            }
            // Microsoft Edge
            else if (UA.Test(@"/Edge\/12/i") && UA.Test(@"/Windows Phone|Windows NT/i"))
            {
                uaData.Browser.Name = "Microsoft Edge";
                uaData.Browser.Version = new Versions()
                {
                    Major = 12,
                    Original = "12"
                };
            }
            // miui browser
            else if ((mc = UA.Match(@"/miuibrowser\/([\w.]+)/i")).Length > 0)
            {
                uaData.Browser.Name = "miui browser";
                uaData.Browser.Version = new Versions()
                {
                    Original = mc[1]
                };
            }

            // Safari
            if (!uaData.Browser.Name.IsNullOrEmpty())
            {
                if ((mc = UA.Match(@"/Safari\/([\w.]+)/i")).Length > 0 && UA.Test(@"/Version/i"))
                {
                    uaData.Browser.Name = "Safari";
                }
            }

            if (!uaData.Browser.Name.IsNullOrEmpty() && uaData.Browser.Version != null)
            {
                if ((mc = UA.Match(@"/Version\/([\w.]+)/i")).Length > 0)
                {
                    uaData.Browser.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
            }

            // if (uaData.Os.Name === 'Windows' && uaData.os.version) {
            //  // Windows 8.1
            //  if (uaData.os.version.alias === 'NT 6.3') {
            //      uaData.os.version.alias = '8.1';
            //  }
            // }
            // handle os
            if (uaData.Os.Name == "Windows" || UA.Test(@"/Windows/i"))
            {
                uaData.Os.Name = "Windows";
                if (UA.Test(@"/NT 6.3/i"))
                {
                    uaData.Os.Alias = "8.1";
                    uaData.Os.Version = new Versions()
                    {
                        Original = "8.1"
                    };
                }
                else if (UA.Test(@"/NT 6.4/i") || UA.Test(@"/NT 10.0/i"))
                {
                    uaData.Os.Alias = "10";
                    uaData.Os.Version = new Versions()
                    {
                        Original = "10"
                    };
                }
            }
            else if (uaData.Os.Name == "Mac OS X")
            {
                uaData.Os.Name = "Mac OS X";
                if ((mc = UA.Match(@"/Mac OS X[\s\_\-\/](\d+[\.\-\_]\d+[\.\-\_]?\d*)/i")).Length > 0)
                {
                    uaData.Os.Alias = mc[1].ReplaceRegex(@"/_/g", ".");
                    uaData.Os.Version = new Versions()
                    {
                        Original = mc[1].ReplaceRegex(@"/_/g", ".")
                    };
                }
                else
                {
                    uaData.Os.Alias = "";
                    uaData.Os.Version = new Versions()
                    {
                        Original = mc[1].ReplaceRegex(@"/_/g", ".")
                    };
                }
            }
            else if (uaData.Os.Name.Test(@"/Android/i"))
            {
                if ((mc = UA.Match(@"/Android[\s\_\-\/i686]?[\s\_\-\/](\d+[\.\-\_]\d+[\.\-\_]?\d*)/i")).Length > 0)
                {
                    uaData.Os.Alias = mc[1];
                    uaData.Os.Version = new Versions()
                    {
                        Original = mc[1]
                    };
                }
            }

            return uaData;
        }

        #endregion
    }
}
