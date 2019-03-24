using System.ComponentModel;

namespace EInfrastructure.Core.WebChat.Enum
{
    /// <summary>
    /// 微信类型
    /// </summary>
    public enum WebChatTypeEnum
    {
        /// <summary>
        /// 微信小程序
        /// </summary>
        [Description("微信小程序")] WeApp = 0,

        /// <summary>
        /// 微信H5
        /// </summary>
        [Description("微信H5")] Mweb = 1,

        /// <summary>
        /// 第三方登录
        /// </summary>
        [Description("第三方登录")] ThirdPartyLogins = 2
    }
}