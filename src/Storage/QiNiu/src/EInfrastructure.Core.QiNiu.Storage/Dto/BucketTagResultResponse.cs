// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.QiNiu.Storage.Dto
{
    /// <summary>
    /// 空间标签
    /// </summary>
    internal class BucketTagResultResponse
    {
        /// <summary>
        /// 标签
        /// </summary>
        public List<KeyValuePair<string, string>> Tags { get; set; }
    }
}
