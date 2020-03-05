using System;
using System.Collections.Generic;
using System.Text;
using EInfrastructure.Core.Config.EntitiesExtensions.SeedWork;
using EInfrastructure.Core.Config.EnumerationExtensions.SeedWork;

namespace EInfrastructure.Core.Http.Enumerations
{
    /// <summary>
    /// Http请求类型
    /// </summary>
    public class RequestType : Enumeration
    {
        /// <summary>
        /// Get请求
        /// </summary>
        public static RequestType Get = new RequestType(1, "Get");

        /// <summary>
        /// Post请求
        /// </summary>
        public static RequestType Post = new RequestType(2, "Post");

        /// <summary>
        /// Put请求
        /// </summary>
        public static RequestType Put = new RequestType(3, "Put");

        /// <summary>
        /// Delete删除
        /// </summary>
        public static RequestType Delete = new RequestType(4, "Delete");

        /// <summary>
        /// 请求类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public RequestType(int id, string name) : base(id, name)
        {
        }
    }
}
