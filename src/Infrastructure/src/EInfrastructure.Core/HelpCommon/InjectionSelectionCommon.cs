// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 选择
    /// </summary>
    public class InjectionSelectionCommon
    {
        #region 得到实现

        /// <summary>
        /// 得到实现
        /// </summary>
        /// <param name="providers"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetImplement<T>(ICollection<T> providers) where T : IWeight
        {
            if (providers == null || providers.Count == 0)
            {
                throw new NotImplementedException();
            }

            return providers.Count == 1
                ? providers.FirstOrDefault()
                : providers.OrderByDescending(x => x.GetWeights()).FirstOrDefault();
        }

        #endregion
    }
}
