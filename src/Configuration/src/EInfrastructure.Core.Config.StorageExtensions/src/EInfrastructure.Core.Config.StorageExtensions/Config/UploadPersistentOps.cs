// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Config.StorageExtensions.Enumeration;

namespace EInfrastructure.Core.Config.StorageExtensions.Config
{
    /// <summary>
    /// 上传策略配置
    /// </summary>
    public class UploadPersistentOps
    {
        /// <summary>
        /// [可选]是否使用Https域名
        /// </summary>
        public virtual bool IsUseHttps { get; set; } = false;

        /// <summary>
        /// [可选]是否用Cdn域
        /// </summary>
        public virtual bool UseCdnDomains { get; set; } = false;

        /// <summary>
        /// [可选]是否覆盖上传
        /// </summary>
        public virtual bool IsAllowOverlap { get; set; } = false;

        /// <summary>
        /// [可选]分片上传默认最大值
        /// </summary>
        public virtual ChunkUnit ChunkUnit { get; set; } = ChunkUnit.U4096K;

        /// <summary>
        /// [可选]文件上传后多少天后自动删除
        /// </summary>
        public virtual int? DeleteAfterDays { get; set; } = null;

        /// <summary>
        /// [可选]设置好上传凭证有效期,单位：秒
        /// </summary>
        public virtual int ExpireInSeconds { get; set; } = 3600;

        /// <summary>
        /// [可选]文件的存储类型，默认为普通存储，设置为1为低频存储
        /// 1、低频存储，
        /// </summary>
        public virtual int? FileType { get; set; } = null;

        /// <summary>
        /// [可选]上传时是否自动检测MIME
        /// 开启 MimeType 侦测功能。设为非 0 值，则忽略上传端传递的文件 MimeType 信息，使用七牛服务器侦测内容后的判断结果。默认设为 0 值，如上传端指定了 MimeType 则直接使用该值，否则按如下顺序侦测 MimeType 值：
        /// 1. 检查文件扩展名
        /// 2. 检查 Key 扩展名；
        /// 3. 侦测内容。
        /// 如不能侦测出正确的值，会默认使用 application/octet-stream。
        /// </summary>
        public virtual int? DetectMime { get; set; } = null;

        /// <summary>
        /// [可选]上传文件MIME限制
        /// </summary>
        public virtual string MimeLimit { get; set; }

        /// <summary>
        /// [可选]上传文件大小限制：最小值
        /// </summary>
        public virtual int? FsizeMin { get; set; } = null;

        /// <summary>
        /// [可选]上传文件大小限制：最大值
        /// </summary>
        public virtual int? FsizeLimit { get; set; } = null;

        /// <summary>
        /// [可选]上传策略
        /// </summary>
        public virtual string PersistentOps { get; set; }

        #region 上传成功

        /// <summary>
        /// Web 端文件上传成功后，浏览器执行 303 跳转的 URL。如不设置 returnUrl，则直接将 returnBody 的内容返回给客户端
        /// </summary>
        public virtual string ReturnUrl { get; set; }

        /// <summary>
        /// 上传成功后，自定义云存储最终返回給上传端（在指定 returnUrl 时是携带在跳转路径参数中）的数据。支持魔法变量和自定义变量。returnBody 要求是合法的 JSON 文本。例如 {"key": $(key), "hash": $(etag), "w": $(imageInfo.width), "h": $(imageInfo.height)}。
        /// </summary>
        public virtual string ReturnBody { get; set; }

        /// <summary>
        /// 上传成功后，云存储向业务服务器发送 POST 请求的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。另外，为了给客户端有一致的体验，我们要求 callbackUrl 返回包 Content-Type 为 "application/json"，即返回的内容必须是合法的 JSON 文本。出于高可用的考虑，本字段允许设置多个 callbackUrl（用英文符号 ; 分隔），在前一个 callbackUrl 请求失败的时候会依次重试下一个 callbackUrl。一个典型例子是：http://<ip1>/callback;http://<ip2>/callback，并同时指定下面的 callbackHost 字段。在 callbackUrl 中使用 ip 的好处是减少对 dns 解析的依赖，可改善回调的性能和稳定性。指定 callbackUrl，必须指定 callbackbody，且值不能为空。
        /// </summary>
        public virtual string CallbackUrl { get; set; }

        /// <summary>
        /// 上传成功后，云存储向业务服务器发送回调通知时的 Host 值。与 callbackUrl 配合使用，仅当设置了 callbackUrl 时才有效。
        /// </summary>
        public virtual string CallbackHost { get; set; }

        /// <summary>
        /// 上传成功后，云存储向业务服务器发送 Content-Type: application/x-www-form-urlencoded 的 POST 请求。业务服务器可以通过直接读取请求的 query 来获得该字段，支持魔法变量和自定义变量。callbackBody 要求是合法的 url query string。例如key=$(key)&hash=$(etag)&w=$(imageInfo.width)&h=$(imageInfo.height)。如果callbackBodyType指定为application/json，则callbackBody应为json格式，例如:{"key":"$(key)","hash":"$(etag)","w":"$(imageInfo.width)","h":"$(imageInfo.height)"}。
        /// </summary>
        public virtual string CallbackBody { get; set; }

        /// <summary>
        /// 上传成功后，云存储向业务服务器发送回调通知 callbackBody 的 Content-Type。默认为 application/x-www-form-urlencoded，也可设置为 application/json。
        /// </summary>
        public virtual string CallbackBodyType { get; set; }

        /// <summary>
        ///接收持久化处理结果通知的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。该 URL 获取的内容和持久化处理状态查询的处理结果一致。发送 body 格式是 Content-Type 为 application/json 的 POST 请求，需要按照读取流的形式读取请求的 body 才能获取。
        /// </summary>
        public virtual string PersistentNotifyUrl { get; set; }

        /// <summary>
        /// 转码队列名。资源上传成功后，触发转码时指定独立的队列进行转码。为空则表示使用公用队列，处理速度比较慢。建议使用专用队列。
        /// </summary>
        public virtual string PersistentPipeline { get; set; }

        /// <summary>
        /// 自定义资源名。支持魔法变量和自定义变量。forceSaveKey 为false时，这个字段仅当用户上传的时候没有主动指定 key 时起作用；forceSaveKey 为true时，将强制按这个字段的格式命名。
        /// </summary>
        public virtual string SaveKey { get; set; }

        #endregion

        #region 上传进度相关

        /// <summary>
        /// 设置文件断点续传进度记录文件
        /// </summary>
        public virtual string ResumeRecordFile { set; get; }

        /// <summary>
        /// 上传可选参数字典，参数名次以 x: 开头
        /// </summary>
        public virtual Dictionary<string, string> Params { get; set; }

        /// <summary>
        /// 指定文件的MimeType
        /// </summary>
        public virtual string MimeType { set; get; }

        /// <summary>
        /// 设置文件上传进度处理器
        ///
        /// 已上传字节数，文件总字节数
        /// </summary>
        public virtual Action<long, long> ProgressAction { set; get; } = null;

        /// <summary>
        /// 设置文件上传的状态控制器
        ///
        /// 设置文件上传状态
        /// </summary>
        public virtual Action<UploadState> UploadController { set; get; }

        /// <summary>
        /// 最大重试次数
        /// </summary>
        public virtual int MaxRetryTimes { set; get; } = 5;

        #endregion
    }
}
