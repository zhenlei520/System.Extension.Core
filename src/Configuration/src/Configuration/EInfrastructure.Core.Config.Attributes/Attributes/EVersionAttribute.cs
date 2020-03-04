// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Config.Attributes.Attributes
{
    /// <summary>
    /// 版本信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EVersionAttribute : Attribute
    {
        private readonly string _version;

        /// <summary>
        /// 版本
        /// </summary>
        public virtual string Version => _version;

        /// <summary>
        /// 版本信息
        /// </summary>
        /// <param name="version"></param>
        public EVersionAttribute(string version)
        {
            _version = version;
        }
    }
}