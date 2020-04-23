// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;
using EInfrastructure.Core.Tools.Attributes;

namespace EInfrastructure.Core.QiNiu.Storage.Enum
{
    /// <summary>
    /// 空间
    /// </summary>
    public enum ZoneEnum
    {
        /// <summary>
        /// 华东
        /// </summary>
        [EName("z0")]
        [Description("华东")] ZoneCnEast = 0,

        /// <summary>
        /// 华北
        /// </summary>
        [EName("z1")]
        [Description("华北")] ZoneCnNorth = 1,

        /// <summary>
        /// 华南
        /// </summary>
        [EName("z2")]
        [Description("华南")] ZoneCnSouth = 2,

        /// <summary>
        /// 北美
        /// </summary>
        [EName("na0")]
        [Description("北美")] ZoneUsNorth = 3,

        /// <summary>
        /// 东南亚
        /// </summary>
        [EName("as0")]
        [Description("东南亚")] ZoneAsSingapore = 4
    }
}
