// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace EInfrastructure.Core.Tools.Systems
{
    /// <summary>
    /// 应用程序集扩展类
    /// </summary>
    public static class AssemblyCommon
    {
        private static readonly FileVersionInfo AssemblyFileVersion =
            FileVersionInfo.GetVersionInfo(fileName: Assembly.GetExecutingAssembly().Location);

        #region 获得Assembly版本号

        /// <summary>
        /// 获得Assembly版本号
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyVersion()
        {
            return $"{AssemblyFileVersion.FileMajorPart}.{AssemblyFileVersion.FileMinorPart}";
        }

        #endregion

        #region 获得Assembly产品版权

        /// <summary>
        /// 获得Assembly产品版权
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyCopyright()
        {
            return AssemblyFileVersion.LegalCopyright;
        }

        #endregion

        #region 得到当前应用的程序集

        private static Assembly[] _assemblys;

        /// <summary>
        /// 得到当前应用的程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAssemblies()
        {
            if (_assemblys == null)
            {
                _assemblys = AppDomain.CurrentDomain.GetAssemblies().ToArray();
            }

            return _assemblys;
        }

        #endregion

        #region 根据类型实例化当前类

        /// <summary>
        /// 根据类型实例化当前类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="noPublic">是否不公开的</param>
        /// <returns></returns>
        public static object CreateInstance(this Type type, bool noPublic = false)
        {
            return Assembly.GetAssembly(type).CreateInstance(type.ToString(), noPublic);
        }

        #endregion

        #region 获取类型信息

        /// <summary>
        /// 获取类型信息
        /// </summary>
        /// <param name="fullName">完整的类名地址：命名空间.类名</param>
        /// <param name="package">包名</param>
        /// <returns></returns>
        public static Type GetType(string fullName, string package)
        {
            Check.True(!string.IsNullOrEmpty(fullName), "fullName is not empty");
            Check.True(!string.IsNullOrEmpty(package), "package is not empty");
            return Type.GetType($"{fullName},{package}");
        }

        #endregion

        #region 得到调用者信息

        /// <summary>
        /// 得到调用者信息
        /// </summary>
        /// <returns></returns>
        public static Type GetReflectedInfo()
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();
            return method.ReflectedType;
        }

        #endregion

        #region 根据指定程序集的强名称（名称，版本，语言，公钥标记）获取程序集信息集合

        /// <summary>
        /// 根据指定程序集的强名称（名称，版本，语言，公钥标记）获取程序集信息集合
        /// 例如.NET 2.0中的FileIOPermission类，它的强名称是：System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
        /// </summary>
        /// <param name="assemblyStringList">程序集的强名称集合</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> Load(IEnumerable<string> assemblyStringList)
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var assemblyString in assemblyStringList)
            {
                assemblies.Add(Assembly.Load(assemblyString));
            }

            return assemblies;
        }

        /// <summary>
        /// 根据指定程序集的强名称（名称，版本，语言，公钥标记）获取程序集信息集合
        /// 例如.NET 2.0中的FileIOPermission类，它的强名称是：System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
        /// </summary>
        /// <param name="assemblyStringList">程序集的强名称集合</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> Load(params string[] assemblyStringList)
        {
            return Load(assemblyStringList.ToList());
        }

        #endregion

        #region 根据指定的文件获得程序集信息集合（会加载目标程序集所引用和依赖的其他程序集）

        /// <summary>
        /// 根据完整的程序集文件路径获得程序集信息集合（会加载目标程序集所引用和依赖的其他程序集）
        /// </summary>
        /// <param name="assemblyFileList"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> LoadFrom(IEnumerable<string> assemblyFileList)
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var assemblyFile in assemblyFileList)
            {
                assemblies.Add(Assembly.LoadFrom(assemblyFile));
            }

            return assemblies;
        }

        /// <summary>
        /// 根据完整的程序集文件路径获得程序集信息集合（会加载目标程序集所引用和依赖的其他程序集）
        /// </summary>
        /// <param name="assemblyFileList"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> LoadFrom(params string[] assemblyFileList)
        {
            return LoadFrom(assemblyFileList.ToList());
        }

        #endregion

        #region 根据指定的文件获得程序集信息集合（不会加载目标程序集所引用和依赖的其他程序集）

        /// <summary>
        /// 根据完整的程序集文件路径获得程序集信息集合（不会加载目标程序集所引用和依赖的其他程序集）
        /// </summary>
        /// <param name="assemblyFileList"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> LoadFile(IEnumerable<string> assemblyFileList)
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var assemblyFile in assemblyFileList)
            {
                assemblies.Add(Assembly.LoadFile(assemblyFile));
            }

            return assemblies;
        }

        /// <summary>
        /// 根据完整的程序集文件路径获得程序集信息集合（不会加载目标程序集所引用和依赖的其他程序集）
        /// </summary>
        /// <param name="assemblyFileList"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> LoadFile(params string[] assemblyFileList)
        {
            return LoadFile(assemblyFileList.ToList());
        }

        #endregion
    }
}
