// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 加密方式
    /// </summary>
    public class SecurityType : Enumeration
    {
        /// <summary>
        /// Aes加密
        /// </summary>
        public static SecurityType Aes = new SecurityType(1, "Aes");

        /// <summary>
        /// Des加密
        /// </summary>
        public static SecurityType Des = new SecurityType(2, "Des");

        /// <summary>
        /// JsAes加密
        /// </summary>
        public static SecurityType JsAes = new SecurityType(2, "JsAes");

        /// <summary>
        ///
        /// </summary>
        public SecurityType()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">加密标识</param>
        /// <param name="name">加密方式描述</param>
        protected SecurityType(int id, string name) : base(id, name)
        {
        }
    }
}
