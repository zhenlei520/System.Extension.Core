// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.QiNiu.Storage.Tools.Enum
{
    /// <summary>
    /// 空间
    /// </summary>
    public enum ZoneEnum
    {
        /// <summary>
        /// 华东
        /// </summary>
        [Description("华东")] ZoneCnEast = 0,

        /// <summary>
        /// 华北
        /// </summary>
        [Description("华北")] ZoneCnNorth = 1,

        /// <summary>
        /// 华南
        /// </summary>
        [Description("华南")] ZoneCnSouth = 2,

        /// <summary>
        /// 北美
        /// </summary>
        [Description("北美")] ZoneUsNorth = 3
    }
}