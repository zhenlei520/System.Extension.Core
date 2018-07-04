namespace EInfrastructure.Core.WebChat.Config
{
    public class LoginResultConfig
    {
        public string OpenId { get; set; }
        
        public string Unionid { get; set; }
        
        public bool Success { get; set; }
        
        public string Error { get; set; }
        
        public string AppId { get; set; }

        public WxUserInfo UserInfo { get; set; }
    }
}