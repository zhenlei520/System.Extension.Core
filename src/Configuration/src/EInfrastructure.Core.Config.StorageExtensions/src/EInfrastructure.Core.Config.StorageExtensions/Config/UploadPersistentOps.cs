// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Config.StorageExtensions.Enum;

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
        public bool IsUseHttps { get; set; } = false;

        /// <summary>
        /// [可选]是否用Cdn域
        /// </summary>
        public bool UseCdnDomains { get; set; } = false;

        /// <summary>
        /// [可选]是否覆盖上传
        /// </summary>
        public bool IsAllowOverlap { get; set; } = false;

        /// <summary>
        /// [可选]分片上传默认最大值
        /// </summary>
        public ChunkUnitEnum ChunkUnit { get; set; } = ChunkUnitEnum.U4096K;

        /// <summary>
        /// [可选]文件上传后多少天后自动删除
        /// </summary>
        public int? DeleteAfterDays { get; set; } = null;

        /// <summary>
        /// [可选]设置好上传凭证有效期,单位：秒
        /// </summary>
        public int ExpireInSeconds { get; set; } = 3600;

        /// <summary>
        /// [可选]文件的存储类型，默认为普通存储，设置为1为低频存储
        /// 1、低频存储，
        /// </summary>
        public int? FileType { get; set; } = null;

        /// <summary>
        /// [可选]上传时是否自动检测MIME
        /// 开启 MimeType 侦测功能。设为非 0 值，则忽略上传端传递的文件 MimeType 信息，使用七牛服务器侦测内容后的判断结果。默认设为 0 值，如上传端指定了 MimeType 则直接使用该值，否则按如下顺序侦测 MimeType 值：
        /// 1. 检查文件扩展名
        /// 2. 检查 Key 扩展名；
        /// 3. 侦测内容。
        /// 如不能侦测出正确的值，会默认使用 application/octet-stream。
        /// </summary>
        public int? DetectMime { get; set; } = null;

        /// <summary>
        /// [可选]上传文件MIME限制
        /// </summary>
        public string MimeLimit { get; set; }

        /// <summary>
        /// [可选]上传文件大小限制：最小值
        /// </summary>
        public int? FsizeMin { get; set; } = null;

        /// <summary>
        /// [可选]上传文件大小限制：最大值
        /// </summary>
        public int? FsizeLimit { get; set; } = null;

        /// <summary>
        /// [可选]上传策略
        /// </summary>
        public string PersistentOps { get; set; }

        #region 上传进度相关

        /// <summary>
        /// 设置文件断点续传进度记录文件
        /// </summary>
        public string ResumeRecordFile { set; get; }

        /// <summary>
        /// 上传可选参数字典，参数名次以 x: 开头
        /// </summary>
        public Dictionary<string, string> Params { get; set; }

        /// <summary>
        /// 指定文件的MimeType
        /// </summary>
        public string MimeType { set; get; }

        /// <summary>
        /// 设置文件上传进度处理器
        ///
        /// 已上传字节数，文件总字节数
        /// </summary>
        public Action<long, long> ProgressAction { set; get; } = null;

        /// <summary>
        /// 设置文件上传的状态控制器
        ///
        /// 设置文件上传状态
        /// </summary>
        public Action<UploadStateEnum> UploadController { set; get; }

        /// <summary>
        /// 最大重试次数
        /// </summary>
        public int MaxRetryTimes { set; get; } = 5;

        #endregion
    }
}