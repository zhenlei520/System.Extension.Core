// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Pictures
{
    /// <summary>
    /// 图片抓取
    /// </summary>
    public class FetchFileParam
    {
        /// <summary>
        /// 源图（必填）
        /// </summary>
        public string SourceFileKey { get; set; }

        /// <summary>
        /// 目标图（必填）
        /// </summary>
        public string Key { get; set; }
    }
}
