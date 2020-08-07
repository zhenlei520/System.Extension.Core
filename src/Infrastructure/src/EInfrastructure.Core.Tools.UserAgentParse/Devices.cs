// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools.UserAgentParse.Property;

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class Devices
    {
        private List<DeviceProperty> _deviceList = new List<DeviceProperty>()
        {
            new DeviceProperty("iOS", new[] {"iPod;"}, DeviceType.Media, "Apple", "iPod Touch", true),
            new DeviceProperty("iOS", new[] {"iPhone;"}, new Regex[]
            {
                new Regex(@"/iPhone\s*\d*s?[cp]?;/i"),
            }, DeviceType.Mobile, "Apple", "iPhone", true),
            new DeviceProperty("iOS", new[] {"iPhone Simulator;"}, DeviceType.Emulator, "", "", true),
            new DeviceProperty("iOS", new[] {""}, DeviceType.Tablet, "Apple", "iPad", true),

            new DeviceProperty("Windows Phone", new[] {""}, DeviceType.Mobile, "", "desktop", true),


            new DeviceProperty("iOS", new[] {"iPod;"}, DeviceType.Media, "Apple", "iPod Touch", true),

            new DeviceProperty("Windows Mobile", new[] {""}, DeviceType.Mobile, "", "", false),
            new DeviceProperty("Windows CE", new[] {""}, DeviceType.Mobile, "", "", true),
        };

        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceType DeviceType { get; internal set; }

        /// <summary>
        /// 设备信息
        /// 例如：IPhone
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; internal set; }

        /// <summary>
        /// 是否确认
        /// </summary>
        public bool Identified { get; internal set; }
    }
}
