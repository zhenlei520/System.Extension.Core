// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Net;
using Aliyun.OSS;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Aliyun.Storage.Validator.Bucket;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Validation.Common;

namespace EInfrastructure.Core.Aliyun.Storage
{
    /// <summary>
    ///
    /// </summary>
    public class BucketProvider : IBucketProvider
    {
        private readonly ALiYunStorageConfig _aLiYunConfig;

        public BucketProvider(ALiYunStorageConfig aliyunConfig)
        {
            _aLiYunConfig = aliyunConfig;
        }

        #region 获取空间列表

        /// <summary>
        /// 获取空间列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BucketItemResultDto GetBucketList(GetBucketParam request)
        {
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            ListBucketsRequest listBucketsRequest = new ListBucketsRequest();
            if (request.PageSize != -1)
            {
                listBucketsRequest.MaxKeys = request.PageSize;
            }

            if (request.TagFilters.Count > 0)
            {
                var keyValue = request.TagFilters.FirstOrDefault();
                listBucketsRequest.Tag = new Tag()
                {
                    Key = keyValue.Key,
                    Value = keyValue.Value
                };
            }

            if (!string.IsNullOrEmpty(request.Prefix))
            {
                listBucketsRequest.Prefix = request.Prefix;
            }

            if (!string.IsNullOrEmpty(request.Marker))
            {
                listBucketsRequest.Marker = request.Marker;
            }

            var ret = client.ListBuckets(listBucketsRequest);
            if (ret.HttpStatusCode == HttpStatusCode.OK)
            {
                return new BucketItemResultDto(ret.Buckets.Select(x => x.Name).ToList(), ret.Prefix, ret.IsTruncated,
                    ret.Marker, ret.NextMaker);
            }

            return new BucketItemResultDto(request.Prefix, request.Marker,
                $"lose RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
        }

        #endregion

        #region 创建空间

        /// <summary>
        /// 创建空间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto Create(CreateBucketParam request)
        {
            new CreateBucketParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            Bucket ret = request.StorageClass != null
                ? client.CreateBucket(request.BucketName, Core.Tools.GetStorageClass(request.StorageClass))
                : client.CreateBucket(request.BucketName);
            if (ret != null)
            {
                return new OperateResultDto(true, "success");
            }

            return new OperateResultDto(false, "lose");
        }

        #endregion

        public OperateResultDto SetSource(SetBucketSource request)
        {
            throw new System.NotImplementedException();
        }

        #region 删除空间

        /// <summary>
        /// 删除空间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto Delete(DeleteBucketParam request)
        {
            Check.TrueByString(request != null, $"{nameof(request)} is null", HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
            client.DeleteBucket(bucket);
            return new OperateResultDto(true, "success");
        }

        #endregion

        #region 判断空间是否存在

        /// <summary>
        /// 判断空间是否存在
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto Exist(ExistBucketParam request)
        {
            Check.TrueByString(request != null, $"{nameof(request)} is null", HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
            if (client.DoesBucketExist(bucket))
            {
                return new OperateResultDto(true, "success");
            }

            return new OperateResultDto(false, "the bucket is not find");
        }

        #endregion

        public DomainResultDto GetHost(GetBucketHostParam request)
        {
            throw new System.NotImplementedException();
        }

        #region 设置空间访问权限

        /// <summary>
        /// 设置空间访问权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto SetPermiss(SetPermissParam request)
        {
            new SetPermissParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            client.SetBucketAcl(Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket),
                Core.Tools.GetCannedAccessControl(request.Permiss));
            return new OperateResultDto(true, "success");
        }

        #endregion

        #region 防盗链管理

        #region 设置防盗链

        /// <summary>
        /// 设置防盗链
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto SetReferer(SetRefererParam request)
        {
            Check.TrueByString(request != null, $"{nameof(request)} is null", HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
            client.SetBucketReferer(new SetBucketRefererRequest(bucket, request.RefererList,
                request.IsAllowNullReferer));
            return new OperateResultDto(true, "success");
        }

        #endregion

        #region 得到防盗链配置

        /// <summary>
        /// 得到防盗链配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RefererResultDto GetReferer(GetRefererParam request)
        {
            Check.TrueByString(request != null, $"{nameof(request)} is null", HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
            var rc = client.GetBucketReferer(bucket);
            if (rc == null)
            {
                return new RefererResultDto("the bucket is not find");
            }

            return new RefererResultDto(rc.AllowEmptyReferer,
                (rc.RefererList?.Referers ?? Array.Empty<string>()).ToList());
        }

        #endregion

        #region 清空防盗链规则

        /// <summary>
        /// 清空防盗链规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto ClearReferer(ClearRefererParam request)
        {
            Check.TrueByString(request != null, $"{nameof(request)} is null", HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
            client.SetBucketReferer(new SetBucketRefererRequest(bucket));
            return new OperateResultDto(true, "success");
        }

        #endregion

        #endregion

        #region 标签管理

        public OperateResultDto SetTag(SetTagBucketParam request)
        {
            throw new System.NotImplementedException();
        }

        public TagResultDto GetTags(GetTagsBucketParam request)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto ClearTag(ClearTagBucketParam request)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
