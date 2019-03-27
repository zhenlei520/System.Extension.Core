using EInfrastructure.Core.WeChat.Enum;

namespace EInfrastructure.Core.WeChat.Config
{
    /// <summary>
    /// 微信配置文件
    /// </summary>
    public class WxConfig
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }
        
        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        
        /// <summary>
        /// 微信授权Token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        public string EncodingAesKey { get; set; }
        
        /// <summary>
        /// 微信类型
        /// </summary>
        public WebChatTypeEnum Type { get; set; }
    }
}