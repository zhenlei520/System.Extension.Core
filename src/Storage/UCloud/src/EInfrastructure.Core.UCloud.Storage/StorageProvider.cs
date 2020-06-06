// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.UCloud.Storage.Config;

namespace EInfrastructure.Core.UCloud.Storage
{
    /// <summary>
    /// UCloud存储实现类
    /// </summary>
    public class StorageProvider : BaseStorageProvider, IStorageProvider
    {
        /// <summary>
        /// UCloud存储实现类
        /// </summary>
        public StorageProvider(ICollection<ILogProvider> logService, UCloudStorageConfig uCloudStorageConfig) : base(
            logService,
            uCloudStorageConfig)
        {
        }

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region 根据文件流上传

        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="isResume">是否允许续传</param>
        /// <returns></returns>
        public UploadResultDto UploadStream(UploadByStreamParam param, bool isResume = false)
        {
            var res = base.UploadFile(param.Stream, param.Key, Path.GetExtension(param.Key));
            return new UploadResultDto(res, "", res ? "上传成功" : "上传失败");
        }

        #endregion

        #region 根据文件字节数组上传

        /// <summary>
        /// 根据文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <param name="isResume">是否允许续传</param>
        /// <returns></returns>
        public UploadResultDto UploadByteArray(UploadByByteArrayParam param, bool isResume = false)
        {
            var res = base.UploadFile(param.ByteArray.ConvertToStream(), param.Key, Path.GetExtension(param.Key));
            return new UploadResultDto(res, "", res ? "上传成功" : "上传失败");
        }

        #endregion

        #region 根据文件token上传

        /// <summary>
        /// 根据文件流以及文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <returns></returns>
        public UploadResultDto UploadByToken(UploadByTokenParam param)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 得到凭证

        #region 得到上传凭证

        /// <summary>
        /// 得到上传凭证
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        public string GetUploadCredentials(UploadPersistentOpsParam opsParam)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 得到管理凭证

        /// <summary>
        /// 得到管理凭证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetManageToken(GetManageTokenParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 得到下载凭证

        /// <summary>
        /// 得到下载凭证
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public string GetDownloadToken(string url)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region 检查文件是否存在

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto Exist(ExistParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取指定前缀的文件列表

        /// <summary>
        /// 获取指定前缀的文件列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <returns></returns>
        public ListFileItemResultDto ListFiles(ListFileFilter filter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取文件信息

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FileInfoDto Get(GetFileParam request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取文件信息集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<FileInfoDto> GetList(GetFileRangeParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 删除文件

        /// <summary>
        /// 根据文件key删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DeleteResultDto Remove(RemoveParam request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据文件key集合删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<DeleteResultDto> RemoveRange(RemoveRangeParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 批量复制文件

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="copyFileParam">复制到新空间的参数</param>
        /// <returns></returns>
        public CopyFileResultDto CopyTo(CopyFileParam copyFileParam)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<CopyFileResultDto> CopyRangeTo(CopyFileRangeParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 批量移动文件（两个文件需要在同一账号下）

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="moveFileParam"></param>
        /// <returns></returns>
        public MoveFileResultDto Move(MoveFileParam moveFileParam)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<MoveFileResultDto> MoveRange(MoveFileRangeParam request)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region 得到访问地址

        /// <summary>
        /// 得到访问地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetVisitUrlResultDto GetVisitUrl(GetVisitUrlParam request)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region 下载

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DownloadResultDto Download(FileDownloadParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 下载文件

        /// <summary>
        /// 下载文件流
        /// </summary>
        /// <param name="request"></param>
        public DownloadStreamResultDto DownloadStream(FileDownloadStreamParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 设置生存时间（超时会自动删除）

        /// <summary>
        /// 设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExpireResultDto SetExpire(SetExpireParam request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ExpireResultDto> SetExpireRange(SetExpireRangeParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 修改文件MimeType

        /// <summary>
        /// 修改文件MimeType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ChangeMimeResultDto ChangeMime(ChangeMimeParam request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量更改文件mime
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ChangeMimeResultDto> ChangeMimeRange(ChangeMimeRangeParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 修改文件存储类型

        /// <summary>
        /// 修改文件存储类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ChangeTypeResultDto ChangeType(ChangeTypeParam request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量更改文件类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ChangeTypeResultDto> ChangeTypeRange(ChangeTypeRangeParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 抓取资源到空间

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFileParam">资源信息</param>
        /// <returns></returns>
        public bool FetchFile(FetchFileParam fetchFileParam)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 文件权限

        #region 设置文件权限

        /// <summary>
        /// 设置文件权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OperateResultDto SetPermiss(SetPermissParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取文件权限

        /// <summary>
        /// 获取文件权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FilePermissResultInfo GetPermiss(GetFilePermissParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}
