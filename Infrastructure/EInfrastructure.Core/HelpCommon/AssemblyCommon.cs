using System;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 程序集相关
    /// </summary>
    public static class AssemblyCommon
    {
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