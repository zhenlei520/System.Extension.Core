// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params;

namespace EInfrastructure.Core.Aliyun.Storage
{
    public class StorageProvider : IStorageProvider
    {
        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 98;
        }

        #endregion

        #region 得到唯一标识

        /// <summary>
        /// 得到唯一标识
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        public UploadResultDto UploadStream(UploadByStreamParam param, bool isResume = false)
        {
            throw new System.NotImplementedException();
        }

        public UploadResultDto UploadByteArray(UploadByByteArrayParam param, bool isResume = false)
        {
            throw new System.NotImplementedException();
        }

        public UploadResultDto UploadByToken(UploadByTokenParam param)
        {
            throw new System.NotImplementedException();
        }

        public string GetUploadCredentials(UploadPersistentOpsParam opsParam)
        {
            throw new System.NotImplementedException();
        }

        public string GetManageToken(string url)
        {
            throw new System.NotImplementedException();
        }

        public string GetManageToken(string url, byte[] body)
        {
            throw new System.NotImplementedException();
        }

        public string GetDownloadToken(string url)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto Exist(string key)
        {
            throw new System.NotImplementedException();
        }

        public ListFileItemResultDto ListFiles(ListFileFilter filter)
        {
            throw new System.NotImplementedException();
        }

        public FileInfoDto Get(string key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FileInfoDto> GetList(string[] keyList)
        {
            throw new System.NotImplementedException();
        }

        public DeleteResultDto Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DeleteResultDto> RemoveRange(string[] keyList)
        {
            throw new System.NotImplementedException();
        }

        public CopyFileResultDto CopyTo(CopyFileParam copyFileParam)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CopyFileResultDto> CopyRangeTo(CopyFileParam[] copyFileParam)
        {
            throw new System.NotImplementedException();
        }

        public MoveFileResultDto Move(MoveFileParam moveFileParam)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MoveFileResultDto> MoveRange(MoveFileParam[] moveFileParamList)
        {
            throw new System.NotImplementedException();
        }

        public string GetPublishUrl(string key)
        {
            throw new System.NotImplementedException();
        }

        public string GetPrivateUrl(string key, int expire = 3600)
        {
            throw new System.NotImplementedException();
        }

        public DownloadResultDto Download(string url, string savePath)
        {
            throw new System.NotImplementedException();
        }

        public ExpireResultDto SetExpire(string key, int expire)
        {
            throw new System.NotImplementedException();
        }

        public List<ExpireResultDto> SetExpireRange(string[] keys, int expire)
        {
            throw new System.NotImplementedException();
        }

        public ChangeMimeResultDto ChangeMime(string key, string mime)
        {
            throw new System.NotImplementedException();
        }

        public List<ChangeMimeResultDto> ChangeMimeRange(string[] keys, string mime)
        {
            throw new System.NotImplementedException();
        }

        public ChangeTypeResultDto ChangeType(string key, int type)
        {
            throw new System.NotImplementedException();
        }

        public List<ChangeTypeResultDto> ChangeTypeRange(string[] keys, int type)
        {
            throw new System.NotImplementedException();
        }
    }
}
