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
            MaxRetryTimes = 5;
            ChunkUnit = EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.ChunkUnit.U1024K;
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
        /// 默认1MB
        /// </summary>
        public EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.ChunkUnit ChunkUnit { get; set; }

        #region 回调相关

        /// <summary>
        /// 上传成功后，七牛云向业务服务器发送 POST 请求的 URL
        /// 可以使用ip，减少对dns的依赖
        /// </summary>
        public string CallbackHost { get; private set; }

        /// <summary>
        /// 上传成功后，七牛云向业务服务器发送 POST 请求的 URL
        /// 可以使用ip，减少对dns的依赖
        /// 相对路径
        /// </summary>
        public string CallbackUrl { get; private set; }

        /// <summary>
        /// 回调内容
        /// </summary>
        public string CallbackBody { get; private set; }

        /// <summary>
        /// 回调内容类型
        /// </summary>
        public int CallbackBodyType { get; private set; }

        /// <summary>
        /// 启用回调
        /// </summary>
        public bool EnableCallback { get; private set; }

        #endregion

        #region 设置回调信息，不调用此方法，不开启成功回调

        /// <summary>
        /// 设置回调信息，不调用此方法，不开启成功回调
        /// </summary>
        /// <param name="callbackBodyType">上传成功后，云存储向业务服务器发送回调通知 callbackBody 的 Content-Type。默认为 application/json，也可设置为 application/x-www-form-urlencoded。</param>
        /// <param name="callbackHost">上传成功后，云存储向业务服务器发送回调通知时的 Host 值。与 callbackUrl 配合使用，仅当设置了 callbackUrl 时才有效。</param>
        /// <param name="callbackUrl">上传成功后，云存储向业务服务器发送 POST 请求的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。另外，为了给客户端有一致的体验，我们要求 callbackUrl 返回包 Content-Type 为 "application/json"，即返回的内容必须是合法的 JSON 文本。出于高可用的考虑，本字段允许设置多个 callbackUrl（用英文符号 ; 分隔），在前一个 callbackUrl 请求失败的时候会依次重试下一个 callbackUrl。一个典型例子是：http://<ip1>/callback;http://<ip2>/callback，并同时指定下面的 callbackHost 字段。在 callbackUrl 中使用 ip 的好处是减少对 dns 解析的依赖，可改善回调的性能和稳定性。指定 callbackUrl，必须指定 callbackbody，且值不能为空。</param>
        /// <param name="callbackBody">上传成功后，云存储向业务服务器发送 Content-Type: application/x-www-form-urlencoded 的 POST 请求。业务服务器可以通过直接读取请求的 query 来获得该字段，支持魔法变量和自定义变量。callbackBody 要求是合法的 url query string。例如bucket=${bucket}&object=${object}&etag=${etag}&size=${size}&mimeType=${mimeType}&imageInfo.height=${imageInfo.height}&imageInfo.width=${imageInfo.width}&imageInfo.format=${imageInfo.format}&my_var=${x:my_var}。如果callbackBodyType指定为application/json，则callbackBody应为json格式，例如:{\"mimeType\":${mimeType},\"size\":${size}}。</param>
        public void SetCallBack(int callbackBodyType, string callbackHost = "", string callbackUrl = "",
            string callbackBody =
                "{\"key\":\"${key}\",\"size\":${size},\"bucket\":\"${bucket}\",\"mimeType\":\"${mimeType}\"}")
        {
            CallbackBodyType = Configuration.Ioc.Plugs.Storage.Enumerations.CallbackBodyType
                .FromValue<EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.
                    CallbackBodyType>(
                    callbackBodyType)?.Id ?? Configuration.Ioc.Plugs.Storage
                .Enumerations.CallbackBodyType.Json.Id;
            CallbackHost = callbackHost;
            if (!string.IsNullOrEmpty(callbackUrl) && callbackUrl.Substring(0, 1) != "/")
            {
                CallbackUrl = "/" + callbackUrl;
            }
            else
            {
                CallbackUrl = callbackUrl;
            }

            CallbackBody = callbackBody;
            EnableCallback = true;
        }

        #endregion

        #region 启用回调（针对配置文件注入使用）

        /// <summary>
        /// 启用回调（针对配置文件注入使用）
        /// </summary>
        internal void SetCallBackState(bool enableCallback)
        {
            EnableCallback = enableCallback;
        }

        #endregion

        #region private methods

        #region 得到OssClient

        /// <summary>
        /// 得到客户端
        /// </summary>
        /// <param name="zone">空间区域</param>
        /// <param name="securityToken">凭证</param>
        /// <returns></returns>
        internal OssClient GetClient(ZoneEnum? zone, string securityToken = null)
        {
            if (zone == null)
            {
                zone = ZoneEnum.HangZhou;
            }

            var endpoint = zone.GetCustomerObj<ENameAttribute>()?.Name;
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new BusinessException<string>("不支持的空间区域", HttpStatus.Err.Name);
            }

            var scheme = "http://";
            return new OssClient($"{scheme}{endpoint}", AccessKey, SecretKey, securityToken);
        }

        #endregion

        #endregion
    }
}
