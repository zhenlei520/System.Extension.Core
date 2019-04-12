// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EInfrastructure.Core.AspNetCore.Filter
{
    /// <summary>
    /// 传参模型校验
    /// </summary>
    public class ModelValidationFilter
    {
        private readonly ILogService _logService;

        public ModelValidationFilter(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        if (string.IsNullOrEmpty(error.ErrorMessage))
                        {
                            AddLog(context);
                            throw new BusinessException("请检查参数格式是否正确");
                        }

                        throw new BusinessException(error.ErrorMessage);
                    }
                }

                AddLog(context);
                throw new BusinessException("参数异常");
            }
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="context"></param>
        private void AddLog(ActionExecutingContext context)
        {
            try
            {
                byte[] byts = new byte[context.HttpContext.Request.Body.Length];
                context.HttpContext.Request.Body.Read(byts, 0, byts.Length);
                string param = System.Text.Encoding.Default.GetString(byts);
                _logService.Error($"模型校验异常，请求接口路径：{context.HttpContext.Request.Scheme}，请求参数：{param}");
            }
            catch (System.Exception e)
            {
                _logService.Error("模型校验异常，处理日志异常");
            }
        }
    }
}