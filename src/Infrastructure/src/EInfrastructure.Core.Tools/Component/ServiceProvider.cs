// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.Tools.Systems;

namespace EInfrastructure.Core.Tools.Component
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceProvider
    {
        /// <summary>
        /// 程序集
        /// </summary>
        private readonly Assembly[] _assblemyArray;

        /// <summary>
        ///
        /// </summary>
        /// <param name="assblemyArray"></param>
        public ServiceProvider(Assembly[] assblemyArray = null)
        {
            _assblemyArray = assblemyArray ?? AssemblyCommon.GetLoadedAssemblies();
        }

        #region 得到服务

        /// <summary>
        /// 得到继承serviceType的实现类
        /// </summary>
        /// <param name="noPublic"></param>
        /// <typeparam name="TService">TService的构造函数是否不公开的，默认只查询公开的构造函数false，否则为true</typeparam>
        /// <returns></returns>
        public IEnumerable<TService> GetServices<TService>(bool noPublic = false) where TService : class
        {
            var types = _assblemyArray.SelectMany(x =>
                x.GetTypes().Where(y => y.GetInterfaces().Contains(typeof(TService)))).ToList();
            foreach (var type in types)
            {
                yield return type.CreateInstance(noPublic) as TService;
            }
        }

        #endregion

        #region 得到服务

        /// <summary>
        /// 得到服务
        /// </summary>
        /// <param name="noPublic">TService的构造函数是否不公开的，默认只查询公开的构造函数false，否则为true</param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService GetService<TService>(bool noPublic = false) where TService : class
        {
            return GetServices<TService>(noPublic).FirstOrDefault();
        }

        #endregion
    }
}
