// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Tools.Configuration
{
    /// <summary>
    /// MimeType与扩展名
    /// </summary>
    public class MimeInfos
    {
        /// <summary>
        ///
        /// </summary>
        public MimeInfos()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="mimeType"></param>
        public MimeInfos(string ext, string mimeType) : this()
        {
            Ext = ext;
            MimeType = mimeType;
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// MimeType
        /// </summary>
        public string MimeType { get; set; }
    }
}
