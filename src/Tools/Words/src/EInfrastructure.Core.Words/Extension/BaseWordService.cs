// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Words.Config;
using EInfrastructure.Core.Words.Config.PinYin;
using EInfrastructure.Core.Words.Config.Text;
using EInfrastructure.Core.Words.Enum;

namespace EInfrastructure.Core.Words.Extension
{
    /// <summary>
    /// 基础类
    /// </summary>
    public abstract class BaseWordService
    {
        /// <summary>
        /// 内容词库
        /// </summary>
        internal static DictTextConfig DictConfig;

        /// <summary>
        /// 拼音词库
        /// </summary>
        internal static DictPinYinConfig DictPinYinConfig;

        /// <summary>
        /// 内容词库地址
        /// </summary>
        internal static DictTextPathConfig DictTextPathConfig;

        /// <summary>
        /// 拼音词库地址
        /// </summary>
        internal static DictPinYinPathConfig DictPinYinPathConfig;

        /// <summary>
        ///
        /// </summary>
        public BaseWordService(EWordConfig wordConfig)
        {
            if (wordConfig == null)
            {
                wordConfig = new EWordConfig();
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DictTextPathConfig = DictTextPathConfig.Get();
            DictPinYinPathConfig = DictPinYinPathConfig.Get();
            DictTextPathConfig.Set(wordConfig.DictTextPathConfig);
            DictPinYinPathConfig.Set(wordConfig.DictPinYinPathConfig);
            if (DictConfig == null)
            {
                Reload(DictTypeEnum.Text);
            }

            if (DictPinYinConfig == null)
            {
                Reload(DictTypeEnum.PinYin);
            }
        }

        #region protected methods

        #region 得到文本内容

        /// <summary>
        /// 得到文本内容
        /// </summary>
        /// <param name="path">绝对路径</param>
        /// <returns></returns>
        internal virtual string GetContent(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] byts = new byte[fileStream.Length];
                fileStream.Read(byts, 0, byts.Length);
                return Encoding.Default.GetString(byts);
            }
        }

        #endregion

        #region 得到文本内容

        /// <summary>
        /// 得到文本内容
        /// </summary>
        /// <param name="path">文件地址（相对路径）</param>
        /// <returns></returns>
        internal virtual string GetContent(List<string> path)
        {
            string filePath = "";
            path.ForEach(item => { filePath = Path.Combine(filePath, item); });
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            string result = GetContent(filePath);
            return result;
        }

        #endregion

        #region 重载词库信息

        /// <summary>
        /// 重载词库信息
        /// </summary>
        /// <param name="dictType">词库类型</param>
        internal virtual void Reload(DictTypeEnum dictType)
        {
            switch (dictType)
            {
                case DictTypeEnum.Text:
                    ReloadTextDict();
                    break;
                case DictTypeEnum.PinYin:
                    ReloadPinYinDict();
                    break;
            }
        }

        #region 重载内容词库

        /// <summary>
        /// 重载内容词库
        /// </summary>
        private void ReloadTextDict()
        {
            DictConfig = new DictTextConfig()
            {
                Simplified = GetContent(DictTextPathConfig.SimplifiedPath.ConvertStrToList<string>('/')),
                Traditional = GetContent(DictTextPathConfig.TraditionalPath.ConvertStrToList<string>('/')),
                Initial = GetContent(DictTextPathConfig.InitialPath.ConvertStrToList<string>('/')),
                SpecialNumber = GetContent(DictTextPathConfig.SpecialNumberPath.ConvertStrToList<string>('/')),
                TranscodingNumber =
                    GetContent(DictTextPathConfig.TranscodingNumberPath.ConvertStrToList<string>('/'))
            };
        }

        #endregion

        #region 重载拼音词库

        /// <summary>
        /// 重载拼音词库
        /// </summary>
        private void ReloadPinYinDict()
        {
            try
            {
                DictPinYinConfig = new DictPinYinConfig()
                {
                    PinYinIndex = GetContent(DictPinYinPathConfig.PinYinIndexPath.ConvertStrToList<string>('/'))
                        .ConvertStrToList<short>(',', false).ToArray(),
                    PinYinData = GetContent(DictPinYinPathConfig.PinYinDataPath.ConvertStrToList<string>('/'))
                        .ConvertStrToList<short>(',', false).ToArray(),
                    PinYinName = GetContent(DictPinYinPathConfig.PinYinNamePath.ConvertStrToList<string>('/'))
                        .ConvertStrToList<string>(',', false),
                    Word = GetContent(DictPinYinPathConfig.WordPath.ConvertStrToList<string>('/', false)),
                    WordPinYin = GetContent(DictPinYinPathConfig.WordPinYinPath.ConvertStrToList<string>('/'))
                        .ConvertStrToList<short>(',', false).ToArray()
                };
            }
            catch (Exception ex)
            {
                throw new BusinessException($"拼音词库异常：{ex}", HttpStatus.Err.Id);
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
