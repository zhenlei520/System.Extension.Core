// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params.VoiceSms;
using EInfrastructure.Core.Tools;
using FluentValidation;

namespace EInfrastructure.Core.AliYun.DaYu.Validator
{
    /// <summary>
    /// 发送语音短信
    /// </summary>
    internal class SendVoiceSmsParamValidator : AbstractValidator<SendVoiceSmsParam>
    {
        /// <summary>
        ///
        /// </summary>
        public SendVoiceSmsParamValidator()
        {
            RuleFor(x => x.Phone).Must(x => x.IsMobile()).WithMessage("手机号不能为空");
            RuleFor(x => x.TemplateCode).Must(x => !string.IsNullOrEmpty(x)).WithMessage("模板不能为空");
            RuleFor(x => x.PlatTimes).Must(x => x > 1).WithMessage("播放次数异常");
            RuleFor(x => x.Volume).Must(x => x > 0).WithMessage("播放音量必须大于0");
        }
    }
}
