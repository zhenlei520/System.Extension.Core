// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Validation;
using FluentValidation;
using Newtonsoft.Json;

namespace EInfrastructure.Core.CustomConfiguration.Core.Domain.Param
{
    /// <summary>
    /// 修改应用信息
    /// </summary>
    public class EditAppParam : IFluentlValidatorEntity
    {
        /// <summary>
        /// 新appid
        /// </summary>
        [JsonProperty(PropertyName = "appid")]
        public string Appid { get; set; }

        /// <summary>
        /// 新应用名称
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class EditAppParamValidator : AbstractValidator<EditAppParam>, IFluentlValidator<EditAppParam>
        {
            public EditAppParamValidator()
            {
                RuleFor(x => x.Appid).Must(x => !string.IsNullOrEmpty(x?.Trim())).WithMessage("appid is not empty");
                RuleFor(x => x.Appid).MaximumLength(50).WithMessage("the appid length is less than or equal to 50")
                    .When(x => x.Appid != null);
                RuleFor(x => x.Name).MaximumLength(50).WithMessage("the name length is less than or equal to 50")
                    .When(x => x.Name != null);
            }
        }
    }
}