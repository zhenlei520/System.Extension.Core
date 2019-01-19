using System.Collections.Generic;
using System.IO;
using System.Text;
using EInfrastructure.Core.AutoConfig;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Words.Config;
using EInfrastructure.Core.Words.Config.PinYin;
using EInfrastructure.Core.Words.Config.Text;

namespace EInfrastructure.Core.Words
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseWordService
    {
        /// <summary>
        /// 环境信息
        /// </summary>
        internal static HostingEnvironmentConfigs HostingEnvironmentConfig;

        /// <summary>
        /// 内容词库
        /// </summary>
        internal static DictTextConfig DictConfig;

        /// <summary>
        /// 拼音词库
        /// </summary>
        internal static DictPinYinConfig DictPinYinConfig;

        public BaseWordService(
            HostingEnvironmentConfigs hostingEnvironmentConfig,
            DictTextPathConfig textPathConfig,
            DictPinYinPathConfig dictPinYinPathConfig)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            HostingEnvironmentConfig = hostingEnvironmentConfig;
            if (DictConfig == null)
            {
                DictConfig = new DictTextConfig()
                {
                    Simplified = GetContent(textPathConfig.SimplifiedPath.ConvertStrToList<string>('/')),
                    Traditional = GetContent(textPathConfig.TraditionalPath.ConvertStrToList<string>('/')),
                    Initial = GetContent(textPathConfig.InitialPath.ConvertStrToList<string>('/')),
                    SpecialNumber = GetContent(textPathConfig.SpecialNumberPath.ConvertStrToList<string>('/')),
                    TranscodingNumber = GetContent(textPathConfig.TranscodingNumberPath.ConvertStrToList<string>('/'))
                };
            }

            if (DictPinYinConfig == null)
            {
                DictPinYinConfig = new DictPinYinConfig()
                {
                    PinYinIndex = GetContent(dictPinYinPathConfig.PinYinIndexPath.ConvertStrToList<string>('/'))
                        .ConvertStrToList<short>('/',false).ToArray(),
                    PinYinData = GetContent(dictPinYinPathConfig.PinYinDataPath.ConvertStrToList<string>('/'))
                        .ConvertStrToList<short>('/',false).ToArray(),
                    PinYinName = GetContent(dictPinYinPathConfig.PinYinNamePath.ConvertStrToList<string>('/'))
                        .ConvertStrToList<string>('/',false),
                    Word = GetContent(dictPinYinPathConfig.WordPath.ConvertStrToList<string>('/',false)),
                    WordPinYin = GetContent(dictPinYinPathConfig.WordPinYinPath.ConvertStrToList<string>('/'))
                        .ConvertStrToList<short>('/',false).ToArray()
                };
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
            filePath = Path.Combine(HostingEnvironmentConfig.ContentRootPath, filePath);
            return GetContent(filePath);
        }

        #endregion

        #endregion
    }
}