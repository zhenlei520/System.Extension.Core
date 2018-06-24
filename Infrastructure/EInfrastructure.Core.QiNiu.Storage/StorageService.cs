using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.Interface.IOC;
using EInfrastructure.Core.Interface.Storage;
using EInfrastructure.Core.Interface.Storage.Dto;
using EInfrastructure.Core.Interface.Storage.FormModel;
using EInfrastructure.Core.QiNiu.Storage.Config;
using Microsoft.Extensions.Options;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 存储
    /// </summary>
    public partial class StorageService : IStorageService, ISingleInstance
    {
        public StorageService(IOptionsSnapshot<QiNiuConfig> qiNiuSnapshot)
        {
            QiNiuConfig = qiNiuSnapshot.Value;
            Config = new Qiniu.Storage.Config()
            {
                Zone = GetZone(QiNiuConfig.Zones),
                MaxRetryTimes = 3,
                UseHttps = true,
                UseCdnDomains = true,
                ChunkSize = ChunkUnit.U512K,
            };
            Mac = new Mac(QiNiuConfig.AccessKey, QiNiuConfig.SecretKey);
            BucketManager = new BucketManager(Mac, Config);
            OperationManager = new OperationManager(Mac, Config);
        }

        #region 文件信息

        #region 获取文件信息

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="fileFormModel">文件信息</param>
        /// <returns></returns>
        public FileInfoDto GetFileInfo(FileFormModel fileFormModel)
        {
            StatResult statRet = BucketManager.Stat(QiNiuConfig.Bucket, fileFormModel.Key);
            return new FileInfoDto()
            {
                Success = true,
                FileNo = fileFormModel.FileNo,
                Host = QiNiuConfig.Host,
                Path = fileFormModel.Key,
                Hash = statRet.Result.Hash,
                Size = statRet.Result.Fsize,
                PutTime = statRet.Result.PutTime,
                MimeType = statRet.Result.MimeType,
                Msg = statRet.Result.FileType == 1 ? "低频存储" : "普通存储"
            };
        }
        #endregion

        #region 删除文件信息

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool DeleteFile(string key, Action<string> action = null)
        {
            HttpResult deleteRet = BucketManager.Delete(QiNiuConfig.Bucket, key);
            action?.Invoke(deleteRet.Text.ToString());
            if (deleteRet.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 复制文件
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="key">源文件</param>
        /// <param name="copyKey">复制后的文件</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool CopyFile(string key, string copyKey, Action<string> action = null)
        {
            HttpResult copyRet = BucketManager.Copy(QiNiuConfig.Bucket, key, QiNiuConfig.Bucket, copyKey, true);
            action?.Invoke(copyRet.Text.ToString());
            if (copyRet.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 移动文件
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="key">源文件</param>
        /// <param name="moveKey">移动后的文件</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool MoveFile(string key, string moveKey, Action<string> action = null)
        {
            HttpResult moveRet = BucketManager.Move(QiNiuConfig.Bucket, key, QiNiuConfig.Bucket, moveKey, true);
            action?.Invoke(moveRet.Text.ToString());
            if (moveRet.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 更改文件Mime
        /// <summary>
        /// 更改文件Mime
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mime">例如：image/x-png</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ChangeMime(string key, string mime, Action<string> action = null)
        {
            HttpResult ret = BucketManager.ChangeMime(QiNiuConfig.Bucket, key, mime);
            action?.Invoke(ret.Text.ToString());
            if (ret.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 更改文件存储类型
        /// <summary>
        /// 更改文件存储类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type">存储类型</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ChangeType(string key, int type, Action<string> action = null)
        {
            HttpResult ret = BucketManager.ChangeType(QiNiuConfig.Bucket, key, type);
            action?.Invoke(ret.Text.ToString());
            if (ret.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 更改文件生命周期
        /// <summary>
        /// 更改文件生命周期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="day"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool DeleteAfterDays(string key, int day, Action<string> action = null)
        {
            HttpResult ret = BucketManager.DeleteAfterDays(QiNiuConfig.Bucket, key, day);
            action?.Invoke(ret.Text.ToString());
            if (ret.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 预创建文件
        /// <summary>
        /// 预创建文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Prefetch(string key, Action<string> action = null)
        {
            HttpResult ret = BucketManager.Prefetch(QiNiuConfig.Bucket, key);
            action?.Invoke(ret.Text.ToString());
            if (ret.Code != (int)HttpCode.OK && !ret.Text.Contains("bucket source not set"))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 获取空间的域名
        /// <summary>
        /// 获取空间的域名
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool GetDomains(string key, Action<string> action = null)
        {
            DomainsResult ret = BucketManager.Domains(QiNiuConfig.Bucket);
            action?.Invoke(ret.Text.ToString());
            if (ret.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 获取空间集合
        /// <summary>
        /// 获取空间集合
        /// </summary>
        /// <returns></returns>
        public bool GetBuckets(Action<List<string>> action = null)
        {
            BucketsResult ret = BucketManager.Buckets(true);
            if (ret.Code != (int)HttpCode.OK)
            {
                return false;
            }
            action?.Invoke(ret.Result.ToList());
            return true;
        }

        #endregion

        #region 获取文件列表
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="marker">标记</param>
        /// <param name="limit">数量限制</param>
        /// <param name="delimiter">分隔符</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool GetListFiles(string prefix, string marker, int limit, string delimiter, Action<List<FileInfoDto>, string> action = null)
        {
            ListResult listRet = BucketManager.ListFiles(QiNiuConfig.Bucket, prefix, marker, limit, delimiter);
            if (listRet.Code != (int)HttpCode.OK)
            {
                return false;
            }
            List<FileInfoDto> fileInfos = new List<FileInfoDto>();
            foreach (var fileInfo in listRet.Result.Items)
            {
                fileInfos.Add(new FileInfoDto()
                {
                    Host = QiNiuConfig.Host,
                    Path = fileInfo.Key,
                    Success = true,
                    Msg = fileInfo.FileType == 1 ? "低质量" : "普通",
                    Size = fileInfo.Fsize,
                    Hash = fileInfo.Hash,
                    MimeType = fileInfo.MimeType,
                    PutTime = fileInfo.PutTime
                });
            }
            action?.Invoke(fileInfos, listRet.Result.Marker);
            return true;
        }

        #endregion

        #region 批量删除文件

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="action"></param>
        public void BatchDelete(string[] keys, Action<bool, string> action = null)
        {
            List<string> ops = new List<string>();
            foreach (string key in keys)
            {
                string op = BucketManager.DeleteOp(QiNiuConfig.Bucket, key);
                ops.Add(op);
            }
            BatchResult ret = BucketManager.Batch(ops);
            if (ret.Code / 100 != 2)
            {
                action?.Invoke(false, ret.Text.ToString());
            }
            foreach (BatchInfo info in ret.Result)
            {
                if (info.Code == (int)HttpCode.OK)
                {
                    action?.Invoke(true, "delete success");
                }
                else
                {
                    action?.Invoke(false, info.Data.Error);
                }
            }
        }

        #endregion

        #region 批量复制文件
        /// <summary>
        /// 批量复制文件
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="action"></param>
        public void BatchCopy(Dictionary<string, string> keyDic, Action<bool, string> action = null)
        {
            List<string> ops = new List<string>();
            foreach (var key in keyDic)
            {
                string op = BucketManager.CopyOp(QiNiuConfig.Bucket, key.Key, QiNiuConfig.Bucket, key.Value, true);
                ops.Add(op);
            }
            BatchResult ret = BucketManager.Batch(ops);
            if (ret.Code / 100 != 2)
            {
                action?.Invoke(false, ret.ToString());
            }
            foreach (BatchInfo info in ret.Result)
            {
                if (info.Code == (int)HttpCode.OK)
                {
                    action?.Invoke(true, "copy success");
                }
                else
                {
                    action?.Invoke(false, info.Data.Error);
                }
            }
        }

        #endregion

        #region 批量移动文件
        /// <summary>
        /// 批量移动文件
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="action"></param>
        public void BatchMove(Dictionary<string, string> keyDic, Action<bool, string> action = null)
        {
            List<string> ops = new List<string>();
            foreach (var key in keyDic)
            {
                string op = BucketManager.MoveOp(QiNiuConfig.Bucket, key.Key, QiNiuConfig.Bucket, key.Value, true);
                ops.Add(op);
            }
            BatchResult ret = BucketManager.Batch(ops);
            if (ret.Code / 100 != 2)
            {
                action?.Invoke(false, ret.ToString());
            }
            foreach (BatchInfo info in ret.Result)
            {
                if (info.Code == (int)HttpCode.OK)
                {
                    action?.Invoke(true, "move success");
                }
                else
                {
                    action?.Invoke(false, info.Data.Error);
                }
            }
        }

        #endregion

        #region 批量更改文件的Mime

        /// <summary>
        /// 批量更改文件的Mime
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        public void BatchChangeMime(Dictionary<string, string> keyDic, string type, Action<bool, string> action = null)
        {
            List<string> ops = new List<string>();
            foreach (var key in keyDic)
            {
                string op = BucketManager.ChangeMimeOp(QiNiuConfig.Bucket, key.Key, type);
                ops.Add(op);
            }
            BatchResult ret = BucketManager.Batch(ops);
            if (ret.Code / 100 != 2)
            {
                action?.Invoke(false, ret.Text.ToString());
            }
            foreach (BatchInfo info in ret.Result)
            {
                if (info.Code == (int)HttpCode.OK)
                {
                    action?.Invoke(true, "chgm success");
                }
                else
                {
                    action?.Invoke(false, info.Data.Error);
                }
            }
        }

        #endregion

        #region 批量更改文件的类型

        /// <summary>
        /// 批量更改文件的类型
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        public void BatchChangeType(Dictionary<string, string> keyDic, int type, Action<bool, string> action = null)
        {
            List<string> ops = new List<string>();
            foreach (var key in keyDic)
            {
                string op = BucketManager.ChangeTypeOp(QiNiuConfig.Bucket, key.Key, type);
                ops.Add(op);
            }
            BatchResult ret = BucketManager.Batch(ops);
            if (ret.Code / 100 != 2)
            {
                action?.Invoke(false, ret.Text.ToString());
            }
            foreach (BatchInfo info in ret.Result)
            {
                if (info.Code == (int)HttpCode.OK)
                {
                    action?.Invoke(true, "chtype success");
                }
                else
                {
                    action?.Invoke(false, info.Data.Error);
                }
            }
        }

        #endregion

        #region 批量更改文件的生命周期
        /// <summary>
        /// 批量更改文件的生命周期
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="day"></param>
        /// <param name="action"></param>
        public void BatchDeleteAfterDays(Dictionary<string, string> keyDic, int day, Action<bool, string> action = null)
        {
            List<string> ops = new List<string>();
            foreach (var key in keyDic)
            {
                string op = BucketManager.DeleteAfterDaysOp(QiNiuConfig.Bucket, key.Key, day);
                ops.Add(op);
            }
            BatchResult ret = BucketManager.Batch(ops);
            if (ret.Code / 100 != 2)
            {
                action?.Invoke(false, ret.Text.ToString());
            }
            foreach (BatchInfo info in ret.Result)
            {
                if (info.Code == (int)HttpCode.OK)
                {
                    action?.Invoke(true, "chtype success");
                }
                else
                {
                    action?.Invoke(false, info.Data.Error);
                }
            }
        }
        #endregion

        #endregion

        #region 下载管理

        #region 得到文件的访问链接
        /// <summary>
        /// 得到文件的访问链接
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isPublic">是否生成共有的链接</param>
        /// <param name="expireInSeconds">过期时长</param>
        /// <returns></returns>
        public string GetFileUrl(string key, bool isPublic = true, int expireInSeconds = 3600)
        {
            if (isPublic)
                return DownloadManager.CreatePublishUrl(QiNiuConfig.Host, key);
            return DownloadManager.CreatePrivateUrl(Mac, QiNiuConfig.Host, key, expireInSeconds);
        }
        #endregion

        #endregion

        #region 操作管理

        #region 持久化到空间
        /// <summary>
        /// 持久化到空间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="saveEntry"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="force"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Pfop(string key, List<string> saveEntry, string notifyUrl = "", bool force = true, Action<string> action = null)
        {
            string[] saveEntrys = new string[saveEntry.Count];
            foreach (var item in saveEntry)
            {
                saveEntrys[saveEntrys.Length] = Base64.UrlSafeBase64Encode(QiNiuConfig.Bucket + ":" + item);
            }
            string fops = string.Join(";", saveEntrys);
            PfopResult pfopRet = OperationManager.Pfop(QiNiuConfig.Bucket, key, fops, QiNiuConfig.PersistentPipeline, notifyUrl, force);
            if (pfopRet.Code != (int)HttpCode.OK)
            {
                action?.Invoke(pfopRet.PersistentId);
                return false;
            }
            action?.Invoke(pfopRet.Text.ToString());
            return true;
        }
        #endregion

        #region 持久化文件
        /// <summary>
        /// 持久化文件
        /// </summary>
        /// <param name="persistentId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Prefop(string persistentId, Action<string> action = null)
        {
            PrefopResult ret = OperationManager.Prefop(persistentId);
            action?.Invoke(ret.Text.ToString());
            if (ret.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region 上传文件

        #region 上传本地文件
        /// <summary>
        /// 上传本地文件
        /// </summary>
        /// <param name="localPath">本地地址</param>
        /// <param name="key"></param>
        /// <param name="expireInSeconds">最长上传时间</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool UploadLocalFile(string localPath, string key, int expireInSeconds = 3600, Action<string> action = null)
        {
            SetPutPolicy(key, true);
            string token = Auth.CreateUploadToken(Mac, PutPolicy.ToJsonString());
            FormUploader target = new FormUploader(Config);
            HttpResult result = target.UploadFile(localPath, key, token, null);
            if (result.Code == (int)HttpCode.OK)
            {
                action?.Invoke(result.Text.ToString());
                return true;
            }
            return false;
        }
        #endregion

        #region 断点续传文件
        /// <summary>
        /// 断点续传文件
        /// </summary>
        /// <param name="localPath">本地地址</param>
        /// <param name="key"></param>
        /// <param name="expireInSeconds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ResumeUploadFile(string localPath, string key, int expireInSeconds = 3600, Action<string> action = null)
        {
            System.IO.Stream fs = System.IO.File.OpenRead(localPath);
            SetPutPolicy(key, true);
            string token = Auth.CreateUploadToken(Mac, PutPolicy.ToJsonString());
            ResumableUploader target = new ResumableUploader(Config);
            PutExtra extra = new PutExtra { ResumeRecordFile = ResumeHelper.GetDefaultRecordKey(localPath, key) };
            //设置断点续传进度记录文件
            HttpResult result = target.UploadStream(fs, key, token, extra);
            if (result.Code == (int)HttpCode.OK)
            {
                action?.Invoke(result.Text.ToString());
                return true;
            }
            return false;
        }

        #endregion

        #region 根据文件流上传文件
        /// <summary>
        /// 根据文件流上传文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="key"></param>
        /// <param name="expireInSeconds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool UploadFile(Stream stream, string key, int expireInSeconds = 3600, Action<string> action = null)
        {
            SetPutPolicy(key, true);
            string token = Auth.CreateUploadToken(Mac, PutPolicy.ToJsonString());
            ResumableUploader target = new ResumableUploader(Config);
            PutExtra extra = new PutExtra()
            {
                MaxRetryTimes = 3,
            };
            HttpResult result = target.UploadStream(stream, key, token, extra);
            if (result.Code == (int)HttpCode.OK)
            {
                action?.Invoke(result.Text.ToString());
                return true;
            }
            return false;
        }
        #endregion

        #region 根据字节数组上传文件
        /// <summary>
        /// 根据字节数组上传文件
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="key"></param>
        /// <param name="expireInSeconds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool UploadFile(byte[] buffer, string key, int expireInSeconds = 3600, Action<string> action = null)
        {
            return UploadFile(new MemoryStream(buffer), key, expireInSeconds, action);
        }
        #endregion

        #region 抓取资源到空间
        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFormModel">资源信息</param>
        /// <param name="action">上传成功回调新</param>
        /// <returns></returns>
        public bool FetchFile(FetchFileFormModel fetchFormModel, Action<FileBaseInfoDto> action = null)
        {
            FetchResult ret = BucketManager.Fetch(fetchFormModel.SourceFileKey, QiNiuConfig.Bucket, fetchFormModel.Key);
            action?.Invoke(new FileBaseInfoDto()
            {
                Success = true,
                FileNo = fetchFormModel.FileNo,
                Host = QiNiuConfig.Host,
                Path = fetchFormModel.Key,
                Msg = ret.Text
            });
            if (ret.Code != (int)HttpCode.OK)
            {
                return false;
            }
            return true;
        }

        #endregion

        #endregion

    }
}
