// Copyright (c) zhenlei520 All rights reserved.

using FluentValidation;

namespace EInfrastructure.Core.Validation
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IFluentlValidator<TEntity> : IValidator
        where TEntity : IFluentlValidatorEntity
    {
    }
}
