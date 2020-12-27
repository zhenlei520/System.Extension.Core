// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Reflection;

namespace EInfrastructure.Core.Configuration.Ioc
{
    /// <summary>
    /// Identify
    /// </summary>
    public interface IIdentify : IWeight
    {
        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        string GetIdentify();
    }

    /// <summary>
    /// 默认Identify
    /// </summary>
    public class IdentifyDefault : WeightDefault, IIdentify
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }
    }
}
