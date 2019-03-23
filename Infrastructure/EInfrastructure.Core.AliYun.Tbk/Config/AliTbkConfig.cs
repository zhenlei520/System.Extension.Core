namespace EInfrastructure.Core.AliYun.Tbk.Config
{
    /// <summary>
    /// 阿里配置
    /// </summary>
    public class AliTbkConfig
    {
        /// <summary>
        /// 推广id
        /// 格式：推广者_媒体_推广位
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 淘口令使用
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 淘口令使用
        /// </summary>
        public string AppSecret { get; set; }
    }
}