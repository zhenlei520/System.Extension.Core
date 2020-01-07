// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Config.EnumerationExtensions
{
    /// <summary>
    /// httpstatus
    /// </summary>
    public class HttpStatus : EntitiesExtensions.SeedWork.Enumeration
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static HttpStatus Ok = new HttpStatus(200, "success", "成功");

        /// <summary>
        /// 错误
        /// </summary>
        public static HttpStatus Err = new HttpStatus(201, "error", "错误");

        /// <summary>
        /// 未授权
        /// </summary>
        public static HttpStatus Unauthorized = new HttpStatus(401, "unauthorized", "未授权");

        /// <summary>
        /// 未发现信息
        /// </summary>
        public static HttpStatus NoFind = new HttpStatus(404, "nofind", "未发现信息");

        /// <summary>
        /// 请求非法
        /// </summary>
        public static HttpStatus IllegalRequest = new HttpStatus(407, "illegal request", "请求非法");

        /// <summary>
        ///访问受限
        /// </summary>
        public static HttpStatus RequestLimit = new HttpStatus(408, "request limit", "访问受限");

        /// <summary>
        /// 系统繁忙
        /// </summary>
        public static HttpStatus TimeOutException = new HttpStatus(503, "system busy", "系统繁忙");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        public HttpStatus(int id, string name, string desc) : base(id, name)
        {
            Desc = desc;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; private set; }
    }
}
