using EInfrastructure.Core.AutoConfig.Extension;
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

        /// <summary>
        /// 导入词库
        /// </summary>
        /// <param name="wordService"></param>
        /// <param name="textPathConfig">字典配置地址</param>
        /// <param name="dictPinYinPathConfig">文字拼音地址</param>
        public ImportWordService(
            IWordService wordService,
            IWritableOptions<DictTextPathConfig> textPathConfig,
            IWritableOptions<DictPinYinPathConfig> dictPinYinPathConfig) : base(
            textPathConfig.Get<DictTextPathConfig>(),
            dictPinYinPathConfig.Get<DictPinYinPathConfig>())
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