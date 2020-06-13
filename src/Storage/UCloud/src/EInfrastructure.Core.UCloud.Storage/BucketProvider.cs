// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;

namespace EInfrastructure.Core.UCloud.Storage
{
    /// <summary>
    /// 空间
    /// </summary>
    public class BucketProvider : IBucketProvider
    {
        public BucketItemResultDto GetBucketList(GetBucketParam request)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto Create(CreateBucketParam request)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto SetSource(SetBucketSource request)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto Delete(DeleteBucketParam request)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto Exist(ExistBucketParam request)
        {
            throw new System.NotImplementedException();
        }

        public DomainResultDto GetHost(GetBucketHostParam request)
        {
            throw new System.NotImplementedException();
        }

        #region 空间管理

        #region 设置空间访问权限

        /// <summary>
        /// 设置空间访问权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto SetPermiss(SetPermissParam request)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region 获取空间的访问权限

        /// <summary>
        /// 获取空间的访问权限
        /// </summary>
        /// <param name="persistentOps"></param>
        /// <returns></returns>
        public BucketPermissItemResultDto GetPermiss(BasePersistentOps persistentOps)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #endregion

        public OperateResultDto SetReferer(SetRefererParam request)
        {
            throw new System.NotImplementedException();
        }

        public RefererResultDto GetReferer(GetRefererParam request)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto ClearReferer(ClearRefererParam request)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto SetTag(SetTagBucketParam param)
        {
            throw new System.NotImplementedException();
        }

        public TagResultDto GetTags(GetTagsBucketParam param)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto ClearTag(ClearTagBucketParam request)
        {
            throw new System.NotImplementedException();
        }
    }
}
