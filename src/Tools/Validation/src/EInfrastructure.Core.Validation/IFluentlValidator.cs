// Copyright (c) zhenlei520 All rights reserved.

using FluentValidation;

namespace EInfrastructure.Core.Validation
{
    public interface IFluentlValidator<TEntity> : IValidator
        where TEntity : IFluentlValidatorEntity
    {
    }
}
