// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations
{
    /// <summary>
    /// 回调内容类型
    /// </summary>
    public class CallbackBodyType : Enumeration
    {
        /// <summary>
        /// application/json
        /// </summary>
        public static CallbackBodyType Json = new CallbackBodyType(1, "application/json");

        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        public static CallbackBodyType Urlencoded = new CallbackBodyType(2, "application/x-www-form-urlencoded");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        public CallbackBodyType(int id, string name) : base(id, name)
        {
        }
    }
}
