// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Validation;
using FluentValidation;
using Newtonsoft.Json;

namespace EInfrastructure.Core.CustomConfiguration.Core.Domain.Param
{
    /// <summary>
    /// 编辑app名称空间
    /// </summary>
    public class EditAppNamespacesParam : IFluentlValidatorEntity
    {
        /// <summary>
        /// 名称空间
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class EditAppNamespacesParamValidator : AbstractValidator<EditAppNamespacesParam>, IFluentlValidator<EditAppNamespacesParam>
        {
            /// <summary>
            /// 
            /// </summary>
            public EditAppNamespacesParamValidator()
            {
                RuleFor(x => x.Name).Must(x => !string.IsNullOrEmpty(x?.Trim())).WithMessage("name is not empty");
                RuleFor(x => x.Name).MaximumLength(50).WithMessage("the name length is less than or equal to 50")
                    .When(x => x.Name != null);
                RuleFor(x => x.Remark).MaximumLength(200).WithMessage("the remark length is less than or equal to 200")
                    .When(x => x.Remark != null);
            }
        }
    }
}