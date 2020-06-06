// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Aliyun.OSS;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Aliyun.Storage.Validator.Bucket;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
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

        #region 设置空间的镜像源

        /// <summary>
        /// 设置空间的镜像源
        /// </summary>
        /// <param name="request"></param>
        public OperateResultDto SetSource(SetBucketSource request)
        {
            return new OperateResultDto(false, "不支持设置空间的镜像源");
        }

        #endregion

        #region 删除空间

        /// <summary>
        /// 删除空间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto Delete(DeleteBucketParam request)
        {
            try
            {
                Check.TrueByString(request != null, $"{nameof(request)} is null", HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                client.DeleteBucket(bucket);
                return new OperateResultDto(true, "success");
            }
            catch (Exception ex)
            {
                return new OperateResultDto(false, ex.Message);
            }
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

        #region 获取空间域名信息

        /// <summary>
        /// 获取空间域名信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DomainResultDto GetHost(GetBucketHostParam request)
        {
            return new DomainResultDto(false, default(List<string>), "不支持获取空间域");
        }

        #endregion

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

        #region 获取空间的访问权限

        /// <summary>
        /// 获取空间的访问权限
        /// </summary>
        /// <param name="persistentOps"></param>
        /// <returns></returns>
        public BucketPermissItemResultDto GetPermiss(BasePersistentOps persistentOps)
        {
            try
            {
                Check.True(persistentOps != null, "策略信息异常");
                var zone = Core.Tools.GetZone(this._aLiYunConfig, persistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, persistentOps.Bucket);
                var ret = client.GetBucketAcl(bucket);
                if (ret != null && ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new BucketPermissItemResultDto(true, Core.Tools.GetPermiss(ret.ACL), "success");
                }

                if (ret != null)
                {
                    return new BucketPermissItemResultDto(false, null,
                        $"lose，RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
                }

                return new BucketPermissItemResultDto(false, null, "lose");
            }
            catch (BusinessException<string>ex)
            {
                return new BucketPermissItemResultDto(false, null, ex.Message);
            }
            catch (Exception ex)
            {
                return new BucketPermissItemResultDto(false, null, Core.Tools.GetMessage(ex));
            }
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
            new SetRefererParamValidator().Validate(request).Check(HttpStatus.Err.Name);
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

        #region 设置标签

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto SetTag(SetTagBucketParam request)
        {
            new SetTagBucketParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
            var setRequest = new SetBucketTaggingRequest(bucket);
            request.Tags.ForEach(tag =>
            {
                setRequest.AddTag(new Tag()
                {
                    Key = tag.Key,
                    Value = tag.Value
                });
            });
            client.SetBucketTagging(setRequest);
            return new OperateResultDto(true, "success");
        }

        #endregion

        #region 查询空间标签

        /// <summary>
        /// 查询空间标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TagResultDto GetTags(GetTagsBucketParam request)
        {
            try
            {
                new GetTagsBucketParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                // 查看Bucket标签。
                var ret = client.GetBucketTagging(bucket);
                if (ret != null && ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new TagResultDto(true,
                        ret.Tags.Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).ToList(), "success");
                }

                if (ret != null)
                {
                    return new TagResultDto(false, null,
                        $"lose，RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
                }

                return new TagResultDto(false, null, "lose");
            }
            catch (BusinessException<string>ex)
            {
                return new TagResultDto(false, null, ex.Message);
            }
            catch (Exception ex)
            {
                return new TagResultDto(false, null, Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 清除标签

        /// <summary>
        /// 清除标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto ClearTag(ClearTagBucketParam request)
        {
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
            // 查看Bucket标签。
            client.DeleteBucketTagging(bucket);
            return new OperateResultDto(true, "success");
        }

        #endregion

        #endregion
    }
}
