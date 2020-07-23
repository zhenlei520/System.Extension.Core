// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Files;
using Microsoft.Extensions.Logging;

namespace EInfrastructure.Core.Infrastructure
{
    /// <summary>
    ///
    /// </summary>
    public class AssemblyProvider : IAssemblyProvider
    {
        /// <summary>
        /// 得到默认的程序集
        /// </summary>
        public static AssemblyProvider GetDefaultAssemblyProvider = new AssemblyProvider()
        {
            AssemblySkipLoadingPattern = new List<string>(),
            LoadAppDomainAssemblies = true
        };

        private readonly ILogger _logger;

        /// <summary>
        ///
        /// </summary>
        public AssemblyProvider()
        {
            AssemblySkipLoadingPattern = new CloneableClass().DeepClone(AssemblySkipLoadingPatternSource);
            LoadAppDomainAssemblies = true;
            AssemblyRestrictToLoadingPattern = ".*";
        }

        /// <summary>
        ///
        /// </summary>
        public AssemblyProvider(ILogger<AssemblyProvider> logger) : this()
        {
            _logger = logger;
        }

        #region Properties

        /// <summary>
        /// The app domain to look for types in.
        /// </summary>
        private AppDomain App => AppDomain.CurrentDomain;

        /// <summary>
        /// Gets the pattern for dlls that we know don't need to be investigated.
        /// </summary>
        private List<string> AssemblySkipLoadingPatternSource = new List<string>()
        {
            "System",
            "mscorlib",
            "Microsoft",
            "AjaxControlToolkit",
            "Antlr3",
            "Autofac",
            "AutoMapper",
            "Castle",
            "ComponentArt",
            "CppCodeProvider",
            "DotNetOpenAuth",
            "EntityFramework",
            "EPPlus",
            "FluentValidation",
            "ImageResizer",
            "itextsharp",
            "log4net",
            "MaxMind",
            "MbUnit",
            "MiniProfiler",
            "Mono.Math",
            "MvcContrib",
            "Newtonsoft",
            "NHibernate",
            "nunit",
            "Org.Mentalis",
            "PerlRegex",
            "QuickGraph",
            "Recaptcha",
            "Remotion",
            "RestSharp",
            "Rhino",
            "Telerik",
            "Iesi",
            "TestDriven",
            "TestFu",
            "UserAgentStringLibrary",
            "VJSharpCodeProvider",
            "WebActivator",
            "WebDev",
            "WebGrease"
        };

        /// <summary>
        ///
        /// </summary>
        private string GetAssemblySkipLoadingPatternString =>
            AssemblySkipLoadingPattern.Select(x => "^" + x).ToList().ConvertListToString('|');

        #endregion

        #region 默认需要加载的程序集dll

        /// <summary>
        /// Gets or sets assemblies loaded a startup in addition to those loaded in the AppDomain.
        /// 默认需要加载的程序集强名称集合
        /// 例如.NET 2.0中的FileIOPermission类，它的强名称是：System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
        /// </summary>
        public IEnumerable<string> AssemblyNames { get; set; }

        #endregion

        #region 跳过的程序集dll

        /// <summary>
        /// 跳过的程序集dll
        /// </summary>
        public IEnumerable<string> AssemblySkipLoadingPattern { get; set; }

        #endregion

        #region 是否加载AppDomain下的程序集

        /// <summary>
        /// 是否加载AppDomain下的程序集
        /// </summary>
        public bool LoadAppDomainAssemblies { get; set; }

        #endregion

        #region 获取或设置要调查的dll的模式。为了便于使用，除了提高性能之外，默认情况下此模式与其他模式匹配，您可能需要配置包含程序集和您自己的程序集的模式

        /// <summary>
        /// Gets or sets the pattern for dll that will be investigated. For ease of use this defaults to match all but to increase performance you might want to configure a pattern that includes assemblies and your own
        /// <remarks>If you change this so that Nop assemblies aren't investigated (e.g. by not including something like "^Nop|..." you may break core functionality.</remarks>
        /// </summary>
        public string AssemblyRestrictToLoadingPattern { get; set; }

        #endregion

        #region 得到应用程序集

        /// <summary>
        /// 得到应用程序集
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            var addedAssemblyNames = new List<string>();
            var assemblies = new List<Assembly>();

            if (LoadAppDomainAssemblies)
                AddAssembliesInAppDomain(addedAssemblyNames, assemblies);
            AddConfiguredAssemblies(addedAssemblyNames, assemblies);
            return assemblies.Distinct();
        }

        /// <summary>
        /// 获取指定文件夹下的应用程序集
        /// </summary>
        /// <param name="directoryPaths">指定文件目录集合</param>
        /// <returns></returns>
        public IEnumerable<Assembly> GetAssemblies(params string[] directoryPaths)
        {
            return GetAssemblies(directoryPaths.ToList());
        }

        /// <summary>
        /// 获取指定文件夹下的应用程序集
        /// </summary>
        /// <param name="directoryPaths">指定文件目录集合</param>
        /// <returns></returns>
        public IEnumerable<Assembly> GetAssemblies(IEnumerable<string> directoryPaths)
        {
            List<Assembly> assemblies = new List<Assembly>();
            var loadedAssemblyNames = new List<string>();
            TryAddAssembly(assemblies, loadedAssemblyNames, GetAssemblies());

            foreach (var directoryPath in directoryPaths)
            {
                if (!FileCommon.IsExistDirectory(directoryPath))
                {
                    continue;
                }

                foreach (var dllPath in FileCommon.GetFiles(directoryPath, "*.dll"))
                {
                    try
                    {
                        var an = AssemblyName.GetAssemblyName(dllPath);
                        if (Matches(an.FullName) && !loadedAssemblyNames.Contains(an.FullName))
                        {
                            App.Load(an);
                            assemblies.Add(Assembly.Load(an.FullName));
                        }
                    }
                    catch (BadImageFormatException ex)
                    {
                        _logger?.LogError(ex.ExtractAllStackTrace());
                    }
                }
            }

            return assemblies.Distinct();
        }

