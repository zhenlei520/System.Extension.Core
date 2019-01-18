using System.Collections.Generic;
using System.IO;
using System.Text;
using EInfrastructure.Core.Configuration.Interface.Config;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Words.Config;
using EInfrastructure.Core.Words.Config.PinYin;
using EInfrastructure.Core.Words.Config.Text;
using Microsoft.AspNetCore.Hosting;

namespace EInfrastructure.Core.Words
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseWordService
    {
        /// <summary>
        /// 词库
        /// </summary>
        internal static DictTextConfig _dictConfig;

        internal static DictPinYinConfig _dictPinYinConfig;

        protected readonly IHostingEnvironment _hostingEnvironment;

        public BaseWordService(IHostingEnvironment hostingEnvironment, DictTextPathConfig textPathConfig,
            DictPinYinPathConfig dictPinYinPathConfig)
        {
            _hostingEnvironment = hostingEnvironment;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (_dictConfig == null)
            {
                _dictConfig = new DictTextConfig()
                {
                    Simplified = GetContent(new List<string>() {textPathConfig.SimplifiedPath}),
                    Traditional = GetContent(new List<string>() {textPathConfig.TraditionalPath}),
                    Initial = GetContent(new List<string>() {textPathConfig.InitialPath}),
                    SpecialNumber = GetContent(new List<string>() {textPathConfig.SpecialNumberPath}),
                    TranscodingNumber = GetContent(new List<string>() {textPathConfig.TranscodingNumberPath})
                };
            }

            if (_dictPinYinConfig == null)
            {
                _dictPinYinConfig = new DictPinYinConfig()
                {
                    PinYinIndex = GetContent(new List<string>() {dictPinYinPathConfig.PinYinIndexPath})
                        .ConvertStrToList<short>(',').ToArray(),
                    PinYinData = GetContent(new List<string>() {dictPinYinPathConfig.PinYinDataPath})
                        .ConvertStrToList<short>(',').ToArray(),
                    PinYinName = GetContent(new List<string>() {dictPinYinPathConfig.PinYinDataPath})
                        .ConvertStrToList<string>(','),
                    Word = GetContent(new List<string>() {dictPinYinPathConfig.WordPath}),
                    WordPinYin = GetContent(new List<string>() {dictPinYinPathConfig.WordPinYinPath})
                        .ConvertStrToList<short>(',').ToArray()
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
            string filePath = _hostingEnvironment.ContentRootPath;
            path.ForEach(item => { filePath = Path.Combine(filePath, item); });
            return GetContent(filePath);
        }

        #endregion

        #endregion
    }
}