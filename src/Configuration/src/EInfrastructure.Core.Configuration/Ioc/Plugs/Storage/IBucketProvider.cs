// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage
{
    /// <summary>
    /// 空间
    /// </summary>
    public interface IBucketProvider : ISingleInstance
    {
        /// <summary>
        /// 获取空间列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BucketItemResultDto GetBucketList(GetBucketParam request);

        /// <summary>
        /// 创建空间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto Create(CreateBucketParam request);

        /// <summary>
        /// 设置空间的镜像源
        /// </summary>
        /// <param name="request"></param>
        OperateResultDto SetSource(SetBucketSource request);

        /// <summary>
        /// 删除空间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto Delete(DeleteBucketParam request);

        /// <summary>
        /// 判断空间是否存在
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto Exist(ExistBucketParam request);

        /// <summary>
        /// 获取空间域名信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DomainResultDto GetHost(GetBucketHostParam request);

        /// <summary>
        /// 设置空间访问权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto SetPermiss(SetPermissParam request);

        #region 防盗链管理

        /// <summary>
        /// 设置防盗链
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto SetReferer(SetRefererParam request);

        /// <summary>
        /// 得到防盗链配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RefererResultDto GetReferer(GetRefererParam request);

        /// <summary>
        /// 清空防盗链规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto ClearReferer(ClearRefererParam request);

        #endregion

        #region 标签管理

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto SetTag(SetTagBucketParam request);

        /// <summary>
        /// 得到空间标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        TagResultDto GetTags(GetTagsBucketParam request);

        /// <summary>
        /// 清除空间标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto ClearTag(ClearTagBucketParam request);

        #endregion
    }
}
