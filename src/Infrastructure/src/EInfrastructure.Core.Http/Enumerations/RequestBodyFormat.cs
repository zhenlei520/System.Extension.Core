// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Http.Enumerations
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public class RequestBodyFormat : Enumeration
    {
        /// <summary>
        /// Json格式
        /// </summary>
        public static RequestBodyFormat Json=new RequestBodyFormat(1,"Json");

        /// <summary>
        /// Xml格式
        /// </summary>
        public static RequestBodyFormat Xml=new RequestBodyFormat(2,"Xml");

        /// <summary>
        /// None
        /// </summary>
        public static RequestBodyFormat None=new RequestBodyFormat(3,"None");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public RequestBodyFormat(int id, string name) : base(id, name)
        {
        }
    }
}
