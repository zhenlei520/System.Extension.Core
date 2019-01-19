namespace EInfrastructure.Core.Interface.Words
{
    /// <summary>
    /// 导入词库
    /// </summary>
    public interface IImportWordService
    {
        /// <summary>
        /// 导入搜狗词库（导入path下的词库文件）
        /// </summary>
        bool ImportBySouGou(string path);
    }
}