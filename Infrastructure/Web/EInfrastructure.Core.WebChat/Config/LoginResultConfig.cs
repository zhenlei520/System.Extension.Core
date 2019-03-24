namespace EInfrastructure.Core.WebChat.Config
{/// <summary>
 /// 
 /// </summary>
    public class LoginResultConfig
    {/// <summary>
     /// 
     /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Unionid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }
/// <summary>
/// 
/// </summary>
        public WxUserInfo UserInfo { get; set; }
    }
}