// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum
{
    /// <summary>
    /// 短信状态码
    /// </summary>
    public enum SmsCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")] Ok = 1,

        /// <summary>
        /// 业务停机
        /// </summary>
        [Description("业务停机")] BusinessStop = 2,

        /// <summary>
        /// 权限不足
        /// </summary>
        [Description("权限不足")] InsufficientPrivileges = 3,

        /// <summary>
        /// 账号异常
        /// </summary>
        [Description("账号异常")] AbnormalAccount = 4,

        /// <summary>
        /// 短信模板不合法
        /// </summary>
        [Description("短信模板不合法")] TemplateIllegal = 5,

        /// <summary>
        /// 短信签名不合法
        /// </summary>
        [Description("短信签名不合法")] SignIllegal = 6,

        /// <summary>
        /// 请重试
        /// </summary>
        [Description("请重试")] SystemError = 7,

        /// <summary>
        /// 参数异常
        /// </summary>
        [Description("参数异常")] InvalidParameters = 8,

        /// <summary>
        /// 手机号格式错误
        /// </summary>
        [Description("手机号格式错误")] MobileNumberIllegal =9,

        /// <summary>
        /// 发送次数限制
        /// </summary>
        [Description("发送次数限制")] BusinessLimitControl = 10,

        /// <summary>
        /// 内容敏感
        /// </summary>
        [Description("内容敏感")] BlackKeyControlLimit = 11,

        /// <summary>
        /// 账户余额不足
        /// </summary>
        [Description("账户余额不足")] AmountNotEnough = 12,

        /// <summary>
        /// 短信长度超出限制
        /// </summary>
        [Description("短信长度超出限制")] LengthError = 13,

        /// <summary>
        /// 未知错误（不一定是发送失败）
        /// </summary>
        [Description("未知错误")] Unknown = 14
    }
}
