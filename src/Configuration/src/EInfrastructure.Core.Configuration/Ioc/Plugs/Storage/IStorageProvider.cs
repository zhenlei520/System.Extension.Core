// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage
{
    /// <summary>
    /// 文件存储
    /// </summary>
    public interface IStorageProvider : ISingleInstance, IIdentify
    {
        #region 根据文件流上传

        /// <summary>
        /// 根据文件流上传
        /// 阿里云会覆盖上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <param name="isResume">是否允许续传</param>
        /// <returns></returns>
        UploadResultDto UploadStream(UploadByStreamParam param, bool isResume = false);

        /// <summary>
        /// 根据文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <param name="isResume">是否允许续传</param>
        /// <returns></returns>
        UploadResultDto UploadByteArray(UploadByByteArrayParam param, bool isResume = false);

        #endregion

        #region 根据文件流以及文件字节数组上传

        /// <summary>
        /// 根据文件流以及文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <returns></returns>
        UploadResultDto UploadByToken(UploadByTokenParam param);

        #endregion

        #region 得到凭证

        #region 得到上传文件凭证

        /// <summary>
        /// 得到上传文件凭证
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        string GetUploadCredentials(UploadPersistentOpsParam opsParam);

        #endregion

        #region 得到管理凭证

        /// <summary>
        /// 得到管理凭证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string GetManageToken(GetManageTokenParam request);

        #endregion

        #endregion

        #region 检查文件是否存在

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto Exist(ExistParam request);

        #endregion

        #region 获取指定前缀的文件列表

        /// <summary>
        /// 获取指定前缀的文件列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <returns></returns>
        ListFileItemResultDto ListFiles(ListFileFilter filter);

        #endregion

        #region 获取文件信息

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        FileInfoDto Get(GetFileParam request);

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<FileInfoDto> GetList(GetFileRangeParam request);

        #endregion

        #region 删除文件

        /// <summary>
        /// 根据文件key删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DeleteResultDto Remove(RemoveParam request);

        /// <summary>
        /// 根据文件key集合删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<DeleteResultDto> RemoveRange(RemoveRangeParam request);

        #endregion

        #region 复制文件

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request">复制到新空间的参数</param>
        /// <returns></returns>
        CopyFileResultDto CopyTo(CopyFileParam request);

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<CopyFileResultDto> CopyRangeTo(CopyFileRangeParam request);

        #endregion

        #region 移动文件

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="moveFileParam"></param>
        /// <returns></returns>
        MoveFileResultDto Move(MoveFileParam moveFileParam);

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<MoveFileResultDto> MoveRange(MoveFileRangeParam request);

        #endregion

        #region 得到访问地址

        /// <summary>
        /// 得到访问地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetVisitUrlResultDto GetVisitUrl(GetVisitUrlParam request);

        #endregion

        /// <summary>
        /// 下载文件（根据已授权的地址）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DownloadResultDto Download(FileDownloadParam request);

        /// <summary>
        /// 下载文件流（根据已授权的地址）
        /// </summary>
        /// <param name="request"></param>
        DownloadStreamResultDto DownloadStream(FileDownloadStreamParam request);

        #region 设置生存时间

        /// <summary>
        /// 设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ExpireResultDto SetExpire(SetExpireParam request);

        /// <summary>
        /// 批量设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<ExpireResultDto> SetExpireRange(SetExpireRangeParam request);

        #endregion

        #region 更改文件mime

        /// <summary>
        /// 修改文件MimeType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ChangeMimeResultDto ChangeMime(ChangeMimeParam request);

        /// <summary>
        /// 批量更改文件mime
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<ChangeMimeResultDto> ChangeMimeRange(ChangeMimeRangeParam request);

        #endregion

        #region 更改文件存储类型

        /// <summary>
        /// 修改文件存储类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ChangeTypeResultDto ChangeType(ChangeTypeParam request);

        /// <summary>
        /// 批量更改文件类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<ChangeTypeResultDto> ChangeTypeRange(ChangeTypeRangeParam request);

        #endregion

        #region 文件访问权限

        /// <summary>
        /// 设置文件访问权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        OperateResultDto SetPermiss(SetPermissParam request);

        /// <summary>
        /// 获取文件的访问权限
        /// 七牛云存储不支持获取文件权限
        /// 阿里云Oss支持
        /// Uc云不支持
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        FilePermissResultInfo GetPermiss(GetFilePermissParam request);

        #endregion

        #region 抓取资源到空间

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFileParam">资源信息</param>
        /// <returns></returns>
        FetchFileResultDto FetchFile(FetchFileParam fetchFileParam);

        #endregion
    }
}
