// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    ///
    /// </summary>
    public class ListFileFilterValidator : AbstractValidator<ListFileFilter>
    {
        /// <summary>
        ///
        /// </summary>
        public ListFileFilterValidator()
        {
            RuleFor(x => x.PageSize).Must(x => x > 0).WithMessage("PageSize必须大于0");
        }
    }
}
