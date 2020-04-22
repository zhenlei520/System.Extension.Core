// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params;

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
        /// <param name="url">地址</param>
        /// <returns></returns>
        string GetManageToken(string url);

        /// <summary>
        /// 得到管理凭证
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        string GetManageToken(string url, byte[] body);

        #endregion

        #region 得到下载凭证

        /// <summary>
        /// 得到下载凭证
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        string GetDownloadToken(string url);

        #endregion

        #endregion

        #region 检查文件是否存在

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        OperateResultDto Exist(string key);

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
        /// <param name="key">文件key</param>
        /// <returns></returns>
        FileInfoDto Get(string key);

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        IEnumerable<FileInfoDto> GetList(string[] keyList);

        #endregion

        #region 删除文件

        /// <summary>
        /// 根据文件key删除
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        DeleteResultDto Remove(string key);

        /// <summary>
        /// 根据文件key集合删除
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        IEnumerable<DeleteResultDto> RemoveRange(string[] keyList);

        #endregion

        #region 复制文件

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="copyFileParam">复制到新空间的参数</param>
        /// <returns></returns>
        CopyFileResultDto CopyTo(CopyFileParam copyFileParam);

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="copyFileParam">复制到新空间的参数</param>
        /// <returns></returns>
        IEnumerable<CopyFileResultDto> CopyRangeTo(CopyFileParam[] copyFileParam);

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
        /// <param name="moveFileParamList"></param>
        /// <returns></returns>
        IEnumerable<MoveFileResultDto> MoveRange(MoveFileParam[] moveFileParamList);

        #endregion

        #region 得到地址

        /// <summary>
        /// 得到公开空间的访问地址
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        string GetPublishUrl(string key);

        /// <summary>
        /// 得到私有空间的访问地址
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="expire">过期时间，单位：s</param>
        /// <returns></returns>
        string GetPrivateUrl(string key, int expire = 3600);

        #endregion

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">文件访问地址</param>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        DownloadResultDto Download(string url, string savePath);

        #region 设置生存时间

        /// <summary>
        /// 设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="expire">过期时间 单位：day</param>
        /// <returns></returns>
        ExpireResultDto SetExpire(string key, int expire);

        /// <summary>
        /// 批量设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="keys">文件key</param>
        /// <param name="expire">过期时间 单位：day</param>
        /// <returns></returns>
        List<ExpireResultDto> SetExpireRange(string[] keys, int expire);

        #endregion

        #region 更改文件mime

        /// <summary>
        /// 修改文件MimeType
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="mime">文件mimeType</param>
        /// <returns></returns>
        ChangeMimeResultDto ChangeMime(string key, string mime);

        /// <summary>
        /// 批量更改文件mime
        /// </summary>
        /// <param name="keys">文件key集合</param>
        /// <param name="mime">问价mime</param>
        /// <returns></returns>
        List<ChangeMimeResultDto> ChangeMimeRange(string[] keys, string mime);

        #endregion

        #region 更改文件存储类型

        /// <summary>
        /// 修改文件存储类型
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="type">0表示普通存储，1表示低频存储</param>
        /// <returns></returns>
        ChangeTypeResultDto ChangeType(string key, int type);

        /// <summary>
        /// 批量更改文件类型
        /// </summary>
        /// <param name="keys">文件key集合</param>
        /// <param name="type">0表示普通存储，1表示低频存储</param>
        /// <returns></returns>
        List<ChangeTypeResultDto> ChangeTypeRange(string[] keys, int type);

        #endregion
    }
}
