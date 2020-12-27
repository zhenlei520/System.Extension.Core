// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.Configuration
{
    /// <summary>
    /// 星座
    /// </summary>
    internal class Constellations
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 星座
        /// </summary>
        public Constellation Key { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public float MinTime { get; set; }

        /// <summary>
        /// 截至时间
        /// </summary>
        public float MaxTime { get; set; }
    }
}
