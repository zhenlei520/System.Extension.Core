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
        public List<BucketItemDto> BucketList { get; private set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; private set; }


        /// <summary>
        /// 请求中返回的结果是否被截断。返回值：true、false
        /// </summary>
        public bool? IsTruncated { get; private set; }

        /// <summary>
        /// 标明这次GetBucket（ListObjects）的起点
        /// </summary>
        public string Marker { get; private set; }

        /// <summary>
        /// 下次的起点
        /// </summary>
        public string NextMaker { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bucketList">空间列表</param>
        /// <param name="prefix">前缀</param>
        /// <param name="isTruncated">请求中返回的结果是否被截断。返回值：true、false</param>
        /// <param name="marker">标明这次GetBucket（ListObjects）的起点</param>
        /// <param name="nextMaker">下次的起点</param>
        public BucketItemResultDto(List<BucketItemDto> bucketList, string prefix, bool? isTruncated, string marker,
            string nextMaker) : base(true, "success")
        {
            BucketList = bucketList;
            Prefix = prefix;
            Marker = marker;
            IsTruncated = isTruncated;
            NextMaker = nextMaker;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="marker">标明这次GetBucket（ListObjects）的起点</param>
        /// <param name="msg">异常提示</param>
        public BucketItemResultDto(string prefix, string marker,
            string msg) : base(false, msg)
        {
            Prefix = prefix;
            Marker = marker;
            IsTruncated = null;
            BucketList = null;
        }

        /// <summary>
        /// 空间详细
        /// </summary>
        public class BucketItemDto
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="zone">空间区域</param>
            /// <param name="name">空间名称</param>
            public BucketItemDto(int? zone, string name)
            {
                this.Zone = zone;
                this.Name = name;
            }

            /// <summary>
            /// 空间区域
            /// null代表获取失败
            /// </summary>
            public int? Zone { get; private set; }

            /// <summary>
            /// 空间名称
            /// </summary>
            public string Name { get; private set; }
        }
    }
}
