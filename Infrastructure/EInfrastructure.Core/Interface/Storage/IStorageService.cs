using System;
using System.Collections.Generic;
using System.IO;
using EInfrastructure.Core.Interface.Storage.Dto;
using EInfrastructure.Core.Interface.Storage.FormModel;
using EInfrastructure.Core.Interface.Storage.Model;

namespace EInfrastructure.Core.Interface.Storage
{
    /// <summary>
    /// 文件存储
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="fileFormModel">文件信息</param>
        /// <returns></returns>
        FileInfoDto GetFileInfo(FileFormModel fileFormModel);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool DeleteFile(string key, Action<string> action = null);

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="key">源文件</param>
        /// <param name="copyKey">复制后的文件</param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool CopyFile(string key, string copyKey, Action<string> action = null);

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="key">源文件</param>
        /// <param name="moveKey">移动后的文件</param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool MoveFile(string key, string moveKey, Action<string> action = null);

        /// <summary>
        /// 更改文件Mime
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mime">例如：image/x-png</param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ChangeMime(string key, string mime, Action<string> action = null);

        /// <summary>
        /// 更改文件存储类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type">存储类型</param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ChangeType(string key, int type, Action<string> action = null);

        /// <summary>
        /// 更改文件生命周期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="day"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool DeleteAfterDays(string key, int day, Action<string> action = null);

        /// <summary>
        /// 预创建文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool Prefetch(string key, Action<string> action = null);

        /// <summary>
        /// 获取空间的域名
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool GetDomains(string key, Action<string> action = null);

        /// <summary>
        /// 获取空间集合
        /// </summary>
        /// <returns></returns>
        bool GetBuckets(Action<List<string>> action = null);

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="marker">标记</param>
        /// <param name="limit">数量限制</param>
        /// <param name="delimiter">分隔符</param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool GetListFiles(string prefix, string marker, int limit, string delimiter,
           Action<List<FileInfoDto>, string> action = null);

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="action"></param>
        void BatchDelete(string[] keys, Action<bool, string> action);

        /// <summary>
        /// 批量复制文件
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="action"></param>
        void BatchCopy(Dictionary<string, string> keyDic, Action<bool, string> action = null);

        /// <summary>
        /// 批量移动文件
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="action"></param>
        void BatchMove(Dictionary<string, string> keyDic, Action<bool, string> action = null);

        /// <summary>
        /// 批量更改文件的Mime
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        void BatchChangeMime(Dictionary<string, string> keyDic, string type, Action<bool, string> action = null);

        /// <summary>
        /// 批量更改文件的类型
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        void BatchChangeType(Dictionary<string, string> keyDic, int type, Action<bool, string> action = null);

        /// <summary>
        /// 批量更改文件的生命周期
        /// </summary>
        /// <param name="keyDic"></param>
        /// <param name="day"></param>
        /// <param name="action"></param>
        void BatchDeleteAfterDays(Dictionary<string, string> keyDic, int day, Action<bool, string> action = null);

        /// <summary>
        /// 得到文件的访问链接
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isPublic">是否生成共有的链接</param>
        /// <param name="expireInSeconds">过期时长</param>
        /// <returns></returns>
        string GetFileUrl(string key, bool isPublic = true, int expireInSeconds = 3600);

        /// <summary>
        /// 持久化到空间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="saveEntry"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="force"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool Pfop(string key, List<string> saveEntry, string notifyUrl = "",
            bool force = true, Action<string> action = null);

        /// <summary>
        /// 持久化文件
        /// </summary>
        /// <param name="persistentId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool Prefop(string persistentId, Action<string> action = null);

        /// <summary>
        /// 上传本地文件
        /// </summary>
        /// <param name="localPath">本地地址</param>
        /// <param name="key"></param>
        /// <param name="expireInSeconds">最长上传时间</param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool UploadLocalFile(string localPath, string key, int expireInSeconds = 3600,
            Action<string> action = null);

        /// <summary>
        /// 断点续传文件
        /// </summary>
        /// <param name="localPath">本地地址</param>
        /// <param name="key"></param>
        /// <param name="expireInSeconds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ResumeUploadFile(string localPath, string key, int expireInSeconds = 3600,
            Action<string> action = null);

        /// <summary>
        /// 根据文件流上传文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="key"></param>
        /// <param name="expireInSeconds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool UploadFile(Stream stream, string key, int expireInSeconds = 3600,
            Action<string> action = null);

        /// <summary>
        /// 根据字节数组上传文件
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="key"></param>
        /// <param name="expireInSeconds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool UploadFile(byte[] buffer, string key, int expireInSeconds = 3600,
            Action<string> action = null);

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFormModel">资源信息</param>
        /// <param name="action">上传成功回调新</param>
        /// <returns></returns>
        bool FetchFile(FetchFileFormModel fetchFormModel, Action<FileBaseInfoDto> action = null);

        /// <summary>
        /// 通过源图片地址上传
        /// </summary>
        /// <param name="uploadImgByOriginFormModel">图片上传策略信息</param>
        /// <param name="action"></param>
        bool UpImageByPath(UploadImgByOriginFormModel uploadImgByOriginFormModel, Action<FileBaseInfoDto> action = null);

        /// <summary>
        /// 通过base64编码上传图片用
        /// </summary>
        /// <param name="base64FormModel">文件base64上传策略</param>
        /// <param name="action">委托</param>
        /// <returns></returns>
        bool UpImage(UploadImgByBase64FormModel base64FormModel,
           Action<PlugResponseInfo> action = null);

        /// <summary>
        /// 通过字节数组上传图片
        /// </summary>
        /// <param name="byteArrayFormModel">字节数组</param>
        /// <param name="action"></param>
        bool UpImage(UploadImgByByteArrayFormModel byteArrayFormModel,
           Action<PlugResponseInfo> action = null);

        /// <summary>
        /// 根据数组流上传图片
        /// </summary>
        /// <param name="streamFormModel">字符流</param>
        /// <param name="action"></param>
        bool UpImage(UploadImgByStreamFormModel streamFormModel,
            Action<PlugResponseInfo> action = null);
    }
}
