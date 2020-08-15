// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.UserAgentParse.Property
{
    /// <summary>
    /// 设备
    /// </summary>
    internal class DeviceProperty
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="deviceMatchType"></param>
        /// <param name="os"></param>
        /// <param name="deviceType">设备类型</param>
        /// <param name="manufacturer">制造商</param>
        /// <param name="model">设备</param>
        /// <param name="identified">是否确认</param>
        internal DeviceProperty(DeviceMatchType deviceMatchType, string os, DeviceType deviceType, string manufacturer,
            string model,
            bool identified)
        {
            DeviceMatchType = deviceMatchType;
            Os = os;
            ManufacturerValue = manufacturer;
            DeviceType = deviceType;
            Model = model;
            Identified = identified;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="os"></param>
        /// <param name="keys"></param>
        /// <param name="deviceType">设备类型</param>
        /// <param name="manufacturer">制造商</param>
        /// <param name="model">设备</param>
        /// <param name="identified">是否确认</param>
        internal DeviceProperty(string os, string[] keys, DeviceType deviceType, string manufacturer, string model,
            bool identified) : this(DeviceMatchType.Regex, os, deviceType, manufacturer, model, identified)
        {
            Keys = keys;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="os"></param>
        /// <param name="keys"></param>
        /// <param name="regexes"></param>
        /// <param name="deviceType">设备类型</param>
        /// <param name="manufacturer">制造商</param>
        /// <param name="model">设备</param>
        /// <param name="identified">是否确认</param>
        internal DeviceProperty(string os, string[] keys, Regex[] regexes,DeviceType deviceType, string manufacturer, string model,
            bool identified) : this(DeviceMatchType.VagueAndRegex, os, deviceType, manufacturer, model, identified)
        {
            Keys = keys;
            Regexs = regexes;
        }

        /// <summary>
        /// os
        /// </summary>
        public string Os { get; private set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string ManufacturerValue { get; private set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceType DeviceType { get; private set; }

        /// <summary>
        /// 设备
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// 是否确认
        /// </summary>
        public bool Identified { get; private set; }

        #region 筛选Deveice名称条件

        /// <summary>
        /// 包括 Key名称
        /// </summary>
        public string[] Keys { get; }

        /// <summary>
        /// 匹配方式
        /// </summary>
        public DeviceMatchType DeviceMatchType { get;  }

        /// <summary>
        /// 除哪些key什么之外
        /// </summary>
        public string[] ExceptKeys { get; }

        /// <summary>
        /// Regex
        /// </summary>
        public Regex[] Regexs { get; }

        /// <summary>
        /// 除哪些Regex
        /// </summary>
        public Regex[] ExceptRegexs { get; }

        #endregion
    }

    /// <summary>
    /// 设备匹配方式
    /// </summary>
    internal class DeviceMatchType : Enumeration
    {
        /// <summary>
        /// 模糊匹配
        /// </summary>
        internal static DeviceMatchType Vague = new DeviceMatchType(1, "模糊匹配");

        /// <summary>
        /// 正则表达式
        /// </summary>
        internal static DeviceMatchType Regex = new DeviceMatchType(2, "正则表达式");

        /// <summary>
        /// 模糊匹配和除什么之外的模糊匹配
        /// </summary>
        internal static DeviceMatchType VagueAndExceptVague = new DeviceMatchType(3, "VagueAndExceptVague");

        /// <summary>
        /// 正则表达式和除什么之外正则表达式匹配
        /// </summary>
        internal static DeviceMatchType RegexAndExceptRegex = new DeviceMatchType(4, "RegexAndExceptRegex");

        /// <summary>
        /// 模糊匹配和除什么之外的正则表达式匹配
        /// </summary>
        internal static DeviceMatchType VagueAndExceptRegex = new DeviceMatchType(5, "VagueAndExceptRegex");

        /// <summary>
        /// 正则表达式和除什么之外模糊匹配
        /// </summary>
        internal static DeviceMatchType RegexAndExceptVague = new DeviceMatchType(5, "RegexAndExceptVague");

        /// <summary>
        /// 模糊匹配和正则表达式
        /// </summary>
        internal static DeviceMatchType VagueAndRegex = new DeviceMatchType(5, "模糊匹配和正则表达式");

        /// <summary>
        /// 模糊匹配和正则表达式以及排除模糊匹配和排除正则表达式
        /// </summary>
        internal static DeviceMatchType All = new DeviceMatchType(6, "All");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        internal DeviceMatchType(int id, string name) : base(id, name)
        {
        }
    }
}
