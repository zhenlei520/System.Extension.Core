namespace EInfrastructure.Core.Interface.Storage.Param.Pictures
{
    /// <summary>
    /// 图片抓取
    /// </summary>
    public class FetchFileParam
    {
        /// <summary>
        /// 源图（必填）
        /// </summary>
        public string SourceFileKey { get; set; }

        /// <summary>
        /// 目标图（必填）
        /// </summary>
        public string Key { get; set; }
    }
}