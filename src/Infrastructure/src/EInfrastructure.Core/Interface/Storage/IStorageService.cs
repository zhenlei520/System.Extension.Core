// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Interface.Storage.Param;

namespace EInfrastructure.Core.Interface.Storage
{
    /// <summary>
    /// 文件存储
    /// </summary>
    public interface IStorageService
    {
        #region 根据文件流上传

        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <returns></returns>
        bool UploadStream(UploadByStreamParam param);

        #endregion

        #region 根据文件上传

        /// <summary>
        /// 根据文件上传
        /// </summary>
        /// <param name="param">文件上传配置</param>
        /// <returns></returns>
        bool UploadFile(UploadByFormFileParam param);

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
        bool Exist(string key);

        #endregion
    }
}