        #endregion

        #region 获取默认需要跳过的程序集

        /// <summary>
        /// Get the default DLL mode that we know we don't need to study
        /// 获取默认需要跳过的程序集
        /// </summary>
        public IEnumerable<string> GetAssemblySkipLoadingPatternDefault()
        {
            return AssemblySkipLoadingPatternSource;
        }

        #endregion

        #region 加载指定目录匹配的应用程序集

        /// <summary>
        /// Makes sure matching assemblies in the supplied folder are loaded in the app domain.
        /// 加载指定目录匹配的应用程序集
        /// </summary>
        /// <param name="directoryPath">
        ///     The physical path to a directory containing dlls to load in the app domain.
        /// </param>
        public void LoadMatchingAssemblies(string directoryPath)
        {
            var loadedAssemblyNames = new List<string>();
            foreach (var assembly in GetAssemblies())
            {
                loadedAssemblyNames.Add(assembly.FullName);
            }

            if (!FileCommon.IsExistDirectory(directoryPath))
            {
                return;
            }

            foreach (var dllPath in FileCommon.GetFiles(directoryPath, "*.dll"))
            {
                try
                {
                    var an = AssemblyName.GetAssemblyName(dllPath);
                    if (Matches(an.FullName) && !loadedAssemblyNames.Contains(an.FullName))
                    {
                        App.Load(an);
                    }
                }
                catch (BadImageFormatException ex)
                {
                    _logger?.LogError(ex.ExtractAllStackTrace());
                }
            }
        }

        #endregion

        #region private methods

        #region 从AppDomain下得到可用程序集

        /// <summary>
        /// Iterates all assemblies in the AppDomain and if it's name matches the configured patterns add it to our list.
        /// </summary>
        /// <param name="addedAssemblyNames"></param>
        /// <param name="assemblies"></param>
        private void AddAssembliesInAppDomain(List<string> addedAssemblyNames, List<Assembly> assemblies)
        {
            TryAddAssembly(assemblies, addedAssemblyNames, AppDomain.CurrentDomain.GetAssemblies().ToList());
        }

        #endregion

        #region 加载指定的应用程序集

        /// <summary>
        /// Adds specifically configured assemblies.
        /// </summary>
        /// <param name="addedAssemblyNames"></param>
        /// <param name="assemblies"></param>
        protected virtual void AddConfiguredAssemblies(List<string> addedAssemblyNames, List<Assembly> assemblies)
        {
            foreach (var assemblyName in AssemblyNames)
            {
                var assembly = Assembly.Load(assemblyName);
                if (addedAssemblyNames.Contains(assembly.FullName))
                    continue;

                assemblies.Add(assembly);
                addedAssemblyNames.Add(assembly.FullName);
            }
        }

        #endregion

        #region 尝试加载程序集

        /// <summary>
        /// 尝试加载程序集
        /// </summary>
        /// <param name="assemblies">已添加的程序集</param>
        /// <param name="addedAssemblyNames">已加载的程序集的Name</param>
        /// <param name="addAssemblies">等待添加的程序集</param>
        /// <returns></returns>
        protected virtual void TryAddAssembly(List<Assembly> assemblies, List<string> addedAssemblyNames,
            params Assembly[] addAssemblies)
        {
            TryAddAssembly(assemblies, addedAssemblyNames, addAssemblies.ToList());
        }

        /// <summary>
        /// 尝试加载程序集
        /// </summary>
        /// <param name="assemblies">已添加的程序集</param>
        /// <param name="addedAssemblyNames">已加载的程序集的Name</param>
        /// <param name="addAssemblies">等待添加的程序集</param>
        /// <returns></returns>
        protected virtual void TryAddAssembly(List<Assembly> assemblies, List<string> addedAssemblyNames,
            IEnumerable<Assembly> addAssemblies)
        {
            foreach (var assembly in addAssemblies)
            {
                if (!Matches(assembly.FullName))
                    continue;
                if (addedAssemblyNames.Contains(assembly.FullName))
                    continue;

                assemblies.Add(assembly);
                addedAssemblyNames.Add(assembly.FullName);
            }
        }

        #endregion

        #region 检查dll是否是我们不需要的

        /// <summary>
        /// Check if a dll is one of the shipped dlls that we know don't need to be investigated.
        /// </summary>
        /// <param name="assemblyFullName">
        /// The name of the assembly to check.
        /// </param>
        /// <returns>
        /// True if the assembly should be loaded into Nop.
        /// </returns>
        public virtual bool Matches(string assemblyFullName)
        {
            return !Matches(assemblyFullName, GetAssemblySkipLoadingPatternString)
                   && Matches(assemblyFullName, AssemblyRestrictToLoadingPattern);
        }


        /// <summary>
        /// 检查dll是否是我们不需要的
        /// Check if a dll is one of the shipped dlls that we know don't need to be investigated.
        /// </summary>
        /// <param name="assemblyFullName">
        /// The assembly name to match.
        /// </param>
        /// <param name="pattern">
        /// The regular expression pattern to match against the assembly name.
        /// </param>
        /// <returns>
        /// True if the pattern matches the assembly name.
        /// </returns>
        protected virtual bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        #endregion

        #endregion
    }
}
