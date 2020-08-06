// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Config.Entities.Extensions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Http.Enumerations;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using EInfrastructure.Core.QiNiu.Storage.Response;
using EInfrastructure.Core.QiNiu.Storage.Validator.Bucket;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Url;
using EInfrastructure.Core.Validation.Common;
using Microsoft.Extensions.Logging;
using Qiniu.Util;
using RestSharp;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 空间
    /// </summary>
    public class BucketProvider : IBucketProvider
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly QiNiuStorageConfig _qiNiuConfig;
        private readonly IStorageProvider _storageProvider;
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        public BucketProvider(ILogger<BucketProvider> logger, QiNiuStorageConfig qiNiuConfig)
        {
            this._logger = logger;
            _qiNiuConfig = qiNiuConfig;
            _storageProvider = new StorageProvider(logger, qiNiuConfig);
            _jsonProvider = new NewtonsoftJsonProvider();
            _httpClient = new HttpClient("http://rs.qbox.me");
        }

        /// <summary>
        ///
        /// </summary>
        public BucketProvider(ILogger<BucketProvider> logger, QiNiuStorageConfig qiNiuConfig,
            IJsonProvider jsonProvider) : this(logger, qiNiuConfig)
        {
            _jsonProvider = jsonProvider;
        }

        #region 根据标签筛选空间获取空间名列表

        /// <summary>
        /// 根据标签筛选空间获取空间名列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BucketItemResultDto GetBucketList(GetBucketParam request)
        {
            UrlParameter urlParameter = new UrlParameter();
            request.TagFilters.ForEach(tag => { urlParameter.Add(tag.Key, tag.Value); });
            string url =
                $"http://rs.qbox.me/buckets?tagCondition={Base64.UrlSafeBase64Encode(urlParameter.GetQueryResult())}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(new GetManageTokenParam(url))}"}
            };
            try
            {
                var response = _httpClient.GetString(url);
                return GetResponse(response, () =>
                    {
                        var bucketList = _jsonProvider.Deserialize<List<string>>(response);
                        Expression<Func<string, bool>> condition = x => true;
                        if (!string.IsNullOrEmpty(request.Prefix))
                        {
                            condition = condition.And(x => x.StartsWith(request.Prefix));
                        }

                        var list = bucketList.Where(condition.Compile()).ToList();
                        if (!string.IsNullOrEmpty(request.Marker))
                        {
                            var index = list.ToList().IndexOf(request.Marker);
                            if (index != -1)
                            {
                                list = list.Skip(index + 1).ToList();
                            }
                        }

                        if (request.PageSize != -1)
                        {
                            var isTruncated = list.Take(request.PageSize).Count() != list.Count;
                            return new BucketItemResultDto(
                                list.Take(request.PageSize).Select(x => new BucketItemResultDto.BucketItemDto(null, x))
                                    .ToList(), request.Prefix,
                                isTruncated, request.Marker,
                                isTruncated ? list.Take(request.PageSize).LastOrDefault() : "");
                        }

                        return new BucketItemResultDto(
                            list.Select(x => new BucketItemResultDto.BucketItemDto(null, x)).ToList(), request.Prefix,
                            false, request.Marker, "");
                    },
                    resultResponse =>
                        new BucketItemResultDto(request.Prefix, request.Marker,
                            $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new BucketItemResultDto(request.Prefix, request.Marker, $"lose {ex.Message}");
            }
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

            var zone = Core.Tools.GetZonePrivate(_qiNiuConfig, request.Zone, () => ZoneEnum.ZoneCnSouth);
            var scheme = Core.Tools.GetScheme(_qiNiuConfig, request.PersistentOps.IsUseHttps);
            string url = $"{scheme}rs.qbox.me/mkbucketv3/{request.BucketName}/region/{Core.Tools.GetRegion(zone)}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(new GetManageTokenParam(url))}"}
            };
            var response = _httpClient.GetStringByPost(url, new { }, RequestBodyFormat.Json);
            return GetResponse(response, () => new OperateResultDto(true,
                    "success"),
                resultResponse =>
                    new OperateResultDto(false, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
        }

        #endregion

        #region 设置空间的镜像源

        /// <summary>
        /// 设置空间的镜像源
        /// </summary>
        /// <param name="request"></param>
        public OperateResultDto SetSource(SetBucketSource request)
        {
            new SetBucketSourceValidator().Validate(request).Check(HttpStatus.Err.Name);
            var scheme = Core.Tools.GetScheme(_qiNiuConfig, request.PersistentOps.IsUseHttps);
            string url =
                $"{scheme}uc.qbox.me/image/{Core.Tools.GetBucket(_qiNiuConfig, request.PersistentOps.Bucket)}/from/{Base64.UrlSafeBase64Encode(request.ImageSource)}/host/{Base64.UrlSafeBase64Encode(request.ReferHost)}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(new GetManageTokenParam(url))}"}
            };
            var response = _httpClient.GetStringByPost(url, new { }, RequestBodyFormat.Json);
            return GetResponse(response, () => new OperateResultDto(true,
                    "success"),
                resultResponse =>
                    new OperateResultDto(false, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
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
            Check.True(request != null, $"{nameof(request)} is null");
            var bucket = Core.Tools.GetBucket(_qiNiuConfig, request.PersistentOps.Bucket);
            var ret = GetBucketList(new GetBucketParam());
            if (!ret.State)
            {
                return new OperateResultDto(false, "lose 请稍后再试");
            }

            if (ret.BucketList.Any(x => x.Name == bucket))
            {
                return new OperateResultDto(true, "success");
            }

            return new OperateResultDto(false, "the bucket is not find");
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
            new DeleteBucketParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var scheme = Core.Tools.GetScheme(_qiNiuConfig, request.PersistentOps.IsUseHttps);
            string url =
                $"{scheme}rs.qbox.me/drop/{Core.Tools.GetBucket(_qiNiuConfig, request.PersistentOps.Bucket)}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(new GetManageTokenParam(url))}"}
            };
            var response = _httpClient.GetStringByPost(url, new { }, RequestBodyFormat.Json);
            return GetResponse(response, () => new OperateResultDto(true,
                    "success"),
                resultResponse =>
                    new OperateResultDto(false, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
        }

        #endregion

        #region 得到域名Host

        /// <summary>
        /// 得到域名Host
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DomainResultDto GetHost(GetBucketHostParam request)
        {
            new GetBucketHostParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var scheme = Core.Tools.GetScheme(_qiNiuConfig, request.PersistentOps.IsUseHttps);
            string url =
                $"{scheme}api.qiniu.com/v6/domain/list?tbl={Core.Tools.GetBucket(_qiNiuConfig, request.PersistentOps.Bucket)}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(new GetManageTokenParam(url))}"}
            };

            try
            {
                string response = _httpClient.GetString(url);
                return GetResponse(response, () => new DomainResultDto(true,
                        _jsonProvider.Deserialize<List<string>>(response),
                        "success"),
                    resultResponse =>
                        new DomainResultDto(false, null, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new DomainResultDto(false, null, $"lose {ex.Message}");
            }
        }

        #endregion

        #region 空间权限管理

        #region 设置 Bucket 访问权限

        /// <summary>
        /// 设置 Bucket 访问权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto SetPermiss(
            EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket.SetPermissParam request)
        {
            new SetPermissParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            string url =
                $"http://uc.qbox.me/private?bucket={Core.Tools.GetBucket(_qiNiuConfig, request.PersistentOps.Bucket)}&private={request.Permiss.Id}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(new GetManageTokenParam(url))}"}
            };
            try
            {
                string response = _httpClient.GetStringByPost(url, new { }, RequestBodyFormat.Json);
                return GetResponse(response, () => new OperateResultDto(true, "success"),
                    resultResponse =>
                        new OperateResultDto(false, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new OperateResultDto(false, $"lose {ex.Message}");
            }
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
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region 防盗链

        #region 设置防盗链

        /// <summary>
        /// 设置防盗链
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto SetReferer(SetRefererParam request)
        {
            return new OperateResultDto(false, "不支持api设置防盗链");
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
            return new RefererResultDto("不支持api获取防盗链配置");
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
            return new OperateResultDto(false, "不支持api配置操作防盗链");
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
            var scheme = Core.Tools.GetScheme(_qiNiuConfig, request.PersistentOps.IsUseHttps);
            RestClient restClient = new RestClient($"{scheme}uc.qbox.me");
            string bucket = Core.Tools.GetBucket(_qiNiuConfig, request.PersistentOps.Bucket);
            RestRequest restRequest = new RestRequest($"/bucketTagging?bucket={bucket}", Method.PUT);
            GetManageTokenParam param =
                new GetManageTokenParam($"{scheme}uc.qbox.me/bucketTagging?bucket={bucket}");
            restRequest.AddHeader("Authorization",
                $"{_storageProvider.GetManageToken(param)}");
            restRequest.AddParameter("application/json; charset=utf-8;", _jsonProvider.Serializer(new
                {
                    Tags = request.Tags
                }),
                ParameterType.RequestBody);
            try
            {
                string response = restClient.Execute(restRequest).Content;
                return GetResponse(response, () => new OperateResultDto(true,
                        "success"),
                    resultResponse =>
                        new OperateResultDto(false, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new OperateResultDto(false, $"lose {ex.Message}");
            }
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
            new GetTagsBucketParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var scheme = Core.Tools.GetScheme(_qiNiuConfig, request.PersistentOps.IsUseHttps);
            string url =
                $"{scheme}uc.qbox.me/bucketTagging?bucket={Core.Tools.GetBucket(_qiNiuConfig, request.PersistentOps.Bucket)}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(new GetManageTokenParam(url))}"}
            };

            try
            {
                string response = _httpClient.GetString(url);
                return GetResponse(response, () =>
                    {
                        var tagInfo = _jsonProvider.Deserialize<BucketTagResultResponse>(response);
                        if (tagInfo != null)
                        {
                            return new TagResultDto(true, tagInfo.Tags, "success");
                        }

                        return new TagResultDto(false, new List<KeyValuePair<string, string>>(),
                            $"{response}，反序列化失败");
                    },
                    resultResponse =>
                        new TagResultDto(false, null, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new TagResultDto(false, null, $"lose {ex.Message}");
            }
        }

        #endregion

        #region 清空标签

        /// <summary>
        /// 清空标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto ClearTag(ClearTagBucketParam request)
        {
            new ClearTagBucketParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var scheme = Core.Tools.GetScheme(_qiNiuConfig, request.PersistentOps.IsUseHttps);
            string bucket = Core.Tools.GetBucket(_qiNiuConfig, request.PersistentOps.Bucket);
            RestClient restClient = new RestClient($"{scheme}uc.qbox.me");
            RestRequest restRequest = new RestRequest($"/bucketTagging?bucket={bucket}", Method.DELETE);

            GetManageTokenParam param =
                new GetManageTokenParam($"{scheme}uc.qbox.me/bucketTagging?bucket={bucket}");
            restRequest.AddHeader("Authorization",
                $"{_storageProvider.GetManageToken(param)}");
            restRequest.AddParameter("application/json; charset=utf-8;", _jsonProvider.Serializer(new { }),
                ParameterType.RequestBody);
            try
            {
                string response = restClient.Execute(restRequest).Content;
                return GetResponse(response, () => new OperateResultDto(true,
                        "success"),
                    resultResponse =>
                        new OperateResultDto(false, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new OperateResultDto(false, $"lose {ex.Message}");
            }
        }

        #endregion

        #endregion

        #region private methods

        #region 得到响应值

        /// <summary>
        ///
        /// </summary>
        /// <param name="response"></param>
        /// <param name="successFunc">成功回调</param>
        /// <param name="errorFunc">失败回调</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetResponse<T>(string response, Func<T> successFunc, Func<ErrorResultResponse, T> errorFunc)
        {
            if (!string.IsNullOrEmpty(response))
            {
                if (!response.Contains("error"))
                {
                    return successFunc();
                }

                var res = _jsonProvider.Deserialize<ErrorResultResponse>(response);
                return errorFunc.Invoke(res ?? new ErrorResultResponse()
                {
                    Error = "lose"
                });
            }

            return errorFunc.Invoke(new ErrorResultResponse()
            {
                Error = "lose"
            });
        }

        #endregion

        #endregion
    }
}
