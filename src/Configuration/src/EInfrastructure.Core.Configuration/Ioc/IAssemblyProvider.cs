// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace EInfrastructure.Core.Configuration.Ioc
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public interface IAssemblyProvider : ISingleInstance
    {
        /// <summary>
        /// Gets or sets assemblies loaded a startup in addition to those loaded in the AppDomain.
        /// 默认需要加载的程序集强名称集合
        /// 例如.NET 2.0中的FileIOPermission类，它的强名称是：System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
        /// </summary>
        IEnumerable<string> AssemblyNames { get; set; }

        /// <summary>
        /// 跳过的dll
        /// </summary>
        IEnumerable<string> AssemblySkipLoadingPattern { get; set; }

        /// <summary>
        /// 是否加载AppDomain下的程序集
        /// </summary>
        bool LoadAppDomainAssemblies { get; set; }

        /// <summary>
        /// Gets or sets the pattern for dll that will be investigated. For ease of use this defaults to match all but to increase performance you might want to configure a pattern that includes assemblies and your own
        /// <remarks>If you change this so that Nop assemblies aren't investigated (e.g. by not including something like "^Nop|..." you may break core functionality.</remarks>
        /// </summary>
        string AssemblyRestrictToLoadingPattern { get; set; }

        /// <summary>
        /// 得到应用程序集
        /// </summary>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies();

        /// <summary>
        /// 获取指定文件夹下的应用程序集
        /// </summary>
        /// <param name="directoryPaths">指定文件目录集合</param>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies(params string[] directoryPaths);

        /// <summary>
        /// 获取指定文件夹下的应用程序集
        /// </summary>
        /// <param name="directoryPaths">指定文件目录集合</param>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies(IEnumerable<string> directoryPaths);

        /// <summary>
        /// Get the default DLL mode that we know we don't need to study
        /// 获取默认需要跳过的程序集
        /// </summary>
        IEnumerable<string> GetAssemblySkipLoadingPatternDefault();

        /// <summary>
        /// 加载指定目录匹配的应用程序集
        /// </summary>
        /// <param name="directoryPath">The physical path to a directory containing dlls to load in the app domain.</param>
        void LoadMatchingAssemblies(string directoryPath);
    }
}
