// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Interface.Compress.Enum
{
    /// <summary>
    /// 压缩方式
    /// </summary>
    public enum CompressTypeEnum
    {
        /// <summary>
        /// BZip2 
        /// </summary>
        [Description("BZip2")]BZip2 = 1,

        /// <summary>
        /// GZip
        /// </summary>
        [Description("GZip")] GZip = 2,

        /// <summary>
        /// Lzw
        /// </summary>
        [Description("Lzw")] Lzw = 3,

        /// <summary>
        /// Tar
        /// </summary>
        [Description("Tar")] Tar = 4,

        /// <summary>
        /// Zip
        /// </summary>
        [Description("Zip")]Zip = 5,

        /// <summary>
        /// 7Z
        /// </summary>
        [Description("7Z")] Zip7 = 6,

        /// <summary>
        /// Rar
        /// </summary>
        [Description("Rar")]Rar = 7,

        /// <summary>
        /// 快压
        /// </summary>
        KZip = 8
    }
}