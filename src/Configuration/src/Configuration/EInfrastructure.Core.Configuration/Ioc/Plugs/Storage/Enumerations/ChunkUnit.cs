// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations
{
    /// <summary>
    ///
    /// </summary>
    public class ChunkUnit : Enumeration
    {
        /// <summary>
        /// 128KB
        /// </summary>
        public static ChunkUnit U128K = new ChunkUnit(1, "128KB");

        /// <summary>
        /// 256KB
        /// </summary>
        public static ChunkUnit U256K = new ChunkUnit(2, "256KB");

        /// <summary>
        /// 512KB
        /// </summary>
        public static ChunkUnit U512K = new ChunkUnit(4, "512KB");

        /// <summary>
        /// 1MB
        /// </summary>
        public static ChunkUnit U1024K = new ChunkUnit(8, "1MB");

        /// <summary>
        /// 2MB
        /// </summary>
        public static ChunkUnit U2048K = new ChunkUnit(16, "2MB");

        /// <summary>
        /// 4MB
        /// </summary>
        public static ChunkUnit U4096K = new ChunkUnit(32, "4MB");

        public ChunkUnit(int id, string name) : base(id, name)
        {
        }
    }
}
