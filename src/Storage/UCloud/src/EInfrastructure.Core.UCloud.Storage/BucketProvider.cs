// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket;

namespace EInfrastructure.Core.UCloud.Storage
{
    /// <summary>
    /// 空间
    /// </summary>
    public class BucketProvider:IBucketProvider
    {
        public List<string> GetBucketList(List<KeyValuePair<string, string>> tagFilter)
        {
            throw new System.NotImplementedException();
        }

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
