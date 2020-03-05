// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Component;
using FluentValidation.Results;

namespace EInfrastructure.Core.Validation.Common
{
    /// <summary>
    /// 校验处理
    /// </summary>
    public static class ValidationCommon
    {
        #region 检查验证

        /// <summary>
        /// 检查验证
        /// </summary>
        /// <param name="entity">待检查的参数类</param>
        /// <param name="msg">默认错误提示信息</param>
        /// <param name="code">错误码</param>
        /// <typeparam name="TEntity"></typeparam>
        public static void Check<TEntity>(this TEntity entity, string msg = "参数异常", string code = "")
            where TEntity : IFluentlValidatorEntity
        {
            Tools.Check.True(entity != null, msg);
            var validatotor = new ServiceProvider().GetService<IFluentlValidator<TEntity>>();
            if (validatotor != null)
            {
                validatotor.Validate(entity)
                    .Check(code);
            }
        }

        #endregion

        #region 检查验证

        /// <summary>
        /// 检查验证
        /// </summary>
        /// <param name="results"></param>
        /// <param name="code">异常码</param>
        public static void Check(this ValidationResult results, string code)
        {
            if (!results.IsValid)
            {
                var msg = results.Errors.Select(x => x.ErrorMessage).FirstOrDefault();
                throw new BusinessException<string>(msg, code ?? HttpStatus.Err.Name);
            }
        }

        /// <summary>
        /// 检查验证
        /// </summary>
        /// <param name="results"></param>
        /// <param name="code">异常码</param>
        public static void Check(this ValidationResult results, int? code = null)
        {
            if (!results.IsValid)
            {
                var msg = results.Errors.Select(x => x.ErrorMessage).FirstOrDefault();
                throw new BusinessException(msg, code ?? HttpStatus.Err.Id);
            }
        }

        #endregion
    }
}
