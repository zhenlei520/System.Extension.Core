// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Ioc;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.AspNetCore
{
    /// <summary>
    ///
    /// </summary>
    public class HttpRequest : IHttpRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public HttpRequest(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region 得到请求Header

        /// <summary>
        /// 得到请求Header
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetHeaders()
        {
            return _httpContextAccessor.HttpContext.Request.Headers
                .Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).ToList();
        }

        #endregion

        #region 是否是Https

        /// <summary>
        /// 是否是Https
        /// </summary>
        /// <returns></returns>
        public bool IsHttps()
        {
            return _httpContextAccessor.HttpContext.Request.IsHttps;
        }

        #endregion

        #region 得到get请求参数

        /// <summary>
        /// 得到get请求参数首个name的值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public string GetQuery(string name)
        {
            return GetQueryList(name).FirstOrDefault();
        }

        /// <summary>
        /// 得到get请求参数name的值，多个参数名为name时，可获取多个参数的值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public List<string> GetQueryList(string name)
        {
            var res = _httpContextAccessor.HttpContext.Request.Query.Where(x => x.Key == name).Select(x => x.Value)
                .FirstOrDefault();
            return res.ToArray().ToList();
        }

        #endregion

        #region 得到Post请求参数信息

        /// <summary>
        /// 得到请求参数信息
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetRequestBody()
        {
            switch (_httpContextAccessor.HttpContext.Request.ContentType)
            {
                case "application/x-www-form-urlencoded":
                case "multipart/form-data":
                    return _httpContextAccessor.HttpContext.Request.Form
                        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                        .ToList();
                case "application/json":
                case "text/plain":
                case "text/html":
                case "application/xml":
                    return await GetByBody(_httpContextAccessor.HttpContext);
            }

            return null;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<object> GetByBody(HttpContext context)
        {
            context.Request.EnableBuffering();
            var requestReader = new StreamReader(context.Request.Body);
            if (requestReader.BaseStream.CanRead)
            {
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                var requestContent = await requestReader.ReadToEndAsync();
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                return requestContent;
            }

            return null;
        }

        #endregion
    }
}
