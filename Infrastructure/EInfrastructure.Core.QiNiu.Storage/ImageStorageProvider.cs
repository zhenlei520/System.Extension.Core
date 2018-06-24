using System;
using System.IO;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.Interface.Storage.Config;
using EInfrastructure.Core.Interface.Storage.Dto;
using EInfrastructure.Core.Interface.Storage.Enum;
using EInfrastructure.Core.Interface.Storage.FormModel;
using EInfrastructure.Core.Interface.Storage.Model;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public partial class StorageService
    {

        #region 通过源图片地址上传
        /// <summary>
        /// 通过源图片地址上传
        /// </summary>
        /// <param name="uploadImgByOriginFormModel">图片上传策略信息</param>
        /// <param name="action"></param>
        public bool UpImageByPath(UploadImgByOriginFormModel uploadImgByOriginFormModel, Action<FileBaseInfoDto> action = null)
        {
            string persistentOpsStr = GetPersistentOps(uploadImgByOriginFormModel.PersistentOps);
            SetPutPolicy(uploadImgByOriginFormModel.Key, true, persistentOpsStr);
            return FetchFile(uploadImgByOriginFormModel, action);
        }
        #endregion

        #region 通过base64编码上传图片用
        /// <summary>
        /// 通过base64编码上传图片用
        /// </summary>
        /// <param name="base64FormModel">文件base64上传策略</param>
        /// <param name="action">委托</param>
        /// <returns></returns>
        public bool UpImage(UploadImgByBase64FormModel base64FormModel, Action<PlugResponseInfo> action = null)
        {
            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(base64FormModel.Base64);
            }
            catch (System.Exception ex)
            {
                action?.Invoke(PlugResponseInfo.Err(ex.Message));
                return false;
            }
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                return UpImage(new UploadImgByStreamFormModel()
                {
                    FileNo = base64FormModel.FileNo,
                    PersistentOps = base64FormModel.PersistentOps,
                    Stream = stream
                }, action);
            }
        }
        #endregion

        #region 通过字节数组上传图片
        /// <summary>
        /// 通过字节数组上传图片
        /// </summary>
        /// <param name="byteArrayFormModel">字节数组</param>
        /// <param name="action"></param>
        public bool UpImage(UploadImgByByteArrayFormModel byteArrayFormModel, Action<PlugResponseInfo> action = null)
        {
            return UpImage(new UploadImgByStreamFormModel()
            {
                FileNo = byteArrayFormModel.FileNo,
                PersistentOps = byteArrayFormModel.PersistentOps,
                Stream = new MemoryStream(byteArrayFormModel.Buffer)
            }, action);
        }
        #endregion

        #region 根据数组流上传图片
        /// <summary>
        /// 根据数组流上传图片
        /// </summary>
        /// <param name="streamFormModel">字符流</param>
        /// <param name="action"></param>
        public bool UpImage(UploadImgByStreamFormModel streamFormModel, Action<PlugResponseInfo> action = null)
        {
            string persistentOpsStr = GetPersistentOps(streamFormModel.PersistentOps);
            SetPutPolicy(streamFormModel.PersistentOps.Key, true, persistentOpsStr);
            return UploadFile(streamFormModel.Stream, streamFormModel.PersistentOps.Key, 3600, (string response) =>
            {
                dynamic str = new JsonCommon().Deserialize<dynamic>(response);
                if (str != null && str.key != null)
                {
                    action(PlugResponseInfo.Success(new FileInfoDto()
                    {
                        Success = true,
                        FileNo = streamFormModel.FileNo,
                        Hash = str.hash.ToString(),
                        Path = str.key.ToString(),
                        Host = QiNiuConfig.Host,
                    }));
                }
                else
                {
                    action(PlugResponseInfo.Err("上传失败"));
                }
            });
        }

        #endregion

        #region private methods

        #region 得到上传策略
        /// <summary>
        /// 得到上传策略
        /// </summary>
        /// <param name="persistentOps">图片上传策略</param>
        /// <returns></returns>
        private string GetPersistentOps(ImgPersistentOps persistentOps)
        {
            int length = persistentOps.ThumOpsList.Count;
            var index = 0;
            if (persistentOps.Mode != ImageModeEnum.Nothing)
            {
                length++;
            }
            string[] imageOpts = new string[length];
            if (persistentOps.Mode != ImageModeEnum.Nothing)
            {
                imageOpts[index] = GetPersistentOps(persistentOps.Mode, persistentOps.Width,
                    persistentOps.Height);
                index++;
            }
            foreach (var thumOps in persistentOps.ThumOpsList)
            {
                string savekey = "saveas/" + Base64.UrlSafeBase64Encode(thumOps.Key);
                imageOpts[index] = savekey + "|" + GetPersistentOps(thumOps.Mode, thumOps.Width,
                                                  thumOps.Height) + "/";
                index++;
            }
            return string.Join(";", imageOpts);
        }
        #endregion

        #region 得到上传策略
        /// <summary>
        /// 得到上传策略 
        /// </summary>
        /// <param name="imageModel">图片缩放信息</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        private string GetPersistentOps(ImageModeEnum imageModel, int width, int height)
        {
            if (imageModel == ImageModeEnum.Hw)
                return "imageMogr2/thumbnail/" + $"{width}x{height}!";
            if (imageModel == ImageModeEnum.W)
                return "imageMogr2/thumbnail/" + $"{width}x";
            if (imageModel == ImageModeEnum.H)
                return "imageMogr2/thumbnail/" + $"x{height}";
            return "";
        }
        #endregion

        #endregion

    }
}
