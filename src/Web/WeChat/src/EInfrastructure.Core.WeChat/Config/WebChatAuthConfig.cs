// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.WeChat.Config
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
