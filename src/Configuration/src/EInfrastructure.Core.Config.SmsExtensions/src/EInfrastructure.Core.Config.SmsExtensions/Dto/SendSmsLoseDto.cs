// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Config.SmsExtensions.Dto
{
    /// <summary>
    /// 发送短信失败
    /// </summary>
    public class SendSmsLoseDto
    {
        /// <summary>
        /// 手机号列表
        /// </summary>
        public List<string> PhoneList { get; set; }

        /// <summary>
        /// 发送短信失败
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 具体失败原因
        /// </summary>
        public string SubMsg { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
    }
}
