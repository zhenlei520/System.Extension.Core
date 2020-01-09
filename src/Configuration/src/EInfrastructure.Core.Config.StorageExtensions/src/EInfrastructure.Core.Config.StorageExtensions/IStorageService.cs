// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Config.StorageExtensions.Dto;
using EInfrastructure.Core.Config.StorageExtensions.Param;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Config.StorageExtensions
{
    /// <summary>
    /// 文件存储
    /// </summary>
    public interface IStorageService : IIdentify
    {
        #region 根据文件流上传

        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <returns></returns>
        UploadResultDto UploadStream(UploadByStreamParam param);

        #endregion

        #region 根据文件上传

        /// <summary>
        /// 根据文件上传
        /// </summary>
        /// <param name="param">文件上传配置</param>
        /// <returns></returns>
        UploadResultDto UploadFile(UploadByFormFileParam param);

        #endregion

        #region 得到上传文件策略信息

        /// <summary>
        /// 得到上传文件策略信息
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        string GetUploadCredentials(UploadPersistentOpsParam opsParam);

        /// <summary>
        /// 得到上传文件策略信息
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        /// <param name="func"></param>
        string GetUploadCredentials(UploadPersistentOpsParam opsParam, Func<string> func);

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
        DeleteResultDto Del(string key);

        /// <summary>
        /// 根据文件key集合删除
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        IEnumerable<DeleteResultDto> DelList(IEnumerable<string> keyList);

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
        IEnumerable<CopyFileResultDto> CopyToList(ICollection<CopyFileParam> copyFileParam);

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
        IEnumerable<MoveFileResultDto> MoveList(List<MoveFileParam> moveFileParamList);

        #endregion
    }
}
