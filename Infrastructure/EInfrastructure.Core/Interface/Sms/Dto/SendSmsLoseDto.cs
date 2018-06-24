using System.Collections.Generic;

namespace EInfrastructure.Core.Interface.Sms.Dto
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
    }
}
