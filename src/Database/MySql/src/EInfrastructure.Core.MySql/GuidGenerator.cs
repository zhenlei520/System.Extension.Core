// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Tools.Enumerations;
using EInfrastructure.Core.Tools.Unique;

namespace EInfrastructure.Core.MySql
{
    /// <summary>
    ///
    /// </summary>
    public class GuidGenerator : IGuidGenerator
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Guid Create()
        {
            return UniqueCommon.Guids(SequentialGuidType.SequentialAsString);
        }

        #region 得到权重

        /// <summary>
        /// 得到权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 98;
        }

        #endregion

        #region 得到唯一标识

        /// <summary>
        /// 得到唯一标识
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion
    }
}
