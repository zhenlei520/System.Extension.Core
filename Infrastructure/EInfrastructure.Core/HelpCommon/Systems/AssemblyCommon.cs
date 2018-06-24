using System.Diagnostics;
using System.Reflection;

namespace EInfrastructure.Core.HelpCommon.Systems
{
    /// <summary>
    /// 应用程序集扩展类
    /// </summary>
    public class AssemblyCommon
    {
        private static readonly FileVersionInfo AssemblyFileVersion = FileVersionInfo.GetVersionInfo(fileName: Assembly.GetExecutingAssembly().Location);

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
    }
}
