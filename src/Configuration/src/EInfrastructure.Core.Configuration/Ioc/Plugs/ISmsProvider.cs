// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs
{
    /// <summary>
    /// 短信
    /// </summary>
    public interface ISmsProvider : IIdentify
    {
        #region 指定短信列表发送短信

        /// <summary>
        /// 指定短信列表发送短信
        /// </summary>
        /// <param name="phoneNumbers">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <param name="loseAction">失败回调函数</param>
        /// <returns></returns>
        bool Send(List<string> phoneNumbers, string templateCode, object content,
            Action<SendSmsLoseDto> loseAction = null);

        #endregion

        #region 指定单个手机号发送短信

        /// <summary>
        /// 指定单个手机号发送短信
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <param name="loseAction">失败回调函数</param>
        /// <returns></returns>
        bool Send(string phoneNumber, string templateCode, object content, Action<SendSmsLoseDto> loseAction = null);

        #endregion
    }

    /// <summary>
    /// 发送短信失败
    /// </summary>
    public class SendSmsLoseDto
    {
        /// <summary>
        /// 手机号列表
        /// </summary>
        [JsonProperty(PropertyName = "phone_list")]
        public List<string> PhoneList { get; set; }

        /// <summary>
        /// 发送短信失败
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }

        /// <summary>
        /// 具体失败原因
        /// </summary>
        [JsonProperty(PropertyName = "sub_msg")]
        public string SubMsg { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }
}
