// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
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

        #region 根据类型实例化当前类

        /// <summary>
        /// 根据类型实例化当前类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CreateInstance(this Type type)
        {
            return Assembly.GetAssembly(type).CreateInstance(type.ToString());
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
    }
}
