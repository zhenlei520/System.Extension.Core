using System;
using System.Diagnostics;
using System.Reflection;

namespace EInfrastructure.Core.HelpCommon.Systems
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
            return System.Reflection.Assembly.GetAssembly(type).CreateInstance(type.ToString());
        }

        #endregion
    }
}