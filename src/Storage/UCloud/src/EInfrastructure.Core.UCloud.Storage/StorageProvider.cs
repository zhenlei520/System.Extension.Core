// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Param;
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
        public StorageProvider(ICollection<ILogProvider> logService, UCloudStorageConfig uCloudStorageConfig) : base(logService,
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
        /// <returns></returns>
        public UploadResultDto UploadStream(UploadByStreamParam param)
        {
            var res = base.UploadFile(param.Stream, param.Key, Path.GetExtension(param.Key));
            return new UploadResultDto(res, res ? "上传成功" : "上传失败");
        }

        #endregion

        #region 根据文件字节数组上传

        /// <summary>
        /// 根据文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <returns></returns>
        public UploadResultDto UploadByteArray(UploadByByteArrayParam param)
        {
            var res = base.UploadFile(param.ByteArray.ConvertToStream(), param.Key, Path.GetExtension(param.Key));
            return new UploadResultDto(res, res ? "上传成功" : "上传失败");
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

        #region 得到上传文件策略信息

        /// <summary>
        /// 得到上传文件策略信息
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        public string GetUploadCredentials(UploadPersistentOpsParam opsParam)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 得到上传文件策略信息
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        /// <param name="func"></param>
        public string GetUploadCredentials(UploadPersistentOpsParam opsParam, Func<string> func)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 检查文件是否存在

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        public OperateResultDto Exist(string key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取文件信息

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        public FileInfoDto Get(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取文件信息集合
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        public IEnumerable<FileInfoDto> GetList(IEnumerable<string> keyList)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 删除文件

        /// <summary>
        /// 根据文件key删除
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        public DeleteResultDto Del(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据文件key集合删除
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        public IEnumerable<DeleteResultDto> DelList(IEnumerable<string> keyList)
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
        /// <param name="copyFileParam">复制到新空间的参数</param>
        /// <returns></returns>
        public IEnumerable<CopyFileResultDto> CopyToList(ICollection<CopyFileParam> copyFileParam)
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
        /// <param name="moveFileParamList"></param>
        /// <returns></returns>
        public IEnumerable<MoveFileResultDto> MoveList(List<MoveFileParam> moveFileParamList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
