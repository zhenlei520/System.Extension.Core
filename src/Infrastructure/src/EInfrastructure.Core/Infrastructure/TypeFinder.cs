// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools;
using Microsoft.Extensions.Logging;

namespace EInfrastructure.Core.Infrastructure
{
    /// <summary>
    ///
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
        private readonly IAssemblyProvider _assemblyProvider;
        private readonly ILogger _logger;

        /// <summary>
        ///
        /// </summary>
        public TypeFinder() : this(AssemblyProvider.GetDefaultAssemblyProvider)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="assemblyProvider"></param>
        public TypeFinder(IAssemblyProvider assemblyProvider) : this(assemblyProvider, null)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="assemblyProvider"></param>
        /// <param name="logger"></param>
        public TypeFinder(IAssemblyProvider assemblyProvider, ILogger<TypeFinder> logger)
        {
            this._assemblyProvider = assemblyProvider ?? AssemblyProvider.GetDefaultAssemblyProvider;
            this._logger = logger;
        }

        #region Methods

        /// <summary>
        /// Find classes of type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
        /// <returns>Result</returns>
        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        /// <summary>
        /// Find classes of type
        /// </summary>
        /// <param name="assignTypeFrom">Assign type from</param>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
        /// <returns>Result</returns>
        /// <returns></returns>
        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(assignTypeFrom, _assemblyProvider.GetAssemblies(), onlyConcreteClasses);
        }

        /// <summary>
        /// Find classes of type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="assemblies">Assemblies</param>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
        /// <returns>Result</returns>
        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
        }

        /// <summary>
        /// Find classes of type
        /// </summary>
        /// <param name="assignTypeFrom">Assign type from</param>
        /// <param name="assemblies">Assemblies</param>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
        /// <param name="ignoreReflectionErrors">是否忽略错误，默认忽略</param>
        /// <returns>Result</returns>
        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies,
            bool onlyConcreteClasses = true, bool ignoreReflectionErrors = true)
        {
            if (assemblies == null)
            {
                return FindClassesOfType(assignTypeFrom, onlyConcreteClasses);
            }

            var result = new List<Type>();
            try
            {
                foreach (var a in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = a.GetTypes();
                    }
                    catch (Exception ex)
                    {
                        //Entity Framework 6 doesn't allow getting types (throws an exception)
                        if (!ignoreReflectionErrors)
                        {
                            throw;
                        }

                        _logger?.LogError(ex.ExtractAllStackTrace());
                    }

                    if (types == null)
                        continue;

                    foreach (var t in types)
                    {
                        if (!assignTypeFrom.IsAssignableFrom(t) &&
                            (!assignTypeFrom.IsGenericTypeDefinition ||
                             !DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                            continue;

                        if (t.IsInterface)
                            continue;

                        if (onlyConcreteClasses)
                        {
                            if (t.IsClass && !t.IsAbstract)
                            {
                                result.Add(t);
                            }
                        }
                        else
                        {
                            result.Add(t);
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);
                _logger.LogError("Message：" + fail.Message + "，ExtractAllStackTrace：" + fail.ExtractAllStackTrace());
                throw fail;
            }

            return result;
        }

        #endregion

        #region parvite methods

        #region 当前类型是否支持泛型

        /// <summary>
        /// 当前类型是否支持泛型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="openGeneric">泛型</param>
        /// <returns></returns>
        private bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();

                //找出类型的接口
                Type[] findInterfaces = type.FindInterfaces((_, __) => true, null);

                foreach (var implementedInterface in findInterfaces)
                {
                    if (!implementedInterface.IsGenericType)
                    {
                        continue;
                    }

                    var isAssignableFrom =
                        genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isAssignableFrom;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #endregion
    }
}
