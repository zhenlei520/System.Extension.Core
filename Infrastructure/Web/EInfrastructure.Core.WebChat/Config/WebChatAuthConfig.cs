namespace EInfrastructure.Core.WebChat.Config
{
  /// <summary>
  /// 微信授权信息
  /// </summary>
  public class WebChatAuthConfig
  {
    /// <summary>
    /// signature
    /// </summary>
    public string Signature { get; set; }

    /// <summary>
    /// timestamp
    /// </summary>
    public string Timestamp { get; set; }

    /// <summary>
    /// nonce
    /// </summary>
    public string Nonce { get; set; }

    /// <summary>
    /// echoString
    /// </summary>
    public string EchoString { get; set; }
  }
}
