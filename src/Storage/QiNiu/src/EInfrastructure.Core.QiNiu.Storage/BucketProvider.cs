// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Enumerable;
using EInfrastructure.Core.Tools.Url;
using Qiniu.Util;

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

        /// <summary>
        ///
        /// </summary>
        public BucketProvider(QiNiuStorageConfig qiNiuConfig, IStorageProvider storageProvider)
        {
            _qiNiuConfig = qiNiuConfig;
            _storageProvider = storageProvider;
            _httpClient = new HttpClient("http://rs.qbox.me");
        }

        #region 根据标签筛选空间获取空间名列表

        /// <summary>
        /// 根据标签筛选空间获取空间名列表
        /// </summary>
        /// <param name="tagFilter"></param>
        /// <returns></returns>
        public List<string> GetBucketList(List<KeyValuePair<string, string>> tagFilter)
        {
            UrlParameter urlParameter = new UrlParameter();
            tagFilter.ForEach(tag =>
            {
                urlParameter.Add(tag.Key, tag.Value);
            });
            string url = $"http://rs.qbox.me/buckets?tagCondition={Base64.UrlSafeBase64Encode(urlParameter.GetQueryResult())}";
            _httpClient.Headers = new Dictionary<string, string>()
            {
                {"Authorization", $"{_storageProvider.GetManageToken(url)}"}
            };
            string res = _httpClient.GetString(url);
            return new List<string>();
        }

        #endregion

        public OperateResultDto Create(string bucketName, int region)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto Delete(string bucketName)
        {
            throw new System.NotImplementedException();
        }

        public DomainResultDto GetHost(string bucketName)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto SetPermiss(string bucketName, int permiss)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto SetTag(string bucketName, string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public TagResultDto GetTags(string bucketName)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto ClearTag(string bucketName)
        {
            throw new System.NotImplementedException();
        }
    }
}
