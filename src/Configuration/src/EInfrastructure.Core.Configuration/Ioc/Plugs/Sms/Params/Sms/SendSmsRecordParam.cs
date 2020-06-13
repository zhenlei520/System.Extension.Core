// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params.Sms
{
    /// <summary>
    /// 发送短信详情
    /// </summary>
    public class SendSmsRecordParam
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone">手机号</param>
        public SendSmsRecordParam(string phone) : this(phone, 1, 10)
        {
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        public SendSmsRecordParam(string phone, int pageIndex, int pageSize)
        {
            Phone = phone;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; }

        /// <summary>
        /// 发送回执ID，即发送流水号(非必填)
        /// </summary>
        public string BizId { get; set; }
    }
}
