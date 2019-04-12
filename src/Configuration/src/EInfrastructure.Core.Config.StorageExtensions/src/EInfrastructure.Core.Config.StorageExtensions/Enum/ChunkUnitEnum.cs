// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Config.StorageExtensions.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public enum ChunkUnitEnum
    {
        /// <summary>
        /// 128KB
        /// </summary>
        [Description("128KB")]U128K = 1,

        /// <summary>
        /// 256KB
        /// </summary>
        [Description("256KB")]U256K = 2,

        /// <summary>
        /// 512KB
        /// </summary>
        [Description("512KB")]U512K = 4,

        /// <summary>
        /// 1MB
        /// </summary>
        [Description("1M")]U1024K = 8,

        /// <summary>
        /// 2MB
        /// </summary>
        [Description("2M")]U2048K = 16,

        /// <summary>
        /// 4MB
        /// </summary>
        [Description("4M")] U4096K = 32
    }
}