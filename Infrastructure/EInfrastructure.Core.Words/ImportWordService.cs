using EInfrastructure.Core.AutoConfig;
using EInfrastructure.Core.HelpCommon.Files;
using EInfrastructure.Core.Interface.Words;
using EInfrastructure.Core.Words.Config.PinYin;
using EInfrastructure.Core.Words.Config.Text;
using EInfrastructure.Core.Words.Enum;
using EInfrastructure.Core.Words.Extension;
using EInfrastructure.Core.Words.Extension.ImportDict;

namespace EInfrastructure.Core.Words
{
    /// <summary>
    /// 导入词库
    /// </summary>
    public class ImportWordService : BaseWordService, IImportWordService
    {
        private readonly IWordService _wordService;

        public ImportWordService(HostingEnvironmentConfigs hostingEnvironmentConfig,
            IWordService wordService,
            DictTextPathConfig textPathConfig,
            DictPinYinPathConfig dictPinYinPathConfig) : base(hostingEnvironmentConfig, textPathConfig,
            dictPinYinPathConfig)
        {
            _wordService = wordService;
        }

        #region 导入搜狗词库（导入path下的词库文件）

        /// <summary>
        /// 导入搜狗词库（导入path下的词库文件）
        /// </summary>
        public bool ImportBySouGou(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            ImportSouGouCommon.Initialize(_wordService, path);
            Reload(DictTypeEnum.PinYin);
            return true;
        }

        #endregion
    }
}