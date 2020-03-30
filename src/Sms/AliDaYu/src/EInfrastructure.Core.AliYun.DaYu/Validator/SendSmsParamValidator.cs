// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params;
using EInfrastructure.Core.Tools;
using FluentValidation;

namespace EInfrastructure.Core.AliYun.DaYu.Validator
{
    /// <summary>
    /// 发送短信校验
    /// </summary>
    internal class SendSmsParamValidator : AbstractValidator<SendSmsParam>
    {
        /// <summary>
        ///
        /// </summary>
        public SendSmsParamValidator()
        {
            RuleFor(x => x.Phone).Must(x => x.IsMobile()).WithMessage("手机号不能为空");
            RuleFor(x => x.TemplateCode).Must(x => !string.IsNullOrEmpty(x)).WithMessage("模板不能为空");
            RuleFor(x => x.SignName).Must(x => !string.IsNullOrEmpty(x)).WithMessage("签名不能为空");
        }
    }
}
