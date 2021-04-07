// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Bucket
{
    /// <summary>
    /// 空间访问权限验证
    /// </summary>
    public class SetPermissParamValidator : AbstractValidator<SetPermissParam>
    {
        /// <summary>
        ///
        /// </summary>
        public SetPermissParamValidator()
        {
            RuleFor(x => x.Permiss).Must(x=>Permiss.GetAll<Permiss>().Any(y=>y.Id==x.Id)).WithMessage("不支持的访问权限")
                .Must(x => x.Id != Permiss.PublicReadWrite.Id).WithMessage("不支持的访问权限");
        }
    }
}
