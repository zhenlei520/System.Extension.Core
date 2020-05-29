// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.UserAgentParse
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class Devices
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceType DeviceType { get;internal set; }

        /// <summary>
        ///
        /// </summary>
        public bool Identified { get;internal set; }
    }
}
