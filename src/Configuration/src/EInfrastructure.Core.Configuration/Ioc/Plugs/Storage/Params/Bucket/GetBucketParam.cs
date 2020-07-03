// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 获取空间信息
    /// 七牛云存储不支持前缀、分页查询等操作，只支持根据标签搜索查询，目前结果是根据响应值计算得到的，如果使用七牛云存储，请尽量只查询标签或者查询全部被空间
    /// 阿里云存储不支持多个标签查询，如果为多个标签赋值，也只有第一个生效
    /// </summary>
    public class GetBucketParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="marker">标明这次GetBucket（ListObjects）的起点</param>
        /// <param name="pageSize">默认查询的空间数量，默认-1，查询全部空间</param>
        /// <param name="tagFilters">标签筛选</param>
        /// <param name="persistentOps">策略</param>
        public GetBucketParam(string prefix = "", string marker = "", int pageSize = -1,
            List<KeyValuePair<string, string>> tagFilters = null,
            BasePersistentOps persistentOps = null)
        {
            Prefix = prefix;
            Marker = marker;
            PageSize = pageSize;
            TagFilters = tagFilters ?? new List<KeyValuePair<string, string>>();
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// 标明这次GetBucket（ListObjects）的起点
        /// </summary>
        public string Marker { get; private set; }

        /// <summary>
        /// 默认查询的空间数量
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 标签
        /// 七牛云支持多标签，阿里云只支持单个标签
        /// </summary>
        public List<KeyValuePair<string, string>> TagFilters { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
