// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.QiNiu.Storage.Enum;
using EInfrastructure.Core.Validation;
using Qiniu.Storage;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage.Config
{
    /// <summary>
    /// 七牛配置
    /// </summary>
    public class QiNiuStorageConfig : IFluentlValidatorEntity
    {
        /// <summary>
        ///
        /// </summary>
        public QiNiuStorageConfig()
        {
            IsUseHttps = false;
            UseCdnDomains = false;
            IsAllowOverlap = false;
            MaxRetryTimes = 5;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        /// <param name="zones"></param>
        /// <param name="host"></param>
        /// <param name="bucket"></param>
        public QiNiuStorageConfig(string accessKey, string secretKey, ZoneEnum zones, string host, string bucket) :
            this()
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
            Zones = zones;
            Host = host;
            Bucket = bucket;
        }

        /// <summary>
        /// 七牛提供的公钥，用于识别用户
        /// </summary>
        public string AccessKey { get; private set; }

        /// <summary>
        /// 七牛提供的秘钥
        /// </summary>
        public string SecretKey { get; private set; }

        /// <summary>
        /// 七牛资源上传服务器地址.
        /// </summary>
        internal string UpHost
        {
            get
            {
                switch (Zones)
                {
                    case ZoneEnum.ZoneCnEast:
                    default:
                        return "http://upload.qiniup.com";
                    case ZoneEnum.ZoneCnNorth:
                        return "http://upload-z1.qiniup.com";
                    case ZoneEnum.ZoneCnSouth:
                        return "http://upload-z2.qiniup.com";
                    case ZoneEnum.ZoneUsNorth:
                        return "http://upload-na0.qiniup.com";
                }
            }
        }

        /// <summary>
        /// 空间
        /// </summary>
        public ZoneEnum Zones { get; private set; }

        /// <summary>
        /// 是否启用https
        /// 默认不启用
        /// </summary>
        public bool IsUseHttps { get; set; }

        /// <summary>
        /// 是否用Cdn域
        /// 默认不启用
        /// </summary>
        public bool UseCdnDomains { get; set; }

        /// <summary>
        /// 文件对外访问的主机名
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// 存储的空间名
        /// </summary>
        public string Bucket { get; private set; }

        /// <summary>
        /// 传输队列
        /// </summary>
        public string PersistentPipeline { get; set; }

        /// <summary>
        /// 持久化结果通知（持久化文件时用到）
        /// </summary>
        public string PersistentNotifyUrl { get; set; }

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
        /// 是否覆盖上传 默认false
        /// </summary>
        public bool IsAllowOverlap { get; set; }

        /// <summary>
        /// 启用回调
        /// </summary>
        public bool EnableCallback { get; private set; }

        /// <summary>
        /// 最大重试次数
        /// </summary>
        public int MaxRetryTimes { set; get; }

        /// <summary>
        /// [可选]分片上传默认最大值
        /// </summary>
        public virtual EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.ChunkUnit ChunkUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 得到空间
        /// </summary>
        /// <returns></returns>
        public Zone GetZone()
        {
            switch (Zones)
            {
                case ZoneEnum.ZoneCnEast:
                default:
                    return Zone.ZONE_CN_East;
                case ZoneEnum.ZoneCnNorth:
                    return Zone.ZONE_CN_North;
                case ZoneEnum.ZoneCnSouth:
                    return Zone.ZONE_CN_South;
                case ZoneEnum.ZoneUsNorth:
                    return Zone.ZONE_US_North;
                case ZoneEnum.ZoneAsSingapore:
                    return Zone.ZONE_AS_Singapore;
            }
        }

        #region 得到Mac

        private Mac _mac;

        /// <summary>
        /// 得到Mac
        /// </summary>
        /// <returns></returns>
        internal Mac GetMac()
        {
            return _mac ?? (_mac = new Mac(this.AccessKey, this.SecretKey));
        }

        #endregion

        #region 设置回调信息，不调用此方法，不开启成功回调

        /// <summary>
        /// 设置回调信息，不调用此方法，不开启成功回调
        /// </summary>
        /// <param name="callbackBodyType">上传成功后，云存储向业务服务器发送回调通知 callbackBody 的 Content-Type。默认为 application/json，也可设置为 application/x-www-form-urlencoded。</param>
        /// <param name="callbackHost">上传成功后，云存储向业务服务器发送回调通知时的 Host 值。与 callbackUrl 配合使用，仅当设置了 callbackUrl 时才有效。</param>
        /// <param name="callbackUrl">上传成功后，云存储向业务服务器发送 POST 请求的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。另外，为了给客户端有一致的体验，我们要求 callbackUrl 返回包 Content-Type 为 "application/json"，即返回的内容必须是合法的 JSON 文本。出于高可用的考虑，本字段允许设置多个 callbackUrl（用英文符号 ; 分隔），在前一个 callbackUrl 请求失败的时候会依次重试下一个 callbackUrl。一个典型例子是：http://<ip1>/callback;http://<ip2>/callback，并同时指定下面的 callbackHost 字段。在 callbackUrl 中使用 ip 的好处是减少对 dns 解析的依赖，可改善回调的性能和稳定性。指定 callbackUrl，必须指定 callbackbody，且值不能为空。</param>
        /// <param name="callbackBody">上传成功后，云存储向业务服务器发送 Content-Type: application/x-www-form-urlencoded 的 POST 请求。业务服务器可以通过直接读取请求的 query 来获得该字段，支持魔法变量和自定义变量。callbackBody 要求是合法的 url query string。例如key=$(key)&hash=$(etag)&w=$(imageInfo.width)&h=$(imageInfo.height)。如果callbackBodyType指定为application/json，则callbackBody应为json格式，例如:{"key":"$(key)","hash":"$(etag)","w":"$(imageInfo.width)","h":"$(imageInfo.height)"}。</param>
        public void SetCallBack(int callbackBodyType, string callbackHost = "", string callbackUrl = "",
            string callbackBody =
                "{\"key\":\"$(key)\",\"hash\":\"$(etag)\",\"fsiz\":$(fsize),\"bucket\":\"$(bucket)\",\"name\":\"$(x:name)\",\"mimeType\":\"$(mimeType)\"}")
        {
            CallbackBodyType = EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.CallbackBodyType
                .FromValue<EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.
                    CallbackBodyType>(
                    callbackBodyType)?.Id ?? EInfrastructure.Core.Configuration.Ioc.Plugs.Storage
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
    }
}
