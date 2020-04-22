// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
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

        #region 得到上传文件凭证

        /// <summary>
        /// 得到上传文件凭证
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        string GetUploadCredentials(UploadPersistentOpsParam opsParam);

        #endregion

        #region 检查文件是否存在

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        OperateResultDto Exist(string key);

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
        IEnumerable<FileInfoDto> GetList(IEnumerable<string> keyList);

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
        IEnumerable<DeleteResultDto> RemoveRange(IEnumerable<string> keyList);

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
        IEnumerable<CopyFileResultDto> CopyRangeTo(ICollection<CopyFileParam> copyFileParam);

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
        IEnumerable<MoveFileResultDto> MoveRange(List<MoveFileParam> moveFileParamList);

        #endregion
    }
}
