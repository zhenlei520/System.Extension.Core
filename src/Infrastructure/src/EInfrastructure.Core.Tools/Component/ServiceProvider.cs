// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Tools.Systems;

namespace EInfrastructure.Core.Tools.Component
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceProvider : IServiceProvider
    {
        #region 得到服务

        /// <summary>
        /// 得到服务
        /// </summary>
        /// <param name="serviceType">接口类</param>
        /// <returns>得到继承serviceType的实现类</returns>
        public IEnumerable<object> GetService(Type serviceType)
        {
            var types = AssemblyCommon.GetAssemblies().SelectMany(x =>
                x.GetTypes().Where(y => y.GetInterfaces().Contains(serviceType))).ToList();
            foreach (var type in types)
            {
                yield return Activator.CreateInstance(type);
            }
        }

        #endregion

        #region 得到服务

        /// <summary>
        /// 得到服务
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TService> GetService<TService>()
        {
            return (IEnumerable<TService>) GetService(typeof(TService));
        }

        #endregion
    }
}
