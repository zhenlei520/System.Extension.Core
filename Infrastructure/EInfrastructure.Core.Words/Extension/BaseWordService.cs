using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EInfrastructure.Core.AutoConfig;
using EInfrastructure.Core.AutoConfig.Config;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Words.Config.PinYin;
using EInfrastructure.Core.Words.Config.Text;
using EInfrastructure.Core.Words.Enum;

namespace EInfrastructure.Core.Words.Extension
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseWordService
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
        /// <param name="textPathConfig"></param>
        /// <param name="dictPinYinPathConfig"></param>
        public BaseWordService(
            DictTextPathConfig textPathConfig,
            DictPinYinPathConfig dictPinYinPathConfig)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DictTextPathConfig = textPathConfig;
            DictPinYinPathConfig = dictPinYinPathConfig;
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
        internal string GetContent(string path)
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
        internal string GetContent(List<string> path)
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
        internal void Reload(DictTypeEnum dictType)
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
            try
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
            catch (System.Exception ex)
            {
                throw new BusinessException("词语词库异常");
            }
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
            catch (System.Exception ex)
            {
                throw new BusinessException("拼音词库异常");
            }
        }

        #endregion

        #endregion

        #endregion
    }
}