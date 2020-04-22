// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage
{
    /// <summary>
    /// 空间
    /// </summary>
    public interface IBucketProvider : ISingleInstance
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="tagFilters">标签筛选</param>
        /// <returns></returns>
        List<string> GetBucketList(List<KeyValuePair<string, string>> tagFilters);

        /// <summary>
        /// 创建空间
        /// </summary>
        /// <param name="bucketName">空间名</param>
        /// <param name="region">区域</param>
        /// <returns></returns>
        OperateResultDto Create(string bucketName, int region);

        /// <summary>
        /// 删除空间
        /// </summary>
        /// <param name="bucketName">空间名</param>
        /// <returns></returns>
        OperateResultDto Delete(string bucketName);

        /// <summary>
        /// 获取域名空间信息
        /// </summary>
        /// <param name="bucketName">空间名</param>
        /// <returns></returns>
        DomainResultDto GetHost(string bucketName);

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="bucketName">空间名</param>
        /// <param name="permiss">权限 0：公开，1：私有</param>
        /// <returns></returns>
        OperateResultDto SetPermiss(string bucketName, int permiss);

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="bucketName">空间名</param>
        /// <param name="key">标签key</param>
        /// <param name="value">标签value</param>
        /// <returns></returns>
        OperateResultDto SetTag(string bucketName, string key, string value);

        /// <summary>
        /// 得到空间标签
        /// </summary>
        /// <param name="bucketName">空间名</param>
        /// <returns></returns>
        TagResultDto GetTags(string bucketName);

        /// <summary>
        /// 清除空间标签
        /// </summary>
        /// <param name="bucketName">空间命</param>
        /// <returns></returns>
        OperateResultDto ClearTag(string bucketName);
    }
}
