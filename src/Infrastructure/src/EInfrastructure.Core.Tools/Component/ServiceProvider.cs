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
        /// 得到服务集合
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns>得到继承serviceType的实现类</returns>
        public IEnumerable<TService> GetServices<TService>() where TService : class
        {
            var types = AssemblyCommon.GetAssemblies().SelectMany(x =>
                x.GetTypes().Where(y => y.GetInterfaces().Contains(typeof(TService)))).ToList();
            List<TService> list = new List<TService>();
            foreach (var type in types)
            {
                list.Add(Activator.CreateInstance(type) as TService);
                // yield return Activator.CreateInstance(type);
            }

            return list;
        }

        #endregion

        #region 得到服务

        /// <summary>
        /// 得到服务
        /// </summary>
        /// <returns></returns>
        public TService GetService<TService>() where TService : class
        {
            return GetServices<TService>().FirstOrDefault();
        }

        #endregion
    }
}
