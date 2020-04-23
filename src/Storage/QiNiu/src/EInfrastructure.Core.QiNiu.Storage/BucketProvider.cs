// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Http.Enumerations;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Dto;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using EInfrastructure.Core.QiNiu.Storage.Validator;
using EInfrastructure.Core.QiNiu.Storage.Validator.Bucket;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Attributes;
using EInfrastructure.Core.Tools.Url;
using EInfrastructure.Core.Validation.Common;
using Qiniu.Util;
using RestSharp;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 空间
    /// </summary>
    public class BucketProvider : IBucketProvider
    {
        private readonly HttpClient _httpClient;
        private readonly QiNiuStorageConfig _qiNiuConfig;
        private readonly IStorageProvider _storageProvider;
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        public BucketProvider(QiNiuStorageConfig qiNiuConfig, IJsonProvider jsonProvider,
            IStorageProvider storageProvider)
        {
            _qiNiuConfig = qiNiuConfig;
            _jsonProvider = jsonProvider;
            _storageProvider = storageProvider;
            _httpClient = new HttpClient("http://rs.qbox.me");
        }

        #region 根据标签筛选空间获取空间名列表

        /// <summary>
        /// 根据标签筛选空间获取空间名列表
        /// </summary>
        /// <param name="tagFilter"></param>
        /// <returns></returns>
        public BucketItemResultDto GetBucketList(List<KeyValuePair<string, string>> tagFilter)
        {
            UrlParameter urlParameter = new UrlParameter();
            tagFilter.ForEach(tag => { urlParameter.Add(tag.Key, tag.Value); });
            string url =
                $"http://rs.qbox.me/buckets?tagCondition={Base64.UrlSafeBase64Encode(urlParameter.GetQueryResult())}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(url)}"}
            };
            try
            {
                var response = _httpClient.GetString(url);
                return GetResponse(response, () => new BucketItemResultDto(true,
                        _jsonProvider.Deserialize<List<string>>(response),
                        "success"),
                    resultResponse =>
                        new BucketItemResultDto(false, null, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
            }
            catch (Exception e)
            {
                return new BucketItemResultDto(false, null, $"lose {e.Message}");
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
            var reg = ((ZoneEnum) request.Region).GetCustomerObj<ENameAttribute>()?.Name ?? "";
            var scheme = Core.Tools.GetScheme(_qiNiuConfig, request.PersistentOps.IsUseHttps);
            string url = $"{scheme}rs.qbox.me/mkbucketv3/{request.BucketName}/region/{reg}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(url)}"}
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
                $"{scheme}uc.qbox.me/image/{request.BucketName}/from/{Base64.UrlSafeBase64Encode(request.ImageSource)}/host/{Base64.UrlSafeBase64Encode(request.ReferHost)}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(url)}"}
            };
            var response = _httpClient.GetStringByPost(url, new { }, RequestBodyFormat.Json);
            return GetResponse(response, () => new OperateResultDto(true,
                    "success"),
                resultResponse =>
                    new OperateResultDto(false, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
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
            string url = $"{scheme}rs.qbox.me/drop/{request.BucketName}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(url)}"}
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
            string url = $"{scheme}api.qiniu.com/v6/domain/list?tbl={request.BucketName}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(url)}"}
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
            catch (Exception e)
            {
                return new DomainResultDto(false, null, $"lose {e.Message}");
            }
        }

        #endregion

        #region 设置 Bucket 访问权限

        /// <summary>
        /// 设置 Bucket 访问权限
        /// </summary>
        /// <param name="bucketName">域名</param>
        /// <param name="permiss">访问权限</param>
        /// <returns></returns>
        public OperateResultDto SetPermiss(string bucketName, BucketPermiss permiss)
        {
            string url = $"http://uc.qbox.me/private?bucket={bucketName}&private={permiss.Id}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(url)}"}
            };
            try
            {
                string response = _httpClient.GetStringByPost(url, new { }, RequestBodyFormat.Json);
                return GetResponse(response, () => new OperateResultDto(true, "success"),
                    resultResponse =>
                        new OperateResultDto(false, $"{resultResponse.Error}|{resultResponse.ErrorCode}"));
            }
            catch (Exception e)
            {
                return new OperateResultDto(false, $"lose {e.Message}");
            }
        }

        #endregion

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
            RestRequest restRequest = new RestRequest($"/bucketTagging?bucket={request.BucketName}", Method.PUT);
            restRequest.AddHeader("Authorization",
                $"{_storageProvider.GetManageToken($"{scheme}uc.qbox.me/bucketTagging?bucket={request.BucketName}")}");
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
            catch (Exception e)
            {
                return new OperateResultDto(false, $"lose {e.Message}");
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
            string url = $"{scheme}uc.qbox.me/bucketTagging?bucket={request.BucketName}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(url)}"}
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
            catch (Exception e)
            {
                return new TagResultDto(false, null, $"lose {e.Message}");
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
            RestClient restClient = new RestClient($"{scheme}uc.qbox.me");
            RestRequest restRequest = new RestRequest($"/bucketTagging?bucket={request.BucketName}", Method.DELETE);
            restRequest.AddHeader("Authorization",
                $"{_storageProvider.GetManageToken($"{scheme}uc.qbox.me/bucketTagging?bucket={request.BucketName}")}");
            restRequest.AddParameter("application/json; charset=utf-8;", _jsonProvider.Serializer(new
                {
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
            catch (Exception e)
            {
                return new OperateResultDto(false, $"lose {e.Message}");
            }
        }

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
