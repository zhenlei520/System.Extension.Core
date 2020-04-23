// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket
{
    /// <summary>
    /// 空间列表
    /// </summary>
    public class BucketItemResultDto : OperateResultDto
    {
        /// <summary>
        /// 空间列表
        /// </summary>
        public List<string> BucketList { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        /// <param name="bucketList">空间列表</param>
        /// <param name="msg"></param>
        public BucketItemResultDto(bool state, List<string> bucketList, string msg) : base(state, msg)
        {
            BucketList = bucketList;
        }
    }
}
