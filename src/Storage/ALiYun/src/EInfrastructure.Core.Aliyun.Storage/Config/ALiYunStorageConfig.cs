// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Aliyun.OSS;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Attributes;
using EInfrastructure.Core.Validation;

namespace EInfrastructure.Core.Aliyun.Storage.Config
{
    /// <summary>
    /// 阿里云存储
    /// </summary>
    public class ALiYunStorageConfig : IFluentlValidatorEntity
    {
        /// <summary>
        ///
        /// </summary>
        public ALiYunStorageConfig()
        {
            this.MaxRetryTimes = 5;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        public ALiYunStorageConfig(string accessKey, string secretKey) : this()
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accessKey">公钥，用于识别用户</param>
        /// <param name="secretKey">秘钥</param>
        /// <param name="zones">空间区域</param>
        /// <param name="host">文件对外访问的主机名</param>
        /// <param name="bucket">默认空间</param>
        public ALiYunStorageConfig(string accessKey, string secretKey, ZoneEnum zones, string host, string bucket) :
            this(accessKey, secretKey)
        {
            DefaultZones = zones;
            DefaultHost = host;
            DefaultBucket = bucket;
        }

        /// <summary>
        /// 公钥，用于识别用户
        /// </summary>
        public string AccessKey { get; private set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecretKey { get; private set; }

        #region 默认空间信息

        /// <summary>
        /// 空间区域
        /// </summary>
        public ZoneEnum? DefaultZones { get; private set; }

        /// <summary>
        /// 文件对外访问的主机名
        /// </summary>
        public string DefaultHost { get; private set; }

        /// <summary>
        /// 存储的空间名
        /// </summary>
        public string DefaultBucket { get; private set; }

        #endregion

        /// <summary>
        /// 最大重试次数
        /// </summary>
        public int MaxRetryTimes { set; get; }

        /// <summary>
        /// [可选]分片上传默认最大值
        /// </summary>
        public EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.ChunkUnit ChunkUnit { get; set; }

        #region private methods

        #region 得到OssClient

        /// <summary>
        /// 得到客户端
        /// </summary>
        /// <param name="zone">空间区域</param>
        /// <returns></returns>
        internal OssClient GetClient(ZoneEnum zone)
        {
            var endpoint = zone.GetCustomerObj<ENameAttribute>()?.Name;
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new BusinessException<string>("不支持的空间区域", HttpStatus.Err.Name);
            }

            var scheme = "http://";
            return new OssClient($"{scheme}{endpoint}", this.AccessKey, this.SecretKey);
        }

        #endregion

        #endregion
    }
}
