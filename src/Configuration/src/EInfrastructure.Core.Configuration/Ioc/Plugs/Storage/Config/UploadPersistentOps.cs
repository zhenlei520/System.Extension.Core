// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config
{
    /// <summary>
    /// 上传策略配置
    /// </summary>
    public class UploadPersistentOps : BasePersistentOps
    {
        /// <summary>
        ///
        /// </summary>
        public UploadPersistentOps():base()
        {
            IsAllowOverlap = null;
            ExpireInSeconds = 3600;
            FileType = null;
            DetectMime = FsizeMin;
            MimeLimit = "";
            FsizeMin = null;
            FsizeLimit = null;
            ProgressAction = null;
            EnablePersistentPipeline = true;
            EnablePersistentNotifyUrl = true;
            EnableCallback = null;
        }

        /// <summary>
        /// [可选]是否覆盖上传
        /// </summary>
        public virtual bool? IsAllowOverlap { get; set; }

        /// <summary>
        /// [可选]文件上传后多少天后自动删除
        /// </summary>
        public virtual int? DeleteAfterDays { get; set; }

        /// <summary>
        /// [可选]设置好上传凭证有效期,单位：秒
        /// </summary>
        public virtual int ExpireInSeconds { get; set; }

        /// <summary>
        /// [可选]文件的存储类型，默认为普通存储，设置为1为低频存储
        /// 1、低频存储，
        /// </summary>
        public virtual int? FileType { get; set; }

        #region MIME

        /// <summary>
        /// [可选]上传时是否自动检测MIME
        /// 开启 MimeType 侦测功能。设为非 0 值，则忽略上传端传递的文件 MimeType 信息，使用七牛服务器侦测内容后的判断结果。默认设为 0 值，如上传端指定了 MimeType 则直接使用该值，否则按如下顺序侦测 MimeType 值：
        /// 1. 检查文件扩展名
        /// 2. 检查 Key 扩展名；
        /// 3. 侦测内容。
        /// 如不能侦测出正确的值，会默认使用 application/octet-stream。
        /// </summary>
        public virtual int? DetectMime { get; set; }

        /// <summary>
        /// 指定文件的MimeType
        /// </summary>
        public virtual string MimeType { set; get; }

        #endregion

        /// <summary>
        /// [可选]上传文件MIME限制
        /// </summary>
        public virtual string MimeLimit { get; set; }

        /// <summary>
        /// [可选]上传文件大小限制：最小值
        /// </summary>
        public virtual int? FsizeMin { get; set; }

        /// <summary>
        /// [可选]上传文件大小限制：最大值
        /// </summary>
        public virtual int? FsizeLimit { get; set; }

        /// <summary>
        /// [可选]上传策略
        /// </summary>
        public virtual string PersistentOps { get; set; }

        #region 上传成功

        #region 表单上传

        /// <summary>
        /// Web 端文件上传成功后，浏览器执行 303 跳转的 URL。如不设置 returnUrl，则直接将 returnBody 的内容返回给客户端
        /// </summary>
        public virtual string ReturnUrl { get; private set; }

        /// <summary>
        /// 上传成功后，自定义云存储最终返回給上传端（在指定 returnUrl 时是携带在跳转路径参数中）的数据。支持魔法变量和自定义变量。returnBody 要求是合法的 JSON 文本。例如 {"key": $(key), "hash": $(etag), "w": $(imageInfo.width), "h": $(imageInfo.height)}。
        /// 客户端上传后给客户端的响应信息
        /// </summary>
        public virtual string ReturnBody { get; private set; }

        #endregion

        /// <summary>
        /// 启用回调
        /// </summary>
        public virtual bool? EnableCallback { get; private set; }

        /// <summary>
        /// 上传成功后，云存储向业务服务器发送 POST 请求的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。另外，为了给客户端有一致的体验，我们要求 callbackUrl 返回包 Content-Type 为 "application/json"，即返回的内容必须是合法的 JSON 文本。出于高可用的考虑，本字段允许设置多个 callbackUrl（用英文符号 ; 分隔），在前一个 callbackUrl 请求失败的时候会依次重试下一个 callbackUrl。一个典型例子是：http://<ip1>/callback;http://<ip2>/callback，并同时指定下面的 callbackHost 字段。在 callbackUrl 中使用 ip 的好处是减少对 dns 解析的依赖，可改善回调的性能和稳定性。指定 callbackUrl，必须指定 callbackbody，且值不能为空。
        /// 相对路径，首位为
        ///
        /// 例如要请求的地址为：https://zhenlei520.com/api/callback，则填写/api/callback
        /// </summary>
        public virtual string CallbackUrl { get; private set; }

        /// <summary>
        /// 上传成功后，云存储向业务服务器发送回调通知时的 Host 值。与 callbackUrl 配合使用，仅当设置了 callbackUrl 时才有效。
        /// </summary>
        public virtual string CallbackHost { get; private set; }

        /// <summary>
        /// 上传成功后，云存储向业务服务器发送 Content-Type: application/x-www-form-urlencoded 的 POST 请求。业务服务器可以通过直接读取请求的 query 来获得该字段，支持魔法变量和自定义变量。callbackBody 要求是合法的 url query string。例如key=$(key)&hash=$(etag)&w=$(imageInfo.width)&h=$(imageInfo.height)。如果callbackBodyType指定为application/json，则callbackBody应为json格式，例如:{"key":"$(key)","hash":"$(etag)","w":"$(imageInfo.width)","h":"$(imageInfo.height)"}。
        /// </summary>
        public virtual string CallbackBody { get; private set; }

        /// <summary>
        /// 上传成功后，云存储向业务服务器发送回调通知 callbackBody 的 Content-Type。默认为 application/json，也可设置为 application/x-www-form-urlencoded。
        /// </summary>
        public virtual string CallbackBodyType { get; private set; }

        /// <summary>
        /// 持久化通知状态，默认true
        /// </summary>
        public virtual bool EnablePersistentNotifyUrl { get; private set; }

        /// <summary>
        /// 接收持久化处理结果通知的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。该 URL 获取的内容和持久化处理状态查询的处理结果一致。发送 body 格式是 Content-Type 为 application/json 的 POST 请求，需要按照读取流的形式读取请求的 body 才能获取。
        /// 目前发现没什么作用
        /// </summary>
        public virtual string PersistentNotifyUrl { get; private set; }

        #region 是否使用私有对列

        /// <summary>
        /// 是否使用专用对列
        /// </summary>
        public bool EnablePersistentPipeline { get; private set; }

        /// <summary>
        /// 转码队列名。资源上传成功后，触发转码时指定独立的队列进行转码。为空则表示使用公用队列，处理速度比较慢。建议使用专用队列。
        /// </summary>
        public virtual string PersistentPipeline { get; private set; }

        #endregion

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
        /// 设置文件上传进度处理器
        ///
        /// 已上传字节数，文件总字节数
        /// </summary>
        public virtual Action<long, long> ProgressAction { set; get; }

        /// <summary>
        /// 设置文件上传的状态控制器
        ///
        /// 设置文件上传状态
        /// </summary>
        public virtual Action<UploadState> UploadController { set; get; }

        #endregion

        #region 使用对列

        /// <summary>
        /// 使用对列
        /// </summary>
        /// <param name="persistentPipeline">队列名（可为空）</param>
        public void SetPersistentPipeline(string persistentPipeline)
        {
            PersistentPipeline = persistentPipeline;
        }

        /// <summary>
        /// 关闭对列
        /// </summary>
        public void ClosePersistentPipeline()
        {
            EnablePersistentPipeline = false;
        }

        #endregion

        #region 接收持久化处理结果通知管理

        /// <summary>
        /// 设置持久化通知
        /// </summary>
        /// <param name="persistentNotifyUrl">接收持久化处理结果通知的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。该 URL 获取的内容和持久化处理状态查询的处理结果一致。发送 body 格式是 Content-Type 为 application/json 的 POST 请求，需要按照读取流的形式读取请求的 body 才能获取。</param>
        public void SetPersistentNotifyUrl(string persistentNotifyUrl)
        {
            PersistentNotifyUrl = persistentNotifyUrl;
        }

        /// <summary>
        /// 接收持久化处理结果通知管理
        /// </summary>
        public void ClosePersistentNotifyUrl()
        {
            EnablePersistentNotifyUrl = false;
        }

        #endregion

        #region 设置响应信息（表单提交）

        /// <summary>
        ///
        /// </summary>
        /// <param name="returnUrl">Web 端文件上传成功后，浏览器执行 303 跳转的 URL。如不设置 returnUrl，则直接将 returnBody 的内容返回给客户端</param>
        /// <param name="returnBody">上传成功后，自定义云存储最终返回給上传端（在指定 returnUrl 时是携带在跳转路径参数中）的数据。支持魔法变量和自定义变量。returnBody 要求是合法的 JSON 文本。例如 {"key": $(key), "hash": $(etag), "w": $(imageInfo.width), "h": $(imageInfo.height)}。</param>
        public void SetReturn(string returnUrl, string returnBody)
        {
            ReturnUrl = returnUrl;
            ReturnBody = returnBody;
        }

        #endregion

        #region 设置回调信息

        /// <summary>
        /// 设置回调信息（不赋值则默认走全局回调）
        /// </summary>
        /// <param name="callbackHost">上传成功后，云存储向业务服务器发送回调通知时的 Host 值。与 callbackUrl 配合使用，仅当设置了 callbackUrl 时才有效。</param>
        /// <param name="callbackUrl">上传成功后，云存储向业务服务器发送 POST 请求的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。另外，为了给客户端有一致的体验，我们要求 callbackUrl 返回包 Content-Type 为 "application/json"，即返回的内容必须是合法的 JSON 文本。出于高可用的考虑，本字段允许设置多个 callbackUrl（用英文符号 ; 分隔），在前一个 callbackUrl 请求失败的时候会依次重试下一个 callbackUrl。一个典型例子是：http://<ip1>/callback;http://<ip2>/callback，并同时指定下面的 callbackHost 字段。在 callbackUrl 中使用 ip 的好处是减少对 dns 解析的依赖，可改善回调的性能和稳定性。指定 callbackUrl，必须指定 callbackbody，且值不能为空。</param>
        /// <param name="callbackBody">上传成功后，云存储向业务服务器发送 Content-Type: application/x-www-form-urlencoded 的 POST 请求。业务服务器可以通过直接读取请求的 query 来获得该字段，支持魔法变量和自定义变量。callbackBody 要求是合法的 url query string。例如key=$(key)&hash=$(etag)&w=$(imageInfo.width)&h=$(imageInfo.height)。如果callbackBodyType指定为application/json，则callbackBody应为json格式，例如:{"key":"$(key)","hash":"$(etag)","w":"$(imageInfo.width)","h":"$(imageInfo.height)"}。</param>
        /// <param name="callbackBodyType">上传成功后，云存储向业务服务器发送回调通知 callbackBody 的 Content-Type。默认为 application/json，也可设置为 application/x-www-form-urlencoded。</param>
        public void SetCallBack(int callbackBodyType, string callbackHost = "", string callbackUrl = "",
            string callbackBody =
                "{\"key\":\"$(key)\",\"hash\":\"$(etag)\",\"fsiz\":$(fsize),\"bucket\":\"$(bucket)\",\"name\":\"$(x:name)\",\"mimeType\":\"$(mimeType)\"}")
        {
            SetCallBack(EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.CallbackBodyType
                .FromValue<CallbackBodyType>(
                    callbackBodyType)?.Name ?? EInfrastructure.Core.Configuration.Ioc.Plugs.Storage
                .Enumerations.CallbackBodyType.Json.Name, callbackHost, callbackUrl, callbackBody);
        }

        /// <summary>
        /// 设置回调信息（不赋值则默认走全局回调）
        /// </summary>
        /// <param name="callbackHost">上传成功后，云存储向业务服务器发送回调通知时的 Host 值。与 callbackUrl 配合使用，仅当设置了 callbackUrl 时才有效。</param>
        /// <param name="callbackUrl">上传成功后，云存储向业务服务器发送 POST 请求的 URL。必须是公网上可以正常进行 POST 请求并能响应 HTTP/1.1 200 OK 的有效 URL。另外，为了给客户端有一致的体验，我们要求 callbackUrl 返回包 Content-Type 为 "application/json"，即返回的内容必须是合法的 JSON 文本。出于高可用的考虑，本字段允许设置多个 callbackUrl（用英文符号 ; 分隔），在前一个 callbackUrl 请求失败的时候会依次重试下一个 callbackUrl。一个典型例子是：http://<ip1>/callback;http://<ip2>/callback，并同时指定下面的 callbackHost 字段。在 callbackUrl 中使用 ip 的好处是减少对 dns 解析的依赖，可改善回调的性能和稳定性。指定 callbackUrl，必须指定 callbackbody，且值不能为空。</param>
        /// <param name="callbackBody">上传成功后，云存储向业务服务器发送 Content-Type: application/x-www-form-urlencoded 的 POST 请求。业务服务器可以通过直接读取请求的 query 来获得该字段，支持魔法变量和自定义变量。callbackBody 要求是合法的 url query string。例如key=$(key)&hash=$(etag)&w=$(imageInfo.width)&h=$(imageInfo.height)。如果callbackBodyType指定为application/json，则callbackBody应为json格式，例如:{"key":"$(key)","hash":"$(etag)","w":"$(imageInfo.width)","h":"$(imageInfo.height)"}。</param>
        /// <param name="callbackBodyType">上传成功后，云存储向业务服务器发送回调通知 callbackBody 的 Content-Type。默认为 application/json，也可设置为 application/x-www-form-urlencoded。</param>
        public void SetCallBack(string callbackBodyType, string callbackHost = "", string callbackUrl = "",
            string callbackBody =
                "{\"key\":\"$(key)\",\"hash\":\"$(etag)\",\"fsiz\":$(fsize),\"bucket\":\"$(bucket)\",\"name\":\"$(x:name)\",\"mimeType\":\"$(mimeType)\"}")
        {
            CallbackHost = callbackHost;
            CallbackUrl = callbackUrl;
            CallbackBody = callbackBody;
            CallbackBodyType = !string.IsNullOrEmpty(callbackBodyType)
                ? callbackBodyType
                : EInfrastructure.Core.Configuration.Ioc.Plugs.Storage
                    .Enumerations.CallbackBodyType.Json.Name;
            EnableCallback = true; //默认启用回调
        }

        /// <summary>
        /// 关闭当前请求回调
        /// </summary>
        public void CloseCallBack()
        {
            EnableCallback = false;
        }

        #endregion
    }
}
