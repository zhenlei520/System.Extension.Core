// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket
{
    /// <summary>
    /// 标签
    /// </summary>
    public class TagResultDto : OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        /// <param name="tags">标签</param>
        /// <param name="msg"></param>
        public TagResultDto(bool state, List<KeyValuePair<string, string>> tags, string msg) : base(state, msg)
        {
            Tags = tags;
        }

        /// <summary>
        /// 标签
        /// </summary>
        public List<KeyValuePair<string, string>> Tags { get; set; }
    }
}
